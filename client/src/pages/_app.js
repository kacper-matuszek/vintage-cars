import React, {useState} from "react";
import AppBase from "../../components/base/AppBaseComponent";
import MainLayout from "../../components/base/mainLayout/MainLayoutComponent";
import PictureContent from "../../components/base/picture-content-component/PictureContent";
import isEmpty from "../../core/models/utils/StringExtension";

export default function App({Component, pageProps, router}) {
    const [loading, setLoading] = useState(false);
    /*errors*/
    const [showErrorResponse, setErrorResponse] = useState(false);
    const [showErrorRespText, setErrorRespText] = useState("");
    const [showValidationResponse, setValidationResponse] = useState(false);
    const [showValidationRespText, setValidationRespText] =  useState("");

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

    return (
        <AppBase title={pageProps.title} loading={loading}
        showError={showErrorResponse} errorMessage={showErrorRespText} handleError={handleError}
        showValidation={showValidationResponse} validationMessage={showValidationRespText}>
            {router.pathname.startsWith('/login') || router.pathname.startsWith('/register') ? 
            <PictureContent>
                <Component {...pageProps} 
                setLoading={setLoading}
                showError={handleShowError}
                showWarning={handleShowWarning}/>
            </PictureContent> : <MainLayout>
            <Component {...pageProps} 
            setLoading={setLoading}
            showError={handleShowError}
            showWarning={handleShowWarning}/>
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