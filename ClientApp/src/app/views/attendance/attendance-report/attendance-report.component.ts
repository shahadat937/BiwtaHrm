import { HttpClient, HttpParams } from '@angular/common/http';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { range, Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { AttendanceReportService } from '../services/attendance-report.service';
import {AttendanceReportEmpService} from '../services/attendance-report-emp.service'
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { DateRange } from '@angular/material/datepicker';
import { Office } from '../../basic-setup/model/office';
import {cibVerizon,cibXPack} from '@coreui/icons'
import { SectionService } from '../../basic-setup/service/section.service';
import { DepartmentService } from '../../basic-setup/service/department.service';


@Component({
  selector: 'app-attendance-report',
  templateUrl: './attendance-report.component.html',
  styleUrl: './attendance-report.component.scss'
})
export class AttendanceReportComponent implements OnInit, OnDestroy {
  staticColumnsBefore:any[] = [
    {"field":"name","header":"Name"},
    {"field":"departmentName","header":"Department"}];

  loading:boolean = false;
  icons = {cibVerizon,cibXPack};

  dynamicColumn: any[] = [];
  tableData: any[] = [];
  OfficeOption:any[] = [];
  DepartmentOption: any[] = [];
  DesignationOption: any[] = [];
  ShiftOption: any[] = [];
  EmployeeOption: any[] = [];
  SectionOption: any[] = [];
  rangeDates: Date | null;
  subscription: Subscription = new Subscription();

  // Selected Option Variable
  selectedSection: number|null;
  selectedOffice: number|null;
  selectedDepartment: number | null;
  selectedDesignation: number | null;
  selectedShift : number | null;
  selectedEmp: number | null;

  constructor(
    private departmentService: DepartmentService,
    private SectionService: SectionService,
    private AtdReportService: AttendanceReportEmpService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.selectedOffice = null;
    this.selectedDepartment = null;
    this.selectedDesignation = null;
    this.selectedShift = null;
    this.selectedEmp = null;
    this.selectedSection = null;
    this.rangeDates = null;
  }

  ngOnInit(): void {
    this.getAllDepartment();
    this.AtdReportService.getEmpOption().subscribe({
      next: response => {
        this.EmployeeOption = response;
      }
    });
  }


  onOfficeChange() {
    if(this.selectedOffice!=null) {
      this.AtdReportService.getDepartmentOption(this.selectedOffice).subscribe(option=> {
        this.DepartmentOption = option;
      })
    } else {
      this.DepartmentOption = [];
    }
    this.selectedDepartment = null;
    this.onDepartmentChange();
    this.getFilteredEmp();
  }

  getAllDepartment() {
    this.subscription = this.departmentService.getSelectedAllDepartment().subscribe({
      next: response => {
        this.DepartmentOption = response;
      }
    })
  }

  onDepartmentChange() {
    if(this.selectedDepartment!=null) {
      //this.AtdReportService.getDesignationOption(this.selectedDepartment).subscribe(option=> {
      //  this.DesignationOption = option;
      //});

      this.subscription = this.SectionService.getSectionByOfficeDepartment(this.selectedDepartment).subscribe({
        next: response => {
          this.SectionOption = response;
        }
      })
    } else {
      //this.DesignationOption = [];
      this.SectionOption = [];
    }
    this.selectedDesignation = null;
    this.selectedEmp = null;
    this.selectedSection = null;
    this.getFilteredEmp();
  }

  onDesignationChange() {
    this.getFilteredEmp();
  }

  getFilteredEmp() {
    let params = new HttpParams();

    console.log(this.selectedDesignation);
    params = this.selectedOffice!=null?params.set('OfficeId',this.selectedOffice):params;
    params = this.selectedDepartment!=null?params.set("DepartmentId",this.selectedDepartment):params;
    params = this.selectedDesignation!=null?params.set("DesignationId",this.selectedDesignation):params;

    this.AtdReportService.getFilteredEmpOption(params).subscribe(option=> {
      this.EmployeeOption = option;
    });
  }

  onSubmit() {

    let params = new HttpParams();
    if(this.rangeDates==null) {
      return;
    }
    params = params.set("From",this.getDate(this.rangeDates.getMonth(),this.rangeDates.getFullYear(),true));
    params = params.set("To",this.getDate(this.rangeDates.getMonth(),this.rangeDates.getFullYear(),false));
    
    params = this.selectedOffice == null?params:params.set("OfficeId",this.selectedOffice);
    params = this.selectedDepartment == null? params: params.set("DepartmentId",this.selectedDepartment);
    params = this.selectedDesignation == null? params: params.set("DesignationId", this.selectedDesignation);
    params = this.selectedSection == null? params: params.set('SectionId', this.selectedSection);
    params = this.selectedShift == null? params: params.set("ShiftId",this.selectedShift);
    params = this.selectedEmp == null? params: params.set("EmpId",this.selectedEmp);


    this.subscription = this.AtdReportService.getIsHolidayWeekend(this.rangeDates.getMonth()+1,this.rangeDates.getFullYear()).subscribe({
      next: IsOffday => {
        this.subscription = this.AtdReportService.getAttendanceReport(params).subscribe(result=> {
          if(result.length>0) {
            this.dynamicColumn=Object.keys(result[0]).slice(4).map(val=> {
              return {"header":val.split('-')[2],"field":val,"offday":IsOffday[parseInt(val.split('-')[2])]};
            });

            result = result.map(data => ({...data,"name":data.firstName+' '+data.lastName}))
            this.tableData = result;
            //this.reset();
          }
        }, err=> {
          console.log(err);
        });
        
      }
    })

  }

  reset() {
    this.selectedOffice = null
    this.rangeDates=null;
    this.DepartmentOption = [];
    this.selectedDepartment = null;
    this.DesignationOption = [];
    this.selectedDepartment = null;
    this.DesignationOption = [];
    this.selectedDesignation = null;
    this.ShiftOption = [];
    this.selectedShift = null;
    //this.EmployeeOption = [];
    //this.selectedEmp = null;
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
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

  check() {
    console.log(this.rangeDates);
  }

  getDate(month:number, year: number,IsFirst:boolean) {
    let dt;

    if(IsFirst) {
      dt = new Date(year,month,1);

      return this.hrmdateResize(dt);
    }

    dt = new Date(year,month+1,0);
    return this.hrmdateResize(dt);
  }
}
