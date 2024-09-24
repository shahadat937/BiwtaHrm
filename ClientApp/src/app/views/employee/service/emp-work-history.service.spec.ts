import { TestBed } from '@angular/core/testing';

import { EmpWorkHistoryService } from './emp-work-history.service';

describe('EmpWorkHistoryService', () => {
  let service: EmpWorkHistoryService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpWorkHistoryService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
