import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReviewLeaveComponent } from './review-leave.component';

describe('ReviewLeaveComponent', () => {
  let component: ReviewLeaveComponent;
  let fixture: ComponentFixture<ReviewLeaveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ReviewLeaveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ReviewLeaveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
