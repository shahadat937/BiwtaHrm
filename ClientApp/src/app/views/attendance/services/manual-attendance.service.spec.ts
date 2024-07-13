import { TestBed } from '@angular/core/testing';

import { ManualAttendanceService } from './manual-attendance.service';

describe('ManualAttendanceService', () => {
  let service: ManualAttendanceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ManualAttendanceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
