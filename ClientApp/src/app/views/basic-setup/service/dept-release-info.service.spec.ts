import { TestBed } from '@angular/core/testing';

import { DeptReleaseInfoService } from './dept-release-info.service';

describe('DeptReleaseInfoService', () => {
  let service: DeptReleaseInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DeptReleaseInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
