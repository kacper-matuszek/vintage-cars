import React, { ReactNode, ReactElement, useState, forwardRef, useImperativeHandle } from 'react'
import Head from 'next/head'
import { ThemeProvider, Container, Grid, CircularProgress, Backdrop, Snackbar, Slide } from '@material-ui/core'
import { theme, useStyles, backdropStyle } from '../theme'
import Alert from '@material-ui/lab/Alert'
import useLog from '../../hooks/fetch/pagedAPI/LogHook'
import NotificationContext from '../../contexts/NotificationContext'
import { TransitionProps } from '@material-ui/core/transitions/transition'

type Props = {
    children?: ReactNode
    title?: string,
    head?: ReactElement,
    loading?: boolean,
}
const AppBase = ({children, title, head, loading}: Props, ref) => {
    const classes = backdropStyle();
    const gridStyles = useStyles();
    const [showSuccessMessage, successMessage, isShowSuccessMessage, successHandleClose, successOnClosed] = useLog();
    const [showErrorMessage, errorMessage, isShowErrorMessage, errorHandleClose, errorOnClosed] = useLog();
    const [showWarningMessage, warningMessage, isShowWarnigMessage, warnignHandleClose, warningOnClosed] = useLog();
    const notificationContextValue = {showSuccessMessage, showErrorMessage, showWarningMessage};
    return(
        <div>
            <Head>
                <title>{title}</title>
                <meta charSet="utf-8"/>
                <meta name="viewport" content="initial-scale=1.0, width=device-width"/>
                {head}
            </Head>
            <ThemeProvider theme={theme}>
                <NotificationContext.Provider value={notificationContextValue}>
                    <Grid container className={gridStyles.root}>
                        <Backdrop className={classes.backdrop} open={loading}>
                            <CircularProgress style={{'color': 'white'}} />
                        </Backdrop>
                         {children}
                    </Grid>
                    <Snackbar open={isShowWarnigMessage} onClose={warnignHandleClose} autoHideDuration={6000} onExited={warningOnClosed}>
                        <Alert severity="warning" onClose={warnignHandleClose} variant="filled">{warningMessage}</Alert>
                    </Snackbar>
                    <Snackbar open={isShowErrorMessage} onClose={errorHandleClose} onExited={errorOnClosed}>
                        <Alert severity="error" onClose={errorHandleClose} variant="filled">{errorMessage}</Alert>
                    </Snackbar>
                    <Snackbar open={isShowSuccessMessage} onClose={successHandleClose} autoHideDuration={6000} onExited={successOnClosed}>
                        <Alert severity="success" onClose={successHandleClose} variant="filled">{successMessage}</Alert>
                    </Snackbar>
                </NotificationContext.Provider>
            </ThemeProvider>
        </div>
)}
export default AppBase;