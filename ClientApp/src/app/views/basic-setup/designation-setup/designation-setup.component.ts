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
import { DesignationSetupService } from '../service/designation-setup.service';

@Component({
  selector: 'app-designation-setup',
  templateUrl: './designation-setup.component.html',
  styleUrl: './designation-setup.component.scss'
})
export class DesignationSetupComponent implements OnInit, OnDestroy, AfterViewInit {

  btnText: string = 'Submit';
  loading = false;
  @ViewChild('DesignationSetupForm', { static: true }) DesignationSetupForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'name', 'nameBangla', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public designationSetupService: DesignationSetupService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllDesignationSetup();
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
    this.designationSetupService.designationSetup = {
      id : 0,
      name : "",
      nameBangla : "",
      remark : "",
      isActive : true,
    };
  }

  resetForm() {
    this.btnText = 'Submit';
    if (this.DesignationSetupForm?.form != null) {
      this.DesignationSetupForm.form.reset();
      this.DesignationSetupForm.form.patchValue({
        id : 0,
        name : "",
        nameBangla : "",
        remark : "",
        isActive : true,
      });
    }
  }

  getDesignationSetupId(id: number){
    this.subscription = this.designationSetupService.find(id).subscribe((item) => {
      if(item){
        this.btnText = "Update";
        this.DesignationSetupForm.form.patchValue(item);
        window.scrollTo(0, 0);
      }
    });
  }

  getAllDesignationSetup() {
    this.subscription = this.designationSetupService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.designationSetupService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.designationSetupService.update(id, form.value)
      : this.designationSetupService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllDesignationSetup();
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
          this.designationSetupService.delete(element.id).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
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
