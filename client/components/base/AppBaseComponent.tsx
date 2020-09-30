import React, { ReactNode, ReactElement, useState, forwardRef, useImperativeHandle } from 'react'
import Head from 'next/head'
import { ThemeProvider, Container, Grid, CircularProgress, Backdrop, Snackbar } from '@material-ui/core'
import { theme, useStyles, backdropStyle } from '../theme'
import Alert from '@material-ui/lab/Alert'

type Props = {
    children?: ReactNode
    title?: string,
    head?: ReactElement,
    loading?: boolean,
    showError?: boolean,
    errorMessage?: string,
    handleError?: (event?: React.SyntheticEvent, reason?: string) => void,
    showValidation?: boolean,
    validationMessage?: string,
    successMessage?:string,
    showSuccessMessage?: boolean,
    handleSuccess?: (event?: React.SyntheticEvent, reason?: string) => void,
}
const AppBase = ({children, title, head, loading, showError, errorMessage, handleError, showValidation, validationMessage, successMessage, showSuccessMessage, handleSuccess}: Props, ref) => {
    const classes = backdropStyle();
    const [open, setOpen] = useState(showError);

    return(
        <div>
            <Head>
                <title>{title}</title>
                <meta charSet="utf-8"/>
                <meta name="viewport" content="initial-scale=1.0, width=device-width"/>
                {head}
            </Head>
            <ThemeProvider theme={theme}>
                <Grid container className={useStyles().root}>
                    <Backdrop className={classes.backdrop} open={loading}>
                        <CircularProgress style={{'color': 'white'}} />
                    </Backdrop>
                     {children}
                </Grid>
                <Snackbar open={showValidation} onClose={handleError}>
                    <Alert severity="warning" onClose={handleError}>{validationMessage}</Alert>
                </Snackbar>
                <Snackbar open={showError} onClose={handleError}>
                    <Alert severity="error" onClose={handleError}>{errorMessage}</Alert>
                </Snackbar>
                <Snackbar open={showSuccessMessage} onClose={handleSuccess}>
                    <Alert severity="success" onClose={handleSuccess}>{successMessage}</Alert>
                </Snackbar>
            </ThemeProvider>
        </div>
)}
export default AppBase;