import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { CountryService } from 'src/app/views/basic-setup/service/Country.service';
import { DistrictService } from 'src/app/views/basic-setup/service/district.service';
import { DivisionService } from 'src/app/views/basic-setup/service/division.service';
import { ThanaService } from 'src/app/views/basic-setup/service/thana.service';
import { UapzilaService } from 'src/app/views/basic-setup/service/uapzila.service';
import { UnionService } from 'src/app/views/basic-setup/service/union.service';
import { WardService } from 'src/app/views/basic-setup/service/ward.service';
import { EmpPermanentAddressService } from '../../../service/emp-permanent-address.service';
import { EmpPresentAddressService } from '../../../service/emp-present-address.service';

@Component({
  selector: 'app-emp-address',
  templateUrl: './emp-address.component.html',
  styleUrl: './emp-address.component.scss'
})
export class EmpAddressComponent  implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  defaultCountryId : number = 0;
  countris: SelectedModel[] = [];
  presentAddressdivisions: SelectedModel[] = [];
  presentAddressdistricts: SelectedModel[] = [];
  presentAddressupazilas: SelectedModel[] = [];
  presentAddressthanas: SelectedModel[] = [];
  presentAddressunions: SelectedModel[] = [];
  presentAddresswards: SelectedModel[] = [];
  
  permanentAddressdivisions: SelectedModel[] = [];
  permanentAddressdistricts: SelectedModel[] = [];
  permanentAddressupazilas: SelectedModel[] = [];
  permanentAddressthanas: SelectedModel[] = [];
  permanentAddressunions: SelectedModel[] = [];
  permanentAddresswards: SelectedModel[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  sameAsPresentAddress: boolean = false;
  presentAddressStatus: boolean = false;
  @ViewChild('EmpPresentAddressForm', { static: true }) EmpPresentAddressForm!: NgForm;
  @ViewChild('EmpPermanentAddressForm', { static: true }) EmpPermanentAddressForm!: NgForm;

  constructor(
    public countryService: CountryService,
    public districtService: DistrictService,
    public uapzilaService: UapzilaService,
    public thanaService: ThanaService,
    public unionService: UnionService,
    public wardService: WardService,
    public divisionService: DivisionService,
    private toastr: ToastrService,
    public empPermanentAddressService: EmpPermanentAddressService,
    public empPresentAddressService: EmpPresentAddressService,) {

  }

  ngOnInit(): void {
    this.getDefaultCountryId();
    this.loadcountris();
    this.getAddressInformation();
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

  getAddressInformation() {
    this.empPresentAddressService.findByEmpId(this.empId).subscribe((res) => {
      if (res) {
        res.countryId ? this.onPresentAddressDivisionNamesChangeByCounterId(res.countryId) : null;
        res.divisionId ? this.onPresentAddressDistrictNamesChangeByDivisionId(res.divisionId) : null;
        res.districtId ? this.onPresentAddressUpazilaNamesChangeByDistrictId(res.districtId) : null;
        res.upazilaId ? this.onPresentAddressThanaNamesChangeByUpazilaId(res.upazilaId) : null;
        // res.thanaId ? this.onPresentAddressUnionNamesChangeByThanaId(res.thanaId) : null;
        // res.unionId ? this.onPresentAddressWardNamesChangeByUnionId(res.unionId) : null;
        this.EmpPresentAddressForm?.form.patchValue(res);
        this.headerText = 'Update Present Address';
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add Present Address';
        this.btnText = 'Submit';
        this.resetPresentAddressForm();
      }
    })
    this.empPermanentAddressService.findByEmpId(this.empId).subscribe((res) => {
      this.presentAddressStatus = res ? false : true;
      if(res){
        res.countryId ? this.onPermanentAddressDivisionNamesChangeByCounterId(res.countryId) : null;
        res.divisionId ? this.onPermanentAddressDistrictNamesChangeByDivisionId(res.divisionId) : null;
        res.districtId ? this.onPermanentAddressUpazilaNamesChangeByDistrictId(res.districtId) : null;
        res.upazilaId ? this.onPermanentAddressThanaNamesChangeByUpazilaId(res.upazilaId) : null;
        // res.thanaId ? this.onPermanentAddressUnionNamesChangeByThanaId(res.thanaId) : null;
        // res.unionId ? this.onPermanentAddressWardNamesChangeByUnionId(res.unionId) : null;
        this.EmpPermanentAddressForm?.form.patchValue(res);
      }
      else {
        this.resetPermanentAddressForm();
      }
    })
  }

  getDefaultCountryId(){
    this.subscription.push(
      this.countryService.getDefaultCountryId().subscribe((res) => {
        if(res){
          this.defaultCountryId = res;
          this.onPresentAddressDivisionNamesChangeByCounterId(res);
          this.onPermanentAddressDivisionNamesChangeByCounterId(res);
        }
      })
    )
  }

  resetPresentAddressForm(form?: NgForm) {
    this.empPresentAddressService.empPresentAddress = {
      id: 0,
      empId: this.empId,
      countryId: this.defaultCountryId,
      divisionId: null,
      districtId: null,
      upazilaId: null,
      thanaId: null,
      // unionId: null,
      // wardId: null,
      zipCode: null,
      address: "",
      email: "",
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
  resetPermanentAddressForm(form?: NgForm) {
    this.empPermanentAddressService.empPermanentAddress = {
      id: 0,
      empId: this.empId,
      countryId: this.defaultCountryId,
      divisionId: null,
      districtId: null,
      upazilaId: null,
      thanaId: null,
      zipCode: null,
      address: "",
      email: "",
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

  resetForm(){
    this.resetPresentAddressForm();
    this.resetPermanentAddressForm();
  }

  onPresentCheckboxChange(event: any): void {
    if (event.target.checked) {
      this.sameAsPresentAddress = true;
      const { id, ...formData } = this.EmpPresentAddressForm.form.value;
      if(this.EmpPresentAddressForm.form.value.countryId){
        this.onPermanentAddressDivisionNamesChangeByCounterId(this.EmpPresentAddressForm.form.value.countryId);
      }
      if(this.EmpPresentAddressForm.form.value.divisionId){
        this.onPermanentAddressDistrictNamesChangeByDivisionId(this.EmpPresentAddressForm.form.value.divisionId);
      }
      if(this.EmpPresentAddressForm.form.value.districtId){
        this.onPermanentAddressUpazilaNamesChangeByDistrictId(this.EmpPresentAddressForm.form.value.districtId);
      }
      if(this.EmpPresentAddressForm.form.value.upazilaId){
        this.onPermanentAddressThanaNamesChangeByUpazilaId(this.EmpPresentAddressForm.form.value.upazilaId);
      }
      // if(this.EmpPresentAddressForm.form.value.thanaId){
      //   this.onPermanentAddressUnionNamesChangeByThanaId(this.EmpPresentAddressForm.form.value.thanaId);
      // }
      // if(this.EmpPresentAddressForm.form.value.unionId){
      //   this.onPermanentAddressWardNamesChangeByUnionId(this.EmpPresentAddressForm.form.value.unionId);
      // }
      
      this.EmpPermanentAddressForm.form.patchValue(formData);
    } else {
      this.sameAsPresentAddress = false;
      this.resetPermanentAddressForm();
    }
  }

  loadcountris() {
  
    this.subscription.push(
       this.countryService.selectGetCountry().subscribe((data) => {
      this.countris = data;
    })
    )
   
  }
  onPresentAddressDivisionNamesChangeByCounterId(counterId: number) {
  
    this.subscription.push(
      this.divisionService.getDivisionByCountryId(counterId).subscribe((data) => {
      this.presentAddressdivisions = data;
    })
    )
    
  }
  onPresentAddressDistrictNamesChangeByDivisionId(divisionId: number) {
   
    this.subscription.push(
      this.districtService.getDistrictByDivisionId(divisionId).subscribe((data) => {
      this.presentAddressdistricts = data;
    })
    )
    
  }
  onPresentAddressUpazilaNamesChangeByDistrictId(districtId: number) {
    // this.subscription = 
    this.subscription.push(
     this.uapzilaService.getUpapzilaByDistrictId(districtId).subscribe((data) => {
      this.presentAddressupazilas = data;
    })
    )
   
  }
  onPresentAddressThanaNamesChangeByUpazilaId(upazilaId: number) {
   
    this.subscription.push(
      this.thanaService.getthanaNamesByUpazilaId(upazilaId).subscribe((data) => {
        this.presentAddressthanas = data;
      })
    )
    
  }
  onPresentAddressUnionNamesChangeByThanaId(thanaId: number) {
   
    this.subscription.push(
      this.unionService.getUnionNamesByThanaId(thanaId).subscribe((data) => {
        this.presentAddressunions = data;
      })
    )
   
  }
  onPresentAddressWardNamesChangeByUnionId(unionId: number) {
   
    this.subscription.push(
      this.wardService.getWardNamesByUnionId(unionId).subscribe((data) => {
        this.presentAddresswards = data;
      })
    )
    
  }
  onPermanentAddressDivisionNamesChangeByCounterId(counterId: number) {
    // this.subscription = 
    this.subscription.push(
      this.divisionService.getDivisionByCountryId(counterId).subscribe((data) => {
        this.permanentAddressdivisions = data;
      })
    )
   
  }
  onPermanentAddressDistrictNamesChangeByDivisionId(divisionId: number) {
   
    this.subscription.push(
      this.districtService.getDistrictByDivisionId(divisionId).subscribe((data) => {
        this.permanentAddressdistricts = data;
  
      })
    )
    
  }
  onPermanentAddressUpazilaNamesChangeByDistrictId(districtId: number) {
    // this.subscription = 
    this.subscription.push(
      this.uapzilaService.getUpapzilaByDistrictId(districtId).subscribe((data) => {
        this.permanentAddressupazilas = data;
      })
    )
    
  }
  onPermanentAddressThanaNamesChangeByUpazilaId(upazilaId: number) {
   
    this.subscription.push(
      this.thanaService.getthanaNamesByUpazilaId(upazilaId).subscribe((data) => {
        this.permanentAddressthanas = data;
  
      })
    )
    
  }
  onPermanentAddressUnionNamesChangeByThanaId(thanaId: number) {

    this.subscription.push(
      this.unionService.getUnionNamesByThanaId(thanaId).subscribe((data) => {
        this.permanentAddressunions = data;
      })
    )
    
  }
  onPermanentAddressWardNamesChangeByUnionId(unionId: number) {
    // this.subscription = 
    this.subscription.push(
      this.wardService.getWardNamesByUnionId(unionId).subscribe((data) => {
        this.permanentAddresswards = data;
  
      })
    )
    
  }


  onSubmit(EmpPresentAddressForm: NgForm, EmpPermanentAddressForm: NgForm,): void {
    this.loading = true;
    this.empPresentAddressService.cachedData = [];
    const presentAddressid = EmpPresentAddressForm.value.id;
    const presentAddressaction$ = presentAddressid
      ? this.empPresentAddressService.updateEmpPresentInfo(presentAddressid, EmpPresentAddressForm.value)
      : this.empPresentAddressService.saveEmpPresentInfo(EmpPresentAddressForm.value);
      
    this.empPermanentAddressService.cachedData = [];
    const permanentAddressid = EmpPermanentAddressForm.value.id;
    const permanentAddressaction$ = permanentAddressid
      ? this.empPermanentAddressService.updateEmpPermanentInfo(permanentAddressid, EmpPermanentAddressForm.value)
      : this.empPermanentAddressService.saveEmpPermanentInfo(EmpPermanentAddressForm.value);

    // this.subscription =
    this.subscription.push(
       permanentAddressaction$.subscribe((response: any) => {})
    )

    // this.subscription = 
    this.subscription.push(
      presentAddressaction$.subscribe((response: any) => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
          this.sameAsPresentAddress = false;
          this.getAddressInformation();
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
