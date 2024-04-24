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
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { CountryService } from '../service/country.service';
import { DivisionService } from './../service/division.service';

@Component({
  selector: 'app-division',
  templateUrl: './division.component.html',
  styleUrl: './division.component.scss',
})
export class DivisionComponent implements OnInit, OnDestroy, AfterViewInit {
  //grades: any[] = [];
  editMode: boolean = false;
  countrys: SelectedModel[] = [];

  btnText: string | undefined;
  loading = false;
  @ViewChild('DivisionForm', { static: true }) DivisionForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo','countryName','divisionName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public devisionService: DivisionService,
    private countryService: CountryService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getALlDivisions();
    this.SelectModelCountry();
    //this.loaddivisions();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('divisionId');
      if (id) {
        this.btnText = 'Update';
        this.devisionService.find(+id).subscribe((res) => {
          this.DivisionForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  // loaddivisions() {
  //   console.log('division')
  //   this.countryService.selectGetCountry().subscribe(data => {
  //     console.log('division'+ data)
  //     this.countrys = data;
  //   });
  // }
  SelectModelCountry() {
    this.countryService.selectGetCountry().subscribe((data) => {
      //console.log(data);
      this.countrys = data;
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

  initaialDivision(form?: NgForm) {
    if (form != null) form.resetForm();
    this.devisionService.divisions = {
      divisionId: 0,
      divisionName: '',
      countryId: 0,
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.DivisionForm?.form != null) {
      this.DivisionForm.form.reset();
      this.DivisionForm.form.patchValue({
        divisionId: 0,
        divisionName: '',
        countryId: 0,
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/division']);
  }
  getALlDivisions() {
    this.subscription = this.devisionService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  // onSubmit(form: NgForm) {
  //   this.devisionService.cachedData = [];
  //   const id = this.DivisionForm.form.get('divisionId')?.value;
  //   if (id) {
  //     this.devisionService.update(+id, this.DivisionForm.value).subscribe((response: any) => {
  //       if (response.success) {
  //         this.toastr.success('Successfully', 'Update', { positionClass: 'toast-top-right' });
  //         this.getALlDivisions()
  //         this.resetForm();
  //         this.router.navigate(["/bascisetup/division"]);
  //       } else {
  //         this.toastr.warning('', `${response.message}`, { positionClass: 'toast-top-right' });
  //       }

  //     }, err => {
  //       console.log(err)
  //     })
  //   } else {
  //     this.subscription = this.devisionService.submit(form?.value).subscribe((response: any) => {
  //       if (response.success) {
  //         this.toastr.success('Successfully', `${response.message}`, { positionClass: 'toast-top-right' });
  //         this.getALlDivisions()
  //         this.resetForm();
  //       } else {
  //         this.toastr.warning('', `${response.message}`, { positionClass: 'toast-top-right' });
  //       }

  //     }, err => {
  //       console.log(err);
  //     })
  //   }

  // }
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.devisionService.cachedData = [];
    const id = form.value.divisionId;
    const action$ = id
      ? this.devisionService.update(id, form.value)
      : this.devisionService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlDivisions();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/division']);
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
          this.devisionService.delete(element.divisionId).subscribe(
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
