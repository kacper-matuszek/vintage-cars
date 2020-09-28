import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";

export const layoutStyle = makeStyles((theme: Theme) => 
    createStyles({
        layoutContainer: {
            position: 'relative',
            display: 'flex',
            width: '100%',
            height: '100vh',
            marginTop: '7vh'
        },
        layoutContent: {
            display: 'flex',
            flexFlow: 'column',
            margin: '0 auto',
            justifyContent: 'flex-start',
            alignItems: 'flex-start',
            width: '90%'
        },
        footerTypography: {
            textAlign: 'center'
        }
}))
