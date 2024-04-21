import { TestBed } from '@angular/core/testing';

import { EyesColorService } from './eyes-color.service';

describe('EyesColorService', () => {
  let service: EyesColorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EyesColorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
