import { TestBed } from '@angular/core/testing';

import { OfficeAddressService } from './office-address.service';

describe('OfficeAddressService', () => {
  let service: OfficeAddressService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficeAddressService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
