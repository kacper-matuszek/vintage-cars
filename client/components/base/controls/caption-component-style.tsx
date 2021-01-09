import { makeStyles } from "@material-ui/core";

const useCaptionStyles = makeStyles(theme => ({
    root: {
        padding: theme.spacing(2, 2),
        display: "flex",
        flexDirection: "column",
        justifyContent: "center"
    }
}));

export default useCaptionStyles;