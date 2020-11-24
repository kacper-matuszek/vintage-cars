import { IconButton, Toolbar, Tooltip, Typography } from "@material-ui/core";
import useStyles from "./table-toolbar-style";
import clsx from "clsx";
import DeleteIcon from '@material-ui/icons/Delete';

interface TableToolbarProps {
    numSelected: number,
    title: string,
    onDeleteClick: () => void
}

const TableToolbar = (props: TableToolbarProps) => {
    const classes = useStyles();
    const { numSelected, title, onDeleteClick }  = props;

    const handleDeleteClick = (e) => {
        e.preventDefault();
        onDeleteClick();
    }

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
        <Tooltip title="Usuń">
          <IconButton aria-label="delete" onClick={handleDeleteClick}>
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      ) : ( <></> )}
    </Toolbar>
    );
};
export default TableToolbar;