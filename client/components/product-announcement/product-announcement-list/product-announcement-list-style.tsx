import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
    root: {
        display: "flex",
        flexDirection: "row", 
        flexWrap: "wrap", 
        width: '100%', 
        justifyContent: "space-around",
        position: 'relative'
    },
    circularProgress: {
        position: 'relative',
        width: '100%'
    },
    infiniteScroll: {
        position: 'relative',
        display: "flex", 
        flex: "column", 
        flexWrap: "wrap", 
        width: "100%",
        justifyContent: "space-around"
    }
}))

export default useStyles;