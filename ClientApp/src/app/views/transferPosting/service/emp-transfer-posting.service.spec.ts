import { TestBed } from '@angular/core/testing';

import { EmpTransferPostingService } from './emp-transfer-posting.service';

describe('EmpTransferPostingService', () => {
  let service: EmpTransferPostingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpTransferPostingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
