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
import { UapzilaService } from './../service/uapzila.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-upazila',
  templateUrl: './upazila.component.html',
  styleUrl: './upazila.component.scss',
})
export class UpazilaComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('UpazilaForm', { static: true }) UpazilaForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo','districtName', 'upazilaName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  districts: SelectedModel[] = [];
  constructor(
    public upazilaService: UapzilaService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('upazilaId');
      if (id) {
        this.btnText = 'Update';
        this.upazilaService.find(+id).subscribe((res) => {
          this.UpazilaForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getALlUpazilas();
    this.loaddistrict();
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
  toggleToast() {
    this.visible = !this.visible;
  }

  onVisibleChange($event: boolean) {
    this.visible = $event;
    this.percentage = !this.visible ? 0 : this.percentage;
  }

  onTimerChange($event: number) {
    this.percentage = $event * 25;
  }
  initaialUpazila(form?: NgForm) {
    if (form != null) form.resetForm();
    this.upazilaService.upazilas = {
      upazilaId: 0,
      upazilaName: '',
      districtId: 0,
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.UpazilaForm?.form.value);
    this.btnText = 'Submit';
    if (this.UpazilaForm?.form != null) {
      // console.log(this.UpazilaForm?.form);
      this.UpazilaForm.form.reset();
      this.UpazilaForm.form.patchValue({
        upazilaId: 0,
        upazilaName: '',
        districtId: 0,
        //  districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  loaddistrict() {
    // console.log('district');
    this.upazilaService.getdistrict().subscribe((data) => {
      // console.log('district' + data);
      this.districts = data;
    });
  }

  getALlUpazilas() {
    this.subscription = this.upazilaService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  // onSubmit(form: NgForm) {
  //   this.upazilaService.cachedData = [];
  //   const id = this.UpazilaForm.form.get('upazilaId')?.value;
  //   if (id) {
  //     this.upazilaService.update(+id, this.UpazilaForm.value).subscribe(
  //       (response: any) => {
  //         //console.log(response);
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlUpazilas();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/upazila']);
  //         } else {
  //           this.toastr.warning('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //         }
  //       },
  //       (err) => {
  //         console.log(err);
  //       }
  //     );
  //   } else {
  //     this.subscription = this.upazilaService.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlUpazilas();
  //           this.resetForm();
  //         } else {
  //           this.toastr.warning('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //         }
  //       },
  //       (err) => {
  //         console.log(err);
  //       }
  //     );
  //   }
  // }
  onSubmit(form: NgForm): void {
    this.upazilaService.cachedData = [];
    const id = form.value.upazilaId;
    const action$ = id
      ? this.upazilaService.update(id, form.value)
      : this.upazilaService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlUpazilas();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/upazila']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.upazilaService.delete(element.upazilaId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
          if (index !== -1) {
            this.dataSource.data.splice(index, 1);
            this.dataSource = new MatTableDataSource(
              this.dataSource.data
            );
          }
            },
            (err) => {
              // console.log(err);

              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }
}
