import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-print-emp-att-report',
  templateUrl: './print-emp-att-report.component.html',
  styleUrl: './print-emp-att-report.component.scss'
})
export class PrintEmpAttReportComponent implements OnInit, OnDestroy {
  companyTitle: string;
  @Input()
  attendanceData: any[];
  photoUrl: string;

  @Input()
  empInfo: any;
  constructor() {
    this.companyTitle = environment.companyTitle
    this.attendanceData = [];
    this.photoUrl = environment.imageUrl;
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    
  }
}
