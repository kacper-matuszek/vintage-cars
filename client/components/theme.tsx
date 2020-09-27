import { createMuiTheme, makeStyles, createStyles } from "@material-ui/core/styles"
import { Theme } from "@material-ui/core"

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
        display: 'flex',
        height: '100vh',
    }
});

export const backdropStyle = makeStyles((theme: Theme) => 
    createStyles({
        backdrop: {
            zIndex: theme.zIndex.drawer + 1,
            color: '#FFF',
        }
    })
)