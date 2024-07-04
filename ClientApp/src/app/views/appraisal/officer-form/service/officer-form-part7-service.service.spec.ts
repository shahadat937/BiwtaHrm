import { TestBed } from '@angular/core/testing';

import { OfficerFormPart7ServiceService } from './officer-form-part7-service.service';

describe('OfficerFormPart7ServiceService', () => {
  let service: OfficerFormPart7ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormPart7ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
