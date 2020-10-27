import { Theme } from "@material-ui/core";
import { makeStyles } from "@material-ui/styles";

export const useStyles = makeStyles((theme: Theme) => ({
    root: {
      flexGrow: 1,
      display: 'flex',
      position: 'relative',
      overflow: 'auto',
    },
    box: {
      display: 'flex',
      maxHeight: '100%',
      position: 'relative'
    }
  }));