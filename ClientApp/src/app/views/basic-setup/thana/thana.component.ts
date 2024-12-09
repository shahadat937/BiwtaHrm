import { UapzilaService } from './../service/uapzila.service';
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
import { ThanaService } from './../service/thana.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-thana',
  templateUrl: './thana.component.html',
  styleUrl: './thana.component.scss',
})
export class ThanaComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('ThanaForm', { static: true }) ThanaForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['upazilaName' ,'thanaName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  upazilas: SelectedModel[] = [];
  constructor(
    public thanaService: ThanaService,
    public uapzilaService: UapzilaService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('thanaId');
      if (id) {
        this.btnText = 'Update';
        this.thanaService.getById(+id).subscribe((res) => {
          this.ThanaForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getALlThanas();
    this.loadupazila();
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
    this.thanaService.thanas = {
      thanaId: 0,
      thanaName: '',
      upazilaId: 0,
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.ThanaForm?.form.value);
    this.btnText = 'Submit';
    if (this.ThanaForm?.form != null) {
      // console.log(this.ThanaForm?.form);
      this.ThanaForm.form.reset();
      this.ThanaForm.form.patchValue({
        thanaId: 0,
        thanaName: '',
        upazilaId: 0,
        //  districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/addressSetup/thana']);
  }

  loadupazila() {
    this.uapzilaService.getupazila().subscribe((data) => {
      this.upazilas = data;
    });
  }

  getALlThanas() {
    this.subscription.push(
    this.thanaService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }

  // onSubmit(form: NgForm) {
  //   const id = this.ThanaForm.form.get('thanaId')?.value;
  //   if (id) {
  //     this.thanaService.update(+id, this.ThanaForm.value).subscribe(
  //       (response: any) => {
  //         //console.log(response);
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlThanas();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/thana']);
  //         } else {
  //           this.toastr.warning('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //         }
  //       },
  //       (err) => {
  //         this.toastr.error('Somethig Wrong ! ', ` `, {
  //           positionClass: 'toast-top-right',})
  //         console.log(err);
  //       }
  //     );
  //   } else {
  //     this.subscription = this.thanaService.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlThanas();
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
    this.thanaService.cachedData = [];
    const id = form.value.thanaId;
    const action$ = id
      ? this.thanaService.update(id, form.value)
      : this.thanaService.submit(form.value);

    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlThanas();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/addressSetup/thana']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    })
    )
      
  }
  delete(element: any) {
    this.subscription.push(
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          console.log('thana id ' + element.thanaId);
          this.thanaService.delete(element.thanaId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
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
      })
    )
    
  }
}
