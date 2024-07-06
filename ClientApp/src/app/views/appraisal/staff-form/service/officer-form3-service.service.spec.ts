import { TestBed } from '@angular/core/testing';

import { OfficerForm3ServiceService } from './officer-form3-service.service';

describe('OfficerForm3ServiceService', () => {
  let service: OfficerForm3ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerForm3ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
