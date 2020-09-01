import React from "react";
import { CssBaseline, Paper, FormControlLabel, TextField, Checkbox, Button, Link, Typography } from "@material-ui/core";
import Grid from "@material-ui/core/Grid";
import { registerFormStyle } from "./register-form-style";

const RegisterForm = (props) => {
    const classes = registerFormStyle();
   return(
   <Grid item xs={12} sm={8} md={7} component={Paper} elevation={6} square>
    <div className={classes.paper}>
        <Typography component="h1" variant="h5">
            Rejestracja
        </Typography>
        <form className={classes.form} noValidate method="POST">
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
            <TextField
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="repeatedPassword"
              label="Powtórz hasło"
              type="password"
              id="repeatedPassword"
              autoComplete="current-password"
            />
            <React.Fragment>
              <div className="g-recaptcha" data-sitekey={props.captcha.publicToken}></div>
            </React.Fragment>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              onClick={() => this.forceUpdate()}
            >
              Zarejestruj
            </Button>
        </form>
    </div>
</Grid>
)}

export default RegisterForm;
