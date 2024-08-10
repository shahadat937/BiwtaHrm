import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionApprovalListComponent } from './promotion-approval-list.component';

describe('PromotionApprovalListComponent', () => {
  let component: PromotionApprovalListComponent;
  let fixture: ComponentFixture<PromotionApprovalListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PromotionApprovalListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PromotionApprovalListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
