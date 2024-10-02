import { TestBed } from '@angular/core/testing';

import { CourseDurationService } from './course-duration.service';

describe('CourseDurationService', () => {
  let service: CourseDurationService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CourseDurationService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
