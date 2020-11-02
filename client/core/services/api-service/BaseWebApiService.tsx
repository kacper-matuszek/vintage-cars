import generateBasicUrl from '../../../configuration/model/apiSettings';
import fetch from 'node-fetch'
import { ICallback, ICallbackBase } from './Callback';
import { ErrorDetails } from '../../models/errors/ErrorDetail';
import { Agent } from 'https';
import { getAuthorizationToken } from '../../models/utils/GetAuthorizationToken';

enum Method {
    get = 'get',
    post = 'post'
}

class BaseWebApiService {  
    public async post<T>(url: string, data: object, callback: ICallback<T>): Promise<T> {
        return await this.baseApiCallWithBodyAndResponse(url, data, callback, Method.post, new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }))
    }
    public async postAuthorized<T>(url: string, data: object, callback: ICallback<T>): Promise<T> {
        return await this.baseApiCallWithBodyAndResponse(url, data, callback, Method.post, new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${getAuthorizationToken()}`,
        }))
    }

    public async postWithoutResponse(url: string, data: object, callback: ICallbackBase): Promise<void> {
        return await this.baseApiCallWithBodyWithoutResponse(url, data, callback, Method.post, new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }))
    }
    public async postWithoutResponseAuthorized(url: string, data: object, callback: ICallbackBase): Promise<void> {
        return await this.baseApiCallWithBodyWithoutResponse(url, data, callback, Method.post, new Headers({
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${getAuthorizationToken()}`,
        }))
    }

    public async get<T>(url: string, callback: ICallback<T>): Promise<T> {
        return await this.baseApiCallWithResponse(url, callback, Method.get, new Headers({'Content-Type': 'application/json'}));
    }

    public async getAuthorized<T>(url: string, callback: ICallback<T>): Promise<T> {
        return await this.baseApiCallWithResponse(url, callback, Method.get, new Headers(
            {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${getAuthorizationToken()}`
            }));
    }
 
    private async baseApiCallWithResponse<T>(url: string, callback: ICallback<T>, method: Method, headers: Headers): Promise<T> {
        return await this.withResult<T>(fetch(`${generateBasicUrl()}${url}`, {
            method: method,
            headers: headers,
            agent: new Agent({
                rejectUnauthorized: false
            })
        }), callback);
    }
    
    private async baseApiCallWithBodyWithoutResponse(url: string, data: object, callback: ICallbackBase, method: Method, headers: Headers): Promise<void>{
        return await this.withoutResult(fetch(`${generateBasicUrl()}${url}`, {
            method: method,
            headers: headers,
            body: JSON.stringify(data),
            agent: new Agent({
                rejectUnauthorized: false
            })
        }), callback);
    }
    private async baseApiCallWithBodyAndResponse<T>(url: string, data: object, callback: ICallback<T>, method: Method, headers: Headers): Promise<T> {
        return await this.withResult<T>(fetch(`${generateBasicUrl()}${url}`, {
            method: method,
            headers: headers,
            body: JSON.stringify(data),
            agent: new Agent({
                rejectUnauthorized: false
            })
        }), callback);
    }
    private async withResult<T>(promise: Promise<any>, callback: ICallback<T>): Promise<T> {
        return await promise.then(response => {
            if(response.status == 204)
            {
                if(callback.OnSuccess)
                    return callback.OnSuccess(null);
            }
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
        });
    }
    private async withoutResult(promise: Promise<any>, callback: ICallbackBase): Promise<void> {
        return await promise.then(response => {
            if(response.ok){
                if(callback.OnSuccess)
                {
                    callback.OnSuccess();
                    return;
                }
            } else {
                console.log(response);
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
        }).catch(error => console.log(error));
    }
}
export default BaseWebApiService;