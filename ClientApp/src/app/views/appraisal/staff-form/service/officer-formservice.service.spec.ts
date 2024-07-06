import { TestBed } from '@angular/core/testing';

import { OfficerFormserviceService } from './officer-formservice.service';

describe('OfficerFormserviceService', () => {
  let service: OfficerFormserviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormserviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
