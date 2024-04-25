import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit, OnDestroy, AfterViewInit  {

  btnText: string | undefined;
  @ViewChild('UserForm', { static: true }) UserForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  userBtnText = " Add User";
  buttonIcon : string = '';
  showUserForm = false;

  constructor(
    public userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
  }

  ngOnInit(): void {
    this.buttonIcon = "cilPencil";
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('bloodGroupId');
      if (id) {
        this.btnText = 'Update';
        // this.bloodGroupService.find(+id).subscribe((res) => {
        //   this.BloodGroupForm?.form.patchValue(res);
        // });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  UserFormView(){
    if(this.userBtnText == " Add User"){
      this.showUserForm = true;
      this.userBtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      console.log(this.buttonIcon)
    }
    else {
      this.showUserForm = false;
      this.userBtnText = " Add User";
      this.buttonIcon = "cilPencil";
      console.log(this.buttonIcon)
    }
  }

  
  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.userService.users = {
      userId: 0,
      userName: '',
      password: '',
      rePassword: '',
      firstName: '',
      lastName: '',
      email: '',
      phoneNumber : 0,
      pNo : '',
      menuPosition: 0,
      isActive : true,
    };
  }

  resetForm(){
    this.btnText = 'Submit';
    if (this.UserForm?.form != null) {
      this.UserForm.form.reset();
      this.UserForm.form.patchValue({
        userId: 0,
        userName: '',
        password: '',
        rePassword: '',
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber : 0,
        pNo : '',
        menuPosition: 0,
        isActive : true,
      });
    }
    this.router.navigate(['/usermanagement/user']);
  }

  onSubmit(form: NgForm): void{
    this.loading = true;
    this.userService.cachedData = [];
    const id = form.value.userId;

    const action$ = id
      ? this.userService.update(id, form.value)
      : this.userService.submit(form.value);

      this.subscription =action$.subscribe((response: any)  => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          // this.getALlBloodGroups();
          this.resetForm();
          if (!id) {
            this.router.navigate(['/usermanagement/user']);
          }
      this.loading = false;
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
        }
        
      this.loading = false;
      });
  }

}
