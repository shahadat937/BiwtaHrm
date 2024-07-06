import { TestBed } from '@angular/core/testing';

import { StaffForm3ServicesService } from './staff-form3-services.service';

describe('StaffForm3ServicesService', () => {
  let service: StaffForm3ServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StaffForm3ServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
