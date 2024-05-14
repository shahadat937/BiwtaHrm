import { BankAccountTypeService } from './../service/bank-account-type.service';
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
  selector: 'app-bank-account-type',
  templateUrl: './bank-account-type.component.html',
  styleUrl: './bank-account-type.component.scss'
})
export class BankAccountTypeComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('BankAccountTypeForm', { static: true }) BankAccountTypeForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'bankAccountTypeName', 'isActive', 'Action'];
  loading = false;
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;

  constructor(
    public bankAccountTypService: BankAccountTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
  }
  ngOnInit(): void {
    this.getBankAccountTypes();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('bankAccountTypeId');
      if (id) {
        this.headerText = 'Update Bank Account Type'
        this.btnText = 'Update';
        this.bankAccountTypService.find(+id).subscribe((res) => {
          this.BankAccountTypeForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Bank Account Type'
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

  initaialBankAccountType(form?: NgForm) {
    if (form != null) form.resetForm();
    this.bankAccountTypService.bankAccountTypes = {
      bankAccountTypeId: 0,
      bankAccountTypeName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    console.log(this.BankAccountTypeForm?.form.value);

    this.btnText = 'Submit';
    if (this.BankAccountTypeForm?.form != null) {
      this.BankAccountTypeForm.form.reset();
      this.BankAccountTypeForm.form.patchValue({
        bankAccountTypeId: 0,
      bankAccountTypeName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/bankAccountType']);
  }

  getBankAccountTypes() {
    this.subscription = this.bankAccountTypService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.bankAccountTypService.cachedData = [];
    const id = form.value.bankAccountTypeId;
    const action$ = id
      ? this.bankAccountTypService.update(id, form.value)
      : this.bankAccountTypService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getBankAccountTypes();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/bankAccountType']);
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
          this.bankAccountTypService.delete(element.bankAccountTypeId).subscribe(
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
          );
        }
      });
  }
}