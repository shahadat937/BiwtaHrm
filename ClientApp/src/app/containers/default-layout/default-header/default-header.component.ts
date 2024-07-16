import { Component, Input } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';

import { ClassToggleService, HeaderComponent } from '@coreui/angular';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { EmpPhotoSignService } from 'src/app/views/employee/service/emp-photo-sign.service';
import { cilAccountLogout, cilPlus } from '@coreui/icons';
@Component({
  selector: 'app-default-header',
  templateUrl: './default-header.component.html',
})
export class DefaultHeaderComponent extends HeaderComponent {

  @Input() sidebarId: string = "sidebar";

  public newMessages = new Array(4)
  public newTasks = new Array(5)
  public newNotifications = new Array(5)
  empId: any;
  photoPreviewUrl: string | ArrayBuffer | null = null;
  subscription: Subscription = new Subscription();

  constructor(
    private classToggler: ClassToggleService,
    private authService: AuthService,
    private router: Router,
    public empPhotoSignService: EmpPhotoSignService,
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
  }

  
  getEmployeeByEmpId() {
    if(this.empId){
      this.subscription = this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
          this.photoPreviewUrl = res.photoUrl ? `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}` : null;
      })
    }
    else {
      this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
    }
  }
  
}
