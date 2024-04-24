import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncrementAndPromotionApprovalComponent } from './increment-and-promotion-approval.component';

describe('IncrementAndPromotionApprovalComponent', () => {
  let component: IncrementAndPromotionApprovalComponent;
  let fixture: ComponentFixture<IncrementAndPromotionApprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IncrementAndPromotionApprovalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IncrementAndPromotionApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
