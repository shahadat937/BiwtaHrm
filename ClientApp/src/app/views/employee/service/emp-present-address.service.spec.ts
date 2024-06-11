import { TestBed } from '@angular/core/testing';

import { EmpPresentAddressService } from './emp-present-address.service';

describe('EmpPresentAddressService', () => {
  let service: EmpPresentAddressService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpPresentAddressService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
