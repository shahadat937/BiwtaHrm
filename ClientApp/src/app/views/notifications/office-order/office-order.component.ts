import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../core/service/auth.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { OrderTypeService } from '../../basic-setup/service/order-type.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { OfficeOrderService } from '../service/office-order.service';

@Component({
  selector: 'app-office-order',
  templateUrl: './office-order.component.html',
  styleUrl: './office-order.component.scss'
})
export class OfficeOrderComponent implements OnInit, OnDestroy {

   subscription: Subscription[]=[]
    displayedColumns: string[] = [
      'slNo',
      'notification',
      'readStatus'];
    dataSource = new MatTableDataSource<any>();
    @ViewChild(MatPaginator)
    paginator!: MatPaginator;
    @ViewChild(MatSort)
    matSort!: MatSort;
    pagination: PaginatorModel = new PaginatorModel();
    featurePermission : FeaturePermission = new FeaturePermission;
    loginEmpId: number = 0;
    orderTypes : SelectedModel[] = [];
    departments : SelectedModel[] = [];

    selectedOrderType : any = null;
    selectedDeptId : any = null;
    orderNo : any;
    fromDate : any;
    toDate : any;
        
    constructor(
      private toastr: ToastrService,
      private route: ActivatedRoute,
      private modalService: BsModalService,
      private confirmService: ConfirmService,
      private router: Router,
      private authService: AuthService,
      public roleFeatureService: RoleFeatureService,
      public orderTypeService: OrderTypeService,
      public departmentService: DepartmentService,
      public officeOrderService: OfficeOrderService,
    ) {
  
    }

      
  ngOnInit(): void {
    this.getPermission();
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('notificationList').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
        this.getSelectedOrderType();
        this.getSelectedDepartment();
        this.getAllOfficeOrderList(this.pagination);
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  getSelectedOrderType(){
    this.subscription.push(
      this.orderTypeService.getSelectedOrderType().subscribe((res) => {
        this.orderTypes = res;
      })
    )
  }

  getSelectedDepartment(){
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
        this.departments = res;
      })
    )
  }

  getAllOfficeOrderList(queryParams: any){
    this.subscription.push(
      this.officeOrderService.getAll(queryParams, this.selectedOrderType, this.selectedDeptId, 0, 0, this.orderNo, this.fromDate, this.toDate).subscribe((employees: any) => {
      this.dataSource.data = employees.items;
      this.pagination.length = employees.totalItemsCount;
    })
    )
  }

}
