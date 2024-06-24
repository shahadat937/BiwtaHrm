import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpPresentAddressComponent } from './emp-present-address.component';

describe('EmpPresentAddressComponent', () => {
  let component: EmpPresentAddressComponent;
  let fixture: ComponentFixture<EmpPresentAddressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmpPresentAddressComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpPresentAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
