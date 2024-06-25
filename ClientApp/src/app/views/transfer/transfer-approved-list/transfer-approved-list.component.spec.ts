import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferApprovedListComponent } from './transfer-approved-list.component';

describe('TransferApprovedListComponent', () => {
  let component: TransferApprovedListComponent;
  let fixture: ComponentFixture<TransferApprovedListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferApprovedListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferApprovedListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
