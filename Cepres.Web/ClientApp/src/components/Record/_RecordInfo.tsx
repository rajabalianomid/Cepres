import React, { Component } from 'react';
import RecordService from '../../services/RecordService';
import PatientService from '../../services/PatientService';
import { IRecordState } from '../../model/IRecord';
import { Form, Alert } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import AsyncSelect from 'react-select/async'
import CurrencyInput from 'react-currency-input-field';

export class _RecordInfo extends Component<{ recordId: any }, IRecordState> {
    constructor(props: any) {
        super(props);

        this.state = {
            autocompleteData: [],
            currentRecord: {
                timeOfEntry: "",
                diseaseName: "",
                name: "",
                patientId: "",
                description: "",
                bill: 0
            },
            id: 0,
            message: null,
            isUpdate: false,
            submitted: false,
            validated: false
        };
        this.onChangeName = this.onChangeName.bind(this);
        this.onChangeDiseaseName = this.onChangeDiseaseName.bind(this);
        this.onChangeDescription = this.onChangeDescription.bind(this);
        this.onChangeBill = this.onChangeBill.bind(this);
        this.newRecord = this.newRecord.bind(this);
        this.saveRecord = this.saveRecord.bind(this);
        this.currentUpdate = this.currentUpdate.bind(this);
        this.getPatient = this.getPatient.bind(this);
    }
    componentDidMount() {
        debugger;
        if (this.props.recordId > 0) {
            RecordService.get(this.props.recordId)
                .then(response => {
                    if (response.data.status) {
                        this.setState({
                            currentRecord: {
                                timeOfEntry: response.data.result.timeOfEntry,
                                diseaseName: response.data.result.diseaseName,
                                name: response.data.result.name,
                                patientId: response.data.result.patientId,
                                description: response.data.result.description,
                                bill: response.data.result.bill
                            },
                            id: response.data.result.id,
                        });
                        console.log(response.data);
                    }
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
    onChangeName(e: any) {
        debugger;
        console.log(e);
        const currentRecord = this.state.currentRecord;
        currentRecord.patientId = e.value;
        currentRecord.name = e.label;

        this.setState({
            currentRecord: currentRecord
        });
        console.log(this.state);
    }
    onChangeDiseaseName(e: any) {
        const currentRecord = this.state.currentRecord;
        currentRecord.diseaseName = e.target.value;
        this.setState({
            currentRecord: currentRecord
        });
    }
    onChangeDescription(e: any) {
        const currentRecord = this.state.currentRecord;
        currentRecord.description = e.target.value;
        this.setState({
            currentRecord: currentRecord
        });
    }
    onChangeBill(e: any) {
        debugger;
        var regex = /^[0-9]+(\.[0-9]{1,2})?$/;
        const currentRecord = this.state.currentRecord;
        if (regex.test(e)) {
            currentRecord.bill = parseInt(e);
        }
        else if (e == undefined) {
            currentRecord.bill = 0;
        }
        this.setState({
            currentRecord: currentRecord
        });
    }
    saveRecord(e: any) {
        e.preventDefault();
        debugger;
        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        this.setState({ validated: true });

        var data = {
            id: this.state.id,
            diseaseName: this.state.currentRecord.diseaseName,
            name: this.state.currentRecord.name,
            patientId: parseInt(this.state.currentRecord.patientId),
            bill: this.state.currentRecord.bill,
            description: this.state.currentRecord.description
        };

        if (form.checkValidity() === true) {
            debugger;
            var service = this.state.id > 0 ? RecordService.update(data) : RecordService.create(data);
            service
                .then(response => {
                    if (response.data.status) {
                        this.setState({
                            currentRecord: {
                                timeOfEntry: "",
                                diseaseName: response.data.result.diseaseName,
                                name: response.data.result.name,
                                patientId: response.data.result.patientId,
                                description: response.data.result.description,
                                bill: response.data.result.bill
                            },
                            isUpdate: response.data.actionName == 'Update',
                            id: response.data.result.id,
                            submitted: true
                        });
                        console.log(response.data);
                    }
                })
                .catch(e => {
                    console.log(e);
                });
        }
    }
    newRecord() {
        this.setState({
            currentRecord: {
                timeOfEntry: "",
                diseaseName: "",
                name: "",
                patientId: "",
                description: "",
                bill: 0
            },
            id: 0,
            message: null,
            submitted: false,
            validated: false
        });
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
    getPatient(inputtext: any, callback: any) {

        PatientService.getByName(inputtext)
            .then(response => {
                if (response.data.status) {
                    this.setState({
                        autocompleteData: response.data.result
                    });
                    console.log(response.data);
                }
            })
            .catch(e => {
                console.log(e);
            });
        callback(this.state.autocompleteData);
    }
    render() {
        return (
            <Form noValidate validated={this.state.validated} onSubmit={this.saveRecord}>
                {
                    this.state.submitted ?
                        (<div>
                            <Alert variant="success">
                                <Alert.Heading>Record Info</Alert.Heading>
                                <p>
                                    {!this.state.isUpdate ? (<>The record added success</>) : (<>The record updated success</>)}
                                </p>
                                <hr />
                                <p className="mb-0">
                                    <Link className="text-dark" to={{ pathname: "/record" }} onClick={this.newRecord}>Add new record</Link>
                                </p>
                                <p className="mb-0">
                                    <Link className="text-dark" to={{ pathname: "/record", search: "?recordId=" + this.props.recordId }} onClick={this.currentUpdate}>Update current record</Link>
                                </p>
                            </Alert>
                        </div>)
                        :
                        (<div>
                            <div className="form-group">
                                <label htmlFor="name">Name</label>
                                <AsyncSelect type="text" id="name" required loadOptions={this.getPatient} option placeholder={'type something ...'} onChange={this.onChangeName} value={{ value: this.state.currentRecord.patientId, label: this.state.currentRecord.name }} name="name" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="diseaseName">Disease Name</label>
                                <Form.Control type="diseaseName" id="diseaseName" value={this.state.currentRecord.diseaseName} onChange={this.onChangeDiseaseName} name="diseaseName" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="bill">Bill</label>
                                <CurrencyInput className="form-control" id="bill" required decimalsLimit={2} value={this.state.currentRecord.bill} onChange={this.onChangeBill} name="bill" />
                            </div>
                            <div className="form-group">
                                <label htmlFor="description">Description</label>
                                <Form.Control as="textarea" rows={3} id="description" value={this.state.currentRecord.description} onChange={this.onChangeDescription} name="description" />
                            </div>
                            <button type="submit" className="btn btn-success">Submit</button>
                        </div>)
                }
            </Form>
        );
    }
}