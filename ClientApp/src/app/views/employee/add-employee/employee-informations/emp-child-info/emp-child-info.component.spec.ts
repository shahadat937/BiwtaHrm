import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpChildInfoComponent } from './emp-child-info.component';

describe('EmpChildInfoComponent', () => {
  let component: EmpChildInfoComponent;
  let fixture: ComponentFixture<EmpChildInfoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpChildInfoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpChildInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
