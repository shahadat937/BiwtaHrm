import { TestBed } from '@angular/core/testing';

import { InputFieldSyncService } from './input-field-sync.service';

describe('InputFieldSyncService', () => {
  let service: InputFieldSyncService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InputFieldSyncService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
