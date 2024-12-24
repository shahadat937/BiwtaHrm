import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr'
import {AuthService} from './auth.service'
import { environment } from '../../../environments/environment';
import { Subscription } from 'rxjs';
import { SignalREventBusService } from './signal-revent-bus.service';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  hubConnection: signalR.HubConnection | null;
  endPoint: string;
  subscription: Subscription[] = [];


  // Register the event here
  EventLists: string [] = [
    'newDevice',
    'notification',
    'newAtd',
    'AttDeviceUpdate'
  ]


  constructor(
    private authService: AuthService,
    private SignalREventBusService: SignalREventBusService
  ) {
    this.hubConnection = null;
    this.endPoint = environment.signalREndpoint;
    const subs = this.authService.currentUser.subscribe(user => {
      if(user==null||user.token==null) {
        this.startConnection(null);
      } else if(user&&user.token) {
        this.startConnection(user.token);
      }
    })

    this.subscription.push(subs);
  }

  private startConnection(token:string|null) {

    if(this.hubConnection) {
      this.hubConnection.stop();
    }

    if(token==null) {
      return;
    }
    
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.endPoint)
      .withAutomaticReconnect() 
      .build();

    // Start the connection
    this.hubConnection
      .start()
      .then(() => {
        this.RegisterEvent();
        //console.log('SignalR connection established');
      })
      .catch((err:any) => console.error('Error while starting connection: ' + err));
  }

  private RegisterEvent() {
    this.EventLists.forEach(event => {
      if(this.hubConnection) {
        this.hubConnection.on(event,(data:any)=> {
          this.SignalREventBusService.emitEvent(event,data);
        })
      }
    })
  }

}
