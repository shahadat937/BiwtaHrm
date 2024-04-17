import { WardService } from './../service/ward.service';
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
  selector: 'app-ward',
  templateUrl: './ward.component.html',
  styleUrl: './ward.component.scss'
})

export class WardComponent   implements OnInit, OnDestroy, AfterViewInit {
    position = 'top-end';
    visible = false;
    percentage = 0;
    btnText: string | undefined;
    @ViewChild('WardForm', { static: true }) WardForm!: NgForm;
    subscription: Subscription = new Subscription();
    displayedColumns: string[] = ['slNo', 'wardName', 'isActive', 'Action'];
    dataSource = new MatTableDataSource<any>();
    @ViewChild(MatPaginator)
    paginator!: MatPaginator;
    @ViewChild(MatSort)
    matSort!: MatSort;
    unions: SelectedModel[] = [];
    constructor(
      public wardService: WardService,
      private snackBar: MatSnackBar,
      private route: ActivatedRoute,
      private router: Router,
      private confirmService: ConfirmService,
      private toastr: ToastrService
    ) 
    { 
  
      this.route.paramMap.subscribe((params) => {
        const id = params.get('wardId');
        if (id) {
          this.btnText = 'Update';
          this.wardService.find(+id).subscribe((res) => {
            console.log(res);
            this.WardForm?.form.patchValue(res);
          });
        } else {
          this.btnText = 'Submit';
        }
      });
    }
    ngOnInit(): void {
      this.getALlWard();
      this.loadunions();
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
    initaialWard(form?: NgForm) {
      if (form != null) form.resetForm();
      this.wardService.wards = {
        wardId: 0,
        wardName: '',
        unionId :0,
        //unionName:"",
        menuPosition: 0,
        isActive: true,
      };
    }
    resetForm() {
      console.log(this.WardForm?.form.value);
      this.btnText = 'Submit';
      if (this.WardForm?.form != null) {
        console.log(this.WardForm?.form);
        this.WardForm.form.reset();
        this.WardForm.form.patchValue({
          wardId: 0,
          wardName: '',
          unionId :0,
        //  unionName:"",
          menuPosition: 0,
          isActive: true,
        });
      }
    }
  
    loadunions() {
      console.log('union')
      this.wardService.getUnion().subscribe(data => {
        console.log('union'+ data)
        this.unions = data;
      });
    }
  
    getALlWard() {
      this.subscription = this.wardService.getAll().subscribe((item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      });
    }
    onSubmit(form: NgForm) {
      const id = this.WardForm.form.get('wardId')?.value;
      if (id) {
        this.wardService.update(+id, this.WardForm.value).subscribe(
          (response: any) => {
            console.log(response);
            if (response.success) {
              this.toastr.success('Successfully', 'Update', {
                positionClass: 'toast-top-right',
              });
              this.getALlWard();
              this.resetForm();
              this.router.navigate(['/bascisetup/ward']);
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
        this.subscription = this.wardService.submit(form?.value).subscribe(
          (response: any) => {
            if (response.success) {
              this.toastr.success('Successfully', `${response.message}`, {
                positionClass: 'toast-top-right',
              });
              this.getALlWard();
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
            this.wardService.delete(element.wardId).subscribe(
              (res) => {  const index = this.dataSource.data.indexOf(element);
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
      