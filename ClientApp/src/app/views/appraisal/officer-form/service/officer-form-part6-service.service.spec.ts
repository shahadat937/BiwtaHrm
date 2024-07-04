import { TestBed } from '@angular/core/testing';

import { OfficerFormPart6ServiceService } from './officer-form-part6-service.service';

describe('OfficerFormPart6ServiceService', () => {
  let service: OfficerFormPart6ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormPart6ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
