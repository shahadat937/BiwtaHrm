import { TestBed } from '@angular/core/testing';

import { OfficeOrderService } from './office-order.service';

describe('OfficeOrderService', () => {
  let service: OfficeOrderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficeOrderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
