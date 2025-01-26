import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeManagementReportingComponent } from './employee-management-reporting.component';

describe('EmployeeManagementReportingComponent', () => {
  let component: EmployeeManagementReportingComponent;
  let fixture: ComponentFixture<EmployeeManagementReportingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeManagementReportingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeManagementReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
