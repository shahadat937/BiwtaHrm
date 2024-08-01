import { TestBed } from '@angular/core/testing';

import { HolidaySetupService } from './holiday-setup.service';

describe('HolidaySetupService', () => {
  let service: HolidaySetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HolidaySetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
