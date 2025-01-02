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
  unreadNotification: any;
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
    public notificationService: NotificationService
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
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.empId = currentUserJSON.empId;
    this.getEmployeeByEmpId();
    this.getUserNotifications();
    this.getUserId();
    const subs = this.realTimeService.eventBus.getEvent('userNotification').subscribe(data => {
      this.getUserNotifications()
    })

    this.subscription.push(subs);
  }

  getUserNotifications(){
    this.subscription.push(
      this.notificationService.getUserNotification(this.pagination, this.empId).subscribe((res) => {
        if(res){
          this.userNoftification = res.items;
          this.unreadNotification = res.items[0].unreadCount;
          this.totalNotification = res.totalItemsCount;
        }
      })
    )
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
