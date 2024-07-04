import { TestBed } from '@angular/core/testing';

import { OfficerFormPart2ServiceService } from './officer-form-part2-service.service';

describe('OfficerFormPart2ServiceService', () => {
  let service: OfficerFormPart2ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormPart2ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
