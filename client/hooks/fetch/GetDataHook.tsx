import { useContext, useEffect, useState } from "react";
import NotificationContext from "../../contexts/NotificationContext";
import BaseWebApiService from "../../core/services/api-service/BaseWebApiService"
import { toCallback } from "../../core/services/api-service/Callback";

const useGetData = <T extends object>(url: string): [receivedModel: T, isLoading: boolean] => {
    const apiService = new BaseWebApiService();
    const [model, setModel] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const notification = useContext(NotificationContext);
   
    async function get () {
        setIsLoading(true);
        await apiService.getAuthorized<T>(`${url}`, toCallback(
            (data) => setModel(data),
            (warning) => notification.showWarningMessage(warning.message),
            (error) => notification.showErrorMessage(error.message)
        )).finally(() => setIsLoading(false));
    };

    useEffect(() => {
        get();
    }, []);
    return [model, isLoading];
};
export default useGetData;