import http from "../Http.Common";

class MetaDataService {
    controller: string = "patientmetadata";

    getAllByPatientId(patientId: any) {
        return http.get(`/${this.controller}/gettall/${patientId}`);
    }
    report() {
        return http.get(`/${this.controller}/report`);
    }
    create(data: any) {
        return http.post(`/${this.controller}/add`, data);
    }
    delete(id: any) {
        return http.delete(`/${this.controller}/remove/${id}`);
    }
}

export default new MetaDataService();