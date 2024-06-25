import { TestBed } from '@angular/core/testing';

import { UnionService } from './union.service';

describe('UnionService', () => {
  let service: UnionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UnionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
