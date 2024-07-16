import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-attendance-report',
  templateUrl: './attendance-report.component.html',
  styleUrl: './attendance-report.component.scss'
})
export class AttendanceReportComponent implements OnInit, OnDestroy, AfterViewInit {
  isVisibleEmpSummary=true;
  EmpSummaryButtonText="Hide";
  OfficeOption:any[] = [];
  DepartmentOption:any[]=[];
  ShiftOption: any[]=[];
  EmpOption: any[] = [];
  PresentText:any="N/A";
  AbsentText: any ="N/A";
  LateText:any = "N/A";
  WorkingDayText: any = "N/A";
  SiteVisitText: any = "N/A";
  OnLeaveText: any = "N/A";
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.isVisibleEmpSummary=true;
  }

  ngOnInit(): void {
    
  }

  ngOnDestroy(): void {
    
  }

  ngAfterViewInit(): void {
    
  }

  onChange() {
    
  }
}
