import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { Constants } from '../../model/Constants';

export class Home extends Component<{}, any> {
    static displayName = Home.name;

    constructor(props: any) {
        super(props);
        this.state = {
            refresh: Number(new URLSearchParams(window.location.search).get('refresh'))
        };
    }
    componentDidMount() {
        var refresh = new URLSearchParams(window.location.search).get('refresh');
        if (refresh)
            window.location.href = Constants.clientRoot;
    }
    render() {
        return (
            this.state.refresh ? (<Redirect to="/" />) :
                (<div>
                    <h1>Hello, Guys!</h1>
                    <p>The application technologies:</p>
                    <ul>
                        <li><a href='#'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
                        <li><a href='#'>React</a> for client-side code</li>
                        <li><a href='#'>Bootstrap</a> for layout and styling</li>
                        <li><a href='#'>SQL Server</a> for Database</li>
                        <li><a href='#'>TSQL, EF, Store Prosedure, Linq</a> for working with Database</li>
                        <li><a href='#'>IOC</a> for Dependency injection</li>
                        <li><a href='#'>Auto Mapper</a> for Mapp objects</li>
                        <li><a href='#'>Identity Server</a>  for Authorize</li>
                        <li><a href='#'>Web API</a> connection server-side to client-side</li>
                    </ul>
                    <p>For use the application you have to login at first</p>
                    <ul>
                        <li><strong>User name:</strong> test</li>
                        <li><strong>Password:</strong> test</li>
                    </ul>
                    <p>The application implemented via<code> Omid Rajabalian</code> you could reach me with my personal website: <code><a href=" http://omidrajabalian.com"> http://omidrajabalian.com</a></code>.</p>
                </div>)
        );
    }
}
