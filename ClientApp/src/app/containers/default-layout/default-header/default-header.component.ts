import { Component, Input } from '@angular/core';
import { FormControl, FormGroup, UntypedFormBuilder } from '@angular/forms';
import { Router } from '@angular/router';

import { ClassToggleService, HeaderComponent } from '@coreui/angular';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { EmpPhotoSignService } from 'src/app/views/employee/service/emp-photo-sign.service';
import { cilAccountLogout, cilPlus } from '@coreui/icons';
import { BasicInfoModule } from 'src/app/views/employee/model/basic-info.module';
import { EmpBasicInfoService } from 'src/app/views/employee/service/emp-basic-info.service';
import { EmpJobDetailsService } from 'src/app/views/employee/service/emp-job-details.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UpdateUserComponent } from 'src/app/views/usermanagement/update-user/update-user.component';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { ChangeProfileComponent } from 'src/app/views/profile/change-profile/change-profile.component';
import { EmployeeInformationComponent } from 'src/app/views/employee/manage-employee/employee-information/employee-information.component';
import { RealTimeService } from 'src/app/core/service/real-time.service';
import { NotificationService } from '../../../views/notifications/service/notification.service';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { UserNotification } from 'src/app/views/notifications/models/user-notification';
import { NotificationReadBy } from 'src/app/views/notifications/models/notification-read-by';
import { ToastrService } from 'ngx-toastr';
import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
  styleUrls: ['./default-header.component.scss'],
})
export class DefaultHeaderComponent extends HeaderComponent {

  @Input() sidebarId: string = "sidebar";

  public newMessages = new Array(4)
  public newTasks = new Array(5)
  public newNotifications = new Array(5)
  empId: number = 0;
  userId: any;
  photoPreviewUrl: string | ArrayBuffer | null = null;
  subscription: Subscription[]=[];
  empBasicInfo: BasicInfoModule = new BasicInfoModule;
  designationName : string = "";
  userNoftification : UserNotification[] = [];
  unreadNotification: number = 0;
  totalNotification: any;

  constructor(
    private classToggler: ClassToggleService,
    private authService: AuthService,
    private router: Router,
    public empPhotoSignService: EmpPhotoSignService,
    public empBasicInfoService: EmpBasicInfoService,
    public empJobDetailsService: EmpJobDetailsService,
    private formBuilder: UntypedFormBuilder,
    private modalService: BsModalService,
    private userService: UserService,
    private realTimeService: RealTimeService,
    public notificationService: NotificationService,
    private toastr: ToastrService,
  ) {
    super();
  }

  icons = {  cilAccountLogout };
  pagination: PaginatorModel = new PaginatorModel();

  logout() {
    this.authService.logout().subscribe((res) => {
      if (!res.success) {
        this.router.navigate(['/login']);
      }
    });
  }

  ngOnInit(): void {
    const currentEncryptedUser =  localStorage.getItem('encryptedUser');
    if(currentEncryptedUser){
      const bytes = CryptoJS.AES.decrypt(currentEncryptedUser, 'secret-key');
      const roleName = JSON.parse(bytes.toString(CryptoJS.enc.Utf8)).role;
      const empId = JSON.parse(bytes.toString(CryptoJS.enc.Utf8)).empId || 0;
      const userId = JSON.parse(bytes.toString(CryptoJS.enc.Utf8)).id;
      const userName = JSON.parse(bytes.toString(CryptoJS.enc.Utf8)).username;
      this.empId = empId;
      
      this.authService.userInformation = {roleName: roleName, empId: empId, userId: userId, userName: userName};
    }
    this.getEmployeeByEmpId();
    this.getUserNotifications(false);
    this.getUserId();
    const subs = this.realTimeService.eventBus.getEvent('userNotification').subscribe(data => {
      this.getUserNotifications(true);
    })


    this.subscription.push(subs);
  }

  getUserNotifications(isNew : boolean){
    this.subscription.push(
      this.notificationService.getUserNotification(this.pagination, this.empId).subscribe((res) => {
        if(res){
          if(isNew && this.totalNotification < res.totalItemsCount){
            this.toastr.info('', `New Notification`, {
              positionClass: 'toast-bottom-right',
            });
          }
          this.userNoftification = res.items;
          this.unreadNotification = res.items[0].unreadCount;
          this.totalNotification = res.totalItemsCount;
        }
      })
    )
  }

  notificationNevigate(notificationId: number, nevigateLink: string, forNotificationId: number, isNotice: boolean){
    const notificationReadBy = new NotificationReadBy();
    notificationReadBy.empId = this.empId;
    notificationReadBy.notificationId =  notificationId;
    if(this.empId != 0){
      if(isNotice != true){
        this.subscription.push(this.notificationService.updateNotificationStatus(notificationReadBy).subscribe((res) => {
            this.router.navigate([nevigateLink], {
              queryParams: { forNotificationId: forNotificationId },
              queryParamsHandling: 'merge', // Merge with existing queryParams
              relativeTo: this.router.routerState.root, // Ensure relative routing works
            });
        }))
      }
      else {
        this.router.navigate(['/notifications/notificationList']);
      }
    }

  }
  

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  getUserId(){
    if(this.empId){
      this.subscription.push(this.userService.getInfoByEmpId(this.empId).subscribe((res) => {
        this.userId = res.id;
      }))
    }
  }
  
  getEmployeeByEmpId() {
    if(this.empId){
      this.subscription.push(this.empBasicInfoService.findByEmpId(this.empId).subscribe((res) => {
        this.empBasicInfo = res;
        this.patchEmpPhoto(res.empGenderName)
      }));
      this.subscription.push(this.empJobDetailsService.findByEmpId(this.empId).subscribe((res) => {
        if(res){
          this.designationName = res.designationName;
        }
      }));
    }
    else {
      this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
    }
  }

  patchEmpPhoto(empGender: string){
    this.subscription.push(this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
      if(res){
        this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}`
      }
      else {
        if(empGender){
          const gender = empGender.charAt(0).toLowerCase();
          if(gender == 'm'){
            this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default_Male.jpg`
          }
          else {
            this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default_Female.jpg`
          }
        }
        else{
          this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
        }
      }
    }))
  }

  
  updateUserInformation(id: string, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(UpdateUserComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      modalRef.onHide.subscribe(() => {
      });
    }
  }
  updatePhotoSign(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(ChangeProfileComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      modalRef.onHide.subscribe(() => {
        this.getEmployeeByEmpId();
      });
    }
  }
    
  viewUserProfile(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(EmployeeInformationComponent, { initialState, backdrop: 'static' });
  }
}
