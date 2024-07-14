import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends UnsubscribeOnDestroyAdapter implements OnInit {
  btnText: string | undefined;
  authForm!: FormGroup;
  submitted = false;
  loading = false;
  error = '';
  hide = true;

  biwtaLogo: string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;

  lastPublishDate: any;
  schoolId: any;
  instructorId: any;
  traineeId: any;

  captchaValue: number = 0;
  captchaImage: string = '';
  isCaptchaVerified: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private toastr: ToastrService,
    public empPhotoSignService: EmpPhotoSignService,
  ) {
    super();
  }

  ngOnInit() {
    this.lastPublishDate = '01/15/2023';
    this.authForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      captcha: ['', Validators.required],
    });
    this.schoolId = 20;
    this.generateCaptcha();
  }

  get f() {
    return this.authForm.controls;
  }

  generateCaptcha() {
    const num1 = Math.floor(Math.random() * 10) + 1;
    const num2 = Math.floor(Math.random() * 10) + 1;
    this.captchaValue = num1 + num2;

    // You can generate an image of the CAPTCHA here or just show it as text
    this.captchaImage = `data:image/svg+xml;base64,${btoa(`
      <svg xmlns="http://www.w3.org/2000/svg" width="100" height="50">
        <text x="10" y="30" font-size="20">${num1} + ${num2} = ?</text>
      </svg>`)}`
  }

  verifyCaptcha() {
    if (this.f['captcha'].value == this.captchaValue) {
      this.isCaptchaVerified = true;
      this.toastr.success('', `Verified successfully`, {
        positionClass: 'toast-top-right',
      });
    } else {
      this.toastr.warning('', `Invalid Answer`, {
        positionClass: 'toast-top-right',
      });
      this.isCaptchaVerified = false;
      this.generateCaptcha(); // regenerate captcha on failure
    }
  }

  onSubmit() {
    this.submitted = true;
    this.loading = true;
    this.error = '';

    if (this.authForm.invalid || !this.isCaptchaVerified) {
      return;
    }

    this.subs.sink = this.authService
      .login(this.f['email'].value, this.f['password'].value)
      .subscribe(
        (res) => {
          if (res) {
            const role = this.authService.currentUserValue.role;
            if (role === Role.MasterAdmin) {
              this.router.navigate(['/dashboard']);
            }
            this.loading = false;
            this.toastr.success('', `login successful`, {
              positionClass: 'toast-top-right',
            });
          } else {
            this.submitted = false;
            this.loading = false;
          }
        },
        (error) => {
          this.submitted = false;
          this.loading = false;
        }
      );
  }
}







//       *************** Simple Chaptcha ********************


// captchaValue: number = 0;
// captchaImage: string = '';
// isCaptchaVerified: boolean = false;

// ngOnInit() {
//   this.lastPublishDate = '01/15/2023';
//   this.authForm = this.formBuilder.group({
//     email: ['', Validators.required],
//     password: ['', Validators.required],
//     captcha: ['', Validators.required],
//   });
//   this.schoolId = 20;
//   this.generateCaptcha();
// }
// generateCaptcha() {
//   const num1 = Math.floor(Math.random() * 10) + 1;
//   const num2 = Math.floor(Math.random() * 10) + 1;
//   this.captchaValue = num1 + num2;

//   // You can generate an image of the CAPTCHA here or just show it as text
//   this.captchaImage = `data:image/svg+xml;base64,${btoa(`
//     <svg xmlns="http://www.w3.org/2000/svg" width="100" height="50">
//       <text x="10" y="30" font-size="20">${num1} + ${num2} = ?</text>
//     </svg>`)}`
// }

// verifyCaptcha() {
//   if (this.f['captcha'].value == this.captchaValue) {
//     this.isCaptchaVerified = true;
//     this.toastr.success('', `Verified successfully`, {
//       positionClass: 'toast-top-right',
//     });
//   } else {
//     this.toastr.warning('', `Invalid Answer`, {
//       positionClass: 'toast-top-right',
//     });
//     this.isCaptchaVerified = false;
//     this.generateCaptcha(); // regenerate captcha on failure
//   }
// }

// onSubmit() {
//   this.submitted = true;
//   this.loading = true;
//   this.error = '';

//   if (this.authForm.invalid || !this.isCaptchaVerified) {
//     return;
//   }

//   this.subs.sink = this.authService
//     .login(this.f['email'].value, this.f['password'].value)
//     .subscribe(
//       (res) => {
//         if (res) {
//           const role = this.authService.currentUserValue.role;
//           if (role === Role.MasterAdmin) {
//             this.router.navigate(['/dashboard']);
//           }
//           this.loading = false;
//           this.toastr.success('', `login successful`, {
//             positionClass: 'toast-top-right',
//           });
//         } else {
//           this.submitted = false;
//           this.loading = false;
//         }
//       },
//       (error) => {
//         this.submitted = false;
//         this.loading = false;
//       }
//     );
// }


