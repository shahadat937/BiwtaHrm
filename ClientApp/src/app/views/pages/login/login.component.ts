import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Role } from 'src/app/core/models/role';
import { AuthService } from 'src/app/core/service/auth.service';
import { UnsubscribeOnDestroyAdapter } from 'src/app/shared/UnsubscribeOnDestroyAdapter';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import {
  MoveDirection,
  ClickEvent,
  HoverEvent,
  OutMode,
  Container,
} from "@tsparticles/engine";

import { loadSlim } from "@tsparticles/slim"; // if you are going to use `loadSlim`, install the "@tsparticles/slim" package too.
import { NgParticlesService } from "@tsparticles/angular";
import { cilEyedropper, cilLowVision } from '@coreui/icons';
import { SiteSettingService } from '../../featureManagement/service/site-setting.service';

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
  siteLogo: string = '';
  siteName: string = '';

  lastPublishDate: any;
  schoolId: any;
  instructorId: any;
  traineeId: any;

  captchaValue: number = 0;
  captchaImage: string = '';

  id = "tsparticles";

  /* Starting from 1.19.0 you can use a remote url (AJAX request) to a JSON with the configuration */


  /* or the classic JavaScript object */
  particlesOptions = {
    background: {
      color: {
        value: "#5f9fc0",
      },
    },
    fpsLimit: 120,
    interactivity: {
      events: {
        onClick: {
          enable: true,
          mode: "push",
        }
    },
      modes: {
        push: {
          quantity: 4,
        },
        repulse: {
          distance: 200,
          duration: 0.4,
        },
      },
    },
    particles: {
      color: {
        value: "#FFF",
      },
      links: {
        color: "#FFF",
        distance: 150,
        enable: true,
        opacity: 0.5,
        width: 1,
      },
      move: {
        direction: MoveDirection.none,
        enable: true,
        outModes: {
          default: OutMode.bounce,
        },
        random: false,
        speed: 4,
        straight: false,
      },
      number: {
        density: {
          enable: true,
          area: 800,
        },
        value: 120,
      },
      opacity: {
        value: 0.6,
      },
      shape: {
        type: "circle",
      },
      size: {
        value: { min: 1, max: 5 },
      },
    },
    detectRetina: true,
  };

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private toastr: ToastrService,
    public empPhotoSignService: EmpPhotoSignService,
    private readonly ngParticlesService: NgParticlesService,
    public siteSettingService: SiteSettingService,
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
    this.getSiteSetting();

    this.ngParticlesService.init(async (engine) => {
      await loadSlim(engine);
    });

  }

  particlesLoaded(container: Container): void {
  }

  getSiteSetting(){
    this.siteSettingService.getActive().subscribe((item) => {
      this.siteLogo = this.empPhotoSignService.imageUrl + 'TempleteImage/' + item.siteLogo;
      this.siteName = item.siteName;
    });
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



