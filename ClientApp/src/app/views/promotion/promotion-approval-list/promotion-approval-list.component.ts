import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { PromotionIncrementInfoComponent } from '../promotion-increment-info/promotion-increment-info.component';
import { EmpPromotionIncrementService } from '../service/emp-promotion-increment.service';
import { IncrementAndPromotionApprovalComponent } from '../increment-and-promotion-approval/increment-and-promotion-approval.component';
import { AuthService } from '../../../core/service/auth.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';

@Component({
  selector: 'app-promotion-approval-list',
  templateUrl: './promotion-approval-list.component.html',
  styleUrl: './promotion-approval-list.component.scss'
})
export class PromotionApprovalListComponent  implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    // 'slNo',
    'PMS Id',
    'fullName',
    'promotedFrom',
    'promotedTo',
    'basicPayFrom',
    'basicPayTo',
    'ApprovalStatus',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  loginEmpId: number = 0;
  loginEmpCurrentDepartmentId = 0;
  noticeForEntryId: number = 0;
  pagination: PaginatorModel = new PaginatorModel();
  featurePermission : FeaturePermission = new FeaturePermission;
  
  constructor(
    private toastr: ToastrService,
    public empPromotionIncrementService: EmpPromotionIncrementService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private router: Router,
    public roleFeatureService: RoleFeatureService,
    private authService: AuthService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    this.getPermission();
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('incrementAndPromotionApproval').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
        this.loginEmpCurrentDepartmentId = this.authService.userInformation.departmentId || 0;
        this.route.queryParams.subscribe((params) => {
          this.noticeForEntryId = params['forNotificationId'] || 0;
          this.getAllPromotionIncrementInfo(this.pagination);
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
    this.router.navigate(['/promotion/incrementAndPromotionApproval']);
  }

  getAllPromotionIncrementInfo(queryParams: any) {
    // this.subscription = 
    this.subscription.push(
    this.empPromotionIncrementService.getAllEmpPromotionIncrementApproveInfo(queryParams, this.loginEmpCurrentDepartmentId, this.noticeForEntryId).subscribe((item) => {
      this.dataSource.data = item.items;
      // this.dataSource.paginator = this.paginator;
      this.pagination.length = item.totalItemsCount;
    })
    )
  }
  
  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.searchText = filterValue;
    this.getAllPromotionIncrementInfo(this.pagination);
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  
  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllPromotionIncrementInfo(event);
  }

  promotionIncrementInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(PromotionIncrementInfoComponent, { initialState, backdrop: 'static' });
  }
  
  promotionIncrementApproval(id: number, clickedButton: string){
    if(this.featurePermission.update == true){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(IncrementAndPromotionApprovalComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        this.subscription.push(
        modalRef.onHide.subscribe(() => {
          this.getAllPromotionIncrementInfo(this.pagination);
        })
        )
      }
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }
}
