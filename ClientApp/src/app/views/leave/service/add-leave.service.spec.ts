import { TestBed } from '@angular/core/testing';

import { AddLeaveService } from './add-leave.service';

describe('AddLeaveService', () => {
  let service: AddLeaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddLeaveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
