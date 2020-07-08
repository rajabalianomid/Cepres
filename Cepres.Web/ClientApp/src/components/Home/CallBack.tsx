import React, { Component, useState } from 'react';
import { UserManager, User } from 'oidc-client';
import { ICallBackState } from '../../model/ICallBackState';
import { Alert } from 'reactstrap';
import { Redirect } from 'react-router-dom';

export class CallBack extends Component<{}, ICallBackState> {
    static displayName = CallBack.name
    constructor(props: any) {
        super(props);
        this.state =
        {
            user: null
        };
    }
    componentDidMount() {
        new UserManager({ response_mode: "query" }).signinRedirectCallback().then(response => {
            debugger;
            this.setState({
                user: response
            });
        }).catch(function (e) {
            console.error(e);
        });
    }
    render() {
        return (
            <div>
                <noscript>
                    <h1>You need to enable JavaScript to run this app.</h1>
                </noscript>
                {
                    (
                        this.state.user
                            ? <><Redirect to="/?refresh=true" /></>
                            : <h1>Authentification callback processing...</h1>
                    )
                }
            </div>
        );
    }
}
