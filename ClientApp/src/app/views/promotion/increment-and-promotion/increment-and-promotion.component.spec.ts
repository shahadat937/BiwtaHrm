import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IncrementAndPromotionComponent } from './increment-and-promotion.component';

describe('IncrementAndPromotionComponent', () => {
  let component: IncrementAndPromotionComponent;
  let fixture: ComponentFixture<IncrementAndPromotionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IncrementAndPromotionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(IncrementAndPromotionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
