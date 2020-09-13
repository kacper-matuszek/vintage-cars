import React, {useState} from "react";
import PictureContent from "../../../components/base/picture-content-component/PictureContent"
import RegisterForm from "../../../components/login/register-form/RegisterForm"
import AppBase from "../../../components/base/AppBaseComponent"
import { CaptchaKeyResponse } from "../../../components/login/models/CaptchaKeyResponse";
import BaseWebApiService from "../../../core/services/api-service/BaseWebApiService";
import { toCallback, postCallback } from "../../../core/services/api-service/Callback";
import RegisterValidator from "../../../components/login/models/validators/RegisterValidator";
import { RegisterAccount } from "../../../components/login/models/RegisterAccount";

const RegisterPage = (props) => {
    const apiService = new BaseWebApiService();
    /*state */
    const [registerData, setData] = useState(new RegisterAccount());
    const [loading, setLoading] = useState(false);
    /*errors*/
    const [errors, setErrors] = useState(new RegisterValidator());
    const [showErrorResponse, setErrorResponse] = useState(false);
    const [showErrorRespText, setErrorRespText] = useState("");
    const [showValidationResponse, setValidationResponse] = useState(false);
    const [showValidationRespText, setValidationRespText] =  useState("");

    /*handlers*/
    const onSubmitHandle = (event) => {
        event.preventDefault();
        const validator = new RegisterValidator();
        validator.validateEmail(registerData.email);
        validator.validatePassword(registerData.password);
        validator.validatePasswords(registerData.password, registerData.repeatedPassword);
        setErrors(validator);
        if(validator.canContinue) {
            setLoading(true);
            apiService.postWithoutResponse("/v1/account/register", registerData, postCallback(
                vErr => {
                    setValidationRespText(vErr.message);
                    setValidationResponse(true);
                },
                err => {
                    setErrorRespText(err.message);
                    setErrorResponse(true);
                },
            )).finally(() => setLoading(false));
        }
    }
    const handleError = (event?: React.SyntheticEvent, reason?: string) => {
        if (reason === 'clickaway') {
            return;
          }
        
        if(showValidationResponse)
        {
            setValidationResponse(false);
            setValidationRespText("");
            return;
        }
        setErrorResponse(!showErrorResponse);
        setErrorRespText("");
    }

    return(
        <AppBase title="Rejestracja" loading={loading} 
                showError={showErrorResponse} errorMessage={showErrorRespText} handleError={handleError}
                showValidation={showValidationResponse} validationMessage={showValidationRespText}>
            <PictureContent>
                <RegisterForm captcha={props.captcha} errors={errors} registerAccount={registerData} setRegisterData={setData} onSubmit={onSubmitHandle}></RegisterForm>
            </PictureContent>
        </AppBase>
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
        captcha
    }
  }
}
export default RegisterPage