import { TestBed } from '@angular/core/testing';

import { AttendanceReportEmpService } from './attendance-report-emp.service';

describe('AttendanceReportEmpService', () => {
  let service: AttendanceReportEmpService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AttendanceReportEmpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
