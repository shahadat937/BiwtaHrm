import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewEmployeePrimeNgComponent } from './view-employee-prime-ng.component';

describe('ViewEmployeePrimeNgComponent', () => {
  let component: ViewEmployeePrimeNgComponent;
  let fixture: ComponentFixture<ViewEmployeePrimeNgComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ViewEmployeePrimeNgComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ViewEmployeePrimeNgComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
