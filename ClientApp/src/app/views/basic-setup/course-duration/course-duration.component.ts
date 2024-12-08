import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { CourseDurationService } from '../service/course-duration.service';

@Component({
  selector: 'app-course-duration',
  templateUrl: './course-duration.component.html',
  styleUrl: './course-duration.component.scss'
})
export class CourseDurationComponent implements OnInit, OnDestroy, AfterViewInit {

  btnText: string = 'Submit';
  loading = false;
  @ViewChild('CourseDurationForm', { static: true }) CourseDurationForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'name', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public courseDurationService: CourseDurationService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllCourseDuration();
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

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.courseDurationService.courseDuration = {
      id : 0,
      duration : "",
      remark : "",
      isActive : true,
    };
  }

  resetForm() {
    this.btnText = 'Submit';
    if (this.CourseDurationForm?.form != null) {
      this.CourseDurationForm.form.reset();
      this.CourseDurationForm.form.patchValue({
        id : 0,
        duration : "",
        remark : "",
        isActive : true,
      });
    }
  }

  getCourseDurationId(id: number){
    this.subscription.push(
    this.courseDurationService.find(id).subscribe((item) => {
      if(item){
        this.btnText = "Update";
        this.CourseDurationForm.form.patchValue(item);
        window.scrollTo(0, 0);
      }
    })
    )
    
  }

  getAllCourseDuration() {
    this.subscription.push(
    this.courseDurationService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.courseDurationService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.courseDurationService.update(id, form.value)
      : this.courseDurationService.submit(form.value);

      this.subscription.push(
  action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllCourseDuration();
        this.resetForm();
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
          this.subscription.push(
          this.courseDurationService.delete(element.id).subscribe(
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
          )
          )
          
        }
      })
    )
    
  }
}

