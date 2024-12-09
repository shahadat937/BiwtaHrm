import { Component, OnDestroy, OnInit } from '@angular/core';
import { WidgetsService } from '../service/widgets.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-chart-widgets',
  templateUrl: './chart-widgets.component.html',
  styleUrl: './chart-widgets.component.scss'
})
export class ChartWidgetsComponent implements OnInit, OnDestroy {

  gendersInfo: any;
  fieldUnfield: any;
  subscription: Subscription[]=[];

  constructor(public widgetsService: WidgetsService) {
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  ngOnInit(): void {
    this.getAllGenderChatInfo();
    this.getAllFieldUnfieldChatInfo();
  }

  getAllGenderChatInfo(){
    this.subscription.push(
      this.widgetsService.getAllGendersWidgetsInfo().subscribe((item) => {
      this.gendersInfo = item;
    })
    )
    
  }
  
  getAllFieldUnfieldChatInfo(){
    this.subscription.push(
      this.widgetsService.getAllFieldUnfieldWidgetsInfo().subscribe((item) => {
      this.fieldUnfield = item;
    })
    )
    
  }

}
