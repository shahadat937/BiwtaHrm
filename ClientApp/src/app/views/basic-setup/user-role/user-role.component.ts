import { UserRoleService } from './../service/user-role.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-user-role',
  templateUrl: './user-role.component.html',
  styleUrl: './user-role.component.scss'
})
export class UserRoleComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  @ViewChild('UserRoleForm', { static: true }) UserRoleForm!: NgForm;
  loading = false;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'userRoleName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public userRoleService: UserRoleService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getAllUserRoles();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('userRoleId');
      if (id) {
        this.btnText = 'Update';
        this.userRoleService.find(+id).subscribe((res) => {
          this.UserRoleForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialUserRole(form?: NgForm) {
    if (form != null) form.resetForm();
    this.userRoleService.userRoles = {
      userRoleId: 0,
      userRoleName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.UserRoleForm?.form != null) {
      this.UserRoleForm.form.reset();
      this.UserRoleForm.form.patchValue({
        userRoleId: 0,
        userRoleName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/userRole']);
  }

  getAllUserRoles() {
    this.subscription.push(
    this.userRoleService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.userRoleService.cachedData = [];
    const id = form.value.userRoleId;
    const action$ = id
      ? this.userRoleService.update(id, form.value)
      : this.userRoleService.submit(form.value);
    
    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllUserRoles();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/userRole']);
        }
    this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      
    this.loading = false;
    })
    )
    
    
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.userRoleService.delete(element.userRoleId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
              console.log(err);
            }
          );
        }
      });
  }
}