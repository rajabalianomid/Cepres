import React, { Component } from 'react';
import PatientService from '../../services/PatientService';
import { IPatientState } from '../../model/IPatient';
import { Form, Alert } from 'react-bootstrap';
import { Link } from 'react-router-dom';

export class _PatientInfo extends Component<{ onCallBackPatinetSubmited: any, patientId: any }, IPatientState> {
    constructor(props: any) {
        super(props);

        this.state = {
            currentPatient: {
                dateOfBirth: "",
                email: "",
                name: "",
                officialId: ""
            },
            id: 0,
            message: null,
            isUpdate: false,
            submitted: false,
            validated: false
        };
        this.onChangeDateOfBirth = this.onChangeDateOfBirth.bind(this);
        this.onChangeName = this.onChangeName.bind(this);
        this.onChangeOfficialId = this.onChangeOfficialId.bind(this);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangeDateOfBirth = this.onChangeDateOfBirth.bind(this);
        this.newPatient = this.newPatient.bind(this);
        this.savePatient = this.savePatient.bind(this);
        this.currentUpdate = this.currentUpdate.bind(this);
        this.isValidDateOfBirth = this.isValidDateOfBirth.bind(this);
    }
    componentDidMount() {
        if (this.props.patientId > 0) {
            PatientService.get(this.props.patientId)
                .then(response => {
                    if (response.data.status) {
                        this.setState({
                            currentPatient: {
                                dateOfBirth: response.data.result.dateOfBirth,
                                email: response.data.result.email,
                                name: response.data.result.name,
                                officialId: response.data.result.officialId
                            },
                            id: response.data.result.id,
                        });
                        if (typeof this.props.onCallBackPatinetSubmited === 'function') {
                            this.props.onCallBackPatinetSubmited(response.data.result.id);
                        }
                        console.log(response.data);
                    }
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
    onChangeName(e: any) {
        const currentPatient = this.state.currentPatient;
        currentPatient.name = e.target.value;
        this.setState({
            currentPatient: currentPatient
        });
    }
    onChangeOfficialId(e: any) {
        const re = /^[0-9\b]+$/;
        if (e.target.value === '' || re.test(e.target.value)) {
            const currentPatient = this.state.currentPatient;
            currentPatient.officialId = e.target.value;
            this.setState({
                currentPatient: currentPatient
            });
        }
    }
    onChangeEmail(e: any) {
        const currentPatient = this.state.currentPatient;
        currentPatient.email = e.target.value;
        this.setState({
            currentPatient: currentPatient
        });
    }
    onChangeDateOfBirth(e: any) {
        const currentPatient = this.state.currentPatient;
        currentPatient.dateOfBirth = e.target.value;
        this.setState({
            currentPatient: currentPatient
        });
    }
    savePatient(e: any) {
        e.preventDefault();
        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        this.setState({ validated: true });

        var data = {
            id: this.state.id,
            dateOfBirth: this.state.currentPatient.dateOfBirth,
            email: this.state.currentPatient.email,
            name: this.state.currentPatient.name,
            officialId: Number(this.state.currentPatient.officialId ?? 0)
        };

        if (form.checkValidity() === true) {
            var service = this.state.id > 0 ? PatientService.update(data) : PatientService.create(data);
            service
                .then(response => {
                    if (response.data.status) {
                        this.setState({
                            currentPatient: {
                                dateOfBirth: response.data.result.dateOfBirth,
                                email: response.data.result.email,
                                name: response.data.result.name,
                                officialId: response.data.result.officialId
                            },
                            isUpdate: response.data.actionName == 'Update',
                            id: response.data.result.id,
                            submitted: true
                        });
                        if (typeof this.props.onCallBackPatinetSubmited === 'function') {
                            this.props.onCallBackPatinetSubmited(response.data.result.id);
                        }
                        console.log(response.data);
                    }
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
    newPatient() {
        this.setState({
            currentPatient: {
                dateOfBirth: "",
                email: "",
                name: "",
                officialId: ""
            },
            id: 0,
            message: null,
            submitted: false,
            validated: false
        });
        this.props.onCallBackPatinetSubmited(0, false);
    }
    currentUpdate() {
        this.setState({ submitted: false });
        this.componentDidMount();
    }
    dateTimeLocalTime(time: any) {
        if (time != null)
            time = new Date(time?.toLocaleDateString());
        return time;
    }
    isValidDateOfBirth() {
        var date = this.state.currentPatient.dateOfBirth;
        var regex = /^((0?[1-9]|1[012])[- /.](0?[1-9]|[12][0-9]|3[01])[- /.](19|20)?[0-9]{2})*$/;
        return date != "" && !regex.test(this.state.currentPatient.dateOfBirth);
    }
    render() {
        return (
            <Form noValidate validated={this.state.validated} onSubmit={this.savePatient}>
                {
                    this.state.submitted ?
                        (<div>
                            <Alert variant="success">
                                <Alert.Heading>Patient Info</Alert.Heading>
                                <p>
                                    {!this.state.isUpdate ? (<>The patient added success</>) : (<>The patient updated success</>)}
                                </p>
                                <hr />
                                <p className="mb-0">
                                    <Link className="text-dark" to={{ pathname: "/patient" }} onClick={this.newPatient}>Add new patient</Link>
                                </p>
                                <p className="mb-0">
                                    <Link className="text-dark" to={{ pathname: "/patient", search: "?patientId=" + this.props.patientId }} onClick={this.currentUpdate}>Update current patient</Link>
                                </p>
                            </Alert>
                        </div>)
                        :
                        (<div>
                            <div className="form-group">
                                <label htmlFor="name">Name</label>
                                <Form.Control type="text" id="name" required value={this.state.currentPatient.name} onChange={this.onChangeName} name="name" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="officialId">Official Id</label>
                                <Form.Control type="text" id="officialId" required value={this.state.currentPatient.officialId} onChange={this.onChangeOfficialId} name="officialId" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="email">Email</label>
                                <Form.Control type="email" id="email" value={this.state.currentPatient.email} onChange={this.onChangeEmail} name="email" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="dateOfBirth">Date of birth</label>
                                <Form.Control type="text" id="name" required value={this.state.currentPatient.dateOfBirth} onChange={this.onChangeDateOfBirth} name="name" isInvalid={this.isValidDateOfBirth()} />
                            </div>
                            <button type="submit" className="btn btn-success">Submit</button>
                        </div>)
                }
            </Form>
        );
    }
}