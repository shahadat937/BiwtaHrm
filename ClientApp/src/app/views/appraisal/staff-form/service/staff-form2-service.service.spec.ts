import { TestBed } from '@angular/core/testing';

import { StaffForm2ServiceService } from './staff-form2-service.service';

describe('StaffForm2ServiceService', () => {
  let service: StaffForm2ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(StaffForm2ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
