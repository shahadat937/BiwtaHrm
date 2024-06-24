import { TestBed } from '@angular/core/testing';

import { EmpForeignTourInfoService } from './emp-foreign-tour-info.service';

describe('EmpForeignTourInfoService', () => {
  let service: EmpForeignTourInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpForeignTourInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
