import { TestBed } from '@angular/core/testing';

import { OfficerFormService } from './officer-form.service';

describe('OfficerFormService', () => {
  let service: OfficerFormService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
