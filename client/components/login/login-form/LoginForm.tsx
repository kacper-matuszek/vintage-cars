import { CssBaseline, Paper, FormControlLabel, TextField, Checkbox, Button, Link, Typography } from "@material-ui/core";
import Grid from '@material-ui/core/Grid';
import { loginFormStyle } from "./login-form-style";

const LoginForm = props => {
    const classes = loginFormStyle();
    return(
        <Grid item xs={12} sm={8} md={7} component={Paper} elevation={6} square>
            <div className={classes.paper}>
                <Typography component="h1" variant="h5">
                    Logowanie
                </Typography>
                <form className={classes.form} noValidate>
                    <TextField
                      error={!!props.errors}
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
    )}