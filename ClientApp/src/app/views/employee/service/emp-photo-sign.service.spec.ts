import { TestBed } from '@angular/core/testing';

import { EmpPhotoSignService } from './emp-photo-sign.service';

describe('EmpPhotoSignService', () => {
  let service: EmpPhotoSignService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpPhotoSignService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
