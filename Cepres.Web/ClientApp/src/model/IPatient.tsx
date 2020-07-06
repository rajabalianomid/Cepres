import { IBaseModel } from "./IBaseModel";
import { IPagination } from "./IPagination";
import { IGeneral } from "./IRecord";

export interface IPatintReportState extends IBaseModel {
    data: IPatintReport[],
    similarPatinet: IPatient[],
    metadata: IGeneral[],
    average: number,
    max: number,
    show: boolean
}
export interface IPatintReport extends IBaseModel {
    id: any,
    recordId: any,
    patientId: any,
    avrage: any,
    standardDiviations: any,
    bill: any,
    name: any,
    diseaseName: any,
    age: any,
    rowNum: any,
    maxVisitMonth: any,
    outliersStandardDiviations: any
}
export interface IListPatintState extends IBaseModel {
    paging: IPagination<IListPatient[]>
    validated: boolean,
}
export interface IPatientState extends IBaseModel {
    currentPatient: IPatient,
    id: number,
    validated: boolean,
    submitted: boolean,
    isUpdate: boolean
}
export interface IPatient {
    name: string,
    officialId?: any | null,
    dateOfBirth: string,
    email: string
}
export interface IListPatient {
    id: number,
    name?: string,
    dateOfBirth?: Date | null,
    lastTimeOfEntry?: Date | null,
    metaDataCount: number
}