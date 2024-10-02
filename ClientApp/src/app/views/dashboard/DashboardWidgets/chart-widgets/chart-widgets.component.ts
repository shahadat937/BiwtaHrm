import { Component, OnInit } from '@angular/core';
import { WidgetsService } from '../service/widgets.service';

@Component({
  selector: 'app-chart-widgets',
  templateUrl: './chart-widgets.component.html',
  styleUrl: './chart-widgets.component.scss'
})
export class ChartWidgetsComponent implements OnInit {

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
