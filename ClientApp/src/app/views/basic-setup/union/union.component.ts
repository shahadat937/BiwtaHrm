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
import { UnionService } from './../service/union.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-union',
  templateUrl: './union.component.html',
  styleUrl: './union.component.scss',
})
export class UnionComponent implements OnInit, OnDestroy, AfterViewInit {
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
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('unionId');
      if (id) {
        this.btnText = 'Update';
        this.unionService.find(+id).subscribe((res) => {
          this.UnionForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getAllUnions();
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



  initaialUnion(form?: NgForm) {
    if (form != null) form.resetForm();
    this.unionService.unions = {
      unionId: 0,
      unionName: '',
      thanaId: 0,
      //thanaName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    console.log(this.UnionForm?.form.value);
    this.btnText = 'Submit';
    if (this.UnionForm?.form != null) {
      this.UnionForm.form.reset();
      this.UnionForm.form.patchValue({
        unionId: 0,
        unionName: '',
        thanaId: 0,
        //  thanaName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/addressSetup/union']);
  }

  loadthanas() {
    this.subscription =this.unionService.getThana().subscribe((data) => {
      this.thanas = data;
    });
  }

  getAllUnions() {
    this.subscription = this.unionService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  // onSubmit(form: NgForm) {
  //   const id = this.UnionForm.form.get('unionId')?.value;
  //   if (id) {
  //     this.unionService.update(+id, this.UnionForm.value).subscribe(
  //       (response: any) => {
  //         console.log(response);
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getAllUnions();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/union']);
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
  //     this.subscription = this.unionService.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getAllUnions();
  //           this.resetForm();
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
  //   }
  // }
  onSubmit(form: NgForm): void {
    this.unionService.cachedData = [];
    const id = form.value.unionId;
    const action$ = id
      ? this.unionService.update(id, form.value)
      : this.unionService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllUnions();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/addressSetup/union']);
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
          console.log(result)
          this.unionService.delete(element.unionId).subscribe(
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
