import AppBase from "../../../components/base/AppBaseComponent";
import Grid from '@material-ui/core/Grid';
import { CssBaseline, Paper, FormControlLabel, TextField, Checkbox, Button, Link, Typography } from "@material-ui/core";
import  loginStyle  from "./login-form-style";

const LoginPage = () => {
    const classes = loginStyle();
    return (
    <AppBase title="Login">
        <Grid container>
            <CssBaseline/>
            <Grid item xs={false} sm={4} md={5} className={classes.image} />
            <Grid item xs={12} sm={8} md={7} component={Paper} elevation={6} square>
            <div className={classes.paper}>
            <Typography component="h1" variant="h5">
                Logowanie
            </Typography>
                <form className={classes.form} noValidate>
                    <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              id="email"
              label="Adres email"
              name="email"
              autoComplete="email"
              autoFocus
            />
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="password"
              label="Hasło"
              type="password"
              id="password"
              autoComplete="current-password"
            />
            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Zapamiętaj mnie"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
            >
              Zaloguj się
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="#" variant="body2">
                  Zapomniałeś hasła ?
                </Link>
              </Grid>
            </Grid>
                    </form>
                </div>
            </Grid>
        </Grid>
    </AppBase>
)}

export default LoginPage;