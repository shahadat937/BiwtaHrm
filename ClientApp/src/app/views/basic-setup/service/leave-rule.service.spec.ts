import { TestBed } from '@angular/core/testing';

import { LeaveRuleService } from './leave-rule.service';

describe('LeaveRuleService', () => {
  let service: LeaveRuleService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LeaveRuleService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
