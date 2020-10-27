import { Theme } from "@material-ui/core";
import { makeStyles } from "@material-ui/styles";

export const useStyles = makeStyles((theme: Theme) => ({
    root: {
      flexGrow: 1,
      backgroundColor: theme.palette.background.paper,
      display: 'flex',
    },
    dialogContent: {
      display: 'flex',
      position: 'relative',
      paddingLeft: '0px',
      paddingRight: '0px',
      padding: '1vh',
    },
    tabs: {
      borderRight: `1px solid ${theme.palette.divider}`,
      minWidth: '90px',
    },
    tab: {
      minWidth: '90px',
      padding: '1vh'
    },
  }));