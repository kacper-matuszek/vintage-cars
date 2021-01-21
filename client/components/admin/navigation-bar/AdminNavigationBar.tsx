import { Button, Grid, Paper } from "@material-ui/core"
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import useStyles from "./admin-navigation-bar-style";
import Box from '@material-ui/core/Box';
import useLocale from "../../../hooks/utils/LocaleHook";

const AdminNavigationBar = () => {
    const classes = useStyles();
    const loc = useLocale('common', ['admin', 'navigation-bar']);
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
                loc.trans(['panel', 'admin']) : 
                loc.trans(['panel', 'user'])}
            </Paper>
            <Box sx={{display: "flex", flexDirection: "row", alignItems: "center"}}>
                {isAdmin ? 
                <Button className={classes.goToButton} startIcon={<ArrowForwardIcon/>} onClick={() => router.push('/')}>
                    {loc.trans(['panel', 'go-to', 'user'])}
                </Button> :
                <Button className={classes.goToButton} startIcon={<ArrowForwardIcon/>} onClick={() => router.push('/admin')}>
                    {loc.trans(['panel', 'go-to', 'admin'])}
                </Button>
                }
            </Box>
          </Grid>
        </Grid>
      </Grid>
    )
}

export default AdminNavigationBar;