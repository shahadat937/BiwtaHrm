import { TestBed } from '@angular/core/testing';

import { ShiftSettingService } from './shift-setting.service';

describe('ShiftSettingService', () => {
  let service: ShiftSettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShiftSettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
