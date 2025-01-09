import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpPromotionIncrementService } from '../service/emp-promotion-increment.service';
import { PromotionIncrementInfoComponent } from '../promotion-increment-info/promotion-increment-info.component';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { AuthService } from '../../../core/service/auth.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';

@Component({
  selector: 'app-manage-promotion',
  templateUrl: './manage-promotion.component.html',
  styleUrl: './manage-promotion.component.scss'
})
export class ManagePromotionComponent implements OnInit, OnDestroy {

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
  pagination: PaginatorModel = new PaginatorModel();
  noticeForEntryId: number = 0;
  featurePermission : FeaturePermission = new FeaturePermission;
  loginEmpId: number = 0;
  
  constructor(
    private toastr: ToastrService,
    public empPromotionIncrementService: EmpPromotionIncrementService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private confirmService: ConfirmService,
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
    this.roleFeatureService.getFeaturePermission('manage-incrementAndPromotion').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
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

  getAllPromotionIncrementInfo(queryParams: any) {
    // this.subscription = 
    this.subscription.push(
    this.empPromotionIncrementService.getAll(queryParams, this.noticeForEntryId).subscribe((item) => {
      this.dataSource.data = item.items;
      this.pagination.length = item.totalItemsCount;
    })
    )
    
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.searchText = filterValue;
    this.getAllPromotionIncrementInfo(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllPromotionIncrementInfo(event);
  }
  
  cancle(){
    this.router.navigate(['/promotion/manage-incrementAndPromotion']);
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  promotionIncrementInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(PromotionIncrementInfoComponent, { initialState, backdrop: 'static' });
  }

  
  updateInformation(id: number, applicationById: number){
    if(this.featurePermission.update == true || (this.featurePermission.update == false && applicationById == this.loginEmpId)){
      this.router.navigate([`/promotion/update-incrementAndPromotion/${id}`]);
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  delete(element: any, applicationById: number) {
    if(this.featurePermission.delete == true || (this.featurePermission.delete == false && applicationById == this.loginEmpId)){
      this.subscription.push(
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empPromotionIncrementService.deleteEmpPromotionIncrement(element.id).subscribe(
              (res) => {
                const index = this.dataSource.data.indexOf(element);
                if (index !== -1) {
                  this.dataSource.data.splice(index, 1);
                  this.dataSource = new MatTableDataSource(this.dataSource.data);
                }
                this.toastr.success('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });
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
