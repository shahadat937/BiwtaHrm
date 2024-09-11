import { Component, OnInit } from '@angular/core';
import { cilOptions, cilArrowTop } from '@coreui/icons';
import { WidgetsService } from '../service/widgets.service';

@Component({
  selector: 'app-promotion-widgets',
  templateUrl: './promotion-widgets.component.html',
  styleUrl: './promotion-widgets.component.scss'
})
export class PromotionWidgetsComponent implements OnInit {

  constructor(
    public widgetService: WidgetsService,
  ) {}

  icons = { cilOptions, cilArrowTop };

  promotionData: any[] = [];
  data: any = {};
  options: any = {};

  labels = [];

  datasets = [];



  ngOnInit(): void {
    this.getPromotionIncrementWidgetsInfo()
  }
  
  getPromotionIncrementWidgetsInfo(){
    this.widgetService.getAllPromotionWidgetsInfo().subscribe((item) => {
      this.promotionData = item;
    })
  }

}
