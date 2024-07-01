import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { UserService } from 'src/app/views/usermanagement/service/user.service';

@Component({
  selector: 'app-update-user-info',
  templateUrl: './update-user-info.component.html',
  styleUrl: './update-user-info.component.scss'
})
export class UpdateUserInfoComponent implements OnInit, OnDestroy  {

  
  @Input() empId!: number;
  @Output() cancel = new EventEmitter<void>();
  @ViewChild('UserForm', { static: true }) UserForm!: NgForm;
  btnText: String = 'Update';
  loading = false;
  subscription: Subscription = new Subscription();

  constructor(
    public userService: UserService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    this.getUserByEmpId();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  
  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.userService.users = {
      id: '',
      userName: '',
      oldPassword : '',
      password: '',
      rePassword: '',
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber : '',
      pNo : '',
      empId: null,
      menuPosition: 0,
      isActive : true,
    };
  }

  getUserByEmpId(){
    this.initaialUser();
    this.userService.getInfoByEmpId(this.empId).subscribe((res) => {
      this.UserForm?.form.patchValue(res);
    });
  }
  
  // ngOnChanges(changes: SimpleChanges) {
  //   if (changes[this.empId]) {
  //     console.log('Employee ID changed:', this.empId);
  //     this.getUserByEmpId();
  //   }
  // }

  cancelUpdate() {
    this.cancel.emit();
  }
  
  onSubmit(form: NgForm): void{
    this.loading = true;
    console.log("Form Value : ", form.value)
    this.userService.cachedData = [];
    this.route.paramMap.subscribe((params) => {
      const id = form.value.id;
      const oldPassword = form.value.oldPassword;
      const currentPassword = form.value.password;

    let action$;

      if (id && oldPassword && currentPassword) {
        action$ = this.userService.updateAndChangePassword(id, form.value);
      } else if (id && !oldPassword && !currentPassword) {
        action$ = this.userService.update(id, form.value);
      } else {
        action$ = this.userService.submit(form.value);
      }

      this.subscription =action$.subscribe((response: any)  => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
          this.cancelUpdate();
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
        }
        this.loading = false;
      });
    });
  }
}

