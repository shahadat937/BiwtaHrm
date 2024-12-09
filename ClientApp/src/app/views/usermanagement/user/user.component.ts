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
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { IncrementAndPromotionApprovalComponent } from '../../promotion/increment-and-promotion-approval/increment-and-promotion-approval.component';
import { UpdateRoleComponent } from '../update-role/update-role.component';
import { UpdateUserComponent } from '../update-user/update-user.component';
import { UserModule } from '../model/user.module';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss'
})
export class UserComponent implements OnInit, OnDestroy, AfterViewInit  {

  btnText: string | undefined;
  @ViewChild('UserForm', { static: true }) UserForm!: NgForm;
  loading = false;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'fullName', 'userName', 'department', 'designation','isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  userBtnText : string | undefined;
  userHeaderText : string | undefined;
  buttonIcon : string = '';
  visible : boolean | undefined;
  resetPasswordUser : UserModule = new UserModule;

  constructor(
    public userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    private modalService: BsModalService,
  ) {
  }

  ngOnInit(): void {
    this.buttonIcon = "cilPencil";
    this.handleRouteParams();
    this.getAllUsers();
  }
  handleRouteParams() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.userHeaderText = "Update User";
        this.userBtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.subscription.push(
        this.userService.find(id).subscribe((res) => {
          this.UserForm?.form.patchValue(res);
        })
        )
        
      } else {
        this.btnText = 'Submit';
        this.userHeaderText = "User List"
        this.userBtnText = " Add User"; 
        this.visible = false;
        this.initaialUser();
      }
    })
    )
    
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
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
    this.initaialUser();
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
      canEditProfile : false,
      departmentName: '',
      designationName: '',
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
        canEditProfile : false,
        departmentName: '',
        designationName: '',
      });
    }
    // this.router.navigate(['/usermanagement/user']);
  }

  getAllUsers(){
    this,this.subscription.push(
    this.userService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }

  updateUserInformation(id: string, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(UpdateUserComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      this.subscription.push(
      modalRef.onHide.subscribe(() => {
        this.getAllUsers();
      })
      )
      
    }
  }
  
  updateRole(id: string){
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(UpdateRoleComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      this.subscription.push(
      modalRef.onHide.subscribe(() => {
        this.getAllUsers();
      })
      )
      
    }
  }

  resetPassword(id: string){
    this.subscription.push(
    this.confirmService
    .confirm('Confirm Reset Password', 'Are You Sure Reset Password')
    .subscribe((result) => {
      if (result) {
        this.resetPasswordUser.id = id;
        this.resetPasswordUser.password = "Admin@123"
        this.userService.resetUserPassword(id, this.resetPasswordUser).subscribe(
          (res: any) => {
            if(res.success){
              this.toastr.success('', `${res.message}`, {
                positionClass: 'toast-top-right',
              });
            }
            else {
              this.toastr.error('', `${res.message}`, {
                positionClass: 'toast-top-right',
              });
            }
          }
        );
      }
    })
    )
    
  }
  

  onSubmit(form: NgForm): void{
    this.loading = true;
    this.userService.cachedData = [];
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      const id = form.value.id;
      const oldPassword = form.value.oldPassword;
      const currentPassword = form.value.password;

    let action$;

      if (id && oldPassword && currentPassword) {
        action$ = this.userService.updatePassword(id, form.value);
      } else if (id && !oldPassword && !currentPassword) {
        action$ = this.userService.update(id, form.value);
      } else {
        action$ = this.userService.submit(form.value);
      }

      this.subscription.push(
      action$.subscribe((response: any)  => {
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
      })
      )
      
    })
    )
    
  }
}
