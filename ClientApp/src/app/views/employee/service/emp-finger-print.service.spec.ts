import { TestBed } from '@angular/core/testing';

import { EmpFingerPrintService } from './emp-finger-print.service';

describe('EmpFingerPrintService', () => {
  let service: EmpFingerPrintService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpFingerPrintService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
