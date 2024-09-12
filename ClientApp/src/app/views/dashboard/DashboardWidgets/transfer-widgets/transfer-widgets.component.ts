import { Component, OnInit } from '@angular/core';
import { cilOptions, cilArrowTop } from '@coreui/icons';
import { getStyle } from '@coreui/utils';
import { WidgetsService } from '../service/widgets.service';

@Component({
  selector: 'app-transfer-widgets',
  templateUrl: './transfer-widgets.component.html',
  styleUrl: './transfer-widgets.component.scss'
})
export class TransferWidgetsComponent implements OnInit {

  constructor(
    public widgetService: WidgetsService,
  ) {}

  icons = { cilOptions, cilArrowTop };

  transferData: any[] = [];
  data: any = {};
  options: any = {};

  labels = [];

  datasets = [];



  ngOnInit(): void {
    this.getTransferPostingWidgetsInfo();
  }

  getTransferPostingWidgetsInfo(){
    this.widgetService.getAllTransferWidgetsInfo().subscribe((item) => {
      this.transferData = item;
    })
  }

}
