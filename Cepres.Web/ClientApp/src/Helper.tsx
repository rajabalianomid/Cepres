import { confirmAlert } from 'react-confirm-alert';
export class Helper {
    public static formatTime(time?: any) {
        debugger;
        if (time != null) {
            time = new Date(time);
            return typeof time == "object" ? time.toLocaleDateString() : "";
        }
        return null;
    }
    public static nullableTimeToTime(time?: any) {
        debugger;
        if (time != null) {
            return new Date(time);
        }
        return null;
    }
    public confirmation(title: string, message: string, callBackFunction: () => void) {
        confirmAlert({
            title: title,
            message: message,
            buttons: [
                {
                    label: 'Yes',
                    onClick: () => callBackFunction()
                },
                {
                    label: 'No',
                    onClick: () => { }
                }
            ]
        });
    }
}