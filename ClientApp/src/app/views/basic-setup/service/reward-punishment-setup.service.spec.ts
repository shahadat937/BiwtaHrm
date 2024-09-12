import { TestBed } from '@angular/core/testing';

import { RewardPunishmentSetupService } from './reward-punishment-setup.service';

describe('RewardPunishmentSetupService', () => {
  let service: RewardPunishmentSetupService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RewardPunishmentSetupService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
