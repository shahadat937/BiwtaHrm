import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffForm3Component } from './staff-form-3.component';

describe('StaffForm3Component', () => {
  let component: StaffForm3Component;
  let fixture: ComponentFixture<StaffForm3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [StaffForm3Component]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(StaffForm3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
