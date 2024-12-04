import { HttpClient, HttpParams } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { range, Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { AttendanceReportService } from '../services/attendance-report.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { DateRange } from '@angular/material/datepicker';
import { SectionService } from '../../basic-setup/service/section.service';

@Component({
  selector: 'app-attendance-summary',
  templateUrl: './attendance-summary.component.html',
  styleUrl: './attendance-summary.component.scss'
})
export class AttendanceSummaryComponent implements OnInit, OnDestroy, AfterViewInit {
  isVisibleEmpSummary=true;
  EmpSummaryButtonText="Hide";
  // subscription:Subscription = new Subscription();
  subscription: Subscription[]=[]

  // For Employee Wise
  OfficeOption:any[] = [];
  DepartmentOption:any[]=[];
  ShiftOption: any[]=[];
  EmpOption: any[] = [];
  SectionOption: any[] = [];

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
  selectedSection: number | null;
  fromDate: Date|null;
  toDate: Date | null;
  rangeDates: Date[] = [];

  //For Department wise
  TotalPresentEmp:any="N/A";
  TotalAbsentEmp: any = "N/A";
  fromDateDw: Date | null;
  toDateDw: Date | null;
  DepartmentOptionDw: any[] = [];
  OfficeOptionDw: any[] = [];
  dataSource: any = new MatTableDataSource();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  displayedColumns = ["date","totalPresentEmp","totalAbsentEmp"];
  rangeDatesDw:Date[] = [];

  // For Department wise
  selectedDepartmentDw : number | null;
  selectedOfficeDw: number | null;
  constructor(
    private SectionService: SectionService,
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
    this.selectedSection = null;
    this.fromDate = null;
    this.toDate = null;
    this.selectedDepartmentDw = null;
    this.selectedOfficeDw = null;
    this.toDateDw = null;
    this.fromDateDw = null;
  }

  ngOnInit(): void {
    this.getAllEmp();
    this.getSelectedDepartment();
    this.onChangeDw();
  }

  ngOnDestroy(): void {
    if(this.subscription!=null) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  ngAfterViewInit(): void {
    
  }


  getAllOffice() {
     this.subscription.push(
      this.AtdReportService.getOfficeOption().subscribe(option=> {
      this.OfficeOption = option;
      this.OfficeOptionDw = option;
    })
     )
     
  }


  getSelectedDepartment() {

    this.subscription.push(
       this.AtdReportService.getSelectedDepartment().subscribe(option => {
      this.DepartmentOption = option;
    })
    )
   
  }


  onEmpChange() {
    //console.log(this.toDate?.toString());
    if(this.selectedEmp==null||this.rangeDates.length<2||this.rangeDates[0]>this.rangeDates[1]) {
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
    filter = filter.set("From", this.hrmdateResize(this.rangeDates[0]));
    filter = filter.set("To", this.hrmdateResize(this.rangeDates[1]));


     
     this.subscription.push(
      this.AtdReportService.getEmpSummary(filter).subscribe(response=> {
        this.PresentText = response.totalPresent;
        this.AbsentText = response.totalAbsent;
        this.LateText = response.totalLate;
        this.WorkingDayText = response.totalWorkingDay;
        this.SiteVisitText = response.totalSiteVisit;
        this.OnLeaveText = response.totalOnLeave;
      })
     )
    
  }

  onChange(IsDepartment: boolean) {
    //console.log(this.selectedDepartment);

    //if(this.selectedOffice!=null)
    //this.AtdReportService.getDepartmentOption(this.selectedOffice).subscribe(
    //  option => this.DepartmentOption = option,
    //);

    //if(this.selectedOffice==null) {
    //  this.DepartmentOption=[];
    //}


    let params = new HttpParams();

    if(this.selectedDepartment!=null&&IsDepartment) {
      this.SectionService.getSectionByOfficeDepartment(this.selectedDepartment).subscribe({
        next: response => {
          this.SectionOption = response;
          this.selectedSection = null;
        }
      })
    }

    if(this.selectedDepartment!=null) {
      params = params.set('DepartmentId',this.selectedDepartment);
    }

    if(this.selectedSection!=null) {
      params = params.set('SectionId', this.selectedSection);
    }

    this.subscription.push(
      this.AtdReportService.getFilteredEmpOption(params).subscribe(response=> {
        this.EmpOption = response;
        this.selectedEmp = null;
    })
    )
    
    
  }

  onChangeDw() {

    this.subscription.push(
       this.AtdReportService.getSelectedDepartment().subscribe(option=> {
      this.DepartmentOptionDw = option;
    })
    )
   

/*     if(this.toDateDw==null||this.fromDateDw==null) {
      // TO DO
      return;
    } */

    if(this.rangeDatesDw.length<2||this.rangeDatesDw[0]>this.rangeDatesDw[1]) {
      return;
    }

    console.log(this.rangeDatesDw);

    

    let params = new HttpParams();
    params = params.set("From", this.hrmdateResize(this.rangeDatesDw[0]));
    params = params.set("To", this.hrmdateResize(this.rangeDatesDw[1]));

    if(this.selectedOfficeDw!=null) {
      params = params.set("OfficeId",this.selectedOfficeDw);
    }

    if(this.selectedDepartmentDw!=null) {
      params = params.set("DepartmentId",this.selectedDepartmentDw);
    }

    this.subscription.push(
      this.AtdReportService.getDepartmentWiseSummary(params).subscribe(item=> {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
    })
    )
   


  }

  hrmdateResize(formDateValue:any){
    let EntryDate="";
    var month;
    var day;
    var dateObj = new Date(formDateValue);
    var dObj=dateObj.toLocaleDateString().split('/');
    month=parseInt(dObj[0]);
    day=parseInt(dObj[1]);
    if(month<10){
      month='0'+month;
    }
    if(day<10){
      day='0'+day;
    }
 
    EntryDate =dObj[2]+'-'+month+'-'+day;
    return EntryDate;
  }

  getAllEmp() {
    let params = new HttpParams();
    this.AtdReportService.getFilteredEmpOption(params).subscribe({
      next: response => {
        this.EmpOption = response;
      }
    })
  }
}
