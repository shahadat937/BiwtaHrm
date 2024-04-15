import { UnionService } from './../service/union.service';
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
  selector: 'app-union',
  templateUrl: './union.component.html',
  styleUrl: './union.component.scss'
})
export class UnionComponent    implements OnInit, OnDestroy, AfterViewInit {
    position = 'top-end';
    visible = false;
    percentage = 0;
    btnText: string | undefined;
    @ViewChild('UnionForm', { static: true }) UnionForm!: NgForm;
    subscription: Subscription = new Subscription();
    displayedColumns: string[] = ['slNo', 'unionName', 'isActive', 'Action'];
    dataSource = new MatTableDataSource<any>();
    @ViewChild(MatPaginator)
    paginator!: MatPaginator;
    @ViewChild(MatSort)
    matSort!: MatSort;
    thanas: SelectedModel[] = [];
    constructor(
      public unionService: UnionService,
      private snackBar: MatSnackBar,
      private route: ActivatedRoute,
      private router: Router,
      private confirmService: ConfirmService,
      private toastr: ToastrService
    ) 
    { 
  
      this.route.paramMap.subscribe((params) => {
        const id = params.get('unionId');
        if (id) {
          this.btnText = 'Update';
          this.unionService.find(+id).subscribe((res) => {
            console.log(res);
            this.UnionForm?.form.patchValue(res);
          });
        } else {
          this.btnText = 'Submit';
        }
      });
    }
    ngOnInit(): void {
      this.getALlUnion();
      this.loadthanas();
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
    initaialUnion(form?: NgForm) {
      if (form != null) form.resetForm();
      this.unionService.unions = {
        unionId: 0,
        unionName: '',
        thanaId :0,
        //thanaName:"",
        menuPosition: 0,
        isActive: true,
      };
    }
    resetForm() {
      console.log(this.UnionForm?.form.value);
      this.btnText = 'Submit';
      if (this.UnionForm?.form != null) {
        console.log(this.UnionForm?.form);
        this.UnionForm.form.reset();
        this.UnionForm.form.patchValue({
          unionId: 0,
          unionName: '',
          thanaId :0,
        //  thanaName:"",
          menuPosition: 0,
          isActive: true,
        });
      }
    }
  
    loadthanas() {
      console.log('thana')
      this.unionService.getThana().subscribe(data => {
        console.log('thana'+ data)
        this.thanas = data;
      });
    }
  
    getALlUnion() {
      this.subscription = this.unionService.getAll().subscribe((item) => {
        this.dataSource = new MatTableDataSource(item);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
      });
    }
    onSubmit(form: NgForm) {
      const id = this.UnionForm.form.get('unionId')?.value;
      if (id) {
        this.unionService.update(+id, this.UnionForm.value).subscribe(
          (response: any) => {
            console.log(response);
            if (response.success) {
              this.toastr.success('Successfully', 'Update', {
                positionClass: 'toast-top-right',
              });
              this.getALlUnion();
              this.resetForm();
              this.router.navigate(['/bascisetup/union']);
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
        this.subscription = this.unionService.submit(form?.value).subscribe(
          (response: any) => {
            if (response.success) {
              this.toastr.success('Successfully', `${response.message}`, {
                positionClass: 'toast-top-right',
              });
              this.getALlUnion();
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
            this.unionService.delete(element.unionId).subscribe(
              (res) => {
                this.getALlUnion();
              },
              (err) => {
                console.log(err);
              }
            );
          }
        });
    }
  }
  
