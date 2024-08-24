import { Occupation } from './../model/Occupation';
import { OccupationService } from './../service/Occupation.service';
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
  selector: 'app-occupation',
  templateUrl: './occupation.component.html',
  styleUrl: './occupation.component.scss'
})
export class OccupationComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  loading = false;
  @ViewChild('OccupationForm', { static: true }) OccupationForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'occupationName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public occupationService: OccupationService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getAllOccupations();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('occupationId');
      if (id) {
        this.btnText = 'Update';
        this.occupationService.find(+id).subscribe((res) => {
          this.OccupationForm?.form.patchValue(res);
        });
      } else {
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

  initaialOccupation(form?: NgForm) {
    if (form != null) form.resetForm();
    this.occupationService.occupations = {
      occupationId: 0,
      occupationName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.OccupationForm?.form != null) {
      this.OccupationForm.form.reset();
      this.OccupationForm.form.patchValue({
        occupationId: 0,
        occupationName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/personalInfoSetup/occupation']);

  }

  getAllOccupations() {
    this.subscription = this.occupationService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.occupationService.cachedData = [];
    const id = form.value.occupationId;
    const action$ = id
      ? this.occupationService.update(id, form.value)
      : this.occupationService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllOccupations();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/personalInfoSetup/occupation']);
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
          this.occupationService.delete(element.occupationId).subscribe(
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
