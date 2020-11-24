import { createStyles, Theme } from "@material-ui/core";
import { makeStyles } from "@material-ui/styles";

export const layoutStyle = makeStyles((theme: Theme) => 
    createStyles({
        layoutContainer: {
            position: 'relative',
            display: 'flex',
            width: '100%',
            height: '100vh',
            marginTop: '12vh',
        },
        shadowBox: {
            position: 'relative',
            width: '100%',
            height: '100%',
            backgroundColor: 'rgba(0,0,0,0.1)',
        },
        layoutContent: {
            display: 'flex',
            flexFlow: 'column',
            margin: '0 auto',
            justifyContent: 'flex-start',
            alignItems: 'flex-start',
            width: '90%',
        },
        paperBox: {
            position: 'relative',
            display: 'flex',
            flexFlow: 'column',
            padding: '2vh',
            width: '100%',
            height: '100%',
            minHeight: '100vh',
            margin: '5vh 0vh',
            zIndex: 1,
        }
    })
)