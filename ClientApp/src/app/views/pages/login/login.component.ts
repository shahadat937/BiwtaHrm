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
  backgroundImage: string = `${this.empPhotoSignService.imageUrl}TempleteImage/login_bg6.mp4`;

  lastPublishDate: any;
  schoolId: any;
  instructorId: any;
  traineeId: any;

  captchaValue: number = 0;
  captchaImage: string = '';

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
      remember: [false]
    });
    this.schoolId = 20;
    this.generateCaptcha();
    this.pathRememberValue();
  }

  pathRememberValue(){
    const savedEmail = localStorage.getItem('rememberedEmail');
    const savedPassword = localStorage.getItem('rememberedPassword');
    if (savedEmail) {
      this.authForm.patchValue({ email: savedEmail });
      this.authForm.patchValue({ password: savedPassword });
      this.authForm.patchValue({ remember: true });
    }
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

  onSubmit() {
    if(this.f['captcha'].value == this.captchaValue){
      this.submitted = true;
      this.loading = true;
      this.error = '';
  
      if (this.authForm.invalid) {
        return;
      }

      if (this.authForm.get('remember')?.value) {
        localStorage.setItem('rememberedEmail', this.f['email'].value);
        localStorage.setItem('rememberedPassword', this.f['password'].value);
      } else {
        localStorage.removeItem('rememberedEmail');
        localStorage.removeItem('rememberedPassword');
      }
  
      this.subs.sink = this.authService
        .login(this.f['email'].value, this.f['password'].value)
        .subscribe(
          (res) => {
            if (res) {
              const role = this.authService.currentUserValue.role;
              this.router.navigate(['/dashboard']);
              this.loading = false;
              this.toastr.success('', `Login successful`, {
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
    else {
      this.toastr.warning('', `Invalid Answer`, {
        positionClass: 'toast-top-right',
      });
    }
  }
}



