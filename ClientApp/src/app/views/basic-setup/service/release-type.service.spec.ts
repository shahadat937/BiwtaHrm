import { TestBed } from '@angular/core/testing';

import { ReleaseTypeService } from './release-type.service';

describe('ReleaseTypeService', () => {
  let service: ReleaseTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReleaseTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
