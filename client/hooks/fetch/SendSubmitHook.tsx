import { useContext, useState } from "react";
import NotificationContext from "../../contexts/NotificationContext";
import isEmpty from "../../core/models/utils/StringExtension";
import BaseWebApiService from "../../core/services/api-service/BaseWebApiService"
import { postCallback } from "../../core/services/api-service/Callback";

const useSendSubmitWithNotification = <T extends object>(url: string, showLoading?: () => void, hideLoading?: () => void, successMessage?: string): [send: (model: any) => Promise<void>] => {
    const apiService = new BaseWebApiService();
    const notification = useContext(NotificationContext);

    const submit = async (object: any) => {
        if(showLoading !== undefined && showLoading !== null)
            showLoading();
        await apiService.postWithoutResponseAuthorized(`${url}`, object, postCallback(
            () => {
                const message = isEmpty(successMessage) ? 'Zapisano pomyślnie' : successMessage
                notification.showSuccessMessage(message);
            },
            (warning) => notification.showWarningMessage(warning.message),
            (error) => notification.showErrorMessage(error.message)
        )).finally(() => 
        {
            if(hideLoading !== undefined && hideLoading !== null)
                hideLoading()
        });
    }

    return [submit];
}
export default useSendSubmitWithNotification;