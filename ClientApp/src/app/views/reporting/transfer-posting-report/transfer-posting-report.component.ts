import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { EmployeeListReporting } from '../models/employee-list-reporting';
import { ReportingService } from '../service/reporting.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';

@Component({
  selector: 'app-transfer-posting-report',
  templateUrl: './transfer-posting-report.component.html',
  styleUrl: './transfer-posting-report.component.scss'
})
export class TransferPostingReportComponent implements OnInit, OnDestroy {

  subscription: Subscription[]=[];
  departments: SelectedModel[] = [];
  fromSections: SelectedModel[] = [];
  toSections: SelectedModel[] = [];
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
  fromDepartmentId: number = 0;
  fromSectionId: number = 0;
  fromDepartmentName: string = "";
  fromSectionName: string = "";
  toDepartmentId: number = 0;
  toSectionId: number = 0;
  toDepartmentName: string = "";
  toSectionName: string = "";
  totalEmployee: number = 0;
  departmentStatus: any = 0;
  sectionStatus: any = 0;
  dateFrom: any;
  dateTo: any;
  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;
  employees!: EmployeeListReporting[];
  constructor(
    public reportingService: ReportingService,
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
    onDepartmentSelect(departmentId : number, fromDepartment: boolean){
      if (this.paginator) {
        this.paginator.firstPage();
      }
      if(fromDepartment){
        this.fromDepartmentName = "";
        this.fromSectionName = "";
        this.fromSectionId = 0;
        this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
          this.fromSections = res;
        });
        this.departmentService.getById(+departmentId).subscribe((res) => {
          if(res){
            this.fromDepartmentName = res.departmentName;
          }
        });
      }
      else {
        this.toDepartmentName = "";
        this.toSectionName = "";
        this.toSectionId = 0;
        this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
          this.toSections = res;
        });
        this.departmentService.getById(+departmentId).subscribe((res) => {
          if(res){
            this.toDepartmentName = res.departmentName;
          }
        });
      }
    }
  
    onSectionSelect(fromSection: boolean){
      if (this.paginator) {
        this.paginator.firstPage();
      }
      if(fromSection){
        this.fromSectionName = "";
        this.sectionService.find(this.fromSectionId).subscribe((res) => {
          if(res){
            this.fromSectionName = res.sectionName;
          }
        });
      }
      else {
        this.toSectionName = "";
        this.sectionService.find(this.fromSectionId).subscribe((res) => {
          if(res){
            this.toSectionName = res.sectionName;
          }
        });
      }
    }
  
    onPageChange(event: any){
      this.pagination.pageSize = event.pageSize;
      this.pagination.pageIndex = event.pageIndex + 1;
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
