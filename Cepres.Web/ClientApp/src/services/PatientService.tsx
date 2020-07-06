import http from "../Http.Common";
import { IPagination } from "../model/IPagination";
import { IListPatient } from "../model/IPatient";

class PatientService {
    controller: string = "patient";

    get(id: any) {
        return http.get(`/${this.controller}/get/${id}`);
    }
    getByName(name: any) {
        return http.get(`/${this.controller}/getByName/${name}`);
    }
    getAll(pagingData: IPagination<IListPatient[]>) {
        return http.post(`/${this.controller}/getall`, pagingData);
    }
    report() {
        return http.get(`/${this.controller}/report`);
    }
    getSimilar(id: number) {
        return http.get(`/${this.controller}/getSimilar/${id}`);
    }
    create(data: any) {
        debugger;
        return http.post(`/${this.controller}/add`, data);
    }
    update(data: any) {
        return http.put(`/${this.controller}/update/`, data);
    }
    delete(id: any) {
        return http.delete(`/${this.controller}/remove/${id}`);
    }
}

export default new PatientService();