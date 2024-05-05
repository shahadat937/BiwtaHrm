import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends UnsubscribeOnDestroyAdapter
implements OnInit
{
authForm!: FormGroup;
submitted = false;
loading = false;
error = '';
hide = true;

lastPublishDate:any;

schoolId:any;
instructorId:any;
traineeId:any;

constructor(
  private formBuilder: FormBuilder,
  private route: ActivatedRoute,
  private router: Router,
  private authService: AuthService,
  private snackBar: MatSnackBar,
  private toastr: ToastrService
) {
  super();
}

ngOnInit() {
  this.lastPublishDate = '01/15/2023';
  this.authForm = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });
  this.schoolId=20;
}
get f() {
  return this.authForm.controls;
}

onSubmit() {
  this.submitted = true;
  this.loading = true;
  this.error = '';
  console.log(this.authForm.value)
  if (this.authForm.invalid) {

    this.toastr.warning('', `login Faild`, {
      positionClass: 'toast-top-right',
    });
   
    return;
  } 
  else {
    this.subs.sink = this.authService
      .login(this.f['email'].value, this.f['password'].value)
      .subscribe(
        (res) => {
          if (res) {
            const role = this.authService.currentUserValue.role
           // const role = "Master Admin"
          console.log(role === Role.MasterAdmin)
             // this.router.navigate(['/dashboard']);
             if ( role === Role.MasterAdmin) {
              alert("login")
           //   this.router.navigate(['/admin/dashboard/main']);
           this.router.navigate(['/dashboard']);
            }
              //this.router.navigate(['/dashboard']);
              this.loading = false;
              this.toastr.success('', `login successful`, {
                positionClass: 'toast-top-right',
              });
          //  }, 1000);
          } else {
           
          }
        },
        (error) => {
          this.toastr.warning('', `login Faild`, {
            positionClass: 'toast-top-right',
          });
          this.submitted = false;
          this.loading = false;
        }
      );
  }
}
}


