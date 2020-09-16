import { CssBaseline, Paper, FormControlLabel, TextField, Checkbox, Button, Link, Typography } from "@material-ui/core";
import Grid from '@material-ui/core/Grid';
import { loginFormStyle } from "./login-form-style";

const LoginForm = (props) => {
    const classes = loginFormStyle();
    return(
        <Grid item xs={12} sm={8} md={7} component={Paper} elevation={6} square>
            <div className={classes.paper}>
                <Typography component="h1" variant="h5">
                    Logowanie
                </Typography>
                <form className={classes.form} noValidate method="POST" onSubmit={props.onSubmit}>
                    <TextField
                      error={!!props.errors.email}
                      value={props.loginAccount.email}
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      id="email"
                      label="Adres email"
                      name="email"
                      autoComplete="email"
                      autoFocus
                      helperText={props.errors.email}
                      onChange={(email) => {
                        const valueEmail = email.target.value;
                        props.setLoginData(prevState => {
                          return {
                            ...prevState,
                            email: valueEmail
                          }
                        });
                      }}
                    />
                    <TextField
                      error={!!props.errors.password}
                      value={props.loginAccount.password}
                      variant="outlined"
                      margin="normal"
                      required
                      fullWidth
                      name="password"
                      label="Hasło"
                      type="password"
                      id="password"
                      autoComplete="current-password"
                      helperText={props.errors.password}
                      onChange={(pass) => {
                        const valuePass = pass.target.value;
                        props.setLoginData(prevState => {
                          return {
                            ...prevState,
                            password: valuePass
                          }
                        });
                      }}
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
export default LoginForm