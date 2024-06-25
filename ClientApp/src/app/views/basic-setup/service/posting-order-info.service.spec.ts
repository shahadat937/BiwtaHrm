import { TestBed } from '@angular/core/testing';

import { PostingOrderInfoService } from './posting-order-info.service';

describe('PostingOrderInfoService', () => {
  let service: PostingOrderInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PostingOrderInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
