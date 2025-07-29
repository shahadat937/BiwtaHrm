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
import { CountryService } from '../service/Country.service';

@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrl: './country.component.scss',
})
export class CountryComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('CountryForm', { static: true }) CountryForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'countryName', 'isDefault', 'isActive', 'Action'];
  loading = false;
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;

  constructor(
    public countryServices: CountryService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getALcountries();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.subscription.push(
    this.route.paramMap.subscribe((params) => {
      const id = params.get('countryId');
      
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Country';
        this.countryServices.find(+id).subscribe((res) => {
          this.CountryForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Country';
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

  initaialBloodGroup(form?: NgForm) {
    if (form != null) form.resetForm();
    this.countryServices.countrys = {
      countryId: 0,
      countryName: '',
      menuPosition: 0,
      isDefault: false,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.CountryForm?.form != null) {
      this.CountryForm.form.reset();
      this.CountryForm.form.patchValue({
        countryId: 0,
        countryName: '',
        menuPosition: 0,
        isDefault: false,
        isActive: true,
      });
    }
   this.router.navigate(['/addressSetup/country']);
  }

  getALcountries() {
    this.subscription.push(
    this.countryServices.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  // onSubmit(form: NgForm) {
  //   this.countryServices.cachedData = [];
  //   const id = this.CountryForm.form.get('countryId')?.value;
  //   if (id) {
  //     this.countryServices.update(+id, this.CountryForm.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALcountries();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/country']);
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
  //     this.subscription = this.countryServices.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALcountries();
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
    this.countryServices.cachedData = [];
    const id = form.value.countryId;
  
    const action$ = id
      ? this.countryServices.update(id, form.value)
      : this.countryServices.submit(form.value);

      this.subscription.push(
      action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALcountries();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/addressSetup/country']);
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
          this.subscription.push(
          this.countryServices.delete(element.countryId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',})
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
