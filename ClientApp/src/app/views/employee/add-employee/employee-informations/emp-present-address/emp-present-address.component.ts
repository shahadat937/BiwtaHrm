import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { DistrictService } from 'src/app/views/basic-setup/service/district.service';
import { DivisionService } from 'src/app/views/basic-setup/service/division.service';
import { ThanaService } from 'src/app/views/basic-setup/service/thana.service';
import { UapzilaService } from 'src/app/views/basic-setup/service/uapzila.service';
import { UnionService } from 'src/app/views/basic-setup/service/union.service';
import { WardService } from 'src/app/views/basic-setup/service/ward.service';
import { EmpPresentAddressService } from '../../../service/emp-present-address.service';
import { ToastrService } from 'ngx-toastr';
import { CountryService } from 'src/app/views/basic-setup/service/Country.service';

@Component({
  selector: 'app-emp-present-address',
  templateUrl: './emp-present-address.component.html',
  styleUrl: './emp-present-address.component.scss'
})
export class EmpPresentAddressComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  countris: SelectedModel[] = [];
  divisions: SelectedModel[] = [];
  districts: SelectedModel[] = [];
  upazilas: SelectedModel[] = [];
  thanas: SelectedModel[] = [];
  unions: SelectedModel[] = [];
  wards: SelectedModel[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  @ViewChild('EmpPresentAddressForm', { static: true }) EmpPresentAddressForm!: NgForm;

  constructor(
    public empPresentAddressService: EmpPresentAddressService,
    public countryService: CountryService,
    public districtService: DistrictService,
    public uapzilaService: UapzilaService,
    public thanaService: ThanaService,
    public unionService: UnionService,
    public wardService: WardService,
    public divisionService: DivisionService,
    private toastr: ToastrService,) {

  }

  ngOnInit(): void {
    this.getEmployeeByEmpId();
    this.loadcountris();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getEmployeeByEmpId() {
    this.subscription.push(
      this.empPresentAddressService.findByEmpId(this.empId).subscribe((res) => {
        if (res) {
          res.countryId ? this.onDivisionNamesChangeByCounterId(res.countryId) : null;
          res.divisionId ? this.onDistrictNamesChangeByDivisionId(res.divisionId) : null;
          res.districtId ? this.onUpazilaNamesChangeByDistrictId(res.districtId) : null;
          res.upazilaId ? this.onThanaNamesChangeByUpazilaId(res.upazilaId) : null;
          // res.thanaId ? this.onUnionNamesChangeByThanaId(res.thanaId) : null;
          // res.unionId ? this.onWardNamesChangeByUnionId(res.unionId) : null;
          this.EmpPresentAddressForm?.form.patchValue(res);
          this.headerText = 'Update Present Address';
          this.btnText = 'Update';
        }
        else {
          this.headerText = 'Add Present Address';
          this.btnText = 'Submit';
          this.initaialForm();
        }
      })
    )
    
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empPresentAddressService.empPresentAddress = {
      id: 0,
      empId: this.empId,
      countryId: null,
      divisionId: null,
      districtId: null,
      upazilaId: null,
      thanaId: null,
      // unionId: null,
      // wardId: null,
      zipCode: null,
      address: "",
      email: "",
      postOffice: "",
      remark: '',
      menuPosition: 0,
      isActive: true,

      countryName: '',
      divisionName: '',
      districtName: '',
      upazilaName: '',
      thanaName: '',
      unionName: '',
      wardName: '',
    };
  }

  resetForm() {
    this.EmpPresentAddressForm.form.reset();
    this.EmpPresentAddressForm.form.patchValue({
      empId: this.empId,
      countryId: null,
      divisionId: null,
      districtId: null,
      upazilaId: null,
      thanaId: null,
      // unionId: null,
      // wardId: null,
      zipCode: null,
      address: "",
      email: "",
      postOffice: "",
      remark: '',
      menuPosition: 0,
      isActive: true
    });
  }

  loadcountris() {
    // this.subscription = 
    this.subscription.push(
      this.countryService.selectGetCountry().subscribe((data) => {
      this.countris = data;
    })
    )
    
  }
  onDivisionNamesChangeByCounterId(counterId: number) {
    // this.subscription = 
    this.subscription.push(
      this.divisionService.getDivisionByCountryId(counterId).subscribe((data) => {
        this.divisions = data;
      })
    )
    
  }
  onDistrictNamesChangeByDivisionId(divisionId: number) {
    // this.subscription = 
    this.subscription.push(
      this.districtService.getDistrictByDivisionId(divisionId).subscribe((data) => {
      this.districts = data;

    })
    )
    
  }
  onUpazilaNamesChangeByDistrictId(districtId: number) {
    // this.subscription = 
    this.subscription.push(
      this.uapzilaService.getUpapzilaByDistrictId(districtId).subscribe((data) => {
        this.upazilas = data;
      })
    )
    
  }
  onThanaNamesChangeByUpazilaId(upazilaId: number) {
    // this.subscription = 
    this.subscription.push(
      this.thanaService.getthanaNamesByUpazilaId(upazilaId).subscribe((data) => {
        this.thanas = data;
  
      })
    )
    
  }
  onUnionNamesChangeByThanaId(thanaId: number) {
    // this.subscription = 
    this.subscription.push(
      this.unionService.getUnionNamesByThanaId(thanaId).subscribe((data) => {
        this.unions = data;
      })
    )
    
  }
  onWardNamesChangeByUnionId(unionId: number) {
    // this.subscription = 
    this.subscription.push(
      this.wardService.getWardNamesByUnionId(unionId).subscribe((data) => {
        this.wards = data;
  
      })
    )
    
  }


  cancel() {
    this.close.emit();
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.empPresentAddressService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empPresentAddressService.updateEmpPresentInfo(id, form.value)
      : this.empPresentAddressService.saveEmpPresentInfo(form.value);

    // this.subscription = 
    this.subscription.push(
      action$.subscribe((response: any) => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
          // this.cancel();
          this.getEmployeeByEmpId();
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
        }
        this.loading = false;
      })
    )
    
  }

}