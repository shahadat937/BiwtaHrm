import { TestBed } from '@angular/core/testing';

import { EmpChildInfoService } from './emp-child-info.service';

describe('EmpChildInfoService', () => {
  let service: EmpChildInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpChildInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
