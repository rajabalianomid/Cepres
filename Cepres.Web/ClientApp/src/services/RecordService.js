"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Http_Common_1 = require("../Http.Common");
var RecordService = /** @class */ (function () {
    function RecordService() {
        this.controller = "record";
    }
    RecordService.prototype.getAll = function (pagingData) {
        return Http_Common_1.default.post("/" + this.controller + "/getall", pagingData);
    };
    RecordService.prototype.get = function (id) {
        return Http_Common_1.default.get("/" + this.controller + "/get/" + id);
    };
    RecordService.prototype.create = function (data) {
        debugger;
        return Http_Common_1.default.post("/" + this.controller + "/add", data);
    };
    RecordService.prototype.update = function (data) {
        return Http_Common_1.default.put("/" + this.controller + "/update/", data);
    };
    RecordService.prototype.delete = function (id) {
        return Http_Common_1.default.delete("/" + this.controller + "/remove/" + id);
    };
    return RecordService;
}());
exports.default = new RecordService();
//# sourceMappingURL=RecordService.js.map