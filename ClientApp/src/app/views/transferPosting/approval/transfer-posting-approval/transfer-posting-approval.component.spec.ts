import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferPostingApprovalComponent } from './transfer-posting-approval.component';

describe('TransferPostingApprovalComponent', () => {
  let component: TransferPostingApprovalComponent;
  let fixture: ComponentFixture<TransferPostingApprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferPostingApprovalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferPostingApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
