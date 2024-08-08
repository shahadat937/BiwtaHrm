import { TestBed } from '@angular/core/testing';

import { EmpPromotionIncrementService } from './emp-promotion-increment.service';

describe('EmpPromotionIncrementService', () => {
  let service: EmpPromotionIncrementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpPromotionIncrementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
