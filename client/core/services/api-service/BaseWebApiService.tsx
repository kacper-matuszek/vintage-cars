import generateBasicUrl from '../../../configuration/model/apiSettings';
import fetch from 'node-fetch'
import { ICallback, ICallbackBase } from './Callback';
import { ErrorDetails } from '../../models/errors/ErrorDetail';
import { Agent } from 'https';

class BaseWebApiService {
    protected headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    };

    public async post<T>(url: string, data: object, callback: ICallback<T>): Promise<T> {
        return await fetch(`${generateBasicUrl()}${url}`, {
            method: 'post',
            headers: new Headers(this.headers),
            body: JSON.stringify(data)
        }).then(response => {
            if(response.ok){
                if(callback.OnSuccess != null)
                {
                    return response.json().then(m => callback.OnSuccess(m));
                }
            } else {
                response.json().then(e => {
                    if(response.status  == 400 && (e as ErrorDetails) && callback.OnValidationError)
                    {
                        callback.OnValidationError(e);
                        return;
                    }
                    if((e as ErrorDetails) && callback.OnError)
                    {
                        callback.OnError(e);
                        return;
                    }
                })
            }
        }).catch(error => console.log(error));
    }

    public async postWithoutResponse(url: string, data: object, callback: ICallbackBase): Promise<void> {
        return await fetch(`${generateBasicUrl()}${url}`, {
            method: 'post',
            headers: new Headers(this.headers),
            body: JSON.stringify(data)
        }).then(response => {
            if(response.ok){
                if(callback.OnSuccess)
                {
                    callback.OnSuccess();
                    return;
                }
            } else {
                response.json().then(e => {
                    if(response.status == 400 && (e as ErrorDetails) && callback.OnValidationError){
                        callback.OnValidationError(e);
                        return;
                    }
                    if((e as ErrorDetails) && callback.OnError) {
                        callback.OnError(e);
                        return;
                    }
                })
            }
        })
    }

    public async get<T>(url: string, callback: ICallback<T>): Promise<T> {
        return await fetch(`${generateBasicUrl()}${url}`, {
            method: 'get',
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            agent: new Agent({
                rejectUnauthorized: false
            })
        }).then(response => {
            if(response.ok) {
                if(callback.OnSuccess) {
                    return response.json().then(m => callback.OnSuccess(m));
                }
            } else {
                response.json().then(e => {
                    if(response.status == 400 && (e as ErrorDetails) && callback.OnValidationError) {
                        callback.OnValidationError(e);
                        return;
                    }
                    if((e as ErrorDetails) && callback.OnError) {
                        callback.OnError(e);
                        return;
                    }
                })
            }
        })
    }
}

export default BaseWebApiService;