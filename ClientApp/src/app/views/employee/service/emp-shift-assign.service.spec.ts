import { TestBed } from '@angular/core/testing';

import { EmpShiftAssignService } from './emp-shift-assign.service';

describe('EmpShiftAssignService', () => {
  let service: EmpShiftAssignService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpShiftAssignService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
