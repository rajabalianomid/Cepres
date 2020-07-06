import React, { Component } from 'react';
import { Alert, Form, Button } from 'react-bootstrap';
import MetaDataService from '../../services/MetaDataService';
import { IMetaDataState, IMetaData } from '../../model/IMetaData';
import { Helper } from '../../Helper';

export class _PatientMetaData extends Component<{ patientId: any, submitted: boolean }, IMetaDataState> {
    constructor(props: any) {
        super(props);

        this.state = {
            metaData: {
                id: 0,
                key: "",
                value: "",
                patientId: 0
            },
            arrMetaData: null,
            message: null,
            validated: false,
            submitted: false
        };
        this.onChangeKey = this.onChangeKey.bind(this);
        this.onChangeValue = this.onChangeValue.bind(this);
        this.newMetaData = this.newMetaData.bind(this);
        this.saveMetaDate = this.saveMetaDate.bind(this);
        this.getAllMetaData = this.getAllMetaData.bind(this);
        this.removeMetaData = this.removeMetaData.bind(this);
    }
    dataBind() {
        this.getAllMetaData();
    }
    componentDidMount() {
        this.getAllMetaData();
    }
    onChangeKey(e: any) {
        const currentMetaData = this.state.metaData;
        currentMetaData.key = e.target.value;
        this.setState({
            metaData: currentMetaData
        });
    }
    onChangeValue(e: any) {
        const currentMetaData = this.state.metaData;
        currentMetaData.value = e.target.value;
        this.setState({
            metaData: currentMetaData
        });
    }
    saveMetaDate(e: any) {
        e.preventDefault();
        debugger;
        const form = e.currentTarget;
        if (form.checkValidity() === false) {
            e.preventDefault();
            e.stopPropagation();
        }
        this.setState({ validated: true });
        var data = {
            key: this.state.metaData.key,
            value: this.state.metaData.value,
            patientId: this.props.patientId
        };

        MetaDataService.create(data)
            .then(response => {
                this.setState({
                    metaData:
                    {
                        id: response.data.result.id,
                        key: response.data.result.key,
                        value: response.data.result.value,
                        patientId: response.data.result.patientId
                    },
                });
                console.log(response.data);
                this.getAllMetaData();
            })
            .catch(e => {
                console.log(e);
            });
    }
    newMetaData() {
        this.setState({
            metaData: {
                id: 0,
                key: "",
                value: "",
                patientId: this.props.patientId
            },
            message: null,
            validated: false
        });
    }
    getAllMetaData() {
        debugger;
        MetaDataService.getAllByPatientId(this.props.patientId)
            .then(response => {
                this.setState({
                    arrMetaData: response.data.result
                });
                console.log(response.data);
                this.newMetaData();
            })
            .catch(e => {
                console.log(e);
            });
    }
    removeMetaData(id: number) {
        new Helper().confirmation('remove confirm', `are you sure to delete item with id:${id}`, () => {
            debugger;
            MetaDataService.delete(id)
                .then(response => {
                    console.log(response.data);
                    this.getAllMetaData();
                })
                .catch(e => {
                    console.log(e);
                });
        });
    }
    renderMetaDatas(metaDatas: IMetaData[]) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Key</th>
                        <th>Value</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {metaDatas.map(metadata =>
                        <tr key={metadata.id}>
                            <td>{metadata.key}</td>
                            <td>{metadata.value}</td>
                            <td><Button variant="danger" onClick={(event: any) => this.removeMetaData(metadata.id)}>X</Button></td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }
    render() {


        let contents = this.state.arrMetaData == null
            ? <p><em>data don't exist</em></p>
            : this.renderMetaDatas(this.state.arrMetaData);

        return (
            <div className="submit-form">
                {
                    this.props.patientId == 0 ?
                        (<div>
                            <Alert variant="info">
                                <Alert.Heading>Add Extra Data</Alert.Heading>
                                <p>
                                    Extra data will be available after add Patient Info
                               </p>
                            </Alert>
                        </div>)
                        :
                        (<div>
                            <div>
                                <Form noValidate validated={this.state.validated} onSubmit={this.saveMetaDate}>
                                    <div className="form-group">
                                        <label htmlFor="key">Key</label>
                                        <input type="text" className="form-control" id="key" required value={this.state.metaData.key} onChange={this.onChangeKey} name="name" />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="value">Value</label>
                                        <input type="text" className="form-control" id="value" required value={this.state.metaData.value} onChange={this.onChangeValue} name="value" />
                                    </div>
                                    <button onClick={this.saveMetaDate} className="btn btn-success">Submit</button>
                                </Form>
                            </div>
                            <div>
                                <h1 id="tabelLabel" >Meta Data</h1>
                                {contents}
                            </div>
                        </div>)
                }
            </div>
        );
    }
}