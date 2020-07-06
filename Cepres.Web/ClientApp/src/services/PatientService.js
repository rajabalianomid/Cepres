"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Http_Common_1 = require("../Http.Common");
var PatientService = /** @class */ (function () {
    function PatientService() {
        this.controller = "patient";
    }
    PatientService.prototype.get = function (id) {
        return Http_Common_1.default.get("/" + this.controller + "/get/" + id);
    };
    PatientService.prototype.getByName = function (name) {
        return Http_Common_1.default.get("/" + this.controller + "/getByName/" + name);
    };
    PatientService.prototype.getAll = function (pagingData) {
        return Http_Common_1.default.post("/" + this.controller + "/getall", pagingData);
    };
    PatientService.prototype.report = function () {
        return Http_Common_1.default.get("/" + this.controller + "/report");
    };
    PatientService.prototype.getSimilar = function (id) {
        return Http_Common_1.default.get("/" + this.controller + "/getSimilar/" + id);
    };
    PatientService.prototype.create = function (data) {
        debugger;
        return Http_Common_1.default.post("/" + this.controller + "/add", data);
    };
    PatientService.prototype.update = function (data) {
        return Http_Common_1.default.put("/" + this.controller + "/update/", data);
    };
    PatientService.prototype.delete = function (id) {
        return Http_Common_1.default.delete("/" + this.controller + "/remove/" + id);
    };
    return PatientService;
}());
exports.default = new PatientService();
//# sourceMappingURL=PatientService.js.map