import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveReportingComponent } from './leave-reporting.component';

describe('LeaveReportingComponent', () => {
  let component: LeaveReportingComponent;
  let fixture: ComponentFixture<LeaveReportingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LeaveReportingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LeaveReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
