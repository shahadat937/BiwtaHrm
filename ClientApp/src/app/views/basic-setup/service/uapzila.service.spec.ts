import { TestBed } from '@angular/core/testing';

import { UapzilaService } from './uapzila.service';

describe('UapzilaService', () => {
  let service: UapzilaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(UapzilaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
