import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../core/service/auth.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { NotificationService } from '../service/notification.service';
import { AddNoticeComponent } from '../add-notice/add-notice.component';
import { NotificationReadBy } from 'src/app/views/notifications/models/notification-read-by';
import { RealTimeService } from 'src/app/core/service/real-time.service';

@Component({
  selector: 'app-notice-list',
  templateUrl: './notice-list.component.html',
  styleUrl: './notice-list.component.scss'
})
export class NoticeListComponent implements OnInit, OnDestroy {

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
    noticeForEntryId: number = 0;
    featurePermission : FeaturePermission = new FeaturePermission;
    loginEmpId: number = 0;
    
    constructor(
      private toastr: ToastrService,
      private route: ActivatedRoute,
      private modalService: BsModalService,
      private confirmService: ConfirmService,
      private router: Router,
      public roleFeatureService: RoleFeatureService,
      private authService: AuthService,
      public notificationService: NotificationService,
      private realTimeService: RealTimeService,
    ) {
  
    }


    
  ngOnInit(): void {
    this.getPermission();
    const subs = this.realTimeService.eventBus.getEvent('userNotification').subscribe(data => {
      this.getPermission();
    })
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('noticeList').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
        this.getAllNotice(this.pagination);
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }

  
  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.searchText = filterValue;
    this.getAllNotice(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllNotice(event);
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  
  getAllNotice(queryParams: any) {
    this.subscription.push(
    this.notificationService.getNoticeList(queryParams, this.loginEmpId).subscribe((item) => {
      this.dataSource.data = item.items;
      this.pagination.length = item.totalItemsCount;
    })
    )
    
  }

  formatDate(dateString: string): string {
    if(dateString == null){
      return '';
    }
    const date = new Date(dateString);
    const now = new Date();
    const seconds = Math.floor((now.getTime() - date.getTime()) / 1000);
    
    let interval = Math.floor(seconds / 31536000);
    if (interval > 1) return `${interval} years ago`;
    interval = Math.floor(seconds / 2592000);
    if (interval > 1) return `${interval} months ago`;
    interval = Math.floor(seconds / 86400);
    if (interval > 1) return `${interval} days ago`;
    interval = Math.floor(seconds / 3600);
    if (interval > 1) return `${interval} hours ago`;
    interval = Math.floor(seconds / 60);
    if (interval > 1) return `${interval} minutes ago`;
    return `${seconds} seconds ago`;
  }

  
  notificationNevigate(notificationId: number, readStatus: boolean){
    const notificationReadBy = new NotificationReadBy();
    notificationReadBy.empId = this.loginEmpId;
    notificationReadBy.notificationId =  notificationId;
    if(readStatus == false){
      this.subscription.push(this.notificationService.updateNotificationStatus(notificationReadBy).subscribe((res) => {
      }))
    }
  }

  addNoticeModal(id: number, clickedButton: string){
      if(clickedButton == "Create" && this.featurePermission.add == true || clickedButton == "Edit" && this.featurePermission.update == true){
        const initialState = {
          id: id,
          clickedButton: clickedButton
        };
        const modalRef: BsModalRef = this.modalService.show(AddNoticeComponent, { initialState, backdrop: 'static' });
    
        if (modalRef.onHide) {
          modalRef.onHide.subscribe(() => {
            this.getAllNotice(this.pagination);
          });
        }
      }
      else {
        this.roleFeatureService.unauthorizeAccress();
      }
  }

}