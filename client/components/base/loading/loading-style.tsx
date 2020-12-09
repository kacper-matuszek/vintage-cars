import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles({
    parent: {
      position: "relative",
      display: 'flex',
      zIndex: 0,
      overflow: 'auto',
    },
    backdrop: {
      position: "absolute",
      display: 'flex',
      overflow: 'auto',
      alignItems: 'center',
      justifyContent: 'center',
    },
    circular: {
        color: 'white'
    }
});
export default useStyles;