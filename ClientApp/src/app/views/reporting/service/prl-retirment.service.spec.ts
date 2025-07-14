import { TestBed } from '@angular/core/testing';

import { PrlRetirmentService } from './prl-retirment.service';

describe('PrlRetirmentService', () => {
  let service: PrlRetirmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PrlRetirmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
