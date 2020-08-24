import React, { ReactNode } from 'react'
import Link from 'next/link'
import Head from 'next/head'
import { ThemeProvider, Container, Grid } from '@material-ui/core'
import { theme, useStyles } from '../theme'

type Props = {
    children?: ReactNode
    title?: string
}

const AppBase = ({children, title}: Props) => (
    <div>
        <Head>
            <title>{title}</title>
            <meta charSet="utf-8"/>
            <meta name="viewport" content="initial-scale=1.0, width=device-width"/>
        </Head>
        <ThemeProvider theme={theme}>
            <Grid container className={useStyles().root}>
                 {children}
            </Grid>
        </ThemeProvider>
    </div>
)

export default AppBase