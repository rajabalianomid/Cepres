import http from "../Http.Common";
import { IPagination } from "../model/IPagination";
import { IListRecord } from "../model/IRecord";

class RecordService {
    controller: string = "record";

    getAll(pagingData: IPagination<IListRecord[]>) {
        return http.post(`/${this.controller}/getall`, pagingData);
    }
    get(id: any) {
        return http.get(`/${this.controller}/get/${id}`);
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

export default new RecordService();