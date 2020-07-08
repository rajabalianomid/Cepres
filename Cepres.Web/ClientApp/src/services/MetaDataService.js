"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ApiService_1 = require("./ApiService");
var MetaDataService = /** @class */ (function () {
    function MetaDataService() {
        this.controller = "patientmetadata";
        this.apiService = new ApiService_1.ApiService();
    }
    MetaDataService.prototype.getAllByPatientId = function (patientId) {
        return this.apiService.callApi("/" + this.controller + "/gettall/" + patientId, 'get');
    };
    MetaDataService.prototype.report = function () {
        return this.apiService.callApi("/" + this.controller + "/report", 'get');
    };
    MetaDataService.prototype.create = function (data) {
        return this.apiService.callApi("/" + this.controller + "/add", 'post', data);
    };
    MetaDataService.prototype.delete = function (id) {
        return this.apiService.callApi("/" + this.controller + "/remove/" + id, 'delete');
    };
    return MetaDataService;
}());
exports.default = new MetaDataService();
//# sourceMappingURL=MetaDataService.js.map