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
  displayedColumns: string[] = ['slNo', 'fullName', 'userName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  userBtnText : string | undefined;
  userHeaderText : string | undefined;
  buttonIcon : string = '';
  visible : boolean | undefined;

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
    this.getAllUsers();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.userHeaderText = "Update User";
        this.userBtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.userService.find(id).subscribe((res) => {
          this.UserForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
        this.userHeaderText = "User List"
        this.userBtnText = " Add User"; 
        this.visible = false;
        this.initaialUser();
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

  UserFormView() : void {
    if(this.userBtnText == " Add User"){
      this.userBtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      this.userHeaderText = "Add New User";
      this.visible = true;
    }
    else {
      this.userBtnText = " Add User";
      this.buttonIcon = "cilPencil";
      this.userHeaderText = "User List";
      this.visible = false;
    }
  }

  toggleCollapse(){
    this.handleRouteParams();
    this.userHeaderText = "Update User";
    this.visible = true;
  }

  cancelUpdate(){
    this.router.navigate(['/usermanagement/user']);
    this.resetForm();
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

  resetForm(){
    // this.btnText = 'Submit';
    if (this.UserForm?.form != null) {
      this.UserForm.form.reset();
      this.UserForm.form.patchValue({
        id: '',
        userName: '',
        password: '',
        rePassword: '',
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber : '',
        pNo : '',
        empId : null,
        menuPosition: 0,
        isActive : true,
      });
    }
    // this.router.navigate(['/usermanagement/user']);
  }

  getAllUsers(){
    this.subscription = this.userService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
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
          this.getAllUsers();
          this.resetForm();
          this.router.navigate(['/usermanagement/user']);
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
        }
        this.loading = false;
      });
    });
  }

  delete(element: any){}
}
