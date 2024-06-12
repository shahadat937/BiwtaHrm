import { TestBed } from '@angular/core/testing';

import { EmpPsiTrainingInfoService } from './emp-psi-training-info.service';

describe('EmpPsiTrainingInfoService', () => {
  let service: EmpPsiTrainingInfoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpPsiTrainingInfoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
