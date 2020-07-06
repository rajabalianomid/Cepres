import React, { Component } from 'react';
import { Tabs, Tab, Navbar, Form, Nav, Button } from 'react-bootstrap';
import { _RecordInfo } from './_RecordInfo';
import { IRecordCreateOrUpdate } from '../../model/IRecordCreateOrUpdate'
import { Link } from 'react-router-dom';

export class RecordCreateOrUpdate extends Component<{}, IRecordCreateOrUpdate> {
    static displayName = RecordCreateOrUpdate.name;

    constructor(props: any) {
        super(props);
        this.state = {
            id: Number(new URLSearchParams(window.location.search).get('recordId')),
            submitted: false
        };
    }
    render() {
        return (
            <div>
                <Navbar bg="light" expand="lg">
                    <Navbar.Brand href="#home">Record</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="mr-auto">

                        </Nav>
                        <Form inline>
                            <Nav className="mr-auto">
                                <Link className="text-dark" to={{ pathname: "/recordlist" }}><Button variant="outline-info">Back to list</Button></Link>
                            </Nav>
                        </Form>
                    </Navbar.Collapse>
                </Navbar>
                <p></p>
                <Tabs defaultActiveKey="home" id="uncontrolled-tab-example">
                    <Tab eventKey="home" title="Record Info">
                        <_RecordInfo recordId={this.state.id} />
                    </Tab>
                </Tabs>
            </div>
        );
    }
}