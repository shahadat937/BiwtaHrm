import { BankBranchService } from './../service/bank-branch.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { BankService } from '../service/bank.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bank-branch',
  templateUrl: './bank-branch.component.html',
  styleUrl: './bank-branch.component.scss'
})
export class BankBranchComponent implements OnInit, OnDestroy, AfterViewInit {
  //grades: any[] = [];
  editMode: boolean = false;
  banks: SelectedModel[] = [];

  btnText: string | undefined;
  headerText: string | undefined;
  loading = false;
  @ViewChild('BankBranchForm', { static: true }) BankBranchForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'bankBranchName', 'bankName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public bankBranchService: BankBranchService,
    private bankService: BankService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService

  ) { }
  ngOnInit(): void {
    this.getAlBankBranchs();
    this.SelectModelCountry();

    //this.loaddivisions();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {

      const id = params.get('bankBranchId');

      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Bank Branch';
        this.bankBranchService.find(+id).subscribe((res) => {
          this.BankBranchForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Bank Branch';
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

    this.bankService.selectGetBank().subscribe((data) => {
      //console.log(data);
      this.banks = data;
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


  initaialBankBranch(form?: NgForm) {

    if (form != null) form.resetForm();
    this.bankBranchService.bankBranchs = {
      bankBranchId: 0,
      bankBranchName: '',
      bankId: 0,
      bankBranchCode: '',
      bankBranchAddress: '',
      bankBranchContractNo: '',
      bankBranchPerson: '',
      email: '',
      noOfEmployee: 0,
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    if (this.BankBranchForm?.form != null) {
      this.BankBranchForm.form.reset();
      this.BankBranchForm.form.patchValue({
        bankBranchId: 0,
        bankBranchName: '',
        bankId: 0,
        bankBranchCode: '',
        bankBranchAddress: '',
        bankBranchContractNo: '',
        bankBranchPerson: '',
        email: '',
        noOfEmployee: 0,

        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bankInfoSetup/bankBranch']);
  }
  getAlBankBranchs() {
    this.subscription = this.bankBranchService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.bankBranchService.cachedData = [];
    const id = form.value.bankBranchId;
    const action$ = id
      ? this.bankBranchService.update(id, form.value)
      : this.bankBranchService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {

      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAlBankBranchs();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bankInfoSetup/bankBranch']);
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
          this.bankBranchService.delete(element.bankBranchId).subscribe(
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
