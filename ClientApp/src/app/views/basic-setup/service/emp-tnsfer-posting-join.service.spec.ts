import { TestBed } from '@angular/core/testing';

import { EmpTnsferPostingJoinService } from './emp-tnsfer-posting-join.service';

describe('EmpTnsferPostingJoinService', () => {
  let service: EmpTnsferPostingJoinService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpTnsferPostingJoinService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
