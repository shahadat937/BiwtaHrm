import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpTransferPostingService } from '../../service/emp-transfer-posting.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { TransferPostingInfoComponent } from '../../transfer-posting-info/transfer-posting-info.component';
import { JoiningReportingComponent } from '../joining-reporting/joining-reporting.component';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { AuthService } from '../../../../core/service/auth.service';
import { FeaturePermission } from '../../../featureManagement/model/feature-permission';
import { RoleFeatureService } from '../../../featureManagement/service/role-feature.service';

@Component({
  selector: 'app-joining-reporting-list',
  templateUrl: './joining-reporting-list.component.html',
  styleUrl: './joining-reporting-list.component.scss'
})
export class JoiningReportingListComponent  implements OnInit, OnDestroy {

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
  currentDepartmentId: number = 0;
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

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    this.getPermission();
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('joiningReportingList').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
        this.currentDepartmentId = this.authService.userInformation.departmentId || 0;
        this.route.queryParams.subscribe((params) => {
          this.noticeForEntryId = params['forNotificationId'] || 0;
          this.getAllEmpTransferPostingJoiningInfo(this.pagination);
        });
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }

  getAllEmpTransferPostingJoiningInfo(queryParams: any) {
    this.subscription.push(
    this.empTransferPostingService.getAllEmpTransferPostingJoiningInfo(queryParams, this.currentDepartmentId, this.noticeForEntryId).subscribe((item) => {
      this.dataSource.data = item.items;
      this.pagination.length = item.totalItemsCount;
    })
    )
    
  }

  cancle(){
    this.router.navigate(['/transferPosting/joiningReportingList']);
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.searchText = filterValue;
    this.getAllEmpTransferPostingJoiningInfo(this.pagination);
  }
  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllEmpTransferPostingJoiningInfo(event);
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  
  transferPostingInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(TransferPostingInfoComponent, { initialState, backdrop: 'static' });
  }

  
  transferPostingJoiningReporting(id: number, clickedButton: string){
    if(this.featurePermission.update == true){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(JoiningReportingComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        modalRef.onHide.subscribe(() => {
          this.getAllEmpTransferPostingJoiningInfo(this.pagination);
        });
      }
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

}
