import { IBaseModel } from "./IBaseModel";
import { _PatientMetaData } from "../components/Patient/_PatientMetaData";

export interface IPatientCreateOrUpdate extends IBaseModel {
    id: number,
    submitted: boolean,
    metaDataComponent: React.RefObject<_PatientMetaData>;
}