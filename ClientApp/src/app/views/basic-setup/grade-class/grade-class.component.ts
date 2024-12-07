import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { GradeClassService } from '../service/GradeClass.service';

@Component({
  selector: 'app-grade-class',
  templateUrl: './grade-class.component.html',
  styleUrl: './grade-class.component.scss',
})
export class GradeClassComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  loading = false;
  @ViewChild('GradeClassForm', { static: true }) GradeClassForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'gradeClassName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public gradeclassService: GradeClassService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getALlGradeClasses();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      const id = params.get('gradeClassId');
      if (id) {
        this.btnText = 'Update';
        this.gradeclassService.find(+id).subscribe((res) => {
          this.GradeClassForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    })
    )
   
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

  initaialGradeClass(form?: NgForm) {
    if (form != null) form.resetForm();
    this.gradeclassService.gradeClass = {
      gradeClassId: 0,
      gradeClassName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.GradeClassForm?.form != null) {
      this.GradeClassForm.form.reset();
      this.GradeClassForm.form.patchValue({
        gradeClassId: 0,
        gradeClassName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/officeSetup/grade-class']);
  }

  getALlGradeClasses() {
    this.subscription.push(
    this.gradeclassService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  // onSubmit(form: NgForm) {
  //   this.gradeclassService.cachedData = [];
  //   const id = this.GradeClassForm.form.get('gradeClassId')?.value;
  //   if (id) {
  //     this.gradeclassService.update(+id, this.GradeClassForm.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlGradeClasses();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/grade-class']);
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
  //     this.subscription = this.gradeclassService.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlGradeClasses();
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
    this.loading = true;
    this.gradeclassService.cachedData = [];
    const id = form.value.gradeClassId;
    const action$ = id
      ? this.gradeclassService.update(id, form.value)
      : this.gradeclassService.submit(form.value);

    this.subscription.push(
     action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlGradeClasses();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/officeSetup/grade-class']);
        }
        this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;
    })
    )
     
  }
  delete(element: any) {
    this.subscription.push(
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.gradeclassService.delete(element.gradeClassId).subscribe(
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
      })
    )
   
  }
}
