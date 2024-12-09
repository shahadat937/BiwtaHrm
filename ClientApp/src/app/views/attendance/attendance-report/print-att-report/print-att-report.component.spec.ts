import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrintAttReportComponent } from './print-att-report.component';

describe('PrintAttReportComponent', () => {
  let component: PrintAttReportComponent;
  let fixture: ComponentFixture<PrintAttReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PrintAttReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PrintAttReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
