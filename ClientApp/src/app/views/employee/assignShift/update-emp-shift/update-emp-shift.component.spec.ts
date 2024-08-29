import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateEmpShiftComponent } from './update-emp-shift.component';

describe('UpdateEmpShiftComponent', () => {
  let component: UpdateEmpShiftComponent;
  let fixture: ComponentFixture<UpdateEmpShiftComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateEmpShiftComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(UpdateEmpShiftComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
