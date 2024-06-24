import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployePostingJoinApproveComponent } from './employe-posting-join-approve.component';

describe('EmployePostingJoinApproveComponent', () => {
  let component: EmployePostingJoinApproveComponent;
  let fixture: ComponentFixture<EmployePostingJoinApproveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmployePostingJoinApproveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmployePostingJoinApproveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
