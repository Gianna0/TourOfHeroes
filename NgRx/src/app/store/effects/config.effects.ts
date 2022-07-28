import { Injectable } from "@angular/core";
import { Effect, ofType, Actions, createEffect } from "@ngrx/effects";
import { config, switchMap } from "rxjs";
import { of } from "rxjs";
import { ConfigService } from './../../services/config.service';
import { IConfig } from "src/app/models/config.interface";
import { EConfigActions, GetConfig, GetConfigSuccess } from "../actions/config.actions";

@Injectable()
export class ConfigEffects {
    
    getConfig$ = createEffect(() => this._actions$.pipe(
        ofType<GetConfig>(EConfigActions.GetConfig),
        switchMap(() => this._configService.getConfig()),
        switchMap((config: IConfig) => {
          return of(new GetConfigSuccess(config));
        })
    ));
    constructor(
        private _configService: ConfigService,
        private _actions$: Actions
    ) {}
}