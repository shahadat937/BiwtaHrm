import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { ReportingService } from '../service/reporting.service';
import { EmpCountOnReportingDto } from '../models/emp-count-on-reporting-dto';

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

  constructor(
    public reportingService: ReportingService,
    ) {
  
    }

  ngOnInit(): void {

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
    this.onQueryTypeChange();
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getEmployeeTypeReportingResult(event);
  }

  onQueryTypeChange(){
    if(this.queryTypeName == 'Employee Type'){
      this.getEmployeeTypeCount();
      this.getEmployeeTypeReportingResult(this.pagination);
    }
  }

  getEmployeeTypeCount(){
    this.subscription.push(
      this.reportingService.getEmployeeTypeCount().subscribe((res: any) => {
      this.queryCount = res;
    })
    )
  }
  getEmployeeTypeReportingResult(queryParams: any){
    this.subscription.push(
      this.reportingService.getEmployeeTypeReportingResult(queryParams, this.typeId, this.unAssigned).subscribe((res: any) => {
      this.dataSource.data = res.items;
      this.pagination.length = res.totalItemsCount;
    })
    )
  }

}
