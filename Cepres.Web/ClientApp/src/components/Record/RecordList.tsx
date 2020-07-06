import React, { Component } from 'react';
import { Button, Pagination, Form, Navbar, Nav } from 'react-bootstrap';
import RecordService from '../../services/RecordService';
import { IListRecordState, IListRecord } from '../../model/IRecord';
import { useHistory, Link } from 'react-router-dom';
import { Helper } from '../../Helper';
import { confirmAlert } from 'react-confirm-alert';
import 'react-confirm-alert/src/react-confirm-alert.css';

export class RecordList extends Component<{}, IListRecordState> {
    constructor(props: any) {
        super(props);

        this.state = {
            paging:
            {
                count: 0,
                data: [],
                next: false,
                pageCount: 0,
                pageIndex: 0,
                pageSize: 5,
                previous: false,
                sort: "Id"
            },
            message: null,
            validated: false
        };
        this.getAllRecord = this.getAllRecord.bind(this);
        this.redirectToEdit = this.redirectToEdit.bind(this);
        this.onChangePage = this.onChangePage.bind(this);
        this.onChangeSort = this.onChangeSort.bind(this);
        this.removeRecord = this.removeRecord.bind(this);
    }
    componentDidMount() {
        this.getAllRecord();
    }
    getAllRecord() {
        debugger;
        RecordService.getAll(this.state.paging)
            .then(response => {
                this.setState({
                    paging: response.data.result
                });
                console.log(response.data);
            })
            .catch(e => {
                console.log(e);
            });
    }
    onChangePage(event: any) {
        let step = event.target.textContent.includes("›") ? 1 : -1;
        let index = this.state.paging.pageIndex;
        let max = this.state.paging.pageCount;
        debugger;
        if (index + step > -1 && index + step < max) {
            this.state.paging.pageIndex += step;
            this.getAllRecord();
        }
    }
    onChangeSort(event: any) {
        debugger;
        this.state.paging.sort = event.target.value;
        this.state.paging.pageIndex = 0;
        this.getAllRecord();
    }
    onChangePageSize(event: any) {
        debugger;
        this.state.paging.pageSize = Number(event.target.value);
        this.state.paging.pageIndex = 0;
        this.getAllRecord();
    }
    redirectToEdit(id: number) {
        const history = useHistory();
        history.push('/RecordCreateOrUpdate', { recordId: id });
    }
    removeRecord(id: number) {
        debugger;
        new Helper().confirmation('remove confirm', `are you sure to delete item with id: ${id}`, () => {
            RecordService.delete(id)
                .then(response => {
                    console.log(response.data);
                    this.getAllRecord();
                })
                .catch(e => {
                    console.log(e);
                });
        });
    }
    renderRecords(Records: IListRecord[]) {

        return (
            <div>
                <Navbar expand="lg">
                    <Navbar.Brand href="#home"><h3>List of Record</h3></Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="mr-auto">
                            <Link className="text-dark" to={{ pathname: "/record" }}><Button variant="outline-primary">Add Record</Button></Link>
                        </Nav>
                        <Form inline>
                            <Nav className="mr-auto">
                                <Form.Control as="select" className="my-1 mr-sm-2" custom onChange={(event: any) => this.onChangeSort(event)}>
                                    <option value="Id">Id Low To High</option>
                                    <option value="Id DESC">Id Hight To Low</option>
                                    <option value="Name">Name Low To Hight</option>
                                    <option value="Name DESC">Name Hight To Low</option>
                                </Form.Control>
                            </Nav>
                        </Form>
                    </Navbar.Collapse>
                </Navbar>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Patient Name</th>
                            <th>Disease Name</th>
                            <th>Time Of Entry</th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {Records.map(Record =>
                            <tr key={Record.id}>
                                <td>{Record.id}</td>
                                <td>{Record.name}</td>
                                <td>{Record.diseaseName}</td>
                                <td>{Helper.formatTime(Record.timeOfEntry)}</td>
                                <td><Link className="text-dark" to={{ pathname: "/record", search: "?recordId=" + Record.id }}><Button variant="outline-info">Edit</Button></Link></td>
                                <td><Button variant="outline-danger" onClick={(event: any) => this.removeRecord(Record.id)}>X</Button></td>
                            </tr>
                        )}
                        <tr>
                            <td colSpan={5}>
                                <b className="mr-sm-2">{this.state.paging.pageIndex + 1}</b>To<b className="ml-sm-2">{this.state.paging.pageCount}</b>
                            </td>
                            <td>
                                <Form.Control as="select" className="my-1 mr-sm-2" custom onChange={(event: any) => this.onChangePageSize(event)}>
                                    <option value="5">5</option>
                                    <option value="10">10</option>
                                    <option value="20">20</option>
                                    <option value="30">30</option>
                                </Form.Control>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <Pagination onClick={(event) => this.onChangePage(event)}>
                    {
                        this.state.paging.previous ?
                            (<Pagination.Prev />) :
                            (<Pagination.Prev disabled />)
                    }
                    {
                        this.state.paging.next ?
                            (<Pagination.Next />) :
                            (<Pagination.Next disabled />)
                    }
                </Pagination>
            </div>
        );
    }
    render() {
        let contents = this.state.paging.data == null
            ? <p><em>data don't exist</em></p>
            : this.renderRecords(this.state.paging.data);

        return (
            <div>
                {contents}
            </div>
        );
    }
}