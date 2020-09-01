import generateBasicUrl from '../../../configuration/model/apiSettings';
import fetch from 'node-fetch'
import { ICallback } from './Callback';
import { ErrorDetails } from '../../models/errors/ErrorDetail';
import { Agent } from 'https';

class BaseWebApiService {
    protected headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
    };

    public post<T>(url: string, data: object, callback: ICallback<T>): void {
        fetch(`${generateBasicUrl()}${url}`, {
            method: 'post',
            headers: new Headers(this.headers),
            body: JSON.stringify(data)
        }).then(response => {
            if(response.ok){
                if(callback.OnSuccess != null)
                {
                    response.json().then(m => callback.OnSuccess(m));
                }
            } else {
                response.json().then(e => {
                    if((e as ErrorDetails) && callback.OnError)
                    {
                        callback.OnError(e);
                        return;
                    }

                    if(callback.OnValidationError)
                        callback.OnValidationError(e);
                })
            }
        }).catch(error => console.log(error));
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
                    if((e as ErrorDetails) && callback.OnError) {
                        callback.OnError(e);
                        return;
                    }
                    if(callback.OnValidationError) {
                        callback.OnValidationError(e);
                        return;
                    }
                })
            }
        })
    }
}

export default BaseWebApiService;