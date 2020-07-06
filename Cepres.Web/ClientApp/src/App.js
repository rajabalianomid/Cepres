import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home/Home';
import { PatientList } from './components/Patient/PatientList';
import { RecordList } from './components/Record/RecordList';
import { PatientCreateOrUpdate } from './components/Patient/PatientCreateOrUpdate';
import { RecordCreateOrUpdate } from './components/Record/RecordCreateOrUpdate';
import { Report } from './components/Report/Report';

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/recordlist' component={RecordList} />
                <Route path='/patientlist' component={PatientList} />
                <Route path='/record' component={RecordCreateOrUpdate} />
                <Route path='/patient' component={PatientCreateOrUpdate} />
                <Route path='/report' component={Report} />
            </Layout>
        );
    }
}
