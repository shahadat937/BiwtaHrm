import { TestBed } from '@angular/core/testing';

import { EmpSpouseInfoService } from './emp-spouse-info.service';

describe('EmpSpouseInfoService', () => {
  let service: EmpSpouseInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpSpouseInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
