import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { INavMenuState } from '../../model/INavMenuState';
import './NavMenu.css';
import { User } from 'oidc-client';
import AuthService from '../../services/AuthService';

export class NavMenu extends Component<{}, any> {
    static displayName = NavMenu.name;

    constructor(props: any) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true
        };
    }
    public componentDidMount() {
        debugger;
        this.getUser();
    }
    public login() {
        debugger;
        AuthService.login();
    };
    public logout() {
        AuthService.logout();
    };
    public getUser() {
        debugger;
        AuthService.getUser().then(user => {
            this.setState({ user });
        });
    };
    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }
    render() {
        var content = this.state.user
            ? (<ul className="navbar-nav flex-grow">
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/recordlist">Record</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/patientlist">Patient</NavLink>
                </NavItem>
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/report">Report</NavLink>
                </NavItem>
            </ul>)
            : (<ul className="navbar-nav flex-grow">
                <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
                </NavItem>
            </ul>);
        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/">Cepres Web</NavbarBrand>
                        <ul className="navbar-nav flex-grow">
                            {
                                this.state.user ?
                                    (<button className="btn btn-outline-danger my-2 my-sm-0" onClick={this.logout}>Logout</button>) :
                                    (<button className="btn btn-outline-success my-2 my-sm-0" onClick={this.login}>Login</button>)
                            }
                        </ul>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                            {content}
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
