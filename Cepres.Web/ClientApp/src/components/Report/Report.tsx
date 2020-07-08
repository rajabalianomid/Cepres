import React, { Component } from 'react';
import { Button, Modal, Badge } from 'react-bootstrap';
import PatientService from '../../services/PatientService';
import { IPatintReportState, IPatintReport } from '../../model/IPatient';
import 'react-confirm-alert/src/react-confirm-alert.css';
import MetaDataService from '../../services/MetaDataService';
import { IGeneral } from '../../model/IRecord';

export class Report extends Component<{}, IPatintReportState> {
    constructor(props: any) {
        super(props);

        this.state = {
            data: [],
            similarPatinet: [],
            metadata: [],
            average: 0,
            max: 0,
            show: false,
            message: null
        };
        this.getAllPatient = this.getAllPatient.bind(this);
        this.openModal = this.openModal.bind(this);
        this.RenderMetaData = this.RenderMetaData.bind(this);
    }
    componentDidMount() {
        this.getAllPatient();
        this.getAllMetaData();
    }
    getAllPatient() {
        PatientService.report()
            .then(response => {
                this.setState({
                    data: response.data.result
                });
                console.log(response.data);
            })
            .catch(e => {
                console.log(e);
            });
    }
    getAllMetaData() {
        MetaDataService.report()
            .then(response => {
                this.setState({
                    metadata: response.data.result.metaData,
                    average: response.data.result.average,
                    max: response.data.result.max
                });
                console.log(response.data);
            })
            .catch(e => {
                console.log(e);
            });
    }
    openModal(patientId: number) {
        PatientService.getSimilar(patientId)
            .then(response => {
                this.setState({
                    similarPatinet: response.data.result
                });
                console.log(response.data);
            })
            .catch(e => {
                console.log(e);
            });
        this.setState({ show: true });
    }
    renderPatients(Patients: IPatintReport[]) {

        return (
            <div>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Age</th>
                            <th>Bill Avrage</th>
                            <th>Standard Diviations</th>
                            <th>Outliers Standard Diviations</th>
                            <th>5th Record Id</th>
                            <th>Max VisitMonth</th>
                        </tr>
                    </thead>
                    <tbody>
                        {Patients.map(Patient =>
                            <tr key={Patient.id}>
                                <td>{Patient.name}</td>
                                <td>{Patient.age}</td>
                                <td>{Patient.avrage}</td>
                                <td>{Patient.standardDiviations}</td>
                                <td>{Patient.outliersStandardDiviations}</td>
                                <td>{Patient.recordId}</td>
                                <td>{Patient.maxVisitMonth}</td>
                                <td><Button variant="outline-info" onClick={() => this.openModal(Patient.id)}>Similar Patient</Button></td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
    renderModal() {
        return (<>
            <Modal show={this.state.show} onHide={() => this.setState({ show: false })} dialogClassName="modal-90w" aria-labelledby="example-custom-modal-styling-title" >
                <Modal.Header closeButton>
                    <Modal.Title id="example-custom-modal-styling-title">
                        Similar Patient
                    </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div>
                        <table className='table table-striped' aria-labelledby="tabelLabel">
                            <thead>
                                <tr>
                                    <th>Name</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.state.similarPatinet.map(Patient =>
                                    <tr>
                                        <td>{Patient.name}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                </Modal.Body>
            </Modal>
        </>);
    }
    RenderMetaData(metadata: IGeneral[]) {
        return (<>
            {
                this.state.metadata.length == 0 ? <div></div> :
                    (<div>
                        <h2>
                            Average number of meta-data used <Badge variant="secondary">{this.state.average}</Badge>
                        </h2>
                        <h2>
                            Max number of meta-data used <Badge variant="secondary">{this.state.max}</Badge>
                        </h2>

                        <div className="border-top my-3"></div>
                        {
                            metadata.map(item =>
                                <h3>
                                    {item.key} Used <Badge variant="secondary">{item.value}</Badge> Times.
                                </h3>
                            )}
                    </div>)
            }
        </>);
    }
    render() {
        let contents = this.state.data == null
            ? <p><em>data don't exist</em></p>
            : this.renderPatients(this.state.data);

        let similarPatient = this.state.similarPatinet == null
            ? <p><em>data don't exist</em></p>
            : this.renderModal();

        let metaData = this.state.metadata == null
            ? <p><em>Meta data don't exist</em></p>
            : this.RenderMetaData(this.state.metadata);

        return (
            <div>
                {contents}
                {similarPatient}
                <div className="border-top my-3"></div>
                {metaData}
            </div>
        );
    }
}