import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferPostingReportComponent } from './transfer-posting-report.component';

describe('TransferPostingReportComponent', () => {
  let component: TransferPostingReportComponent;
  let fixture: ComponentFixture<TransferPostingReportComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferPostingReportComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransferPostingReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
