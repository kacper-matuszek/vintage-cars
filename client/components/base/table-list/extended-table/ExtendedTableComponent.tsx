import { Checkbox, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TablePagination, TableRow, Tooltip } from "@material-ui/core";
import { forwardRef, Ref, useEffect, useImperativeHandle, useState } from "react";
import ISelectable from "../../../../core/models/base/ISelectable";
import getComparator from "../../../../core/models/utils/Comparator";
import stableSort from "../../../../core/models/utils/StableSort";
import ExtendedTableHead, { Order } from "../table-head/TableHeadComponent";
import TableToolbar from "../table-toolbar/TableToolbarComponent";
import useStyles from "./extended-table-style"
import React from "react";
import useToHeadCell from "../../../../hooks/utils/ToHeadCellHook";
import { Guid } from "guid-typescript";
import PagedList from "../../../../core/models/paged/PagedList";
import Paged from "../../../../core/models/paged/Paged";
import EditIcon from '@material-ui/icons/Edit';
import useToCellContent from "../../../../hooks/utils/ToCellContent";

interface ExtendedTableProps<T extends ISelectable> {
    rows: PagedList<T>,
    fetchData?: (paged: Paged) => void,
    title: string,
    children: JSX.Element[], 
    onDeleteClick?: (selectedItems: Guid[]) => void,
    onAddClick?: () => void,
    onEditClick?: (model: T) => void,
    showSelection?: boolean,
    onSelected?: (selectedItems: Guid[]) => void
}
const ExtendedTable = <T extends ISelectable>(props: ExtendedTableProps<T>) => {
    const classes = useStyles();
    const { rows, children, title, fetchData, onDeleteClick, onAddClick, onEditClick, showSelection, onSelected} = props;
    const [orderBy, setOrderBy] = useState<keyof T>('name');
    const [order, setOrder] = useState<Order>('asc');
    const [selected, setSelected] = useState<Guid[]>([]);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(5);
    const headers = useToHeadCell<T>(children);
    const cellContents = useToCellContent(children);
    const additionalHeaders = [{
      label: "Edytuj", visible: false, width: "50px", add: onEditClick !== undefined
    }]

    const handleRequestSort = (event: React.MouseEvent<unknown>, property: keyof T) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
      if (event.target.checked) {
          const newSelecteds = rows.source.filter(n => n.cantSelect).map((n) => n.id);
          setSelected(newSelecteds);
          return;
        }
        setSelected([]);
    };

    const handleClick = (event: React.MouseEvent<unknown>, id: Guid) => {
        const selectedIndex = selected.indexOf(id);
        let newSelected: Guid[] = [];

        if (selectedIndex === -1) {
          newSelected = newSelected.concat(selected, id);
          const object = rows.source.filter(x => x.id === id)[0];
          if(object?.cantSelect === undefined || !object.cantSelect)
            setSelected(newSelected);
          return;
        } else if (selectedIndex === 0) {
          newSelected = newSelected.concat(selected.slice(1));
        } else if (selectedIndex === selected.length - 1) {
          newSelected = newSelected.concat(selected.slice(0, -1));
        } else if (selectedIndex > 0) {
          newSelected = newSelected.concat(
            selected.slice(0, selectedIndex),
            selected.slice(selectedIndex + 1),
          );
        }
        
        setSelected(newSelected);
    };

    const handleChangePage = (event: unknown, newPage: number) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };

    const handleDeleteClick = () => {
      const selectedAttributes = [...selected];
      setSelected([]);
      onDeleteClick(selectedAttributes);
    }
    const handleAddClick = () => onAddClick();
    const handleEditClick = (event: React.MouseEvent<unknown>, id: Guid) => {
      const rowObject = rows.source.find(n => n.id === id);
      onEditClick(rowObject);
    }

    const isSelected = (id: Guid) => selected.indexOf(id) !== -1;

    useEffect(() => {
      if(fetchData !== undefined && fetchData !== null)
        fetchData(new Paged(page, rowsPerPage));
    }, [page, rowsPerPage]);
    useEffect(() => {
      if(onSelected !== undefined && onSelected !== null) {
        onSelected(selected);
      }
    }, [selected])
    return (
        <div className={classes.root}>
          <Paper className={classes.paper}>
            <TableToolbar 
              numSelected={selected.length} 
              title={title} 
              onDeleteClick={ onDeleteClick !== undefined ? handleDeleteClick : undefined}
              onAddClick={ onAddClick !== undefined ? handleAddClick : undefined} />
            <TableContainer className={classes.tableContainer}>
              <Table stickyHeader
                className={classes.table}
                aria-labelledby="tableTitle"
                size="medium"
                aria-label="enhanced table"
              >
                <ExtendedTableHead<T>
                  headers={headers}
                  additionalHeaders={additionalHeaders}
                  numSelected={selected.length}
                  order={order}
                  orderBy={orderBy as string}
                  onSelectAllClick={handleSelectAllClick}
                  onRequestSort={handleRequestSort}
                  rowCount={rows.source.length}
                  showSelectAll={showSelection === undefined ? false : showSelection}
                />
                <TableBody>
                  {stableSort(rows.source, getComparator(order, orderBy))
                    .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                    .map((row, index) => {
                      const isItemSelected = isSelected(row.id);
                      const labelId = `enhanced-table-checkbox-${index}`;
    
                      return (
                        <TableRow
                          hover
                          onClick={(event) => handleClick(event, row.id)}
                          role="checkbox"
                          aria-checked={isItemSelected}
                          tabIndex={-1}
                          key={row.id}
                          selected={isItemSelected}
                        >
                          { showSelection !== undefined && showSelection ? (<TableCell padding="checkbox">
                            <Checkbox
                              checked={isItemSelected}
                              inputProps={{ 'aria-labelledby': labelId }}
                            />
                          </TableCell>) : (<></>)}
                          {cellContents.map((cell, index) => {
                            return(
                              <TableCell component="th" id={`${labelId}-${index}`} key={`${labelId}-${index}`} scope="row" padding="default">
                                { cell.content !== undefined && cell.content !== null ?
                                  (
                                    cell.content(row)
                                  ) :
                                (typeof row[cell.id] === "boolean" ? 
                                  (
                                    <Checkbox
                                      checked={row[cell.id]}
                                    />
                                  ) : (row[cell.id]))}
                              </TableCell>
                            )
                          })}
                          { onEditClick !== undefined ? <TableCell component="th" scope="row" padding="default" align="right" width="50px">
                            <Tooltip title="Edytuj">
                              <IconButton aria-label="edytuj" onClick={(event) => handleEditClick(event, row.id)}>
                                  <EditIcon color="primary"/>
                              </IconButton>
                            </Tooltip>
                          </TableCell> : <></>}
                        </TableRow>
                      );
                    })}
                </TableBody>
              </Table>
            </TableContainer>
            <TablePagination
              rowsPerPageOptions={[5, 10, 25, 50, 100]}
              component="div"
              count={rows.totalCount !== undefined ? rows.totalCount : 0}
              rowsPerPage={rowsPerPage}
              page={page}
              onPageChange={handleChangePage}
              onRowsPerPageChange={handleChangeRowsPerPage}
              labelRowsPerPage="Wierszy na stronę:"
              labelDisplayedRows={({from, to, count}) => `${from}-${to} z ${count}`}
              nextIconButtonProps={{title: 'Następna strona'}}
              backIconButtonProps={{title: 'Poprzednia strona'}}
            />
          </Paper>
        </div>
      );
}
export default ExtendedTable;