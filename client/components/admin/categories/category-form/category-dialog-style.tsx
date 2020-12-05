import { createStyles, makeStyles, Theme } from "@material-ui/core";

const useStyles = makeStyles((theme: Theme) => 
    createStyles({
        nameField: {
            paddingRight: '1vh',
        },
        linkAttribute: {
            padding: '3px',
            marginTop: '1vh',
            marginBottom: '1vh'
        }
    })
);
export default useStyles;