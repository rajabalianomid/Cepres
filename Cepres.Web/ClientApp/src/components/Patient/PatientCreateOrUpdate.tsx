import React, { Component } from 'react';
import { Tabs, Tab, Navbar, Form, Nav, Button } from 'react-bootstrap';
import { _PatientInfo } from './_PatientInfo';
import { _PatientMetaData } from './_PatientMetaData';
import { IPatientCreateOrUpdate } from '../../model/IPatientCreateOrUpdate'
import { Link } from 'react-router-dom';

export class PatientCreateOrUpdate extends Component<{}, IPatientCreateOrUpdate> {
    static displayName = PatientCreateOrUpdate.name;

    constructor(props: any) {
        super(props);
        this.state = {
            id: Number(new URLSearchParams(window.location.search).get('patientId')),
            submitted: false,
            metaDataComponent: React.createRef()
        };
    }
    onCallBackPatinetSubmited = (patientId: any, submitted: boolean = true) => {
        this.setState({
            id: patientId,
            submitted: submitted
        });
        this.state.metaDataComponent.current?.dataBind();
    }
    render() {
        return (
            <div>
                <Navbar bg="light" expand="lg">
                    <Navbar.Brand href="#home">Patient</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="mr-auto">

                        </Nav>
                        <Form inline>
                            <Nav className="mr-auto">
                                <Link className="text-dark" to={{ pathname: "/patientlist" }}><Button variant="outline-info">Back to list</Button></Link>
                            </Nav>
                        </Form>
                    </Navbar.Collapse>
                </Navbar>
                <p></p>
                <Tabs defaultActiveKey="home" id="uncontrolled-tab-example">
                    <Tab eventKey="home" title="Patient Info">
                        <_PatientInfo patientId={this.state.id} onCallBackPatinetSubmited={this.onCallBackPatinetSubmited} />
                    </Tab>
                    <Tab eventKey="profile" title="Patient extra data">
                        <_PatientMetaData patientId={this.state.id} submitted={this.state.submitted} ref={this.state.metaDataComponent} />
                    </Tab>
                </Tabs>
            </div>
        );
    }
}