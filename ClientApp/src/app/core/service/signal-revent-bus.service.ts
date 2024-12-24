import { Injectable } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { SignalRService } from './signal-r.service';


interface SignalREvent {
  [key: string]: Subject<any>;
}


@Injectable({
  providedIn: 'root'
})
export class SignalREventBusService {
  private events: SignalREvent = {};
  constructor(
  ) { }

    // Subscribe to an event by name
  public getEvent(eventName: string): Subject<any> {
    if (!this.events[eventName]) {
      this.events[eventName] = new Subject<any>();
    }
    return this.events[eventName];
  }

  // Emit an event to all subscribers
  public emitEvent(eventName: string, data: any): void {
    if (this.events[eventName]) {
      this.events[eventName].next(data);
    }
  }
}
