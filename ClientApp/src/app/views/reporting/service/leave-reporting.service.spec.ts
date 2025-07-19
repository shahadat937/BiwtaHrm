import { TestBed } from '@angular/core/testing';

import { LeaveReportingService } from './leave-reporting.service';

describe('LeaveReportingService', () => {
  let service: LeaveReportingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeaveReportingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
