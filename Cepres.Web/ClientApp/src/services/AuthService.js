"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var oidc_client_1 = require("oidc-client");
var Constants_1 = require("../model/Constants");
var AuthService = /** @class */ (function () {
    function AuthService() {
        var settings = {
            authority: Constants_1.Constants.stsAuthority,
            client_id: Constants_1.Constants.clientId,
            redirect_uri: Constants_1.Constants.clientRoot + "callback",
            //silent_redirect_uri: `${Constants.clientRoot}silent-renew.html`,
            // tslint:disable-next-line:object-literal-sort-keys
            post_logout_redirect_uri: "" + Constants_1.Constants.clientRoot,
            response_type: 'code',
            scope: Constants_1.Constants.clientScope
        };
        this.userManager = new oidc_client_1.UserManager(settings);
        oidc_client_1.Log.logger = console;
        oidc_client_1.Log.level = oidc_client_1.Log.INFO;
    }
    AuthService.prototype.getUser = function () {
        return this.userManager.getUser();
    };
    AuthService.prototype.login = function () {
        return this.userManager.signinRedirect();
    };
    AuthService.prototype.renewToken = function () {
        return this.userManager.signinSilent();
    };
    AuthService.prototype.logout = function () {
        return this.userManager.signoutRedirect();
    };
    return AuthService;
}());
exports.AuthService = AuthService;
exports.default = new AuthService();
//# sourceMappingURL=AuthService.js.map