import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { GradeService } from '../service/Grade.service';
import { ScaleService } from './../service/Scale.service';

@Component({
  selector: 'app-scale',
  templateUrl: './scale.component.html',
  styleUrl: './scale.component.scss',
})
export class ScaleComponent implements OnInit, OnDestroy, AfterViewInit {
  //grades: any[] = [];
  editMode: boolean = false;
  grades: any = [];

  btnText: string | undefined;
  loading = false;
  @ViewChild('ScaleForm', { static: true }) ScaleForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'scaleName',
    'basicPay',
    'gradeId',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public ScaleService: ScaleService,
    private gradeService: GradeService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getALlScales();
    this.SelectModelGrade();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('scaleId');
      if (id) {
        this.btnText = 'Update';
        this.ScaleService.find(+id).subscribe((res) => {
          this.ScaleForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  SelectModelGrade() {
    this.gradeService.selectModelGrade().subscribe((data) => {
      this.grades = data;
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

  initaialScale(form?: NgForm) {
    if (form != null) form.resetForm();
    this.ScaleService.scales = {
      scaleId: 0,
      scaleName: '',
      basicPay: 0,
      gradeId: 0,
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.ScaleForm?.form != null) {
      this.ScaleForm.form.reset();
      this.ScaleForm.form.patchValue({
        scaleId: 0,
        scaleName: '',
        basicPay: 0,
        gradeId: 0,
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/officeSetup/scale']);
  }
  getALlScales() {
    this.subscription = this.ScaleService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  // onSubmit(form: NgForm) {
  //   this.ScaleService.cachedData = [];
  //   const id = this.ScaleForm.form.get('scaleId')?.value;
  //   if (id) {
  //     this.ScaleService.update(+id, this.ScaleForm.value).subscribe((response: any) => {
  //       if (response.success) {
  //         this.toastr.success('Successfully', 'Update', { positionClass: 'toast-top-right' });
  //         this.getALlScales()
  //         this.resetForm();
  //         this.router.navigate(["/bascisetup/scale"]);
  //       } else {
  //         this.toastr.warning('', `${response.message}`, { positionClass: 'toast-top-right' });
  //       }

  //     }, err => {
  //       console.log(err)
  //     })
  //   } else {
  //     this.subscription = this.ScaleService.submit(form?.value).subscribe((response: any) => {
  //       if (response.success) {
  //         this.toastr.success('Successfully', `${response.message}`, { positionClass: 'toast-top-right' });
  //         this.getALlScales()
  //         this.resetForm();
  //       } else {
  //         this.toastr.warning('', `${response.message}`, { positionClass: 'toast-top-right' });
  //       }

  //     }, err => {
  //       console.log(err);
  //     })
  //   }

  // }
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.ScaleService.cachedData = [];
    const id = form.value.scaleId;
    const action$ = id
      ? this.ScaleService.update(id, form.value)
      : this.ScaleService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlScales();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/officeSetup/scale']);
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
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.ScaleService.delete(element.scaleId).subscribe(
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
