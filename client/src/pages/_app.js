import React, {useState} from "react";
import AppBase from "../../components/base/AppBaseComponent";
import MainLayout from "../../components/base/mainLayout/MainLayoutComponent";
import PictureContent from "../../components/base/picture-content-component/PictureContent";
import isEmpty from "../../core/models/utils/StringExtension";
import CookieDictionary from "../../core/models/settings/cookieSettings/CookieDictionary";
import Router from 'next/router';
import Cookie from 'universal-cookie';
import useLog from "../../hooks/fetch/pagedAPI/LogHook";

export default function App({Component, pageProps, router}) {
    const [loading, setLoading] = useState(false);
    /*router*/
    Router.onRouteChangeStart = () => {
        setLoading(true);
    }
    Router.onRouteChangeComplete = () => {
        setLoading(false);
    }
    Router.onRouteChangeError = () => {
        setLoading(false);
    }

    const checkAuthorization = () => {
        const value = new Cookie().get(CookieDictionary.Token);
        return !isEmpty(value);
    }

    return (
        <AppBase title={pageProps.title} loading={loading} setLoading={setLoading}>
            {router.pathname.startsWith('/login') || router.pathname.startsWith('/register') ? 
            <PictureContent>
                <Component {...pageProps} 
                setLoading={setLoading}/>
            </PictureContent> : <MainLayout isAuthorized={checkAuthorization()}>
            <Component {...pageProps} 
            setLoading={setLoading}/>
            </MainLayout>
            }
        </AppBase>
    )
}