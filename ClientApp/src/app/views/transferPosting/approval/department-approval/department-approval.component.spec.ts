import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentApprovalComponent } from './department-approval.component';

describe('DepartmentApprovalComponent', () => {
  let component: DepartmentApprovalComponent;
  let fixture: ComponentFixture<DepartmentApprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentApprovalComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepartmentApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
