import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintEmpAttReportComponent } from './print-emp-att-report.component';

describe('PrintEmpAttReportComponent', () => {
  let component: PrintEmpAttReportComponent;
  let fixture: ComponentFixture<PrintEmpAttReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PrintEmpAttReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrintEmpAttReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
