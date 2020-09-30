import React, {useState} from "react";
import AppBase from "../../components/base/AppBaseComponent";
import MainLayout from "../../components/base/mainLayout/MainLayoutComponent";
import PictureContent from "../../components/base/picture-content-component/PictureContent";
import isEmpty from "../../core/models/utils/StringExtension";
import CookieDictionary from "../../core/models/settings/cookieSettings/CookieDictionary";
import Router from 'next/router';
import Cookie from 'universal-cookie';

export default function App({Component, pageProps, router}) {
    const [loading, setLoading] = useState(false);
    /*router*/
    Router.onRouteChangeStart = () => {
        setLoading(true);
    }
    Router.onRouterChangeComplete = () => {
        setLoading(false);
    }
    Router.onRouterChangeError = () => {
        setLoading(false);
    }
    /*errors*/
    const [showErrorResponse, setErrorResponse] = useState(false);
    const [showErrorRespText, setErrorRespText] = useState("");
    const [showValidationResponse, setValidationResponse] = useState(false);
    const [showValidationRespText, setValidationRespText] =  useState("");
    const [showSuccess, setSuccess] = useState(false);
    const [showSuccessText, setSuccessText] = useState("");

    const handleError = (event, reason) => {
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

    const handleShowError = (content) => {
        if(!isEmpty(content)) {
            setErrorRespText(content);
            setErrorResponse(true);
        }
    }

    const handleShowWarning = (content) => {
        if(!isEmpty(content)) {
            setValidationRespText(content);
            setValidationResponse(true);
        }
    }
    
    const handleSuccess = (event, reason) => {
        if(reson === 'clickway')
            return;
        setSuccess(false);
        setSuccessText("");
    }

    const handleShowSuccess = (content) => {
        if(!isEmpty(content)) {
            setSuccessText(content);
            setSuccess(true);
        }
    }

    const checkAuthorization = () => {
        const value = new Cookie().get(CookieDictionary.Token);
        return !isEmpty(value);
    }

    return (
        <AppBase title={pageProps.title} loading={loading}
        showError={showErrorResponse} errorMessage={showErrorRespText} handleError={handleError}
        showValidation={showValidationResponse} validationMessage={showValidationRespText} handleSuccess={handleSuccess}
        showSuccess={showSuccess} showSuccessMessage={showSuccessText}>
            {router.pathname.startsWith('/login') || router.pathname.startsWith('/register') ? 
            <PictureContent>
                <Component {...pageProps} 
                setLoading={setLoading}
                showError={handleShowError}
                showWarning={handleShowWarning}
                showSuccess={handleSuccess}/>
            </PictureContent> : <MainLayout isAuthorized={checkAuthorization()}>
            <Component {...pageProps} 
            setLoading={setLoading}
            showError={handleShowError}
            showWarning={handleShowWarning}
            showSuccess={handleSuccess}/>
            </MainLayout>
            }
        <style jsx global>
            {
                `
                body {
                    margin: 0;
                    padding: 0;
                }
                `
            }
        </style>
        </AppBase>
    )
}