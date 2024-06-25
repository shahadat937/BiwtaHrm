import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpSpouseInfoComponent } from './emp-spouse-info.component';

describe('EmpSpouseInfoComponent', () => {
  let component: EmpSpouseInfoComponent;
  let fixture: ComponentFixture<EmpSpouseInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpSpouseInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpSpouseInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
