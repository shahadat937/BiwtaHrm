import { TestBed } from '@angular/core/testing';

import { AttendanceDeviceService } from './attendance-device.service';

describe('AttendanceDeviceService', () => {
  let service: AttendanceDeviceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AttendanceDeviceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
