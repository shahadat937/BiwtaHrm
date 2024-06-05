import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransferApprovedComponent } from './transfer-approved.component';

describe('TransferApprovedComponent', () => {
  let component: TransferApprovedComponent;
  let fixture: ComponentFixture<TransferApprovedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [TransferApprovedComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TransferApprovedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
