import { TestBed } from '@angular/core/testing';

import { SignalREventBusService } from './signal-revent-bus.service';

describe('SignalREventBusService', () => {
  let service: SignalREventBusService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignalREventBusService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
