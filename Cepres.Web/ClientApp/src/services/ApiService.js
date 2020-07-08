"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
var Constants_1 = require("../model/Constants");
var AuthService_1 = require("./AuthService");
var ApiService = /** @class */ (function () {
    function ApiService() {
        this.authService = new AuthService_1.AuthService();
    }
    ApiService.prototype.callApi = function () {
        var _this = this;
        return this.authService.getUser().then(function (user) {
            if (user && user.access_token) {
                return _this._callApi(user.access_token).catch(function (error) {
                    if (error.response.status === 401) {
                        return _this.authService.renewToken().then(function (renewedUser) {
                            return _this._callApi(renewedUser.access_token);
                        });
                    }
                    throw error;
                });
            }
            else if (user) {
                return _this.authService.renewToken().then(function (renewedUser) {
                    return _this._callApi(renewedUser.access_token);
                });
            }
            else {
                throw new Error('user is not logged in');
            }
        });
    };
    ApiService.prototype._callApi = function (token) {
        var headers = {
            Accept: 'application/json',
            Authorization: 'Bearer ' + token
        };
        return axios_1.default.get(Constants_1.Constants.apiRoot + 'test', { headers: headers });
    };
    return ApiService;
}());
exports.default = new ApiService();
//# sourceMappingURL=ApiService.js.map