import { Checkbox, Paper, Table, TableBody, TableCell, TableContainer, TablePagination, TableRow } from "@material-ui/core";
import { ReactElement, ReactNode, useState } from "react";
import ISelectable from "../../../../core/models/base/ISelectable";
import getComparator from "../../../../core/models/utils/Comparator";
import stableSort from "../../../../core/models/utils/StableSort";
import { HeadCell } from "../table-head/HeadCell";
import ExtendedTableHead, { Order } from "../table-head/TableHeadComponent";
import TableToolbar from "../table-toolbar/TableToolbarComponent";
import useStyles from "./extended-table-style"
import TableContent from "../table-content/TableContentComponent";
import React from "react";
import useToHeadCell from "../../../../hooks/utils/ToHeadCellHook";

interface ExtendedTableProps<T extends ISelectable> {
    rows: Array<T>,
    title: string,
    children: JSX.Element[]
}
const ExtendedTable = <T extends ISelectable>(props: ExtendedTableProps<T>) => {
    const classes = useStyles();
    const { rows, children, title } = props;
    const [orderBy, setOrderBy] = useState<keyof T>('name');
    const [order, setOrder] = useState<Order>('asc');
    const [selected, setSelected] = useState<string[]>([]);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(5);
    const headers = useToHeadCell<T>(children);

    const handleRequestSort = (event: React.MouseEvent<unknown>, property: keyof T) => {
        const isAsc = orderBy === property && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(property);
    };

    const handleSelectAllClick = (event: React.ChangeEvent<HTMLInputElement>) => {
        if (event.target.checked) {
          const newSelecteds = rows.map((n) => n.name);
          setSelected(newSelecteds);
          return;
        }
        setSelected([]);
    };

    const handleClick = (event: React.MouseEvent<unknown>, name: string) => {
        const selectedIndex = selected.indexOf(name);
        let newSelected: string[] = [];
    
        if (selectedIndex === -1) {
          newSelected = newSelected.concat(selected, name);
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

    const isSelected = (name: string) => selected.indexOf(name) !== -1;
    const emptyRows = rowsPerPage - Math.min(rowsPerPage, rows.length - page * rowsPerPage);

    return (
        <div className={classes.root}>
          <Paper className={classes.paper}>
            <TableToolbar numSelected={selected.length} title={title} />
            <TableContainer>
              <Table
                className={classes.table}
                aria-labelledby="tableTitle"
                size="medium"
                aria-label="enhanced table"
              >
                <ExtendedTableHead<T>
                  headers={headers}
                  numSelected={selected.length}
                  order={order}
                  orderBy={orderBy as string}
                  onSelectAllClick={handleSelectAllClick}
                  onRequestSort={handleRequestSort}
                  rowCount={rows.length}
                />
                <TableBody>
                  {stableSort(rows, getComparator(order, orderBy))
                    .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                    .map((row, index) => {
                      const isItemSelected = isSelected(row.name);
                      const labelId = `enhanced-table-checkbox-${index}`;
    
                      return (
                        <TableRow
                          hover
                          onClick={(event) => handleClick(event, row.name)}
                          role="checkbox"
                          aria-checked={isItemSelected}
                          tabIndex={-1}
                          key={row.name}
                          selected={isItemSelected}
                        >
                          <TableCell padding="checkbox">
                            <Checkbox
                              checked={isItemSelected}
                              inputProps={{ 'aria-labelledby': labelId }}
                            />
                          </TableCell>
                          {headers.map(header => {
                            return(
                              <TableCell component="th" id={labelId} scope="row" padding="none">
                                {row[header.id]}
                              </TableCell>
                            )
                          })}
                        </TableRow>
                      );
                    })}
                </TableBody>
              </Table>
            </TableContainer>
            <TablePagination
              rowsPerPageOptions={[5, 10, 25]}
              component="div"
              count={rows.length}
              rowsPerPage={rowsPerPage}
              page={page}
              onChangePage={handleChangePage}
              onChangeRowsPerPage={handleChangeRowsPerPage}
            />
          </Paper>
        </div>
      );
}
export default ExtendedTable;