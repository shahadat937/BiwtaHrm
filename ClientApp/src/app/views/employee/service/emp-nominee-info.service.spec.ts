import { TestBed } from '@angular/core/testing';

import { EmpNomineeInfoService } from './emp-nominee-info.service';

describe('EmpNomineeInfoService', () => {
  let service: EmpNomineeInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpNomineeInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
