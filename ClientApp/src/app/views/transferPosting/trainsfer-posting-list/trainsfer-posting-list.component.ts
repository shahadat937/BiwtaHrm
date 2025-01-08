import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { EmpTransferPostingService } from '../service/emp-transfer-posting.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { MatSort } from '@angular/material/sort';
import { TransferPostingInfoComponent } from '../transfer-posting-info/transfer-posting-info.component';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { AuthService } from '../../../core/service/auth.service';

@Component({
  selector: 'app-trainsfer-posting-list',
  templateUrl: './trainsfer-posting-list.component.html',
  styleUrl: './trainsfer-posting-list.component.scss'
})
export class TrainsferPostingListComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    // 'slNo',
    'PMS Id',
    'fullName',
    'transferFrom',
    'transferTo',
    // 'ApprovalStatus',
    'DeptStatus',
    'JoiningStatus',
    // 'ApplicationStatus',
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
    public empTransferPostingService: EmpTransferPostingService,
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
    this.roleFeatureService.getFeaturePermission('transferPostingList').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
        this.route.queryParams.subscribe((params) => {
          this.noticeForEntryId = params['forNotificationId'] || 0;
          this.getAllTransferPostingInfo(this.pagination);
        });
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }

  getAllTransferPostingInfo(queryParams: any) {
    this.subscription.push(
    this.empTransferPostingService.getAll(queryParams, this.noticeForEntryId).subscribe((item) => {
      this.dataSource.data = item.items;
      this.pagination.length = item.totalItemsCount;
    })
    )
    
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.searchText = filterValue;
    this.getAllTransferPostingInfo(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllTransferPostingInfo(event);
  }
  
  cancle(){
    this.router.navigate(['/transferPosting/transferPostingList']);
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  addTrasnderPosting(){
    if(this.featurePermission.add == true){
      this.router.navigate(['/transferPosting/transferPostingApplication']);
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  transferPostingInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(TransferPostingInfoComponent, { initialState, backdrop: 'static' });
  }

  updateInformation(id: number, applicationById: number){
    if(this.featurePermission.update == true || (this.featurePermission.update == false && applicationById == this.loginEmpId)){
      this.router.navigate([`transferPosting/update-transferPostingApplication/${id}`]);
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
              this.empTransferPostingService.deleteEmpTransferPosting(element.id).subscribe(
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
