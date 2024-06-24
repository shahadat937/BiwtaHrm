import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferPostingHistoryComponent } from './transfer-posting-history.component';

describe('TransferPostingHistoryComponent', () => {
  let component: TransferPostingHistoryComponent;
  let fixture: ComponentFixture<TransferPostingHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferPostingHistoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferPostingHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
