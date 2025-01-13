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
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { NotificationService } from '../service/notification.service';
import { NotificationReadBy } from 'src/app/views/notifications/models/notification-read-by';
import { RealTimeService } from 'src/app/core/service/real-time.service';

@Component({
  selector: 'app-notification-list',
  templateUrl: './notification-list.component.html',
  styleUrl: './notification-list.component.scss'
})
export class NotificationListComponent implements OnInit, OnDestroy {

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
    this.roleFeatureService.getFeaturePermission('notificationList').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.loginEmpId = this.authService.userInformation.empId || 0;
        this.getAllNotifications(this.pagination);
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
    this.getAllNotifications(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllNotifications(event);
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  
  getAllNotifications(queryParams: any) {
    this.subscription.push(
    this.notificationService.getUserNotification(queryParams, this.loginEmpId).subscribe((item) => {
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

  
  notificationNevigate(notificationId: number, nevigateLink: string, forNotificationId: number, readStatus: boolean){
    const notificationReadBy = new NotificationReadBy();
    notificationReadBy.empId = this.loginEmpId;
    notificationReadBy.notificationId =  notificationId;
    if(readStatus == false){
      this.subscription.push(this.notificationService.updateNotificationStatus(notificationReadBy).subscribe((res) => {
        this.router.navigate([nevigateLink], {
          queryParams: { forNotificationId: forNotificationId },
          queryParamsHandling: 'merge', // Merge with existing queryParams
          relativeTo: this.router.routerState.root, // Ensure relative routing works
        });
      }))
    }
    else {
      this.router.navigate([nevigateLink], {
        queryParams: { forNotificationId: forNotificationId },
        queryParamsHandling: 'merge', // Merge with existing queryParams
        relativeTo: this.router.routerState.root, // Ensure relative routing works
      });
    }

  }

}
