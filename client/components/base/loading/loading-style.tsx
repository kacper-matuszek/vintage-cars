import { makeStyles } from "@material-ui/styles";

const useStyles = makeStyles({
    parent: {
      position: "relative",
      display: 'flex',
      height: '100%',
      width: '100%',
      zIndex: 0,
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