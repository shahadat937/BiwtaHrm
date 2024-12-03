import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignEmployeeToDeviceComponent } from './assign-employee-to-device.component';

describe('AssignEmployeeToDeviceComponent', () => {
  let component: AssignEmployeeToDeviceComponent;
  let fixture: ComponentFixture<AssignEmployeeToDeviceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssignEmployeeToDeviceComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignEmployeeToDeviceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
