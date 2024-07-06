import { TestBed } from '@angular/core/testing';

import { OfficerFormPart5ServiceService } from './officer-form-part5-service.service';

describe('OfficerFormPart5ServiceService', () => {
  let service: OfficerFormPart5ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormPart5ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
