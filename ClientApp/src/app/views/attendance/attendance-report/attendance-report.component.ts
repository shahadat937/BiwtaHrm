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

@Component({
  selector: 'app-attendance-report',
  templateUrl: './attendance-report.component.html',
  styleUrl: './attendance-report.component.scss'
})
export class AttendanceReportComponent implements OnInit {
  staticColumnsBefore:any[] = [{"field":"empId", "header":"EmployeeId"},
    {"field":"empFirstName","header":"First Name"},
    {"field":"empLastName","header":"Last Name"},
    {"field":"date","header":"Date"}];

  loading:boolean = false;

  dynamicColumn: any[] = [];
  staticColumnsAfter: any[] = ["Action"];
  displayedColumns: any[] = [];
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
    for(let i = 1; i<=31;i++) {
      this.dynamicColumn.push({"field":i.toString(),"header":i.toString()});
    }

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
}
