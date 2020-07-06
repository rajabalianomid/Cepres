import { IBaseModel } from "./IBaseModel";
import { IPagination } from "./IPagination";

export interface IListRecordState extends IBaseModel {
    paging: IPagination<IListRecord[]>
    validated: boolean,
}
export interface IRecordState extends IBaseModel {
    currentRecord: IRecord,
    autocompleteData: IGeneral[]
    id: number,
    validated: boolean,
    submitted: boolean,
    isUpdate: boolean
}
export interface IRecord {
    diseaseName: string,
    name: string,
    patientId: string,
    timeOfEntry: string,
    description: string,
    bill: number
}
export interface IGeneral {
    value: string,
    label: string,
    key: string
}
export interface IListRecord {
    id: number,
    name: string,
    diseaseName: string,
    timeOfEntry: Date | null
}