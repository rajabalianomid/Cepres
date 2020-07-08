import axios, { AxiosInstance } from 'axios';
import { Constants } from '../model/Constants';
import { AuthService } from './AuthService';

export class ApiService {
    private authService: AuthService;

    constructor() {
        this.authService = new AuthService();
    }

    public callApi(endpoint: string, method: string, data?: any): Promise<any> {
        return this.authService.getUser().then(user => {
            debugger;
            if (user && user.access_token) {
                return this._callApi(endpoint, method, user.access_token, data).catch(error => {
                    if (error.response.status === 401) {
                        return this.authService.renewToken().then(renewedUser => {
                            return this._callApi(endpoint, method, renewedUser.access_token, data);
                        });
                    }
                    throw error;
                });
            } else if (user) {
                return this.authService.renewToken().then(renewedUser => {
                    return this._callApi(endpoint, method, renewedUser.access_token, data);
                });
            } else {
                throw new Error('user is not logged in');
            }
        });
    }

    private _callApi(endpoint: string, method: string, token: string, data?: any) {
        const headers = {
            Accept: 'application/json',
            Authorization: 'Bearer ' + token
        };
        switch (method.toLowerCase()) {
            case "post":
                return axios.post(endpoint, data, { headers });
            case "put":
                return axios.put(endpoint, data, { headers });
            case "delete":
                return axios.delete(endpoint, { headers });
            default:
                return axios.get(endpoint, { headers });
        }

    }
}

export default new ApiService();
