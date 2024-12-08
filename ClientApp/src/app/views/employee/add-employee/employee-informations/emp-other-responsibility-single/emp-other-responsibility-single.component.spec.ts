import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpOtherResponsibilitySingleComponent } from './emp-other-responsibility-single.component';

describe('EmpOtherResponsibilitySingleComponent', () => {
  let component: EmpOtherResponsibilitySingleComponent;
  let fixture: ComponentFixture<EmpOtherResponsibilitySingleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpOtherResponsibilitySingleComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmpOtherResponsibilitySingleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
