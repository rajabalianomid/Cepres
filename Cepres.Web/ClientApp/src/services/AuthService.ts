import { Log, User, UserManager } from 'oidc-client';

import { Constants } from '../model/Constants';

export class AuthService {
    public userManager: UserManager;

    constructor() {
        const settings = {
            authority: Constants.stsAuthority,
            client_id: Constants.clientId,
            redirect_uri: `${Constants.clientRoot}callback`,
            //silent_redirect_uri: `${Constants.clientRoot}silent-renew.html`,
            // tslint:disable-next-line:object-literal-sort-keys
            post_logout_redirect_uri: `${Constants.clientRoot}`,
            response_type: 'code',
            scope: Constants.clientScope
        };
        this.userManager = new UserManager(settings);

        Log.logger = console;
        Log.level = Log.INFO;
    }

    getUser() {
        return this.userManager.getUser();
    }

    login() {
        return this.userManager.signinRedirect();
    }

    renewToken(){
        return this.userManager.signinSilent();
    }

    logout() {
        return this.userManager.signoutRedirect();
    }
}

export default new AuthService();
