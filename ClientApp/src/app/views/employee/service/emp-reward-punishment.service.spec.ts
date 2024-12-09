import { TestBed } from '@angular/core/testing';

import { EmpRewardPunishmentService } from '../service/emp-reward-punishment.service';

describe('EmpRewardPunishmentService', () => {
  let service: EmpRewardPunishmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(EmpRewardPunishmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
