import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmpPermanentAddressComponent } from './emp-permanent-address.component';

describe('EmpPermanentAddressComponent', () => {
  let component: EmpPermanentAddressComponent;
  let fixture: ComponentFixture<EmpPermanentAddressComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmpPermanentAddressComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EmpPermanentAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
