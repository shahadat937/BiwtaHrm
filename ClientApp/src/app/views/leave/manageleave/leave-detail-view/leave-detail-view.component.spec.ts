import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveDetailViewComponent } from './leave-detail-view.component';

describe('LeaveDetailViewComponent', () => {
  let component: LeaveDetailViewComponent;
  let fixture: ComponentFixture<LeaveDetailViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LeaveDetailViewComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LeaveDetailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
