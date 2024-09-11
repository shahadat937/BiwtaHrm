import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';

import { DashboardChartsData, IChartProps } from './dashboard-charts-data';
import { WidgetsService } from './DashboardWidgets/service/widgets.service';

interface IUser {
  name: string;
  state: string;
  registered: string;
  country: string;
  usage: number;
  period: string;
  payment: string;
  activity: string;
  avatar: string;
  status: string;
  color: string;
}

@Component({
  templateUrl: 'dashboard.component.html',
  styleUrls: ['dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  gendersInfo: any;
  fieldUnfield: any;

  constructor(public widgetsService: WidgetsService) {
  }

  ngOnInit(): void {
    this.getAllGenderChatInfo();
    this.getAllFieldUnfieldChatInfo();
  }

  getAllGenderChatInfo(){
    this.widgetsService.getAllGendersWidgetsInfo().subscribe((item) => {
      this.gendersInfo = item;
    })
  }
  
  getAllFieldUnfieldChatInfo(){
    this.widgetsService.getAllFieldUnfieldWidgetsInfo().subscribe((item) => {
      this.fieldUnfield = item;
    })
  }

}
