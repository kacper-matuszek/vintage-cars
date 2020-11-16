import { Button, Grid, Paper } from "@material-ui/core"
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import { useEffect } from "react";
import useStyles from "./admin-navigation-bar-style";

const AdminNavigationBar = () => {
    const classes = useStyles();
    return (
      <Grid container xs={12} className={classes.adminBarRoot}>
        <Grid container className={classes.adminBar}>
          <Grid item container className={classes.adminContainer}>
            <Paper className={classes.adminLabel}>
                {window.location.href.includes('/admin') ? 
                "Strefa administracyjna" : 
                "Strefa użytkownika"}
            </Paper>
            <Grid item direction="row"  alignItems="center">
                {window.location.href.includes('/admin') ? 
                <Button className={classes.goToButton} startIcon={<ArrowForwardIcon/>} href="/">
                    Przejdź do strefy użytkownika
                </Button> :
                <Button className={classes.goToButton} startIcon={<ArrowForwardIcon/>} href="/admin">
                    Przejdź do strefy administracyjnej
                </Button>
                }
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    )
}

export default AdminNavigationBar;