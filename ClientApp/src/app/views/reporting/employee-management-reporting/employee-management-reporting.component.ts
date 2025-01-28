import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { ReportingService } from '../service/reporting.service';
import { EmpCountOnReportingDto } from '../models/emp-count-on-reporting-dto';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';

@Component({
  selector: 'app-employee-management-reporting',
  templateUrl: './employee-management-reporting.component.html',
  styleUrl: './employee-management-reporting.component.scss'
})
export class EmployeeManagementReportingComponent  implements OnInit, OnDestroy {

  subscription: Subscription[]=[];
  informationType: number = 0;
  queryTypeName: string = '';
  queryType: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  displayedColumns: string[] = [
      // 'slNo',
      'employee',
      'department/section',
      'designation',
      'typeName',
      'phone',
      'status'
    ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  pagination: PaginatorModel = new PaginatorModel();
  queryCount: EmpCountOnReportingDto = new EmpCountOnReportingDto();
  typeId: number = 0;
  typeName: string = 'All';
  unAssigned: boolean = false;
  departmentId: number = 0;
  sectionId: number = 0;

  constructor(
    public reportingService: ReportingService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
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

  onTypeValueChange(type: string, id: number){
    if(type == 'All'){
      this.unAssigned = false;
      this.typeId = 0;
      this.typeName = type;
    }
    else if(type == 'Unassigned'){
      this.unAssigned = true;
      this.typeId = 0;
      this.typeName = type;
    }
    else {
      this.unAssigned = false;
      this.typeId = id;
      this.typeName = type;
    }
    this.onQueryTypeChange(false);
  }

  
  getAllSelectedDepartments(){
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
    )
  }
  onDepartmentSelect(departmentId : number){
    this.sectionId = 0;
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
    });
    this.onQueryTypeChange(false);
  }
  onSectionSelect(){
    this.onQueryTypeChange(false);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    this.pagination.pageIndex = event.pageIndex + 1;
    this.onQueryTypeChange(true);
  }

  onTypeChange(){
    this.departmentId = 0;
    this.sectionId = 0;
    this.typeId = 0;
    this.typeName = 'All';
    this.onQueryTypeChange(false);
  }
  onQueryTypeChange(pageChanged: boolean){
    if(!pageChanged){
      this.pagination.pageIndex = 1;
      if (this.paginator) {
        this.paginator.firstPage();
      }
    }
    if(this.queryTypeName == 'Employee Type'){
      this.getEmployeeTypeCount();
      this.getEmployeeTypeReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Blood Group'){
      this.getBloodGroupCount();
      this.getBloodGroupReportingResult(this.pagination);
    }
    else if(this.queryTypeName == 'Religion'){
      this.getReligionCount();
      this.getReligionReportingResult(this.pagination);
    }
    else {
      this.dataSource.data = [];
      this.pagination.length = 0;
      this.queryCount = new EmpCountOnReportingDto();
    }
  }

      // Employee Type
  getEmployeeTypeCount(){
    this.subscription.push(
      this.reportingService.getEmployeeTypeCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getEmployeeTypeReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getEmployeeTypeReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }


      // Religion
  getReligionCount(){
    this.subscription.push(
      this.reportingService.getReligionCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getReligionReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getReligionReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }

  
  // Blood Group
  getBloodGroupCount(){
    this.subscription.push(
      this.reportingService.getBloodGroupCount(this.departmentId, this.sectionId).subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getBloodGroupReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getBloodGroupReportingResult(queryParams, this.typeId, this.unAssigned, this.departmentId, this.sectionId).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }

}
