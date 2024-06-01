import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmployeeService } from '../../../service/employee.service';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrl: './personal-information.component.scss'
})
export class PersonalInformationComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  fullName: string = '';
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  employeeType: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  @ViewChild('PersonalInfoForm', { static: true }) PersonalInfoForm!: NgForm;

  constructor(public bsModalRef: BsModalRef,
    public employeeService: EmployeeService,) { }

  ngOnInit(): void {
    this.getEmployeeByEmpId();
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
    this.employeeService.findByEmpId(this.empId).subscribe((res) => {
      if (res.genderId) {
        this.headerText = 'Update Personal Information';
        this.PersonalInfoForm?.form.patchValue(res);
        console.log(res)
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add Personal Information';
        this.btnText = 'Submit';
        this.initaialForm();
      }
    });
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.employeeService.personalInfo = {
      empId : this.empId,
      genderId : null ,
      maritalStatusId : null ,
      bloodGroupId : null ,
      nationalatyId : null ,
      religionId : null ,
      hairColorId : null ,
      eyesColor : null ,
      mabileNumber : null ,
      email : '',
      birthRegNo : null ,
      tinNo : null ,
      drivingLicenceNo : null ,
      drivingLicenceDate : null,
      height : '',
      weight : '',
      passportNo : '',
      passportExpireDate : null,
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
      mabileNumber : null ,
      email : '',
      birthRegNo : null ,
      tinNo : null ,
      drivingLicenceNo : null ,
      drivingLicenceDate : null,
      height : '',
      weight : '',
      passportNo : '',
      passportExpireDate : null,
    });
  }

  cancel() {
    this.close.emit();
  }

  onSubmit(form: NgForm): void {

  }
}
