import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionIncrementInfoComponent } from './promotion-increment-info.component';

describe('PromotionIncrementInfoComponent', () => {
  let component: PromotionIncrementInfoComponent;
  let fixture: ComponentFixture<PromotionIncrementInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PromotionIncrementInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PromotionIncrementInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
