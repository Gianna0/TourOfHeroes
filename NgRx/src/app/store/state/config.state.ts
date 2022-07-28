import { IConfig } from "src/app/models/config.interface";

export interface IConfigState {
    config: IConfig | null;
}

export const initialConfigState: IConfigState = {
    config: null,
}