import { TestBed } from '@angular/core/testing';

import { EmpLanguageInfoService } from './emp-language-info.service';

describe('EmpLanguageInfoService', () => {
  let service: EmpLanguageInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpLanguageInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
