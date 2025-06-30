import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ReportingService } from '../service/reporting.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { EmployeeListReporting } from '../models/employee-list-reporting';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { LeaveReportingService } from '../service/leave-reporting.service';

@Component({
  selector: 'app-leave-reporting',
  templateUrl: './leave-reporting.component.html',
  styleUrl: './leave-reporting.component.scss'
})
export class LeaveReportingComponent implements OnInit, OnDestroy {

  subscription: Subscription[]=[];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  leaveTypes: SelectedModel[] = [];
  displayedColumns: string[] = [
      // 'slNo',
      'employee',
      'designation',
      'section',
      'phone',
      'joinDate'
    ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  pagination: PaginatorModel = new PaginatorModel();
  departmentId: number = 0;
  sectionId: number = 0;
  designationId: number = 0;
  leaveTypeId: number = 0;
  departmentName: string = "";
  sectionName: string = "";
  designationName: string = "";
  leaveTypeName: string = "";
  totalEmployee: number = 0;
  fromDate: any;
  toDate: any;
  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;
  employees!: EmployeeListReporting[];
  constructor(
    public leaveReportingService: LeaveReportingService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    public empPhotoSignService: EmpPhotoSignService,
    ) {
  
    }

  ngOnInit(): void {
    this.getAllSelectedDepartments();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }
  getAllSelectedDepartments(){
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
    )
  }
  onDepartmentSelect(departmentId : number){
    this.departmentName = "";
    this.sectionName = "";
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.sectionId = 0;
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
    });
    this.departmentService.getById(+departmentId).subscribe((res) => {
      if(res){
        this.departmentName = res.departmentName;
      }
    });
    this.getLeaveReportingResult(this.pagination);
  }

  onSectionSelect(){
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.sectionName = "";
    this.sectionService.find(this.sectionId).subscribe((res) => {
      if(res){
        this.sectionName = res.sectionName;
      }
    });
    this.getLeaveReportingResult(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.getLeaveReportingResult(this.pagination);
  }

  getLeaveReportingResult(queryParams: any){
    this.subscription.push(
      this.leaveReportingService.getLeaveReporting(queryParams, this.departmentId, this.sectionId, this.designationId, this.leaveTypeId, this.fromDate, this.toDate).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.employees = res.items;
      this.employees = this.employees.map(emp => ({
        ...emp,
        groupKey: `${emp.departmentName} - ${emp.sectionName || 'No Section'}`
      }));
      this.pagination.length = res.totalItemsCount;
      this.totalEmployee = res.items[0].allTotal;
    })
    )
  }

  
  printSection() {
    // Get the basic information and the specific section to print
    const tableData = document.getElementById('tableData')?.innerHTML;
    const heading = document.getElementById('report_heading')?.innerHTML;

    // Create a new window for printing
    const printWindow = window.open('', 'blank', 'width=800,height=600');
    printWindow?.document.write(`
      <html>
        <head>
          <title>Employee List</title>
          <style>
            @media print {
              @page {
                margin-top: 0;
                padding-top: 40px;
              }
              header {
                display: none !important;
              }
            }
            table { border-collapse: collapse; text-align: left; width: 100%}
            th, td {border: 1px solid #000; padding: 5px; font-size: 13px;}
            c-col { 
              float: left; 
            }
            c-card-footer {display: none;}
            c-card-header {text-align: end; margin-bottom: 10px;}
            .joinDate {width: 70px;}
            .group-header {background: #add8e6;}
          </style>
        </head>
        <body>
          <div>${heading}</div>
          <div>${tableData}</div>
        </body>
      </html>
    `);
    printWindow?.document.close();
    printWindow?.print();
  }
}
