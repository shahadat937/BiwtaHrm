import { TestBed } from '@angular/core/testing';

import { JobDetailsSetupService } from './job-details-setup.service';

describe('JobDetailsSetupService', () => {
  let service: JobDetailsSetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JobDetailsSetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
