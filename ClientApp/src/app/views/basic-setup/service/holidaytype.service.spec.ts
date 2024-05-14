import { TestBed } from '@angular/core/testing';

import { HolidaytypeService } from './holidaytype.service';

describe('HolidaytypeService', () => {
  let service: HolidaytypeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HolidaytypeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
