import PictureContent from "../../../components/base/picture-content-component/PictureContent"
import RegisterForm from "../../../components/login/register-form/RegisterForm"
import AppBase from "../../../components/base/AppBaseComponent"
import { CaptchaKeyResponse } from "../../../components/login/models/CaptchaKeyResponse";
import BaseWebApiService from "../../../core/services/api-service/BaseWebApiService";
import { toCallback } from "../../../core/services/api-service/Callback";

const RegisterPage = (props) => {
    const head = () => {
        return <script src="https://www.google.com/recaptcha/api.js" async defer></script>
    }

    return(
        <AppBase title="Rejestracja" head={head()}>
            <PictureContent>
                <RegisterForm captcha={props.captcha}></RegisterForm>
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
export default RegisterPage;