import { IconButton, Toolbar, Tooltip, Typography } from "@material-ui/core";
import useStyles from "./table-toolbar-style";
import clsx from "clsx";
import DeleteIcon from '@material-ui/icons/Delete';
import AddCircleIcon from '@material-ui/icons/AddCircle';
import useLocale from "../../../../hooks/utils/LocaleHook";

interface TableToolbarProps {
    numSelected: number,
    title: string,
    onDeleteClick?: () => void,
    onAddClick?: () => void
}

const TableToolbar = (props: TableToolbarProps) => {
    const classes = useStyles();
    const loc = useLocale('common', ['base', 'table-list', 'toolbar']);
    const { numSelected, title, onDeleteClick, onAddClick }  = props;

    const handleDeleteClick = (e) => {
        e.preventDefault();
        onDeleteClick();
    }

    const handleAddClick = (e) => {
      e.preventDefault();
      onAddClick();
    }
    return (
        <Toolbar
      className={clsx(classes.root, {
        [classes.highlight]: numSelected > 0,
      })}
    >
      {numSelected > 0 ? (
        <Typography className={classes.title} color="inherit" variant="subtitle1" component="div">
          {loc.transQuery('selected', {amount: numSelected})} 
        </Typography>
      ) : (
        <Typography className={classes.title} variant="h6" id="tableTitle" component="div">
          {title}
        </Typography>
      )}
      {numSelected > 0 ? 
        onDeleteClick !== undefined ? 
        (<Tooltip title={loc.trans('delete-tooltip')}>
          <IconButton aria-label="delete" onClick={handleDeleteClick}>
            <DeleteIcon />
          </IconButton>
        </Tooltip>) : (<></>)
       : onAddClick ? ( <Tooltip title={loc.trans('add-tooltip')}>
              <IconButton aria-label="add" onClick={handleAddClick}>
                <AddCircleIcon color="primary" fontSize="large" />
              </IconButton>
            </Tooltip> ) : (<></>)}
    </Toolbar>
    );
};
export default TableToolbar;