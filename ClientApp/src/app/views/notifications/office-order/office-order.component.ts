import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { OfficeOrderModalComponent } from '../office-order-modal/office-order-modal.component';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-office-order',
  templateUrl: './office-order.component.html',
  styleUrl: './office-order.component.scss'
})
export class OfficeOrderComponent implements OnInit, OnDestroy {

   subscription: Subscription[]=[]
    displayedColumns: string[] = [
      'slNo',
      'designation',
      'orderDept',
      'orderDate',
      'action'];
    dataSource = new MatTableDataSource<any>();
    @ViewChild(MatPaginator)
    paginator!: MatPaginator;
    @ViewChild(MatSort)
    matSort!: MatSort;
    pagination: PaginatorModel = new PaginatorModel();
    featurePermission : FeaturePermission = new FeaturePermission;
    loginEmpId: number = 0;
    orderTypes : any[] = [];
    totalOfficeOrder: number = 0;
    departments : SelectedModel[] = [];

    selectedOrderType : number = 0;
    selectedDeptId : number = 0;
    orderNo : string = "";
    fromDate : any = "";
    toDate : any = "";

    filePath = environment.imageUrl;
        
    constructor(
      private router: Router,
      private toastr: ToastrService,
      private route: ActivatedRoute,
      private modalService: BsModalService,
      private confirmService: ConfirmService,
      private authService: AuthService,
      public orderTypeService: OrderTypeService,
      public officeOrderService: OfficeOrderService,
      public departmentService: DepartmentService,
      public roleFeatureService: RoleFeatureService,
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
    this.getAllOfficeOrderList(this.pagination);
  }


  onTabChange(index: number): void {
    // index 0 is the "All" tab
    if (index === 0) {
      this.selectedOrderType = 0;
    } else {
      const selectedOrderType = this.orderTypes[index - 1]; // offset by 1 due to "All" tab
      this.selectedOrderType = selectedOrderType?.id ?? 0;
    }

    this.getAllOfficeOrderList(this.pagination);
  }

  OnFilter(){
    this.getAllOfficeOrderList(this.pagination);
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
        this.totalOfficeOrder = res[0].totalCount;
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

  openFile(url: string): void {
    window.open(this.filePath + 'OfficeOrder/' + url, '_blank');
  }

  openOfficeOrderModal(id: number, clickedButton: string){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(OfficeOrderModalComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        modalRef.onHide.subscribe(() => {
          this.getAllOfficeOrderList(this.pagination)
          this.getSelectedOrderType();
        });
      }
    }

    isNewOrder(orderDate: string | Date | null | undefined): boolean {
      if (!orderDate) return false;

      const today = new Date();
      const order = new Date(orderDate);
      
      const diffTime = Math.abs(today.getTime() - order.getTime());
      const diffDays = diffTime / (1000 * 3600 * 24);

      return diffDays <= 7 && order <= today;
    }

    delete(element: any) {
    if(this.featurePermission.delete == true){
      this.subscription.push(
        this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.officeOrderService.delete(element.id).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              this.toastr.warning('Delete Successfull', ` `, {
                positionClass: 'toast-top-right',
              });
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      })
      )
      
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

}
