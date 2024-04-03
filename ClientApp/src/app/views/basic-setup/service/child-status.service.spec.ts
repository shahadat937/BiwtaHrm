import { TestBed } from '@angular/core/testing';

import { ChildStatusService } from './child-status.service';

describe('ChildStatusService', () => {
  let service: ChildStatusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ChildStatusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
