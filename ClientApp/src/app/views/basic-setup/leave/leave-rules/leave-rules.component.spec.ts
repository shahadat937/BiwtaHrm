import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LeaveRulesComponent } from './leave-rules.component';

describe('LeaveRulesComponent', () => {
  let component: LeaveRulesComponent;
  let fixture: ComponentFixture<LeaveRulesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [LeaveRulesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(LeaveRulesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
