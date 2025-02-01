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

@Component({
  selector: 'app-employee-list-reporting',
  templateUrl: './employee-list-reporting.component.html',
  styleUrl: './employee-list-reporting.component.scss'
})
export class EmployeeListReportingComponent implements OnInit, OnDestroy {

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
  totalEmployee: number = 0;
  employees!: EmployeeListReporting[];
  constructor(
    public reportingService: ReportingService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    ) {
  
    }

  ngOnInit(): void {
    this.getAllSelectedDepartments();
    this.getEmployeeListReportingResult(this.pagination);
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
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.sectionId = 0;
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
    });
    this.getEmployeeListReportingResult(this.pagination);
  }

  onSectionSelect(){
    if (this.paginator) {
      this.paginator.firstPage();
    }
    this.getEmployeeListReportingResult(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.getEmployeeListReportingResult(this.pagination);
  }

  
  getEmployeeListReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getEmployeeListReportingResult(queryParams, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.employees = res.items;
      this.pagination.length = res.totalItemsCount;
      this.totalEmployee = res.items[0].allTotal;
    })
    )
  }
  

}
