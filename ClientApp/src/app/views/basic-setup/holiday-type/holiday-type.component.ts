import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { HolidaytypeService } from '../service/holidaytype.service';

@Component({
  selector: 'app-holiday-type',
  templateUrl: './holiday-type.component.html',
  styleUrl: './holiday-type.component.scss'
})

export class HolidayTypeComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('HolidayTypeForm', { static: true }) HolidayTypeForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'holidayTypeName', 'isActive', 'remark','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public holidayTypeService: HolidaytypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    
  }

  ngOnInit(): void {
    this.getALlYears();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('holidayTypeId');
      if (id) {
        this.headerText = 'Update Holiday Type';
        this.btnText = 'Update';
        this.holidayTypeService.find(+id).subscribe((res) => {
          this.HolidayTypeForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add New Holiday Type';
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

  
  getALlYears() {
    this.subscription = this.holidayTypeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  
  initaialBloodGroup(form?: NgForm) {
    if (form != null) form.resetForm();
    this.holidayTypeService.holidayType = {
      holidayTypeId: 0,
      holidayTypeName: '',
      remark: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.HolidayTypeForm?.form != null) {
      this.HolidayTypeForm.form.reset();
      this.HolidayTypeForm.form.patchValue({
        holidayTypeId: 0,
        holidayTypeName: '',
        remark: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/holidaytype']);
  }


  onSubmit(form: NgForm): void{
    this.loading = true;
    this.holidayTypeService.cachedData = [];
    const id = form.value.holidayTypeId;
    const action$ = id
      ? this.holidayTypeService.update(id, form.value)
      : this.holidayTypeService.submit(form.value);
    
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlYears();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/holidaytype']);
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
          this.holidayTypeService.delete(element.holidayTypeId).subscribe(
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