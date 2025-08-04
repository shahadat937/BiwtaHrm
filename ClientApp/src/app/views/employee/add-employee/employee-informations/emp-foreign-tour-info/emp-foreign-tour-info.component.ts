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
import { SharedService } from '../../../../../shared/shared.service'

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
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  defaultCountryId : number = 0;
  empForeignTour: EmpForeignTourInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empForeignTourInfoService: EmpForeignTourInfoService,
    public countryService: CountryService,
    private fb: FormBuilder,
    public sharedService: SharedService
  ) { }


  ngOnInit(): void {
    this.getDefaultCountryId();
    this.getSelectedCountries();
    this.getEmployeeForeignTourInfoByEmpId();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  getDefaultCountryId(){
    this.subscription.push(
      this.countryService.getDefaultCountryId().subscribe((res) => {
        if(res){
          this.defaultCountryId = res;
        }
      })
    )
  }

  getEmployeeForeignTourInfoByEmpId() {
    this.empForeignTourInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Updat Foreign Tour Information';
        this.btnText = 'Update';
           res.forEach(item => {
            item.toDate = this.sharedService.parseDate(item.toDate);
            item.fromDate = this.sharedService.parseDate(item.fromDate);
          });
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
      countryId: new FormControl(this.defaultCountryId, Validators.required),
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
    // this.subscription = 
    this.subscription.push(
      this.countryService.selectGetCountry().subscribe((data) => {
      this.countris = data;
    })
    )
    
  }

  cancel() {
    this.close.emit();
  }

  insertForeignTour() {
    this.loading = true;

    
    const toreList = this.EmpForeignTourInfoForm.get("empForeignTourList")?.value.map((item: any) => ({
      ...item,
      toDate: this.sharedService.formatDateOnly(item.toDate),
      fromDate: this.sharedService.formatDateOnly(item.fromDate),
    }));

    this.empForeignTourInfoService.saveEmpForeignTourInfo(toreList).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        // this.cancel();
        this.getEmployeeForeignTourInfoByEmpId();
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

