import { Component, Input } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-print-att-report',
  templateUrl: './print-att-report.component.html',
  styleUrl: './print-att-report.component.scss'
})
export class PrintAttReportComponent {
  companyTitle: string;
  
  @Input()
  staticColumn: any[]

  @Input()
  dynamicColumn: any[]

  @Input()
  leaveTypeReport : any[];
  @Input()
  tableData: any[];
  constructor() {
    this.companyTitle = environment.companyTitle;
    this.staticColumn = [];
    this.dynamicColumn = [];
    this.tableData = [];
    this.leaveTypeReport = [];
  }
}
