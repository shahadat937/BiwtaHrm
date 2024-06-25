import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmpPersonalInfoService } from '../../../service/emp-personal-info.service';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrl: './personal-information.component.scss'
})
export class PersonalInformationComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  genders: SelectedModel[] = [];
  maritalStatuses: SelectedModel[] = [];
  bloodGroups: SelectedModel[] = [];
  religions: SelectedModel[] = [];
  hairColors: SelectedModel[] = [];
  eyeColors: SelectedModel[] = [];
  relations: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  @ViewChild('PersonalInfoForm', { static: true }) PersonalInfoForm!: NgForm;

  constructor(public bsModalRef: BsModalRef,
    public empPersonalInfoService: EmpPersonalInfoService,
    public userService: UserService,
    private toastr: ToastrService,) { }

  ngOnInit(): void {
    this.getEmployeeByEmpId();
    this.getSelectedGenders();
    this.getSelectedMaritalStatuses();
    this.getSelectedBloodGroups();
    this.getSelectedReligions();
    this.getSelectedHairColors();
    this.getSelectedEyeColors();
    this.getSelectedRelation();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getEmployeeByEmpId() {
    this.empPersonalInfoService.findByEmpId(this.empId).subscribe((res) => {
      this.PersonalInfoForm?.form.patchValue(res);
      if (res) {
        this.headerText = 'Update Personal Information';
        console.log(res)
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add Personal Information';
        this.btnText = 'Submit';
        this.initaialForm();
        this.getUserDetails();
      }
    });
  }

  getUserDetails(){
    this.empPersonalInfoService.findBasicInfoByEmpId(this.empId).subscribe((res) => {
      this.userService.find(res.aspNetUserId).subscribe((response) =>{
        this.PersonalInfoForm.form.patchValue({
          mobileNumber: response.phoneNumber,
          email: response.email,
        });
      })
    })
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empPersonalInfoService.personalInfo = {
      id: 0,
      empId : this.empId,
      genderId : null ,
      maritalStatusId : null ,
      bloodGroupId : null ,
      nationalityId : null ,
      religionId : null ,
      hairColorId : null ,
      eyesColor : null ,
      mobileNumber : '' ,
      fatherName: '',
      fatherNameBangla: '',
      motherName: '',
      motherNameBangla: '',
      gurdianName: '',
      gurdianNameBangla: '',
      gurdianRelationId: null,
      emergencyContact: '',
      email : '',
      birthRegNo : null ,
      placeOfBirth : '',
      tinNo : null ,
      drivingLicenceNo : null ,
      drivingLicenceDate : null,
      height : '',
      weight : '',
      passportNo : '',
      passportExpireDate : null,
      remark: '',
    };
  }

  resetForm() {
    this.PersonalInfoForm.form.reset();
    this.PersonalInfoForm.form.patchValue({
      empId : this.empId,
      genderId : null ,
      maritalStatusId : null ,
      bloodGroupId : null ,
      nationalatyId : null ,
      religionId : null ,
      hairColorId : null ,
      eyesColor : null ,
      mobileNumber : null ,
      fatherName: '',
      fatherNameBangla: '',
      motherName: '',
      motherNameBangla: '',
      gurdianName: '',
      gurdianNameBangla: '',
      gurdianRelationId: null,
      emergencyContact: '',
      email : '',
      birthRegNo : null ,
      placeOfBirth : '',
      tinNo : null ,
      drivingLicenceNo : null ,
      drivingLicenceDate : null,
      height : '',
      weight : '',
      passportNo : '',
      passportExpireDate : null,
      remark: '',
    });
  }

  
  getSelectedGenders(){
    this.subscription=this.empPersonalInfoService.getSelectedGender().subscribe((data) => { 
      this.genders = data;
    });
  }
  getSelectedMaritalStatuses(){
    this.subscription=this.empPersonalInfoService.getSelectedMaritalStatus().subscribe((data) => { 
      this.maritalStatuses = data;
    });
  }
  getSelectedBloodGroups(){
    this.subscription=this.empPersonalInfoService.getSelectedBloodGroup().subscribe((data) => { 
      this.bloodGroups = data;
    });
  }
  getSelectedReligions(){
    this.subscription=this.empPersonalInfoService.getSelectedReligion().subscribe((data) => { 
      this.religions = data;
    });
  }
  getSelectedHairColors(){
    this.subscription=this.empPersonalInfoService.getSelectedHairColor().subscribe((data) => { 
      this.hairColors = data;
    });
  }
  getSelectedEyeColors(){
    this.subscription=this.empPersonalInfoService.getSelectedEyeColor().subscribe((data) => { 
      this.eyeColors = data;
    });
  }
  getSelectedRelation(){
    this.subscription=this.empPersonalInfoService.getSelectedRelationType().subscribe((data) => { 
      this.relations = data;
    });
  }

  cancel() {
    this.close.emit();
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.empPersonalInfoService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empPersonalInfoService.updateEmpPersonalInfo(id, form.value)
      : this.empPersonalInfoService.saveEmpPersonalInfo(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.cancel();
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    });
  }
}
