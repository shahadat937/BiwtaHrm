import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionWidgetsComponent } from './promotion-widgets.component';

describe('PromotionWidgetsComponent', () => {
  let component: PromotionWidgetsComponent;
  let fixture: ComponentFixture<PromotionWidgetsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PromotionWidgetsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PromotionWidgetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
