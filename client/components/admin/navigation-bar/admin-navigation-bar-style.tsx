import { makeStyles, createStyles, Theme } from "@material-ui/core";

const useStyles = makeStyles((theme: Theme) => 
    createStyles({
        adminBarRoot: {
            position: 'absolute',
            width: '100%',
            zIndex: 1000,
            height: '100%',
            maxHeight: '50px',
            top: '64px',
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