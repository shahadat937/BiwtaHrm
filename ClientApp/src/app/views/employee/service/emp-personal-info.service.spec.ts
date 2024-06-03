import { TestBed } from '@angular/core/testing';

import { EmpPersonalInfoService } from './emp-personal-info.service';

describe('EmpPersonalInfoService', () => {
  let service: EmpPersonalInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpPersonalInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
