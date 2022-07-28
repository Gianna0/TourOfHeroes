import { Injectable} from "@angular/core";
import { ofType, Actions, createEffect } from "@ngrx/effects";
import { Store, select } from "@ngrx/store";
import { map, of, switchMap, withLatestFrom } from "rxjs";
import { IAppState } from "../state/app.state";
import { GetUsersSuccess, EUserActions, GetUserSuccess, GetUser, GetUsers } from "../actions/user.actions";
import { selectUserList } from '../selectors/user.selector';
import { IUserHttp } from "src/app/models/http-models/user-http.interface";
import { UserService } from "src/app/services/user.service";


@Injectable()
export class UserEffects {
    
    getUser$ = createEffect(() => this._actions$.pipe(
        ofType<GetUser>(EUserActions.GetUser),
        map(action => action.payload),
        withLatestFrom(this._store.pipe(select(selectUserList))),
        switchMap(([id, users]) => {
            if (!users) {
                users = [];
            }
            const selectedUser = users.filter(user => user.id == +id)[0];
            return of(new GetUserSuccess(selectedUser));
        })
    ));

    getUsers$ = createEffect(() => this._actions$.pipe(
        ofType<GetUsers>(EUserActions.GetUsers),
        switchMap(() => this._userService.getUsers()),
        switchMap((userHttp: IUserHttp) => of(new GetUsersSuccess(userHttp.users)))
    ));

    constructor(private _userService: UserService, private _actions$: Actions, private _store: Store<IAppState>) {}
}