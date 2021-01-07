import { useContext, useEffect, useState } from "react";
import NotificationContext from "../../contexts/NotificationContext";
import UrlHelper from "../../core/models/utils/UrlHelper";
import BaseWebApiService from "../../core/services/api-service/BaseWebApiService"
import { toCallback } from "../../core/services/api-service/Callback";
import { Dispatch, SetStateAction } from "react";

const useGetData = <T extends object>(url: string, initGet: boolean = true): [receivedModel: T, isLoading: boolean, pingGetData: Dispatch<SetStateAction<any>>] => {
    const apiService = new BaseWebApiService();
    const [model, setModel] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const notification = useContext(NotificationContext);
    const [parameters, setParameters] = useState({});
   
    async function get () {
        let params = parameters;
        if(params === null || params === undefined)
            params = {}
        
        setIsLoading(true);
        await apiService.getAuthorized<T>(UrlHelper.generateParameters(url, params), toCallback(
            (data) => setModel(data),
            (warning) => notification.showWarningMessage(warning.message),
            (error) => notification.showErrorMessage(error.message)
        )).finally(() => setIsLoading(false));
    };

    useEffect(() => {
        if(initGet)
            get();
    }, []);

    useEffect(() => {
        get();
    }, [parameters])
    return [model, isLoading, setParameters];
};
export default useGetData;