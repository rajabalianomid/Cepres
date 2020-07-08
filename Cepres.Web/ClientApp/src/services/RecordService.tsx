import http from "../Http.Common";
import { IPagination } from "../model/IPagination";
import { IListRecord } from "../model/IRecord";
import { ApiService } from "./ApiService";

class RecordService {
    controller: string = "record";
    apiService: ApiService = new ApiService();

    getAll(pagingData: IPagination<IListRecord[]>) {
        return this.apiService.callApi(`/${this.controller}/getall`, 'post', pagingData);
    }
    get(id: any) {
        return this.apiService.callApi(`/${this.controller}/get/${id}`, 'get');
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

export default new RecordService();