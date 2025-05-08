import { TestBed } from '@angular/core/testing';

import { RetiredReasonService } from './retired-reason.service';

describe('RetiredReasonService', () => {
  let service: RetiredReasonService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RetiredReasonService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
