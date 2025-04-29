import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell, cilViewModule } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpTransferPostingService } from '../../service/emp-transfer-posting.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { TransferPostingInfoComponent } from '../../transfer-posting-info/transfer-posting-info.component';
import { DepartmentApprovalComponent } from '../department-approval/department-approval.component';
import { PaginatorModel } from '../../../../../../src/app/core/models/paginator-model';
import { FeaturePermission } from '../../../featureManagement/model/feature-permission';
import { RoleFeatureService } from '../../../featureManagement/service/role-feature.service';
import { AuthService } from '../../../../core/service/auth.service';

@Component({
  selector: 'app-department-approval-list',
  templateUrl: './department-approval-list.component.html',
  styleUrl: './department-approval-list.component.scss'
})
export class DepartmentApprovalListComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    // 'slNo',
    'PMS Id',
    'fullName',
    'transferFrom',
    'transferTo',
    'ApprovalStatus',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  loginEmpId: number = 0;
  currentDepartmentId : any;
  noticeForEntryId: number = 0;
  pagination: PaginatorModel = new PaginatorModel();
  featurePermission : FeaturePermission = new FeaturePermission;
  
  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private router: Router,
    public roleFeatureService: RoleFeatureService,
    private authService: AuthService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell, cilViewModule };


  ngOnInit(): void {
    this.getPermission();
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('departmentApprovalList').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
        this.currentDepartmentId = this.authService.userInformation.departmentId || 0
        console.log(this.currentDepartmentId);
        this.route.queryParams.subscribe((params) => {
          this.noticeForEntryId = params['forNotificationId'] || 0;
          this.getAllEmpTransferPostingDeptApproveInfo(this.pagination);
        });
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }


  cancle(){
    this.router.navigate(['/transferPosting/departmentApprovalList']);
  }

  getAllEmpTransferPostingDeptApproveInfo(queryParams: any) {
    this.subscription.push(
    this.empTransferPostingService.getAllEmpTransferPostingDeptApproveInfo(queryParams, this.loginEmpId, this.noticeForEntryId).subscribe((item) => {
      this.dataSource.data = item.items;
      // this.dataSource.paginator = this.paginator;
      this.pagination.length = item.totalItemsCount;
    })
    )
   
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.searchText = filterValue;
    this.getAllEmpTransferPostingDeptApproveInfo(this.pagination);
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  
  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllEmpTransferPostingDeptApproveInfo(event);
  }

  
  transferPostingInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(TransferPostingInfoComponent, { initialState, backdrop: 'static' });
  }

  
  transferPostingDeptApproval(id: number, clickedButton: string){
    if(this.featurePermission.update == true){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(DepartmentApprovalComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        this.subscription.push(
          modalRef.onHide.subscribe(() => {
            this.getAllEmpTransferPostingDeptApproveInfo(this.pagination);
          })
        )
       
      }
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

}
