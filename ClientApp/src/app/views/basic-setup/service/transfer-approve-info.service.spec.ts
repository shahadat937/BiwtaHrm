import { TestBed } from '@angular/core/testing';

import { TransferApproveInfoService } from './transfer-approve-info.service';

describe('TransferApproveInfoService', () => {
  let service: TransferApproveInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransferApproveInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
