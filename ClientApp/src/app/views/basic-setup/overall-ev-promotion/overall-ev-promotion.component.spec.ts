import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OverallEVPromotionComponent } from './overall-ev-promotion.component';

describe('OverallEVPromotionComponent', () => {
  let component: OverallEVPromotionComponent;
  let fixture: ComponentFixture<OverallEVPromotionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OverallEVPromotionComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OverallEVPromotionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
