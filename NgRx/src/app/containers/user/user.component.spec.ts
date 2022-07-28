import { ComponentFixture, TestBed } from '@angular/core/testing';
import { appReducers } from '../../store/reducers/app.reducers';
import { StoreModule } from '@ngrx/store';
import { UserComponent } from './user.component';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { AppRoutingModule } from '../../app-routing.module';
import { RouterModule } from '@angular/router';

describe('UserComponent', () => {
  let component: UserComponent;
  let fixture: ComponentFixture<UserComponent>;

  beforeEach(async() => {
    TestBed.configureTestingModule({
      declarations: [ UserComponent ],
      imports: [
        StoreModule.forRoot(appReducers),
        AppRoutingModule,
        StoreRouterConnectingModule.forRoot({stateKey: 'router'}),
        RouterModule.forRoot([]),
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});