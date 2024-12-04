import { TestBed } from '@angular/core/testing';

import { NavbarSettingService } from './navbar-setting.service';

describe('NavbarSettingService', () => {
  let service: NavbarSettingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NavbarSettingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
