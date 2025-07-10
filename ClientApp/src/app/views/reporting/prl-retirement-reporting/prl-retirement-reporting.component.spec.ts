import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrlRetirementReportingComponent } from './prl-retirement-reporting.component';

describe('PrlRetirementReportingComponent', () => {
  let component: PrlRetirementReportingComponent;
  let fixture: ComponentFixture<PrlRetirementReportingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PrlRetirementReportingComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrlRetirementReportingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
