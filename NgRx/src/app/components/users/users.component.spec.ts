import { ComponentFixture, TestBed } from '@angular/core/testing';
import { StoreModule } from '@ngrx/store';
import { appReducers } from '../../store/reducers/app.reducers';
import { UsersComponent } from './users.component';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { AppRoutingModule } from '../../app-routing.module';
import { RouterModule } from '@angular/router';

describe('UsersComponent', () => {
  let component: UsersComponent;
  let fixture: ComponentFixture<UsersComponent>;

  beforeEach(async() => {
    TestBed.configureTestingModule({
      declarations: [ UsersComponent ],
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
    fixture = TestBed.createComponent(UsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});