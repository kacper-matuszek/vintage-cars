import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";

export const columnStyle = makeStyles((theme: Theme) =>
createStyles({
    column: {
        position: 'relative',
        display: 'flex',
        flexDirection: 'column',
        justifyContent: 'center',
        flexWrap: 'wrap',
        width: '100%',
        padding: '1vh',
        gap: '1vh',
    },
    row: {
        position: 'relative',
        display: 'flex',
        flexDirection: 'row',
        justifyContent: 'center',
        flexWrap: 'wrap',
        width: '100%',
        padding: '1vh',
        gap: '1vh',
    }
}))