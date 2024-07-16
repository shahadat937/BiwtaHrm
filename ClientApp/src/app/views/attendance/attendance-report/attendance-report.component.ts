import { HttpClient, HttpParams } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { AttendanceReportService } from '../services/attendance-report.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-attendance-report',
  templateUrl: './attendance-report.component.html',
  styleUrl: './attendance-report.component.scss'
})
export class AttendanceReportComponent implements OnInit, OnDestroy, AfterViewInit {
  isVisibleEmpSummary=true;
  EmpSummaryButtonText="Hide";
  subscription:Subscription = new Subscription();

  // For Employee Wise
  OfficeOption:any[] = [];
  DepartmentOption:any[]=[];
  ShiftOption: any[]=[];
  EmpOption: any[] = [];

  // For Employee Wise
  PresentText:any="N/A";
  AbsentText: any ="N/A";
  LateText:any = "N/A";
  WorkingDayText: any = "N/A";
  SiteVisitText: any = "N/A";
  OnLeaveText: any = "N/A";

  // For Employee Wise
  selectedEmp: number|null;
  selectedDepartment: number | null;
  selectedOffice: number|null;
  fromDate: Date|null;
  toDate: Date | null;

  //For Department wise
  TotalPresentEmp:any="N/A";
  TotalAbsentEmp: any = "N/A";
  fromDateDw: Date | null;
  toDateDw: Date | null;
  DepartmentOptionDw: any[] = [];
  OfficeOptionDw: any[] = [];
  dataSource: any = new MatTableDataSource();
  displayedColumns = ["attendanceDate","totalPresent","totalAbsent"];

  // For Department wise
  selectedDepartmentDw : number | null;
  selectedOfficeDw: number | null;
  constructor(
    private AtdReportService: AttendanceReportService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.isVisibleEmpSummary=true;
    this.selectedDepartment=null;
    this.selectedEmp = null;
    this.selectedOffice = null;
    this.fromDate = null;
    this.toDate = null;
    this.selectedDepartmentDw = null;
    this.selectedOfficeDw = null;
    this.toDateDw = null;
    this.fromDateDw = null;
  }

  ngOnInit(): void {
    this.getAllOffice();
  }

  ngOnDestroy(): void {
    if(this.subscription!=null) {
      this.subscription.unsubscribe();
    }
  }

  ngAfterViewInit(): void {
    
  }


  getAllOffice() {
     this.subscription = this.AtdReportService.getOfficeOption().subscribe(option=> {
      this.OfficeOption = option;
      this.OfficeOptionDw = option;
    })
  }


  onEmpChange() {
    //console.log(this.toDate?.toString());
    if(this.selectedEmp==null||this.toDate==null||this.fromDate==null) {
        this.PresentText="N/A";
        this.AbsentText ="N/A";
        this.LateText = "N/A";
        this.WorkingDayText = "N/A";
        this.SiteVisitText = "N/A";
        this.OnLeaveText = "N/A";
        return;
    }

    let filter = new HttpParams();
    filter = filter.set("EmpId",this.selectedEmp);
    filter = filter.set("From", this.fromDate.toString());
    filter = filter.set("To", this.toDate.toString());

    console.log(this.selectedEmp);

     this.subscription = this.AtdReportService.getEmpSummary(filter).subscribe(response=> {
      this.PresentText = response.totalPresent;
      this.AbsentText = response.totalAbsent;
      this.LateText = response.totalLate;
      this.WorkingDayText = response.totalWorkingDay;
      this.SiteVisitText = response.totalSiteVisit;
    });
  }

  onChange() {
    //console.log(this.selectedDepartment);

    if(this.selectedOffice!=null)
    this.AtdReportService.getDepartmentOption(this.selectedOffice).subscribe(
      option => this.DepartmentOption = option,
    );

    if(this.selectedOffice==null) {
      this.DepartmentOption=[];
    }

    let params = new HttpParams();
    if(this.selectedDepartment!=null) {
      params = params.set('DepartmentId',this.selectedDepartment==null?"":this.selectedDepartment);
    }
    if(this.selectedOffice!=null) {
      params = params.set("OfficeId",this.selectedOffice==null?"":this.selectedOffice);
    }


    console.log(params.get('officeId'));

    if(this.selectedOffice!=null&&this.selectedDepartment!=null) {
       this.subscription = this.AtdReportService.getFilteredEmpOption(params).subscribe(response=> {
        this.EmpOption = response;
        console.log(response);
      });
    }
  }

  onChangeDw() {
    if(this.selectedOfficeDw!=null) {
      this.AtdReportService.getDepartmentOption(this.selectedOfficeDw).subscribe(option=> {
        this.DepartmentOptionDw = option;
      })
    } else {
      this.DepartmentOptionDw = [];
      this.selectedDepartmentDw = null;
    }

    if(this.toDateDw==null||this.fromDateDw==null) {
      // TO DO
      return;
    }


  }
}
