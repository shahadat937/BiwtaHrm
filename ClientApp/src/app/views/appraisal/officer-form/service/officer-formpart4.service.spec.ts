import { TestBed } from '@angular/core/testing';

import { OfficerFormpart4Service } from './officer-formpart4.service';

describe('OfficerFormpart4Service', () => {
  let service: OfficerFormpart4Service;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(OfficerFormpart4Service);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
