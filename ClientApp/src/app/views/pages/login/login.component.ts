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

    this.snackBar.open('Email and Password not valid !', '', {
      duration: 2000,
      verticalPosition: 'bottom',
      horizontalPosition: 'right',
      panelClass: 'snackbar-danger'
    });
   
    return;
  } 
  else {
    this.subs.sink = this.authService
      .login(this.f['email'].value, this.f['password'].value)
      .subscribe(
        (res) => {
          if (res) {
            this.toastr.success('', `login successful`, {
              positionClass: 'toast-top-right',
            });
             // this.router.navigate(['/dashboard']);
              this.router.navigate(['/dashboard']);
              this.loading = false;
          //  }, 1000);
          } else {
           
          }
        },
        (error) => {
          this.error = error;
          this.submitted = false;
          this.loading = false;
        }
      );
  }
}
}


