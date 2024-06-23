import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpEducationInfoComponent } from './emp-education-info.component';

describe('EmpEducationInfoComponent', () => {
  let component: EmpEducationInfoComponent;
  let fixture: ComponentFixture<EmpEducationInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpEducationInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpEducationInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
