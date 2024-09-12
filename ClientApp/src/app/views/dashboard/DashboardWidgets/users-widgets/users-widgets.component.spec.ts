import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersWidgetsComponent } from './users-widgets.component';

describe('UsersWidgetsComponent', () => {
  let component: UsersWidgetsComponent;
  let fixture: ComponentFixture<UsersWidgetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UsersWidgetsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UsersWidgetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
