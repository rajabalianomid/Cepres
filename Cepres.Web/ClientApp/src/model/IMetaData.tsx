import { IBaseModel } from "./IBaseModel";

export interface IMetaDataState extends IBaseModel {
    metaData: IMetaData,
    arrMetaData?: IMetaData[] | null,
    validated: boolean,
    submitted: boolean
}
export interface IMetaData {
    id: number
    key: string,
    value: string,
    patientId: number
}