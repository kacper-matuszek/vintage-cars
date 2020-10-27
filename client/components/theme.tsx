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
    },
    overrides: {
        MuiCssBaseline: {
            '@global': {
                '*::-webkit-scrollbar': {
                    width: '0.7em'
                  },
                  '*::-webkit-scrollbar-track': {
                    '-webkit-box-shadow': 'inset 0 0 6px rgba(255,255,255,0.9)'
                  },
                  '*::-webkit-scrollbar-thumb': {
                    backgroundColor: 'rgba(146,123,89,1)',
                  }
            }
        }
    }
});

export const useStyles = makeStyles({
    root: {
        display: 'flex',
        height: '100vh',
        boxSizing: 'border-box',
        margin: '0',
        padding: '0',
    }
});

export const backdropStyle = makeStyles((theme: Theme) => 
    createStyles({
        backdrop: {
            zIndex: theme.zIndex.drawer + 1,
            color: '#FFF',
        },
    })
)