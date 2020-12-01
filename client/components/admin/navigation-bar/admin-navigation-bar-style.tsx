import { createStyles, Theme } from "@material-ui/core";
import { makeStyles } from "@material-ui/styles";

const useStyles = makeStyles((theme: Theme) => 
    createStyles({
        adminBarRoot: {
            position: 'fixed',
            width: '100%',
            zIndex: 1000,
            height: '100%',
            maxHeight: '50px',
          },
          adminBar: {
            width: '100%',
            padding: '4px',
            backgroundColor: theme.palette.primary.dark,
          },
          adminLabel: {
            padding: '5px',
          },
          adminContainer: {
            display: 'flex',
            width: '55%',
            flexDirection: 'row',
            justifyContent: 'space-between',
            alignItems: 'center'
          },
          goToButton: {
            color: theme.palette.primary.contrastText,
          }
    })
);

export default useStyles;