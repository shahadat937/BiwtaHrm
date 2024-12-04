import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';

import {JobDetailsSetupService} from '../service/job-details-setup.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { CountryService } from '../service/Country.service';
import { DistrictService } from '../service/district.service';
import { DivisionService } from '../service/division.service';
import { ThanaService } from '../service/thana.service';
import { UapzilaService } from '../service/uapzila.service';
import { UnionService } from '../service/union.service';
import { WardService } from '../service/ward.service';

@Component({
  selector: 'app-job-details-setup',
  templateUrl: './job-details-setup.component.html',
  styleUrl: './job-details-setup.component.scss'
})
export class JobDetailsSetupComponent implements OnInit, OnDestroy, AfterViewInit {

  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('JobDetailsSetupForm', { static: true }) JobDetailsSetupForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'prlAge', 'retirmentAge', 'orderStartDate', 'orderEndDate','isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;

  constructor(
    public jobDetailsSetupService: JobDetailsSetupService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getAllJobDetailsSetup();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Job Details Setup';
        this.jobDetailsSetupService.find(+id).subscribe((res) => {
          this.JobDetailsSetupForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Job Details Setup';
        this.btnText = 'Submit';
        this.initaialReleaseType();
      }
    });
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

  initaialReleaseType(form?: NgForm) {
    if (form != null) form.resetForm();
    this.jobDetailsSetupService.jobDetailsSetups = {
      id: 0,
      prlAge: null,
      retirmentAge: null,
      orderStartDate: null,
      orderEndDate: null,
      remark: "",
      isActive: true
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.JobDetailsSetupForm?.form != null) {
      this.JobDetailsSetupForm.form.reset();
      this.JobDetailsSetupForm.form.patchValue({
        id: 0,
        prlAge: null,
        retirmentAge: null,
        orderStartDate: null,
        orderEndDate: null,
        remark: "",
        isActive: true
      });
    }
    this.router.navigate(['/personalInfoSetup/job-details-setup']);
  }

  getAllJobDetailsSetup() {
    this.subscription = this.jobDetailsSetupService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.jobDetailsSetupService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.jobDetailsSetupService.update(id, form.value)
      : this.jobDetailsSetupService.submit(form.value);
    
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllJobDetailsSetup();
        this.resetForm();
        this.router.navigate(['/personalInfoSetup/job-details-setup']);
      this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      this.loading = false;
      }
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.jobDetailsSetupService.delete(element.id).subscribe(
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