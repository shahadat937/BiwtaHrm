import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpForeignTourInfoModule } from '../../../model/emp-foreign-tour-info.module';
import { EmpForeignTourInfoService } from '../../../service/emp-foreign-tour-info.service';
import { CountryService } from 'src/app/views/basic-setup/service/Country.service';

@Component({
  selector: 'app-emp-foreign-tour-info',
  templateUrl: './emp-foreign-tour-info.component.html',
  styleUrl: './emp-foreign-tour-info.component.scss'
})
export class EmpForeignTourInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>(); visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  countris: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  empForeignTour: EmpForeignTourInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empForeignTourInfoService: EmpForeignTourInfoService,
    public countryService: CountryService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeForeignTourInfoByEmpId();
    this.getSelectedCountries();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  getEmployeeForeignTourInfoByEmpId() {
    this.empForeignTourInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Updat Foreign Tour Information';
        this.btnText = 'Update';
        this.patchForeignTourInfo(res);
      }
      else {
        this.headerText = 'Add Foreign Tour Information';
        this.btnText = 'Submit';
        this.addForeignTour();
      }
    })
  }

  patchForeignTourInfo(foreignTourInfoList: any[]) {
    const control = <FormArray>this.EmpForeignTourInfoForm.controls['empForeignTourList'];
    control.clear();

    foreignTourInfoList.forEach(foreignTourInfo => {
      control.push(this.fb.group({
        id: [foreignTourInfo.id],
        empId: [foreignTourInfo.empId],
        countryId: [foreignTourInfo.countryId, Validators.required],
        fromDate: [foreignTourInfo.fromDate],
        toDate: [foreignTourInfo.toDate],
        purpose: [foreignTourInfo.purpose],
        remark: [foreignTourInfo.remark],
      }));
    });
  }
  EmpForeignTourInfoForm: FormGroup = new FormGroup({
    empForeignTourList: new FormArray([])
  });

  get empForeignTourListArray() {
    return this.EmpForeignTourInfoForm.controls["empForeignTourList"] as FormArray;
  }

  addForeignTour() {
    this.empForeignTourListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      countryId: new FormControl(undefined, Validators.required),
      fromDate: new FormControl(undefined),
      toDate: new FormControl(undefined),
      purpose: new FormControl(undefined),
      remark: new FormControl(undefined),
    }));
  }

  removeForeignTourList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empForeignTourInfoService.deleteEmpForeignTourInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empForeignTourListArray.controls.length > 0)
                  this.empForeignTourListArray.removeAt(index);
                this.getEmployeeForeignTourInfoByEmpId();
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
      if (this.empForeignTourListArray.controls.length > 0)
        this.empForeignTourListArray.removeAt(index);
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }


  getSelectedCountries() {
    this.subscription = this.countryService.selectGetCountry().subscribe((data) => {
      this.countris = data;
    });
  }

  cancel() {
    this.close.emit();
  }

  insertForeignTour() {
    this.loading = true;
    this.empForeignTourInfoService.saveEmpForeignTourInfo(this.EmpForeignTourInfoForm.get("empForeignTourList")?.value).subscribe(((res: any) => {
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

