import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeListReportingComponent } from './employee-list-reporting.component';

describe('EmployeeListReportingComponent', () => {
  let component: EmployeeListReportingComponent;
  let fixture: ComponentFixture<EmployeeListReportingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployeeListReportingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeListReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
