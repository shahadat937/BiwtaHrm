import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpBankInfoModule } from '../../../model/emp-bank-info.module';
import { EmpBankInfoService } from '../../../service/emp-bank-info.service';

@Component({
  selector: 'app-emp-bank-info',
  templateUrl: './emp-bank-info.component.html',
  styleUrl: './emp-bank-info.component.scss'
})
export class EmpBankInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>(); visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  accountTypes: SelectedModel[] = [];
  banks: SelectedModel[] = [];
  bankBranches: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  empBank: EmpBankInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empBankInfoService: EmpBankInfoService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeBankInfoByEmpId();
    this.getSelectedBankAccountType();
    this.getSelectedBankName();
    this.getSelectedBankBranchName();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  getEmployeeBankInfoByEmpId() {
    this.empBankInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Update Bank Information';
        this.btnText = 'Update';
        this.empBank = res;
        this.patchBankInfo(res);
      }
      else {
        this.headerText = 'Add Bank Information';
        this.btnText = 'Submit';
        this.addBank();
      }
    })
  }

  patchBankInfo(bankInfoList: any[]) {
    const control = <FormArray>this.EmpBankInfoForm.controls['empBankList'];
    control.clear();

    bankInfoList.forEach(bankInfo => {
      control.push(this.fb.group({
        id: [bankInfo.id],
        empId: [bankInfo.empId],
        accountName: [bankInfo.accountName, Validators.required],
        accountNumber: [bankInfo.accountNumber, Validators.required],
        accountTypeId: [bankInfo.accountTypeId],
        bankId: [bankInfo.bankId, Validators.required],
        branchId: [bankInfo.branchId, Validators.required],
        routingNo: [bankInfo.routingNo, Validators.pattern],
        remark: [bankInfo.remark],
      }));
    });
  }
  EmpBankInfoForm: FormGroup = new FormGroup({
    empBankList: new FormArray([])
  });

  get empBankListArray() {
    return this.EmpBankInfoForm.controls["empBankList"] as FormArray;
  }

  addBank() {
    this.empBankListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      accountName: new FormControl(undefined, Validators.required),
      accountNumber: new FormControl(undefined, Validators.required),
      accountTypeId: new FormControl(undefined),
      bankId: new FormControl(undefined, Validators.required),
      branchId: new FormControl(undefined, Validators.required),
      routingNo: new FormControl(undefined),
      remark: new FormControl(undefined),
    }));
  }

  removeBankList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empBankInfoService.deleteEmpBankInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empBankListArray.controls.length > 0)
                  this.empBankListArray.removeAt(index);
                this.getEmployeeBankInfoByEmpId();
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
    else if (id == 0) {
      if (this.empBankListArray.controls.length > 0)
        this.empBankListArray.removeAt(index);
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getSelectedBankAccountType() {
    this.empBankInfoService.getSelectedBankAccountType().subscribe((res) => {
      this.accountTypes = res;
    })
  }
  getSelectedBankName() {
    this.empBankInfoService.getSelectedBankName().subscribe((res) => {
      this.banks = res;
    })
  }
  getSelectedBankBranchName() {
    this.empBankInfoService.getSelectedBankBranchName().subscribe((res) => {
      this.bankBranches = res;
    })
  }

  cancel() {
    this.close.emit();
  }

  insertBank() {
    this.loading = true;
    this.empBankInfoService.saveEmpBankInfo(this.EmpBankInfoForm.get("empBankList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.cancel();
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
    )
  }
}


