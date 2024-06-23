import { TestBed } from '@angular/core/testing';

import { EmpBankInfoService } from './emp-bank-info.service';

describe('EmpBankInfoService', () => {
  let service: EmpBankInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpBankInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
