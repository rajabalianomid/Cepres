import http from "../Http.Common";
import { IPagination } from "../model/IPagination";
import { IListPatient } from "../model/IPatient";
import { ApiService } from "./ApiService";

class PatientService {
    controller: string = "patient";
    apiService: ApiService = new ApiService();

    get(id: any) {
        return this.apiService.callApi(`/${this.controller}/get/${id}`, 'get');
    }
    getByName(name: any) {
        return this.apiService.callApi(`/${this.controller}/getByName/${name}`, 'get');
    }
    getAll(pagingData: IPagination<IListPatient[]>) {
        return this.apiService.callApi(`/${this.controller}/getall`, 'post', pagingData);
    }
    report() {
        return this.apiService.callApi(`/${this.controller}/report`, 'get');
    }
    getSimilar(id: number) {
        return this.apiService.callApi(`/${this.controller}/getSimilar/${id}`, 'get');
    }
    create(data: any) {
        return this.apiService.callApi(`/${this.controller}/add`, 'post', data);
    }
    update(data: any) {
        return this.apiService.callApi(`/${this.controller}/update/`, 'put', data);
    }
    delete(id: any) {
        return this.apiService.callApi(`/${this.controller}/remove/${id}`, 'delete');
    }
}

export default new PatientService();