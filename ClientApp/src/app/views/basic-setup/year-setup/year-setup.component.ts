import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { YearSetupService } from '../service/year-setup.service';

@Component({
  selector: 'app-year-setup',
  templateUrl: './year-setup.component.html',
  styleUrl: './year-setup.component.scss'
})
export class YearSetupComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('YearForm', { static: true }) YearForm!: NgForm;
  loading = false;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'yearName', 'isActive', 'remark','Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public yearService: YearSetupService,
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
      const id = params.get('yearId');
      if (id) {
        this.headerText = 'Update Year';
        this.btnText = 'Update';
        this.yearService.find(+id).subscribe((res) => {
          this.YearForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add New Year';
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
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  
  getALlYears() {
    this.subscription.push(
    this.yearService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }

  
  initaialBloodGroup(form?: NgForm) {
    if (form != null) form.resetForm();
    this.yearService.years = {
      yearId: 0,
      yearName: undefined,
      remark: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.YearForm?.form != null) {
      this.YearForm.form.reset();
      this.YearForm.form.patchValue({
        yearId: 0,
        yearName: undefined,
        remark: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/personalInfoSetup/yearsetup']);
  }


  onSubmit(form: NgForm): void{
    this.loading = true;
    this.yearService.cachedData = [];
    const id = form.value.yearId;
    const action$ = id
      ? this.yearService.update(id, form.value)
      : this.yearService.submit(form.value);
    
    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlYears();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/personalInfoSetup/yearsetup']);
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
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.yearService.delete(element.yearId).subscribe(
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
