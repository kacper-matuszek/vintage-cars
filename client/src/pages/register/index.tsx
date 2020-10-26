import React, {useContext, useState} from "react";
import RegisterForm from "../../../components/login/register-form/RegisterForm"
import { CaptchaKeyResponse } from "../../../components/login/models/CaptchaKeyResponse";
import BaseWebApiService from "../../../core/services/api-service/BaseWebApiService";
import { toCallback, postCallback } from "../../../core/services/api-service/Callback";
import RegisterValidator from "../../../components/login/models/validators/RegisterValidator";
import { RegisterAccount } from "../../../components/login/models/RegisterAccount";
import { useRouter } from "next/router";
import NotificationContext from "../../../contexts/NotificationContext";

const RegisterPage = (props) => {
    const {showSuccessMessage, showErrorMessage, showWarningMessage} = useContext(NotificationContext);
    const apiService = new BaseWebApiService();
    const router = useRouter();
    /*state */
    const [registerData, setData] = useState(new RegisterAccount());
    /*errors*/
    const [errors, setErrors] = useState(new RegisterValidator());
    /*handlers*/
    const onSubmitHandle = (event) => {
        event.preventDefault();
        const validator = new RegisterValidator();
        validator.validateEmail(registerData.email);
        validator.validatePassword(registerData.password);
        validator.validatePasswords(registerData.password, registerData.repeatedPassword);
        setErrors(validator);
        if(validator.canContinue) {
            props.setLoading(true);
            apiService.postWithoutResponse("/v1/account/register", registerData, postCallback(
                vErr => showWarningMessage(vErr.message),
                err => showErrorMessage(err.message),
                () => {
                    showSuccessMessage("Zarejestrowano pomyślnie. Przechodzenie do logowania...");
                    router.push('/login')
                }
            )).finally(() => props.setLoading(false));
        }
    }

    return(
        <RegisterForm captcha={props.captcha} errors={errors} registerAccount={registerData} setRegisterData={setData} onSubmit={onSubmitHandle}></RegisterForm>
    )
}

export async function getStaticProps() {
    let captcha: CaptchaKeyResponse;
    const apiService = new BaseWebApiService();
    await apiService.get<CaptchaKeyResponse>("/settings/captcha-key", toCallback(
                 (data) => captcha = data,
                 validationError => console.log(validationError),
                 error => console.log(error),
             ));
    return {
      props: {
        captcha,
        title: "Rejestracja"
    }
  }
}
export default RegisterPage