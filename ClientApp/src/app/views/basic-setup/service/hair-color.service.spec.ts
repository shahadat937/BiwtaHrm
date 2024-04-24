import { TestBed } from '@angular/core/testing';

import { HairColorService } from './hair-color.service';

describe('HairColorService', () => {
  let service: HairColorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HairColorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
