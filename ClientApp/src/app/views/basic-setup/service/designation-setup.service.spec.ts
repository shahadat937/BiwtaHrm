import { TestBed } from '@angular/core/testing';

import { DesignationSetupService } from './designation-setup.service';

describe('DesignationSetupService', () => {
  let service: DesignationSetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DesignationSetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
