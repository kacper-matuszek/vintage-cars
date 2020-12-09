import { makeStyles, createStyles, lighten, Theme } from "@material-ui/core";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      paddingLeft: theme.spacing(2),
      paddingRight: theme.spacing(1),
    },
    highlight: {
            backgroundColor: lighten(theme.palette.secondary.light, 0.85),
            color: theme.palette.text.primary,
          },
    title: {
      flex: '1 1 100%',
    },
  }),
);
export default useStyles;