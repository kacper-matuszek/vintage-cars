import { Button, Grid, Paper } from "@material-ui/core"
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import useStyles from "./admin-navigation-bar-style";

const AdminNavigationBar = () => {
    const classes = useStyles();
    const [isAdmin, setIsAdmin] = useState(false);
    const router = useRouter();

    useEffect(() => {
      setIsAdmin(window.location.href.includes('/admin'));
    }, []);

    return (
      <Grid container xs={12} className={classes.adminBarRoot}>
        <Grid container className={classes.adminBar}>
          <Grid item container className={classes.adminContainer}>
            <Paper className={classes.adminLabel}>
                {isAdmin ? 
                "Strefa administracyjna" : 
                "Strefa użytkownika"}
            </Paper>
            <Grid item direction="row"  alignItems="center">
                {isAdmin ? 
                <Button className={classes.goToButton} startIcon={<ArrowForwardIcon/>} onClick={() => router.push('/')}>
                    Przejdź do strefy użytkownika
                </Button> :
                <Button className={classes.goToButton} startIcon={<ArrowForwardIcon/>} onClick={() => router.push('/admin')}>
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