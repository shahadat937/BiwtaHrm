import { TestBed } from '@angular/core/testing';

import { EmpPermanentAddressService } from './emp-permanent-address.service';

describe('EmpPermanentAddressService', () => {
  let service: EmpPermanentAddressService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpPermanentAddressService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
