import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpOtherResponsibilityComponent } from './emp-other-responsibility.component';

describe('EmpOtherResponsibilityComponent', () => {
  let component: EmpOtherResponsibilityComponent;
  let fixture: ComponentFixture<EmpOtherResponsibilityComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpOtherResponsibilityComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmpOtherResponsibilityComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
