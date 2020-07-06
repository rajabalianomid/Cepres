import { IBaseModel } from "./IBaseModel";

export interface IRecordCreateOrUpdate extends IBaseModel {
    id: number,
    submitted: boolean,
}