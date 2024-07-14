import { TestBed } from '@angular/core/testing';

import { AttendanceRecordServiceService } from './attendance-record-service.service';

describe('AttendanceRecordServiceService', () => {
  let service: AttendanceRecordServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AttendanceRecordServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
