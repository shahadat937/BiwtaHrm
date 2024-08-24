import { TestBed } from '@angular/core/testing';

import { FormRecordService } from './form-record.service';

describe('FormRecordService', () => {
  let service: FormRecordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormRecordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
