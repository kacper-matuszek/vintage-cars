import React, { useState} from "react";
import BaseWebApiService from "../../../core/services/api-service/BaseWebApiService";
import { postCallback, toCallback } from "../../../core/services/api-service/Callback";
import LoginForm from "../../../components/login/login-form/LoginForm";
import LoginAccount from "../../../components/login/models/LoginAccount";
import LoginResponse from "../../../components/login/models/LoginResponse";
import { ValidatorManage, ValidatorType } from "../../../components/login/models/validators/Validator";
import { LoginResult } from "../../../components/login/models/enums/LoginResult";
import Cookie from 'universal-cookie';
import RecoveryPassword from "../../../components/login/login-form/RecoveryPassword";
import CookieDictionary from "../../../core/models/settings/cookieSettings/CookieDictionary";
import { useRouter } from "next/router";
import { redirectTo } from "../../../core/models/utils/Redirect";

const LoginPage = (props) => {
    const apiService = new BaseWebApiService();
    const router = useRouter();
    /*state*/
    const [loginData, setData] = useState(new LoginAccount());
    /*errors*/
    const [errors, setErrors] = useState({
        email: "",
        password: "",
    });
    const validatorManager = new ValidatorManage();
    validatorManager.setValidators({
        ["email"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Email jest wymagany.",
            isValid: true
        }],
        ["password"]: [{
            type: ValidatorType.NotEmpty,
            paramValue: null,
            message: "Hasło jest wymagane.",
            isValid: true
        }]
    });

    /*handlers*/
    const onSubmitHandle = (event) => {
        event.preventDefault();
        validatorManager.isValid(loginData);
        setErrors({...errors, email: validatorManager.getMessageByKey("email"), password: validatorManager.getMessageByKey("password")});
        if(validatorManager.isAllValid()) {
            props.setLoading(true);
            apiService.post<LoginResponse>("/v1/account/login", loginData, toCallback<LoginResponse>(
                success => {
                    if(success.loginResult === LoginResult.Successful) {
                        new Cookie().set(CookieDictionary.Token, success.token, {
                            expires: new Date(
                                        new Date().getFullYear(),
                                        new Date().getMonth(),
                                        new Date().getDate() + 1,
                                        new Date().getHours(),
                                        new Date().getMinutes())
                        });
                        redirectTo('/');
                        return;
                    }
                    props.showError("Nie udało się zalogować. Sprawdź poprawność email oraz hasła.");
                },
                vErr => props.showWarning(vErr.message),
                err => props.showError(err.message),
            )).finally(() => props.setLoading(false));
        }
    }

    const sendRecoveryPasswordHandle = (email) => {
        props.setLoading(true);
        apiService.postWithoutResponse("/v1/account/recovery-password", {email}, postCallback(
            vErr => props.showWarning(vErr.message),
            err => props.showError(err.message),
        )).finally(() => props.setLoading(false))
    }
    return (
        <LoginForm errors={errors} loginAccount={loginData} setLoginData={setData} onSubmit={onSubmitHandle}>
            <RecoveryPassword sendRecoveryPassword={sendRecoveryPasswordHandle}></RecoveryPassword>
        </LoginForm>
    );
}
export async function getStaticProps() {
    return {
        props: {
            title: "Logowanie",
        }
    }
}
export default LoginPage