import React from "react";
import ReCAPTCHA from "react-google-recaptcha";
import { Paper, TextField, Button, Typography, CircularProgress } from "@material-ui/core";
import Grid from "@material-ui/core/Grid";
import { registerFormStyle } from "./register-form-style";
import { RegisterAccount } from "../models/RegisterAccount";

const RegisterForm = (props) => {
    const classes = registerFormStyle();
    const onRecaptchaChangeValue = (value) => {
        const recaptcha = value;
        props.setRegisterData(prevState => {
        return {
          ...prevState,
          googleRecaptchaResponse: recaptcha,
          isEnabled: true
        }
      });
    }
    let recaptcha;

   return(
   <Grid item xs={12} sm={8} md={7} component={Paper} elevation={6} square>
    <div className={classes.paper}>
        <Typography component="h1" variant="h5">
            Rejestracja
        </Typography>
        <form className={classes.form} noValidate method="POST" onSubmit={props.onSubmit}>
            <TextField
              error={!!props.errors.username}
              value={props.registerAccount.username}
              onChange={(username) => {
                const valueUsername = username.target.value;
                props.setRegisterData(prevState => {
                return {
                  ...prevState,
                  username: valueUsername
                }
              });}}
              
              variant="outlined"
              margin="normal"
              required
              fullWidth
              id="username"
              label="Login"
              name="login"
              autoComplete="login"
              autoFocus
              helperText={props.errors.username}
            />
            <TextField
              error={!!props.errors.email}
              value={props.registerAccount.email}
              onChange={(email) => {
                const valueEmail = email.target.value;
                props.setRegisterData(prevState => {
                return {
                  ...prevState,
                  email: valueEmail
                }
              });}}
              
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
            />
            <TextField
              error={!!props.errors.password}
              value={props.registerAccount.password}
              onChange={(pass) => {
                const valuePass = pass.target.value;
                props.setRegisterData(prevState => {
                return {
                  ...prevState,
                  password: valuePass
                }
              });}}
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
            />
            <TextField
              error={!!props.errors.repeatedPassword}
              value={props.registerAccount.repeatedPassword}
              onChange={(rPass) => {
                const valueRPass = rPass.target.value;
                props.setRegisterData(prevState => {
                return {
                  ...prevState,
                  repeatedPassword: valueRPass
                }
              });}}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="repeatedPassword"
              label="Powtórz hasło"
              type="password"
              id="repeatedPassword"
              autoComplete="current-password"
              helperText={props.errors.repeatedPassword}
            />
            <React.Fragment>
              <ReCAPTCHA
                ref={(el) => {recaptcha = el;}}
                sitekey={props.captcha.publicToken}
                onChange={onRecaptchaChangeValue}
              />
            </React.Fragment>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
            >
              Zarejestruj
            </Button>
        </form>
    </div>
</Grid>
)}
export default RegisterForm;
