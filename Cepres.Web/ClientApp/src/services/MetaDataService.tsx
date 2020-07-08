import http from "../Http.Common";
import { ApiService } from "./ApiService";

class MetaDataService {
    controller: string = "patientmetadata";
    apiService: ApiService = new ApiService();

    getAllByPatientId(patientId: any) {
        return this.apiService.callApi(`/${this.controller}/gettall/${patientId}`, 'get');
    }
    report() {
        return this.apiService.callApi(`/${this.controller}/report`, 'get');
    }
    create(data: any) {
        return this.apiService.callApi(`/${this.controller}/add`, 'post', data);
    }
    delete(id: any) {
        return this.apiService.callApi(`/${this.controller}/remove/${id}`, 'delete');
    }
}

export default new MetaDataService();