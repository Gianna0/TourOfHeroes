import { Component } from '@angular/core';

import { Store, select } from '@ngrx/store';
import { GetConfig } from './store/actions/config.actions';
import { selectConfig } from './store/selectors/config.selector';
import { IAppState } from './store/state/app.state';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'NgRx';
  config$ = this._store.pipe(select(selectConfig));

  constructor(private _store: Store<IAppState>) {}

  ngOnInit() {
    this._store.dispatch(new GetConfig());
  }
}
