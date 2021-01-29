import { makeStyles } from "@material-ui/core";

const useStyles = makeStyles(theme => ({
    carousel: {
        width: '100%',
        display: 'flex',
        flexFlow: 'row',
        justifyContent: 'center',
        alignItems: 'center',
        position: 'relative',
        maxHeight: 200
    },
    cardMedia: {
        position: 'relative',
        objectFit: 'scale-down',
    },
    containerCardMedia: {
        display: 'flex',
        flexFlow: 'row',
        justifyContent: 'center',
    }
}))
export default useStyles;