import { Dispatch, SetStateAction, useCallback, useEffect, useState } from "react";
import { IModel } from "../../../core/models/base/IModel";
import { ErrorDetails } from "../../../core/models/errors/ErrorDetail";
import Paged from "../../../core/models/paged/Paged";
import PagedList from "../../../core/models/paged/PagedList";
import BaseWebApiService from "../../../core/services/api-service/BaseWebApiService";
import { toCallback } from "../../../core/services/api-service/Callback";

const usePagedList = <T extends IModel>(url: string, onError?: (message: string) => void): [Dispatch<SetStateAction<Paged>>, boolean, PagedList<T>] => {
    const [isLoading, setIsLoading] = useState(false);
    const [response, setResponse] = useState(new PagedList<T>());
    const [paged, setPaged] = useState<Paged>(null);
    const apiService = new BaseWebApiService();

    async function getData() {
        setIsLoading(true);
        await apiService.get<PagedList<T>>(`${url}?pageIndex=${paged.pageIndex}&pageSize=${paged.pageSize}`, toCallback(
            (data) => setResponse(data),
            (validError) => handleError(validError),
            (error) => handleError(error)
        ))
    }
    const handleError = (error: ErrorDetails) => {
        if(onError) {
            onError(error?.message);
        }
    }
    
    useEffect(() => {
        if(paged === null) return;
        getData();
    }, [paged]);

    return [setPaged, isLoading, response];
}
export default usePagedList;