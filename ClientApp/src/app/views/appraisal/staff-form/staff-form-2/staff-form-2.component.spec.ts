import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffForm2Component } from './staff-form-2.component';

describe('StaffForm2Component', () => {
  let component: StaffForm2Component;
  let fixture: ComponentFixture<StaffForm2Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffForm2Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StaffForm2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
