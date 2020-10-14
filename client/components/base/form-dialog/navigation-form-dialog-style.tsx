import { Theme } from "@material-ui/core";
import { makeStyles } from "@material-ui/styles";

export const useStyles = makeStyles((theme: Theme) => ({
    root: {
      flexGrow: 1,
      backgroundColor: theme.palette.background.paper,
      display: 'flex',
      height: 224,
    },
    contentTab: {
        display: 'flex',
        margin: '1vh'
    },
    tabs: {
      borderRight: `1px solid ${theme.palette.divider}`,
    },
  }));