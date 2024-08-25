import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpAddressComponent } from './emp-address.component';

describe('EmpAddressComponent', () => {
  let component: EmpAddressComponent;
  let fixture: ComponentFixture<EmpAddressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmpAddressComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
