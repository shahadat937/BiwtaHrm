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
import { GradeClassService } from '../service/GradeClass.service';
import { GradeTypeService } from '../service/GradeType.service';

@Component({
  selector: 'app-grade',
  templateUrl: './grade.component.html',
  styleUrl: './grade.component.scss',
})
export class GradeComponent implements OnInit, OnDestroy, AfterViewInit {
  editMode: boolean = false;
  gradeType: any = [];
  gradeClass: any = [];

  btnText: string | undefined;
  loading = false;
  @ViewChild('GradeForm', { static: true }) GradeForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'gradeName',
    'gradeTypeId',
    'gradeClassId',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;

  constructor(
    public gradeService: GradeService,
    private snackBar: MatSnackBar,
    private gradeServiceClass: GradeClassService,
    private gradeTypeService: GradeTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getALlGrades();
    this.GetModelGradeType();
    this.GetModelGradeClass();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('gradeId');
      if (id) {
        this.btnText = 'Update';
        this.gradeService.find(+id).subscribe((res) => {
          this.GradeForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  GetModelGradeClass() {
    this.gradeServiceClass.getSelectedGradeClass().subscribe((res) => {
      console.log(res);
      this.gradeClass = res;
    });
  }
  GetModelGradeType() {
    this.gradeTypeService.getSelectGradeType().subscribe((res) => {
      console.log(res);
      this.gradeType = res;
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

  initaialGrade(form?: NgForm) {
    if (form != null) form.resetForm();
    this.gradeService.grades = {
      gradeId: 0,
      gradeName: '',
      gradeTypeId: 0,
      gradeClassId: 0,
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.GradeForm?.form != null) {
      this.GradeForm.form.reset();
      this.GradeForm.form.patchValue({
        gradeId: 0,
        gradeName: '',
        gradeTypeId: 0,
        gradeClassId: 0,
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/officeSetup/grade']);
  }
  getALlGrades() {
    this.subscription = this.gradeService.getAll().subscribe((item) => {
      console.log(item);
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.gradeService.cachedData = [];
    const id = form.value.gradeId;
    const action$ = id
      ? this.gradeService.update(id, form.value)
      : this.gradeService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlGrades();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/officeSetup/grade']);
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
          this.gradeService.delete(element.gradeId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
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
