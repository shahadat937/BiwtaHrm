import { Component, OnDestroy, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-print-emp-att-report',
  templateUrl: './print-emp-att-report.component.html',
  styleUrl: './print-emp-att-report.component.scss'
})
export class PrintEmpAttReportComponent implements OnInit, OnDestroy {
  companyTitle: string;
  constructor() {
    this.companyTitle = environment.companyTitle
  }

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    
  }
}
