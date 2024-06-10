import { TestBed } from '@angular/core/testing';

import { EmpJobDetailsService } from './emp-job-details.service';

describe('EmpJobDetailsService', () => {
  let service: EmpJobDetailsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpJobDetailsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
