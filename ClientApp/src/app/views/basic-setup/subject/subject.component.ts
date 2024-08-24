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
import { SubjectService } from './../service/subject.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-subject',
  templateUrl: './subject.component.html',
  styleUrl: './subject.component.scss',
})
export class SubjectComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('SubjectForm', { static: true }) SubjectForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'subjectName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public subjectService: SubjectService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('subjectId');
      if (id) {
        this.btnText = 'Update';
        this.subjectService.getById(+id).subscribe((res) => {
          this.SubjectForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });

  }
  ngOnInit(): void {
    this.getALlSubjects();
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
    this.subjectService.subjects = {
      subjectId: 0,
      subjectName: '',
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.SubjectForm?.form.value);
    this.btnText = 'Submit';
    if (this.SubjectForm?.form != null) {
      // console.log(this.SubjectForm?.form);
      this.SubjectForm.form.reset();
      this.SubjectForm.form.patchValue({
        subjectId: 0,
        subjectName: '',
        //  districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/educationSetup/subject']);
  }

  getALlSubjects() {
    this.subscription = this.subjectService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

 
  onSubmit(form: NgForm): void {
    this.subjectService.cachedData = [];
    const id = form.value.subjectId;
    const action$ = id
      ? this.subjectService.update(id, form.value)
      : this.subjectService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? 'Update' : 'Successfully';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlSubjects();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/educationSetup/subject']);
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
         // console.log('subject id ' + element.subjectId);
          this.subjectService.delete(element.subjectId).subscribe(
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
      });
  }
}
