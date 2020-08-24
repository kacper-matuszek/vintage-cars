import { createMuiTheme, makeStyles } from "@material-ui/core/styles"

export const theme = createMuiTheme({
    palette: {
        primary: {
            main: "#927b59"
        },
        secondary: {
            main: "#6a563b"
        }
    }
});

export const useStyles = makeStyles({
    root: {
        height: '100vh',
    }
});