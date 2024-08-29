import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpShiftListComponent } from './emp-shift-list.component';

describe('EmpShiftListComponent', () => {
  let component: EmpShiftListComponent;
  let fixture: ComponentFixture<EmpShiftListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpShiftListComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpShiftListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
