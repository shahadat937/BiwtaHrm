import { TestBed } from '@angular/core/testing';

import { EmpTrainingInfoService } from './emp-training-info.service';

describe('EmpTrainingInfoService', () => {
  let service: EmpTrainingInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpTrainingInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
