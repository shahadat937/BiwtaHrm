import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DepartmentApprovalListComponent } from './department-approval-list.component';

describe('DepartmentApprovalListComponent', () => {
  let component: DepartmentApprovalListComponent;
  let fixture: ComponentFixture<DepartmentApprovalListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DepartmentApprovalListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DepartmentApprovalListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
