import { IconButton, Toolbar, Tooltip, Typography } from "@material-ui/core";
import useStyles from "./table-toolbar-style";
import clsx from "clsx";
import DeleteIcon from '@material-ui/icons/Delete';

interface TableToolbarProps {
    numSelected: number,
    title: string,
}

const TableToolbar = (props: TableToolbarProps) => {
    const classes = useStyles();
    const { numSelected, title }  = props;

    return (
        <Toolbar
      className={clsx(classes.root, {
        [classes.highlight]: numSelected > 0,
      })}
    >
      {numSelected > 0 ? (
        <Typography className={classes.title} color="inherit" variant="subtitle1" component="div">
          {numSelected} wybrano
        </Typography>
      ) : (
        <Typography className={classes.title} variant="h6" id="tableTitle" component="div">
          {title}
        </Typography>
      )}
      {numSelected > 0 ? (
        <Tooltip title="Delete">
          <IconButton aria-label="delete">
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      ) : ( <></> )}
    </Toolbar>
    );
};
export default TableToolbar;