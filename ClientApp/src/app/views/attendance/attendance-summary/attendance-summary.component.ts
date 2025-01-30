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
import { cilSearch } from '@coreui/icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { Feature } from '../../featureManagement/model/feature';
import { AuthService } from 'src/app/core/service/auth.service';
import { TimeScale } from 'chart.js';

@Component({
  selector: 'app-attendance-summary',
  templateUrl: './attendance-summary.component.html',
  styleUrl: './attendance-summary.component.scss'
})
export class AttendanceSummaryComponent implements OnInit, OnDestroy, AfterViewInit {
  // Feature Permission
  featurePermission: FeaturePermission = new FeaturePermission();

  isVisibleEmpSummary=true;
  EmpSummaryButtonText="Hide";
  icons = {cilSearch}
  // subscription:Subscription = new Subscription();
  subscription: Subscription[]=[]
  summaryDetail: any[];

  // For Employee Wise
  OfficeOption:any[] = [];
  DepartmentOption:any[]=[];
  ShiftOption: any[]=[];
  EmpOption: any[] = [];
  SectionOption: any[] = [];

  // For Employee Wise
  PMIS: string;
  EmpName: string;
  PresentText:any="N/A";
  AbsentText: any ="N/A";
  LateText:any = "N/A";
  WorkingDayText: any = "N/A";
  SiteVisitText: any = "N/A";
  OnLeaveText: any = "N/A";

  summary: any ;

  // For Employee Wise
  selectedEmp: number|null;
  selectedDepartment: number | null;
  selectedOffice: number|null;
  selectedSection: number | null;
  fromDate: string;
  toDate: string;
  rangeDates: Date[] = [];
  empInfo: any;

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
    private authService: AuthService,
    private roleFeatureService: RoleFeatureService,
    private empBasicInfoService: EmpBasicInfoService,
    private modalService: BsModalService,
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
    this.fromDate = "";
    this.toDate = "";
    this.selectedDepartmentDw = null;
    this.selectedOfficeDw = null;
    this.toDateDw = null;
    this.fromDateDw = null;
    this.summaryDetail = [];
    this.PMIS = "";
    this.EmpName = "";


    this.empInfo = {
      empName: "",
      pmis: "",
      designation: "",
      department: "",
      photoUrl: "",
    }

    this.setSummary();
  }

  ngOnInit(): void {

    this.getPermission();
    this.setEmployee();
    this.getAllEmp();
    this.getSelectedDepartment();
    this.onChangeDw();
    
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('attendanceSummary').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        // To do
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }

  setEmployee() {
    const subs = this.authService.currentUser.subscribe(user => {
      if(user!=null&&user.empId!=null) {
        let empID = parseInt(user.empId);

        if(empID !=undefined) {
          this.empBasicInfoService.findByEmpId(empID).subscribe({
            next: response => {
              this.selectedEmp = response.id;
              this.EmpName = [response.firstName, response.lastName].join(' ');
            }
          })
        }
      }
    })
  }


  setSummary() {
    this.summary = {
      Present: this.PresentText,
      Absent: this.AbsentText,
      Late: this.LateText,
      Workingday: this.WorkingDayText,
      Sitevisit: this.SiteVisitText,
      Leave: this.OnLeaveText
    };
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
        this.summaryDetail = [];
        this.fromDate = "";
        this.toDate = "";
        this.setSummary();
        return;
    }

    let filter = new HttpParams();
    let from = this.hrmdateResize(this.rangeDates[0])
    let to = this.hrmdateResize(this.rangeDates[1])
    this.fromDate = from;
    this.toDate = to;
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
        this.setSummary();
      })
     )

     let params = new HttpParams();
     params = params.set('empId',this.selectedEmp);
     params = params.set('startDate', from);
     params = params.set('endDate', to);
     
     const subs = this.AtdReportService.getAttendanceDetailSummary(params).subscribe({
      next: (response:any) => {
        this.summaryDetail = response.attendance;
        this.empInfo = response.employeeInfo
      }
     });

     this.subscription.push(subs);
    
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
    var dObj=dateObj.toLocaleDateString("en-US").split('/');
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

  openEmployeeModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });

    const subs = modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
      if(idCardNo){
          this.PMIS = idCardNo;
          this.onPMISChange();
      }
    });
  }

  onPMISChange() {
      const subs = this.empBasicInfoService.getEmpInfoByCard(this.PMIS).subscribe({
        next: response => {
          if(response!=null&&response.id!=null) {
            this.selectedEmp = response.id;
            this.EmpName = [response.firstName,response.lastName].join(' ');
            this.onEmpChange();
          }
        }
      })  
  }
}
