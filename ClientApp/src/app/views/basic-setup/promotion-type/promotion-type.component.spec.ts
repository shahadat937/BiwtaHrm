import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionTypeComponent } from './promotion-type.component';

describe('PromotionTypeComponent', () => {
  let component: PromotionTypeComponent;
  let fixture: ComponentFixture<PromotionTypeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PromotionTypeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PromotionTypeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
