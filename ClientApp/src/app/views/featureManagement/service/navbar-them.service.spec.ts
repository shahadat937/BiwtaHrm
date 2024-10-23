import { TestBed } from '@angular/core/testing';

import { NavbarThemService } from './navbar-them.service';

describe('NavbarThemService', () => {
  let service: NavbarThemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NavbarThemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
