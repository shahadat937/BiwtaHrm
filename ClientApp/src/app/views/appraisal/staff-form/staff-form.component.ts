import { StaffFormServiceService } from './service/staff-form-service.service';

import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { OfficerFormService } from '../officer-form/service/officer-form.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { FormRecordService } from '../services/form-record.service';

@Component({
  selector: 'app-staff-form',
  templateUrl: './staff-form.component.html',
  styleUrl: './staff-form.component.scss',
})
export class StaffFormComponent implements OnInit, OnDestroy {
  formId:number = 2;
  loading: boolean;
  submitLoading: boolean;
  formData: any;
  subscription: Subscription = new Subscription();
  currentSection:number ;
  
  constructor(
    private formRecordService: FormRecordService,
    public officerFormService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService
  ) {
    
    this.loading = false;
    this.submitLoading = false;
    this.currentSection = 0;
  }

  ngOnInit(): void {
    this.loading=true;
    this.getFormInfo(); 
  }


  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  getFormInfo() {
    this.officerFormService.getFormInfo(this.formId).subscribe({
      next: response => {
        this.formData=null;
        this.formData = response;
      },
      error: err => {
        console.log(err);
        this.loading = false;
      },
      complete: () => {
        this.loading = false
      }
    });
  }

  onChange() {
    console.log(this.formData);
  }

  onSubmit() {
    console.log("Hello World");
  }

  onReset() {
    this.getFormInfo();
  }

  saveFormData() {
    this.submitLoading=true;
    this.officerFormService.saveFormData(this.formData).subscribe({
      next: (response)=> {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
          this.formRecordService.cachedData=[];
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          });
        }
      },
      error: (err)=> {
        this.submitLoading=false;
      },
      complete: ()=> {
        this.submitLoading=false;
        console.log("complete");
      }
    })
  }



  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    let abc = {
      division: '',
      yearStartDate: new Date(),
      yearEndDate: new Date(),
      name: '',
      employeeCode: 0,
      fathersName: '',
      mothersName: '',
      dateofBirth: new Date(),
      designation: '',
      presentSalary: 0,
      scaleOfpay: '',
      joiningDate: new Date(),
      presentDesignationJoiningDate: new Date(),
      education: '',
      lingutstics: '',
      trainingSpecialTraining: '',
      reportinFromDate: new Date(),
      reportingEndDate: new Date(),
    };
  }
}
