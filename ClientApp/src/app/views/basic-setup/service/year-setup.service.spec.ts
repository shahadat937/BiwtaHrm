import { TestBed } from '@angular/core/testing';

import { YearSetupService } from './year-setup.service';

describe('YearSetupService', () => {
  let service: YearSetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(YearSetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
