import { User } from "oidc-client";

export interface ICallBackState {
    user?: User | null,
}