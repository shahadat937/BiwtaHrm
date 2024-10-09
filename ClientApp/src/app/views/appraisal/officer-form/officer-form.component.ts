import { Division } from './../../basic-setup/model/division';
import { Router } from '@angular/router';
import { SharedService } from './service/shared.service';
import { NgForm } from '@angular/forms';
import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {OfficerFormService} from './service/officer-form.service'
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { delay, of, Subscription } from 'rxjs';
import { FieldComponent } from '../field/field.component';
import { FormRecordService } from '../services/form-record.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UpdateFormComponent } from '../update-form/update-form.component';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';

@Component({
  selector: 'app-officer-form',
  templateUrl: './officer-form.component.html',
  styleUrl: './officer-form.component.scss'
})
export class OfficerFormComponent implements OnInit, OnDestroy {

  @Input()
  ActiveSection:boolean[];
  @Input()
  formRecordId;
  IdCardNo:string;
  formId:number = 1;
  loading: boolean;
  submitLoading: boolean;
  formData: any;
  subscription: Subscription = new Subscription();
  currentSection:number ;
  empSubs: Subscription = new Subscription();
  empReqSub: Subscription = new Subscription();
  autoSetFields: any = [{fieldName:"Name",MapTo:"firstName"}, {fieldName: "Father Name",MapTo:"fatherName"},
    {fieldName: "Mother Name", MapTo:"motherName"},
    {fieldName: "Joining Date", MapTo: "joiningDate", Transform:"DateFormat"},
    {fieldName: "Designation", MapTo: "designation"},
    {fieldName: "Birthdate", MapTo: "birthDate", Transform: "DateFormat"},
    {fieldName: "Joining Date Of Current Designation", MapTo: "currentDesignationJoiningDate", Transform: "DateFormat"}
  ]

  reportDates:any[]= [];
  
  constructor(
    private empPhotoSignService: EmpPhotoSignService,
    private formRecordService: FormRecordService,
    public officerFormService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private modalService: BsModalService
  ) {
    
    this.IdCardNo = "";
    this.loading = false;
    this.submitLoading = false;
    this.currentSection = 0;
    this.ActiveSection = [true,true,true,true,true,true,true];
    this.formRecordId = 0;
  }

  ngOnInit(): void {
    this.loading=true;

    if(this.formRecordId==0) {
      this.getFormInfo(); 
    } else {
      this.getFormData();
    }
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

    if(this.formRecordId!=0) {
      this.updateFormData();
      return;
    }

    console.log(this.reportDates);
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

          this.formRecordService.cachedData = [];
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

  onUpdate(formRecordId:number) {
    const initialState = {
      formRecordId : formRecordId
    } 

    this.modalService.show(UpdateFormComponent,{initialState:initialState});
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

  getPhotoInfo(empId:number) {
    let signature;

    this.empPhotoSignService.findByEmpId(empId).subscribe({
      next: response => {
        signature = response;
      }
    })
  }


  updateFormData() {
    this.submitLoading=true;
    if(this.reportDates.length<2) {
      this.toastr.warning('',"Report Duration is required", {
        positionClass: 'toast-top-right'
      });
      return;
    }

    this.formData.reportFrom = this.reportDates[0];
    this.formData.reportTo = this.reportDates[1];
    
    this.formRecordService.updateFormData(this.formData).subscribe({
      next: response=> {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: err=> {
        this.submitLoading=false;
      },
      complete: () => {
        this.submitLoading=false;
      }
    })
  }

  getFormData() {
    this.formRecordService.getFormData(this.formRecordId).subscribe({
      next: (response)=> {
        this.formData = response;
        let datefrom = new Date(this.formData.reportFrom);
        let dateto = new Date(this.formData.reportTo);
        this.reportDates.push(datefrom);
        this.reportDates.push(dateto);
        console.log(this.reportDates);
        this.loading=false;
      },
      error: (err)=> {
        console.log(err);
        this.loading=false;
      },
      complete:()=>  {
        this.loading=false;
      }
    })
  }

}
