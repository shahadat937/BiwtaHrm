import { Injectable } from '@angular/core';
import { SignalREventBusService } from './signal-revent-bus.service';
import { SignalRService } from './signal-r.service';

@Injectable({
  providedIn: 'root'
})
export class RealTimeService {

  constructor(
    private signalRService: SignalRService,
    public eventBus: SignalREventBusService
  ) { 

  }
}
