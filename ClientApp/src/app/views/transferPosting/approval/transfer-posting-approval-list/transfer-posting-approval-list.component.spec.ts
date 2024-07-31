import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferPostingApprovalListComponent } from './transfer-posting-approval-list.component';

describe('TransferPostingApprovalListComponent', () => {
  let component: TransferPostingApprovalListComponent;
  let fixture: ComponentFixture<TransferPostingApprovalListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferPostingApprovalListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferPostingApprovalListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
