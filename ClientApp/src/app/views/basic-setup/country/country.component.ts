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
import { CountryService } from '../service/country.service';

@Component({
  selector: 'app-country',
  templateUrl: './country.component.html',
  styleUrl: './country.component.scss',
})
export class CountryComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  @ViewChild('CountryForm', { static: true }) CountryForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'countryName', 'isActive', 'Action'];
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
    this.getALlCountry();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('countrytId');
      if (id) {
        this.btnText = 'Update';
        this.countryServices.find(+id).subscribe((res) => {
          this.CountryForm?.form.patchValue(res);
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

  initaialBloodGroup(form?: NgForm) {
    if (form != null) form.resetForm();
    this.countryServices.countrys = {
      countrytId: 0,
      countryName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    console.log(this.CountryForm?.form.value);
    this.btnText = 'Submit';
    if (this.CountryForm?.form != null) {
      this.CountryForm.form.reset();
      this.CountryForm.form.patchValue({
        countrytId: 0,
        countryName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/country']);
  }

  getALlCountry() {
    this.subscription = this.countryServices.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  onSubmit(form: NgForm) {
    this.countryServices.cachedData = [];
    const id = this.CountryForm.form.get('countrytId')?.value;
    if (id) {
      this.countryServices.update(+id, this.CountryForm.value).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('Successfully', 'Update', {
              positionClass: 'toast-top-right',
            });
            this.getALlCountry();
            this.resetForm();
            this.router.navigate(['/bascisetup/country']);
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    } else {
      this.subscription = this.countryServices.submit(form?.value).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.getALlCountry();
            this.resetForm();
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.countryServices.delete(element.countrytId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',})
              console.log(err)
            }
          );
        }
      });
  }
}
