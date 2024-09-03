import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterTestingModule } from '@angular/router/testing';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { NgParticlesService } from "@tsparticles/angular";
import { LoginComponent } from './login.component';
import { Router } from '@angular/router';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: jasmine.SpyObj<AuthService>;
  let toastrService: jasmine.SpyObj<ToastrService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['login']);
    const toastrServiceSpy = jasmine.createSpyObj('ToastrService', ['success', 'warning']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    const empPhotoSignServiceStub = { imageUrl: 'https://example.com/' };

    await TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        MatSnackBarModule,
        ToastrModule.forRoot(),
      ],
      declarations: [LoginComponent],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: ToastrService, useValue: toastrServiceSpy },
        { provide: EmpPhotoSignService, useValue: empPhotoSignServiceStub },
        { provide: NgParticlesService, useValue: jasmine.createSpyObj('NgParticlesService', ['init']) },
        { provide: Router, useValue: routerSpy },
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    toastrService = TestBed.inject(ToastrService) as jasmine.SpyObj<ToastrService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize form on ngOnInit', () => {
    expect(component.authForm).toBeDefined();
    expect(component.authForm.controls['email'].value).toBe('');
    expect(component.authForm.controls['password'].value).toBe('');
    expect(component.authForm.controls['captcha'].value).toBe('');
    expect(component.authForm.controls['remember'].value).toBe(false);
  });

  it('should generate a captcha on ngOnInit', () => {
    const initialCaptchaValue = component.captchaValue;
    component.generateCaptcha();
    expect(component.captchaValue).not.toEqual(initialCaptchaValue);
  });

  it('should show warning if captcha is incorrect', () => {
    component.authForm.controls['captcha'].setValue(999); // Incorrect captcha
    component.onSubmit();
    expect(toastrService.warning).toHaveBeenCalledWith('', 'Invalid Answer', { positionClass: 'toast-top-right' });
  });

  it('should login and navigate to dashboard on valid form submission', () => {
    component.captchaValue = 5; // Set expected captcha value
    component.authForm.controls['email'].setValue('test@example.com');
    component.authForm.controls['password'].setValue('password');
    component.authForm.controls['captcha'].setValue(5); // Correct captcha
    authService.login.and.returnValue(of(true));

    component.onSubmit();

    expect(authService.login).toHaveBeenCalledWith('test@example.com', 'password');
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    expect(toastrService.success).toHaveBeenCalledWith('', 'Login successful', { positionClass: 'toast-top-right' });
  });

  it('should not submit if form is invalid', () => {
    component.captchaValue = 5;
    component.authForm.controls['email'].setValue(''); // Invalid email

    component.onSubmit();

    expect(authService.login).not.toHaveBeenCalled();
    expect(router.navigate).not.toHaveBeenCalled();
  });

  it('should store credentials if "remember" is checked', () => {
    spyOn(localStorage, 'setItem');
    component.captchaValue = 5;
    component.authForm.controls['email'].setValue('test@example.com');
    component.authForm.controls['password'].setValue('password');
    component.authForm.controls['captcha'].setValue(5);
    component.authForm.controls['remember'].setValue(true);

    authService.login.and.returnValue(of(true));
    component.onSubmit();

    expect(localStorage.setItem).toHaveBeenCalledWith('rememberedEmail', 'test@example.com');
    expect(localStorage.setItem).toHaveBeenCalledWith('rememberedPassword', 'password');
  });

  it('should remove credentials from localStorage if "remember" is unchecked', () => {
    spyOn(localStorage, 'removeItem');
    component.captchaValue = 5;
    component.authForm.controls['email'].setValue('test@example.com');
    component.authForm.controls['password'].setValue('password');
    component.authForm.controls['captcha'].setValue(5);
    component.authForm.controls['remember'].setValue(false);

    authService.login.and.returnValue(of(true));
    component.onSubmit();

    expect(localStorage.removeItem).toHaveBeenCalledWith('rememberedEmail');
    expect(localStorage.removeItem).toHaveBeenCalledWith('rememberedPassword');
  });
});
