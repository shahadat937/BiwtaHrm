import { TestBed } from '@angular/core/testing';

import { ResponsibilityTypeService } from './responsibility-type.service';

describe('ResponsibilityTypeService', () => {
  let service: ResponsibilityTypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ResponsibilityTypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
