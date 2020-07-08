import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './Menu/NavMenu';

export class LayoutSimple extends Component {
    static displayName = LayoutSimple.name;

    render() {
        return (
            <div>
                <Container>
                    {this.props.children}
                </Container>
            </div>
        );
    }
}
