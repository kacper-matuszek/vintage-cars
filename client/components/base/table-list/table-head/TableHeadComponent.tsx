import { TableCell, TableHead, TableRow, TableSortLabel } from "@material-ui/core"
import Checkbox from '@material-ui/core/Checkbox';
import { HeadCell } from "./HeadCell";
import useStyles from "./table-head-style";

export type Order = 'asc' | 'desc';

interface ExtendedTableProps<T> {
    headers: HeadCell<T>[],
    numSelected: number,
    onRequestSort: (event: React.MouseEvent<unknown>, property: keyof T) => void;
    onSelectAllClick: (event: React.ChangeEvent<HTMLInputElement>) => void;
    order: Order;
    orderBy: string;
    rowCount: number;
}

const ExtendedTableHead = <T extends object>(props: ExtendedTableProps<T>) => {
    const classes = useStyles();
    const { headers, onSelectAllClick, order, orderBy, numSelected, rowCount, onRequestSort } = props;
    const createSortHandler = (property: keyof T) => (event: React.MouseEvent<unknown>) => {
        onRequestSort(event, property);
    };
    return (
        <TableHead>
            <TableRow>
                <TableCell padding="checkbox">
                    <Checkbox
                        indeterminate={numSelected > 0 && numSelected < rowCount}
                        checked={rowCount > 0 && numSelected === rowCount}
                        onChange={onSelectAllClick}
                        inputProps={{'aria-label': 'select all'}}
                    />
                </TableCell>
                {headers.map((headCell, index) => (
                    <TableCell
                    key={headCell.id as string}
                    align={index === 0 ? 'left' : 'right'}
                    padding='default'
                    sortDirection={orderBy === headCell.id ? order : false}
                    >
                    <TableSortLabel
                      active={orderBy === headCell.id}
                      direction={orderBy === headCell.id ? order : 'asc'}
                      onClick={createSortHandler(headCell.id)}
                    >
                      {headCell.label}
                      {orderBy === headCell.id ? (
                        <span className={classes.visuallyHidden}>
                          {order === 'desc' ? 'sorted descending' : 'sorted ascending'}
                        </span>
                      ) : null}
                    </TableSortLabel>
                  </TableCell>
                ))}
            </TableRow>
        </TableHead>
    )
}
export default ExtendedTableHead;