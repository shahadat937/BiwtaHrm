import { ShiftService } from './../service/shift.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {
  AfterViewInit,
  Component,
  OnInit,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { Subscription } from 'rxjs';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-shift',
  templateUrl: './shift.component.html',
  styleUrl: './shift.component.scss'
})
export class ShiftComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('ShiftForm', { static: true }) ShiftForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'shiftName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort; 
  constructor(
    public shiftService: ShiftService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('shiftId');
      if (id) {
        this.btnText = 'Update';
        this.shiftService.getById(+id).subscribe((res) => {
          this.ShiftForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getALlShift();
    
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
    this.shiftService.shifts = {
      shiftId: 0,
      shiftName: '', 
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.ShiftForm?.form.value);
    this.btnText = 'Submit';
    if (this.ShiftForm?.form != null) {
      // console.log(this.ShiftForm?.form);
      this.ShiftForm.form.reset();
      this.ShiftForm.form.patchValue({
        shiftId: 0,
        shiftName: '', 
        //  districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  

  getALlShift() {
    this.subscription = this.shiftService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm) {
    const id = this.ShiftForm.form.get('shiftId')?.value;
    if (id) {
      this.shiftService.update(+id, this.ShiftForm.value).subscribe(
        (response: any) => {
          //console.log(response);
          if (response.success) {
            this.toastr.success('Successfully', 'Update', {
              positionClass: 'toast-top-right',
            });
            this.getALlShift();
            this.resetForm();
            this.router.navigate(['/bascisetup/shift']);
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    } else {
      this.subscription = this.shiftService.submit(form?.value).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('Successfully', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.getALlShift();
            this.resetForm();
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          console.log('shift id ' +element.shiftId);
          this.shiftService.delete(element.shiftId).subscribe(
            (res) => {
              this.getALlShift();
            },
            (err) => {
              console.log(err);
            }
          );
        }
      });
  }
}
