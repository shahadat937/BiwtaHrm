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


@Component({
  selector: 'app-attendance-report',
  templateUrl: './attendance-report.component.html',
  styleUrl: './attendance-report.component.scss'
})
export class AttendanceReportComponent implements OnInit, OnDestroy {
  staticColumnsBefore:any[] = [{"field":"empId", "header":"EmployeeId"},
    {"field":"firstName","header":"First Name"},
    {"field":"lastName","header":"Last Name"}];

  loading:boolean = false;
  icons = {cibVerizon,cibXPack};

  dynamicColumn: any[] = [];
  tableData: any[] = [];
  OfficeOption:any[] = [];
  DepartmentOption: any[] = [];
  DesignationOption: any[] = [];
  ShiftOption: any[] = [];
  EmployeeOption: any[] = [];
  rangeDates: Date[] = [];
  subscription: Subscription = new Subscription();

  // Selected Option Variable
  selectedOffice: number|null;
  selectedDepartment: number | null;
  selectedDesignation: number | null;
  selectedShift : number | null;
  selectedEmp: number | null;

  constructor(
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
  }

  ngOnInit(): void {
    this.subscription = this.AtdReportService.getOfficeOption().subscribe(option=> {
      this.OfficeOption = option;
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
    console.log(this.rangeDates.length);
  }

  onDepartmentChange() {
    if(this.selectedDepartment!=null) {
      this.AtdReportService.getDesignationOption(this.selectedDepartment).subscribe(option=> {
        this.DesignationOption = option;
      });
    } else {
      this.DesignationOption = [];
    }
    this.selectedDesignation = null;
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
    console.log(this.hrmdateResize(this.rangeDates[0]));

    let params = new HttpParams();
    params = params.set("From",this.hrmdateResize(this.rangeDates[0]));
    params = params.set("To",this.hrmdateResize(this.rangeDates[1]));
    
    params = this.selectedOffice == null?params:params.set("OfficeId",this.selectedOffice);
    params = this.selectedDepartment == null? params: params.set("DepartmentId",this.selectedDepartment);
    params = this.selectedDesignation == null? params: params.set("DesignationId", this.selectedDesignation);
    params = this.selectedShift == null? params: params.set("ShiftId",this.selectedShift);
    params = this.selectedEmp == null? params: params.set("EmpId",this.selectedEmp);


    this.subscription = this.AtdReportService.getAttendanceReport(params).subscribe(result=> {
      if(result.length>0) {
        this.dynamicColumn=Object.keys(result[0]).slice(3).map(val=> {
          return {"header":val,"field":val};
        });
        this.tableData = result;
        this.reset();
      }
    }, err=> {
      console.log(err);
    });

  }

  reset() {
    this.selectedOffice = null
    this.rangeDates=[];
    this.DepartmentOption = [];
    this.selectedDepartment = null;
    this.DesignationOption = [];
    this.selectedDepartment = null;
    this.DesignationOption = [];
    this.selectedDesignation = null;
    this.ShiftOption = [];
    this.selectedShift = null;
    this.EmployeeOption = [];
    this.selectedEmp = null;
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
}
