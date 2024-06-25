import { ExamTypeService } from './../service/exam-type.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-exam-type',
  templateUrl: './exam-type.component.html',
  styleUrl: './exam-type.component.scss'
})
export class ExamTypeComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('ExamTypeForm', { static: true }) ExamTypeForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'examTypeName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public examTypeService: ExamTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getAllExamTypes();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('examTypeId');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Exam Type';
        this.examTypeService.find(+id).subscribe((res) => {
          this.ExamTypeForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Exam Type';
        this.btnText = 'Submit';
      }
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

  initaialExamType(form?: NgForm) {
    if (form != null) form.resetForm();
    this.examTypeService.examTypes = {
      examTypeId: 0,
      examTypeName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.ExamTypeForm?.form != null) {
      this.ExamTypeForm.form.reset();
      this.ExamTypeForm.form.patchValue({
        examTypeId: 0,
        examTypeName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/examType']);
  }

  getAllExamTypes() {
    this.subscription = this.examTypeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
 
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.examTypeService.cachedData = [];
    const id = form.value.examTypeId;
    const action$ = id
      ? this.examTypeService.update(id, form.value)
      : this.examTypeService.submit(form.value);
    
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllExamTypes();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/examType']);
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
          this.examTypeService.delete(element.examTypeId).subscribe(
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
      });
  }
}