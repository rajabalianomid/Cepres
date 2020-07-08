"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
var AuthService_1 = require("./AuthService");
var ApiService = /** @class */ (function () {
    function ApiService() {
        this.authService = new AuthService_1.AuthService();
    }
    ApiService.prototype.callApi = function (endpoint, method, data) {
        var _this = this;
        return this.authService.getUser().then(function (user) {
            debugger;
            if (user && user.access_token) {
                return _this._callApi(endpoint, method, user.access_token, data).catch(function (error) {
                    if (error.response.status === 401) {
                        return _this.authService.renewToken().then(function (renewedUser) {
                            return _this._callApi(endpoint, method, renewedUser.access_token, data);
                        });
                    }
                    throw error;
                });
            }
            else if (user) {
                return _this.authService.renewToken().then(function (renewedUser) {
                    return _this._callApi(endpoint, method, renewedUser.access_token, data);
                });
            }
            else {
                throw new Error('user is not logged in');
            }
        });
    };
    ApiService.prototype._callApi = function (endpoint, method, token, data) {
        var headers = {
            Accept: 'application/json',
            Authorization: 'Bearer ' + token
        };
        switch (method.toLowerCase()) {
            case "post":
                return axios_1.default.post(endpoint, data, { headers: headers });
            case "put":
                return axios_1.default.put(endpoint, data, { headers: headers });
            case "delete":
                return axios_1.default.delete(endpoint, { headers: headers });
            default:
                return axios_1.default.get(endpoint, { headers: headers });
        }
    };
    return ApiService;
}());
exports.ApiService = ApiService;
exports.default = new ApiService();
//# sourceMappingURL=ApiService.js.map