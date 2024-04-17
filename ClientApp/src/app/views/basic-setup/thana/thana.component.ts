import { ThanaService } from './../service/thana.service';
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
  selector: 'app-thana',
  templateUrl: './thana.component.html',
  styleUrl: './thana.component.scss'
})
export class ThanaComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('ThanaForm', { static: true }) ThanaForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'thanaName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  upazilas: SelectedModel[] = [];
  constructor(
    public thanaService: ThanaService,
    private snackBar: MatSnackBar,
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
    this.getALlThana();
    this.loadupazila();
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
  }

  loadupazila() {
    // console.log('district');
    this.thanaService.getupazila().subscribe((data) => {
      // console.log('district' + data);
      this.upazilas = data;
    });
  }

  getALlThana() {
    this.subscription = this.thanaService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm) {
    const id = this.ThanaForm.form.get('thanaId')?.value;
    if (id) {
      this.thanaService.update(+id, this.ThanaForm.value).subscribe(
        (response: any) => {
          //console.log(response);
          if (response.success) {
            this.toastr.success('Successfully', 'Update', {
              positionClass: 'toast-top-right',
            });
            this.getALlThana();
            this.resetForm();
            this.router.navigate(['/bascisetup/thana']);
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
      this.subscription = this.thanaService.submit(form?.value).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('Successfully', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.getALlThana();
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
          console.log('thana id ' +element.thanaId);
          this.thanaService.delete(element.thanaId).subscribe(
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
                    positionClass: 'toast-top-right',})
                }
              );
            }
          });
      }
    }
    