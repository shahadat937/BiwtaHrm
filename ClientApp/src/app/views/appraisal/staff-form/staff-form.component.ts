import { StaffFormServiceService } from './service/staff-form-service.service';

import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { delay, of, Subscription } from 'rxjs';
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
  reportDates: any[] = [];
  IdCardNo: string;
  empSubs: Subscription = new Subscription();
  empReqSub: Subscription = new Subscription();
  autoSetFields: any = [{fieldName:"Name",MapTo:"firstName"}, {fieldName: "Father Name",MapTo:"fatherName"},
    {fieldName: "Mother Name", MapTo:"motherName"},
    {fieldName: "Joining Date", MapTo: "joiningDate", Transform:"DateFormat"},
    {fieldName: "Designation", MapTo: "designation"},
    {fieldName: "Birthdate", MapTo: "birthDate", Transform: "DateFormat"},
    {fieldName: "Joining Date Of Current Designation", MapTo: "currentDesignationJoiningDate", Transform: "DateFormat"}
  ]
  
  constructor(
    private formRecordService: FormRecordService,
    public officerFormService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService
  ) {
    
    this.loading = false;
    this.submitLoading = false;
    this.currentSection = 0;
    this.IdCardNo = "";
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
    if(this.reportDates.length<2||this.reportDates[0]==null||this.reportDates[1]==null) {
      this.toastr.warning('',"Report Duration is required", {
        positionClass: 'toast-top-right'
      });
      this.submitLoading=false;
      return;
    }
    this.formData.reportFrom = this.reportDates[0];
    this.formData.reportTo = this.reportDates[1];
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

  getEmpInfo() {
    const source$ = of (this.IdCardNo);
    const delay$ = source$.pipe(
      delay(700)
    );

    if(this.empSubs) {
      this.empSubs.unsubscribe();
    }

    if(this.empReqSub) {
      this.empReqSub.unsubscribe();
    }

    if(this.IdCardNo.trim()=="") {
      return;
    }

    this.empSubs = delay$.subscribe(data=> {
      this.empReqSub = this.formRecordService.empInfo(data).subscribe({
        next: response=> {
          console.log(response);
          if(response.success==true) {
            this.processEmpInfo(response);
          } else {
            this.formData.empId = 0;
          }
        },
        error: (err)=> {
          this.formData.empId = 0;
        }
      })
    })

  }


  processEmpInfo(empInfo:any) {
    this.formData.empId = empInfo.empId;

    function findField(fieldName:string) {
      const compare = (data:any)=> {
        return data.fieldName == fieldName; 
      }

      return compare;
    }

    this.autoSetFields.forEach((field:any) => { 
      const result = this.formData.sections[0].fields.find(findField(field.fieldName));
      
      if(result!=undefined) {
        let fieldValue = empInfo[field.MapTo];
        if(field.Transform!=undefined&&field.Transform=="DateFormat") {
          fieldValue = fieldValue.split('T')[0];
        }
        result.fieldValue = fieldValue;
      }
    });
  }

}
