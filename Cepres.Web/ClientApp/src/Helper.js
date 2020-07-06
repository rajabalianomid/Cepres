"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var react_confirm_alert_1 = require("react-confirm-alert");
var Helper = /** @class */ (function () {
    function Helper() {
    }
    Helper.formatTime = function (time) {
        debugger;
        if (time != null) {
            time = new Date(time);
            return typeof time == "object" ? time.toLocaleDateString() : "";
        }
        return null;
    };
    Helper.nullableTimeToTime = function (time) {
        debugger;
        if (time != null) {
            return new Date(time);
        }
        return null;
    };
    Helper.prototype.confirmation = function (title, message, callBackFunction) {
        react_confirm_alert_1.confirmAlert({
            title: title,
            message: message,
            buttons: [
                {
                    label: 'Yes',
                    onClick: function () { return callBackFunction(); }
                },
                {
                    label: 'No',
                    onClick: function () { }
                }
            ]
        });
    };
    return Helper;
}());
exports.Helper = Helper;
//# sourceMappingURL=Helper.js.map