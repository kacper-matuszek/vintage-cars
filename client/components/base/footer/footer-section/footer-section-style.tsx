import { createStyles, makeStyles, Theme } from "@material-ui/core";

export const footerSectionStyle = makeStyles((theme: Theme) => 
createStyles({
    footerSection: {
        display: 'flex',
        flexDirection: 'column',
        alignContent: 'center',
        justifyContent: 'flex-start',
    },
    footerBox: {
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        alignContent: 'center',
        padding: '2vh'
    }
}));