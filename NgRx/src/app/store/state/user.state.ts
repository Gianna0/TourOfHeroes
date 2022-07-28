import { IUser } from "src/app/models/user.interface";

export interface IUserState {
    users: IUser[] | null;
    selectedUser: IUser | null;
}

export const initialUserState: IUserState = {
    users: null,
    selectedUser: null,
}