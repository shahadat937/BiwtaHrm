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
  subscription: Subscription = new Subscription();
  empBasicInfo: BasicInfoModule = new BasicInfoModule;
  designationName : string = "";

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
  ) {
    super();
  }

  icons = {  cilAccountLogout };

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
    this.getUserId();
  }

  getUserId(){
    if(this.empId){
      this.subscription = this.userService.getInfoByEmpId(this.empId).subscribe((res) => {
        this.userId = res.id;
      })
    }
  }
  
  getEmployeeByEmpId() {
    if(this.empId){
      this.subscription = this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
        if(res){
          this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}`
        }
        else {
          this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
        }
      })
      this.subscription = this.empBasicInfoService.findByEmpId(this.empId).subscribe((res) => {
        this.empBasicInfo = res;
      });
      this.subscription = this.empJobDetailsService.findByEmpId(this.empId).subscribe((res) => {
        this.designationName = res.designationName;
      });
    }
    else {
      this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
    }
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
