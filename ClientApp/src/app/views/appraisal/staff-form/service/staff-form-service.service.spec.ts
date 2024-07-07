import { TestBed } from '@angular/core/testing';

import { StaffFormServiceService } from './staff-form-service.service';

describe('StaffFormServiceService', () => {
  let service: StaffFormServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StaffFormServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
