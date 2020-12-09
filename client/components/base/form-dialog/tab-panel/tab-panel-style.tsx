import { makeStyles, createStyles, Theme } from "@material-ui/core";

export const useStyles = makeStyles((theme: Theme) => 
  createStyles({
    root: {
      flexGrow: 1,
      display: 'flex',
      position: 'relative',
      maxHeight: '100%',
    },
    box: {
      display: 'flex',
      maxHeight: '100%',
      position: 'relative',
      padding: '1vh'
    }
  }));