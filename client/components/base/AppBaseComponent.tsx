import React, { ReactNode, ReactElement } from 'react'
import Head from 'next/head'
import { ThemeProvider, Container, Grid } from '@material-ui/core'
import { theme, useStyles } from '../theme'

type Props = {
    children?: ReactNode
    title?: string,
    head?: ReactElement
}
const AppBase = ({children, title, head}: Props) => {
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
                 {children}
            </Grid>
        </ThemeProvider>
    </div>
)}
export default AppBase;