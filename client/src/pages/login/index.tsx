import React, {useState} from "react";
import AppBase from "../../../components/base/AppBaseComponent";
import BaseWebApiService from "../../../core/services/api-service/BaseWebApiService";
import { toCallback } from "../../../core/services/api-service/Callback";
import PictureContent from "../../../components/base/picture-content-component/PictureContent";
import LoginForm from "../../../components/login/login-form/LoginForm";
import LoginAccount from "../../../components/login/models/LoginAccount";
import LoginResponse from "../../../components/login/models/LoginResponse";
import { Validator, ValidatorManage, ValidatorType } from "../../../components/login/models/validators/Validator";

const LoginPage = () => {
    const apiService = new BaseWebApiService();
    /*state*/
    const [loginData, setData] = useState(new LoginAccount());
    const [loading, setLoading] = useState(false);
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
    const [showErrorResponse, setErrorResponse] = useState(false);
    const [showErrorRespText, setErrorRespText] = useState("");
    const [showValidationResponse, setValidationResponse] = useState(false);
    const [showValidationRespText, setValidationRespText] =  useState("");

    /*handlers*/
    const onSubmitHandle = (event) => {
        event.preventDefault();
        validatorManager.isValid(loginData);
        setErrors({...errors, email: validatorManager.getMessageByKey("email"), password: validatorManager.getMessageByKey("password")});
        if(validatorManager.isAllValid()) {
            setLoading(true);
            apiService.post<LoginResponse>("/v1/account/login", loginData, toCallback<LoginResponse>(
                success => console.log(success),
                vErr => {
                    setValidationRespText(vErr.message);
                    setValidationResponse(true);
                },
                err => {
                    setErrorRespText(err.message);
                    setErrorResponse(true);
                }
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
    return (
        <AppBase title="Login" loading={loading} 
            showError={showErrorResponse} errorMessage={showErrorRespText} handleError={handleError}
            showValidation={showValidationResponse} validationMessage={showValidationRespText}>
            <PictureContent>
                <LoginForm errors={errors} loginAccount={loginData} setLoginData={setData} onSubmit={onSubmitHandle}></LoginForm>
            </PictureContent>
        </AppBase>
    )
}

export default LoginPage