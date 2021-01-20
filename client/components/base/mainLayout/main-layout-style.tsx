import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";

export const layoutStyle = makeStyles((theme: Theme) => 
    createStyles({
        layoutContainer: {
            position: 'relative',
            display: 'flex',
            width: '100%',
            minHeight: '100vh',
            marginTop: '7vh',
        },
        layoutContainerAdmin: {
            marginTop: '12vh',
        },
        layoutContent: {
            display: 'flex',
            flexFlow: 'column',
            margin: '0 auto',
            justifyContent: 'flex-start',
            alignItems: 'flex-start',
            width: '90%',
        },
        shadowBox: {
            position: 'relative',
            width: '100%',
            height: '100%',
            backgroundColor: 'rgba(0,0,0,0.1)',
        },
        paperBox: {
            position: 'relative',
            display: 'flex',
            flexFlow: 'column',
            padding: '2vh',
            width: '100%',
            minHeight: '100vh',
            marginTop: '5vh',
            marginBottom: '-5vh',
            zIndex: 1,
        },
        footerTypography: {
            textAlign: 'center'
        }
}))
