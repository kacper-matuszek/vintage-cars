import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
    root: {
        maxWidth: 345,
        minWidth: 245,
        marginLeft: 5,
        marginRight: 5,
        marginTop: 10,
        marginBottom: 10
    },
    media: {
        height: 140
    },
    cardContent: {
        backgroundColor: theme.palette.primary.main,
        color: theme.palette.primary.contrastText,
    },
    description: {
        color: theme.palette.primary.contrastText
    }
}));
export default useStyles;