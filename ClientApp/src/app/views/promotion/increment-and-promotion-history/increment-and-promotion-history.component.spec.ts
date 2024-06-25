import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncrementAndPromotionHistoryComponent } from './increment-and-promotion-history.component';

describe('IncrementAndPromotionHistoryComponent', () => {
  let component: IncrementAndPromotionHistoryComponent;
  let fixture: ComponentFixture<IncrementAndPromotionHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IncrementAndPromotionHistoryComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IncrementAndPromotionHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
