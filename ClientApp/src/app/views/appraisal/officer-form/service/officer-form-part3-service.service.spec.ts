import { TestBed } from '@angular/core/testing';

import { OfficerFormPart3ServiceService } from './officer-form-part3-service.service';

describe('OfficerFormPart3ServiceService', () => {
  let service: OfficerFormPart3ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormPart3ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
