import React, { ReactNode, ReactElement, useState, forwardRef, useImperativeHandle, Dispatch, SetStateAction } from 'react'
import Head from 'next/head'
import { ThemeProvider, Container, Grid, CircularProgress, Backdrop, Snackbar, Slide, CssBaseline } from '@material-ui/core'
import { theme, useStyles, backdropStyle } from '../theme'
import Alert from '@material-ui/lab/Alert'
import useLog from '../../hooks/fetch/pagedAPI/LogHook'
import NotificationContext from '../../contexts/NotificationContext'
import { TransitionProps } from '@material-ui/core/transitions/transition'
import LoadingContext from '../../contexts/LoadingContext'

type Props = {
    children?: ReactNode
    title?: string,
    head?: ReactElement,
    loading?: boolean,
    setLoading: Dispatch<SetStateAction<boolean>>
}
const AppBase = ({children, title, head, loading, setLoading}: Props, ref) => {
    const classes = backdropStyle();
    const gridStyles = useStyles();
    const [showSuccessMessage, successMessage, isShowSuccessMessage, successHandleClose, successOnClosed] = useLog();
    const [showErrorMessage, errorMessage, isShowErrorMessage, errorHandleClose, errorOnClosed] = useLog();
    const [showWarningMessage, warningMessage, isShowWarnigMessage, warnignHandleClose, warningOnClosed] = useLog();
    const notificationContextValue = {showSuccessMessage, showErrorMessage, showWarningMessage};
    const loadingContextValue = {showLoading: () => setLoading(true), hideLoading: () => setLoading(false), useGlobal: false};
    return(
        <div>
            <Head>
                <title>{title}</title>
                <meta charSet="utf-8"/>
                <meta name="viewport" content="initial-scale=1.0, width=device-width"/>
                {head}
            </Head>
            <ThemeProvider theme={theme}>
                <CssBaseline/>
                <NotificationContext.Provider value={notificationContextValue}>
                    <LoadingContext.Provider value={loadingContextValue}>
                        <Grid container className={gridStyles.root}>
                            <Backdrop className={classes.backdrop} open={loading}>
                                <CircularProgress style={{'color': 'white'}} />
                            </Backdrop>
                             {children}
                        </Grid>
                    </LoadingContext.Provider>
                    <Snackbar anchorOrigin={{vertical: 'bottom', horizontal: 'center'}} open={isShowWarnigMessage} onClose={warnignHandleClose} autoHideDuration={6000} onExit={() => warningOnClosed}>
                        <Alert severity="warning" onClose={warnignHandleClose} variant="filled">{warningMessage}</Alert>
                    </Snackbar>
                    <Snackbar anchorOrigin={{vertical: 'bottom', horizontal: 'center'}} open={isShowErrorMessage} onClose={errorHandleClose} onExit={() => errorOnClosed}>
                        <Alert severity="error" onClose={errorHandleClose} variant="filled">{errorMessage}</Alert>
                    </Snackbar>
                    <Snackbar anchorOrigin={{vertical: 'bottom', horizontal: 'center'}} open={isShowSuccessMessage} onClose={successHandleClose} autoHideDuration={6000} onExit={() => successOnClosed}>
                        <Alert severity="success" onClose={successHandleClose} variant="filled">{successMessage}</Alert>
                    </Snackbar>
                </NotificationContext.Provider>
            </ThemeProvider>
        </div>
)}
export default AppBase;