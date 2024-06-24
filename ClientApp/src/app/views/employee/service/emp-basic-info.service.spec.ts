import { TestBed } from '@angular/core/testing';

import { EmpBasicInfoService } from './emp-basic-info.service';

describe('EmpBasicInfoService', () => {
  let service: EmpBasicInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpBasicInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
