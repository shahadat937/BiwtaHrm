import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpNomineeInfoComponent } from './emp-nominee-info.component';

describe('EmpNomineeInfoComponent', () => {
  let component: EmpNomineeInfoComponent;
  let fixture: ComponentFixture<EmpNomineeInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpNomineeInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpNomineeInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
