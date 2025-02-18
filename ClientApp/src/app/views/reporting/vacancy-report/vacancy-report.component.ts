import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { VacancyDetailsDto } from '../models/vacancy-report-dto';
import { ReportingService } from '../service/reporting.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';

@Component({
  selector: 'app-vacancy-report',
  templateUrl: './vacancy-report.component.html',
  styleUrl: './vacancy-report.component.scss'
})
export class VacancyReportComponent implements OnInit, OnDestroy {

  subscription: Subscription[]=[];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
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
  departmentName: string = "";
  sectionName: string = "";
  totalPost: number = 0;
  totalInService: number = 0;
  totalVacant: number = 0;
  employees!: VacancyDetailsDto[];
  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;
  constructor(
    public reportingService: ReportingService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    public empPhotoSignService: EmpPhotoSignService,
    ) {
  
    }

  ngOnInit(): void {
    this.getAllSelectedDepartments();
    this.getVacantListReportingResult(this.pagination);
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
    this.getVacantListReportingResult(this.pagination);
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
    this.getVacantListReportingResult(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.getVacantListReportingResult(this.pagination);
  }

  
  getVacantListReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getVacantListReportingResult(queryParams, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.employees = res.items[0].vacancyDetailsDto;
      this.employees = this.employees.map(emp => ({
        ...emp,
        groupKey: `${emp.departmentName} - ${emp.sectionName || 'No Section'}`
      }));
      this.pagination.length = res.totalItemsCount;
      this.totalPost = res.items[0].totalPost;
      this.totalInService = res.items[0].totalInService;
      this.totalVacant = res.items[0].totalVacant;
    })
    )
  }

  calculateTotalPost(groupKey: string): number {
    let total = 0;
  
    if (this.employees) {
      for (let emp of this.employees) {
        let details = emp; // Get first item in vacancyDetailsDto
        if (details && `${details.departmentName} - ${details.sectionName || 'No Section'}` === groupKey) {
          total += details.totalPost;
        }
      }
    }
    return total;
  }
  
  calculateTotalInService(groupKey: string): number {
    let total = 0;
  
    if (this.employees) {
      for (let emp of this.employees) {
        let details = emp; // Get first item in vacancyDetailsDto
        if (details && `${details.departmentName} - ${details.sectionName || 'No Section'}` === groupKey) {
          total += details.totalInService;
        }
      }
    }
    return total;
  }
  
  calculateTotalVacant(groupKey: string): number {
    let total = 0;
  
    if (this.employees) {
      for (let emp of this.employees) {
        let details = emp; // Get first item in vacancyDetailsDto
        if (details && `${details.departmentName} - ${details.sectionName || 'No Section'}` === groupKey) {
          total += details.totalVacantPost;
        }
      }
    }
    return total;
  }

  printSection() {
    // Get the basic information and the specific section to print
    const tableData = document.getElementById('tableData')?.innerHTML;
    const heading = document.getElementById('report_heading')?.innerHTML;

    // Create a new window for printing
    const printWindow = window.open('', '_blank', 'width=800,height=600');
    printWindow?.document.write(`
      <html>
        <head>
          <title>Vacancy Report</title>
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
            c-card-header {text-align: end; margin: 20px 0; display: block;}
            c-card-header label {border: 1px solid black; padding: 5px;}
            .joinDate {width: 70px;}
            .group-header {background: #add8e6;}
            .groupfooter {
                background-color: azure;
                border-bottom: 1px solid gray;
            }
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
