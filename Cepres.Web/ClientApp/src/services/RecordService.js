"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ApiService_1 = require("./ApiService");
var RecordService = /** @class */ (function () {
    function RecordService() {
        this.controller = "record";
        this.apiService = new ApiService_1.ApiService();
    }
    RecordService.prototype.getAll = function (pagingData) {
        return this.apiService.callApi("/" + this.controller + "/getall", 'post', pagingData);
    };
    RecordService.prototype.get = function (id) {
        return this.apiService.callApi("/" + this.controller + "/get/" + id, 'get');
    };
    RecordService.prototype.create = function (data) {
        return this.apiService.callApi("/" + this.controller + "/add", 'post', data);
    };
    RecordService.prototype.update = function (data) {
        return this.apiService.callApi("/" + this.controller + "/update/", 'put', data);
    };
    RecordService.prototype.delete = function (id) {
        return this.apiService.callApi("/" + this.controller + "/remove/" + id, 'delete');
    };
    return RecordService;
}());
exports.default = new RecordService();
//# sourceMappingURL=RecordService.js.map