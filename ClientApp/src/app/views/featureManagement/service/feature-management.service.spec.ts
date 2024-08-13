import { TestBed } from '@angular/core/testing';

import { FeatureManagementService } from './feature-management.service';

describe('FeatureManagementService', () => {
  let service: FeatureManagementService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FeatureManagementService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
