import { TestBed } from '@angular/core/testing';

import { EmpEducationInfoService } from './emp-education-info.service';

describe('EmpEducationInfoService', () => {
  let service: EmpEducationInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpEducationInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
