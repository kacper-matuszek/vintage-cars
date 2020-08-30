import AppBase from "../../../components/base/AppBaseComponent";
import Grid from '@material-ui/core/Grid';
import { CssBaseline, Paper, FormControlLabel, TextField, Checkbox, Button, Link, Typography } from "@material-ui/core";
import  loginStyle  from "./login-form-style";
import BaseWebApiService from "../../../core/services/api-service/BaseWebApiService";
import { toCallback } from "../../../core/services/api-service/Callback";

const LoginPage = () => {
    const classes = loginStyle();
    return (
    <AppBase title="Login">
        <Grid container>
            <CssBaseline/>
            <Grid item xs={false} sm={4} md={5} className={classes.image} />
        </Grid>
    </AppBase>
)}

class LoginResult {
    public loginResult: number;
    public token: string;
}

LoginPage.getInitialProps = async () => {
    const apiService = new BaseWebApiService();
    const login = {
        "email": "admin@gmail.com",
        "password": "Admin1!!!"
    };
    apiService.get<any>("/settings/captcha-key", toCallback(
        data => console.log(data),
        validationError => console.log(validationError),
        error => console.log(error),
    ));

    return {
        objects: "cos"
    }
}

export default LoginPage;