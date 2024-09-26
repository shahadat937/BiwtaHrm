import { TestBed } from '@angular/core/testing';

import { EmpOtherResponsibilityService } from './emp-other-responsibility.service';

describe('EmpOtherResponsibilityService', () => {
  let service: EmpOtherResponsibilityService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpOtherResponsibilityService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
