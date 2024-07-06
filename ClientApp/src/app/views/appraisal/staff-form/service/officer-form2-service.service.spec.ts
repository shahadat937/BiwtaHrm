import { TestBed } from '@angular/core/testing';

import { OfficerForm2ServiceService } from './officer-form2-service.service';

describe('OfficerForm2ServiceService', () => {
  let service: OfficerForm2ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerForm2ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
