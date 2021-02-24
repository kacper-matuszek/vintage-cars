import React, {useState} from "react";
import AppBase from "../../components/base/AppBaseComponent";
import MainLayout from "../../components/base/mainLayout/MainLayoutComponent";
import PictureContent from "../../components/base/picture-content-component/PictureContent";
import { isStringNullOrEmpty } from "../../core/models/utils/StringExtension";
import CookieDictionary from "../../core/models/settings/cookieSettings/CookieDictionary";
import Router from 'next/router';
import Cookie from 'universal-cookie';
import useIsAdmin from "../../hooks/authorization/IsAdminHook";
import AdminLayout from "../../components/admin/admin-layout/AdminLayoutComponent";

export default function App({Component, pageProps, router}) {
    const [loading, setLoading] = useState(false);
    const isAdmin = useIsAdmin();
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
        return !isStringNullOrEmpty(value);
    }

    return (
        <AppBase title={pageProps.title} loading={loading} setLoading={setLoading}>
            {router.pathname.startsWith('/admin') && isAdmin() ? 
                <AdminLayout isAuthorized={checkAuthorization()}>
                    <Component {...pageProps}/>
                </AdminLayout>
            :
            router.pathname.startsWith('/login') || router.pathname.startsWith('/register') ? 
            <PictureContent>
                <Component {...pageProps} 
                setLoading={setLoading}/>
            </PictureContent> 
            : <MainLayout isAuthorized={checkAuthorization()}>
            <Component {...pageProps} 
            setLoading={setLoading}/>
            </MainLayout>
            }
        </AppBase>
    )
}