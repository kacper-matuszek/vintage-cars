import { createMuiTheme, makeStyles, createStyles } from "@material-ui/core/styles"
import { Theme } from "@material-ui/core"

export const theme = createMuiTheme({
    palette: {
        primary: {
            main: "#927b59",
            contrastText: '#FFF',
        },
        secondary: {
            main: "#6a563b",
            contrastText: '#FFF',
        }
    },
    components: {
        MuiCssBaseline: {
            styleOverrides: {
                '@global': {
                    '*::-webkit-scrollbar': {
                        width: '0.7em',
                      },
                      '*::-webkit-scrollbar-track': {
                        '-webkit-box-shadow': 'inset 0 0 6px rgba(255,255,255,0.9)'
                      },
                      '*::-webkit-scrollbar-thumb': {
                        backgroundColor: 'rgba(146,123,89,1)',
                      }
                }
            }
        },
        MuiListItem: {
            styleOverrides: {
                root: {
                    "&$selected": {
                      backgroundColor: "#c3aa86"
                    }
                }
            }
        },
        MuiTab: {
            styleOverrides: {
                root: {
                    fullWidth: true
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
            zIndex: 1400,
            color: '#FFF',
        },
    })
)