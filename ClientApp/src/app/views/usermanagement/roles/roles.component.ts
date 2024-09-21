import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RolesService } from '../service/roles.service';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrl: './roles.component.scss'
})
export class RolesComponent  implements OnInit, OnDestroy, AfterViewInit {

  btnText: string = 'Submit';
  heading: string = 'Add New Roles'
  loading = false;
  @ViewChild('RolesForm', { static: true }) RolesForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'name','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public rolesService: RolesService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllRoles();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.rolesService.aspNetRoles = {
      id : '',
      name : "",
    };
  }

  resetForm() {
    this.btnText = 'Submit';
    this.heading = 'Add New Roles';
    if (this.RolesForm?.form != null) {
      this.RolesForm.form.reset();
      this.RolesForm.form.patchValue({
        id : "",
        name : "",
      });
    }
  }

  getRolesById(id: string){
    this.subscription = this.rolesService.find(id).subscribe((item) => {
      if(item){
        this.btnText = "Update";
        this.heading = 'Update Roles';
        this.RolesForm.form.patchValue(item);
        window.scrollTo(0, 0);
      }
    });
  }

  getAllRoles() {
    this.subscription = this.rolesService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.rolesService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.rolesService.update(id, form.value)
      : this.rolesService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllRoles();
        this.resetForm();
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;
    });
  }

  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.rolesService.delete(element.id).subscribe(
            (res: any) => {
              if(res.success == true){
                this.toastr.warning('', `${res.message}`, {
                  positionClass: 'toast-top-right',
                });
                const index = this.dataSource.data.indexOf(element);
                if (index !== -1) {
                  this.dataSource.data.splice(index, 1);
                  this.dataSource = new MatTableDataSource(this.dataSource.data);
                }
              }
              else {
                this.toastr.error('', `${res.message}`, {
                  positionClass: 'toast-top-right',
                });
              }
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
