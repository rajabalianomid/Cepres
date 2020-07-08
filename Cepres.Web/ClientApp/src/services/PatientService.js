"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ApiService_1 = require("./ApiService");
var PatientService = /** @class */ (function () {
    function PatientService() {
        this.controller = "patient";
        this.apiService = new ApiService_1.ApiService();
    }
    PatientService.prototype.get = function (id) {
        return this.apiService.callApi("/" + this.controller + "/get/" + id, 'get');
    };
    PatientService.prototype.getByName = function (name) {
        return this.apiService.callApi("/" + this.controller + "/getByName/" + name, 'get');
    };
    PatientService.prototype.getAll = function (pagingData) {
        debugger;
        return this.apiService.callApi("/" + this.controller + "/getall", 'post', pagingData);
    };
    PatientService.prototype.report = function () {
        return this.apiService.callApi("/" + this.controller + "/report", 'get');
    };
    PatientService.prototype.getSimilar = function (id) {
        return this.apiService.callApi("/" + this.controller + "/getSimilar/" + id, 'get');
    };
    PatientService.prototype.create = function (data) {
        debugger;
        return this.apiService.callApi("/" + this.controller + "/add", 'post', data);
    };
    PatientService.prototype.update = function (data) {
        return this.apiService.callApi("/" + this.controller + "/update/", 'put', data);
    };
    PatientService.prototype.delete = function (id) {
        return this.apiService.callApi("/" + this.controller + "/remove/" + id, 'delete');
    };
    return PatientService;
}());
exports.default = new PatientService();
//# sourceMappingURL=PatientService.js.map