import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganogramDepartmentComponent } from './organogram-department.component';

describe('OrganogramDepartmentComponent', () => {
  let component: OrganogramDepartmentComponent;
  let fixture: ComponentFixture<OrganogramDepartmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OrganogramDepartmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OrganogramDepartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
