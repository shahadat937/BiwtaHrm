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
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';
import { HttpParams } from '@angular/common/http';
import {AppraisalRole} from '../enum/appraisal-role';
import { environment } from 'src/environments/environment';
import { cilArrowLeft } from '@coreui/icons';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-officer-form',
  templateUrl: './officer-form.component.html',
  styleUrl: './officer-form.component.scss'
})
export class OfficerFormComponent implements OnInit, OnDestroy {
  @ViewChild('officerForm') officerForm!: NgForm;
  @Input()
  ActiveSection:boolean[];
  @Input()
  formRecordId;
  @Input()
  showHeader: boolean;
  @Input()
  updateRole: number;
  appraisalRole = AppraisalRole;
  IdCardNo:string;
  formId:number;
  loading: boolean;
  submitLoading: boolean;
  @Input()
  submitButtonText: string;
  formData: any;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
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
  icons = {cilArrowLeft};

  EmpOption: any[] = []

  reportDates:any[]= [];

  department: string;
  @Input()
  firstSection:number;
  @Input()
  lastSection:number;
  
  constructor(
    private authService: AuthService,
    private empService: EmpBasicInfoService,
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
    this.showHeader = false
    this.department = "ICT"
    this.updateRole = AppraisalRole.User
    this.formId = environment.officerFormId
    this.firstSection = 0;
    this.lastSection = 6;
    this.submitButtonText = "Submit";
  }

  ngOnInit(): void {
    console.log(this.firstSection);
    console.log(this.lastSection);
    this.loading=true;
    this.subscription.push(
      this.empService.getFilteredSelectedEmp(new HttpParams()).subscribe({
      next: response => {
        this.EmpOption = response
      }
    })
    )
    

    if(this.formRecordId==0) {
      this.getFormInfo(); 
      this.subscription.push(
        this.authService.currentUser.subscribe(user => {
        if(user&&user.empId) {
          let empId = parseInt(user.empId);
          this.empService.findByEmpId(empId).subscribe({
            next: response => {
              this.IdCardNo = response.idCardNo;
              this.getEmpInfo();
            }
          })
        }
      })
      )
      
    } else {
      this.getFormData();
    }

  }


  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    } 
  }

  getFormInfo() {
    this.subscription.push(
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
    })
    )
    
  }

  onChange() {
    console.log(this.formData);
  }

  onSubmit() {
    console.log("Hello World");
  }

  onReset() {
    this.officerForm.form.reset();
    this.getFormInfo();
    this.reportDates = [];
  }

  saveFormData() {
    this.submitLoading=true;

    if(this.formRecordId!=0) {
      this.updateFormData();
      return;
    }

    if(this.formData.reportFrom==null||this.formData.reportTo==null) {
      this.toastr.warning('',"Report Duration is required", {
        positionClass: 'toast-top-right'
      });
      this.submitLoading=false;
      return;
    }
    //if(this.reportDates.length<2||this.reportDates[0]==null||this.reportDates[1]==null) {
    //  this.toastr.warning('',"Report Duration is required", {
    //    positionClass: 'toast-top-right'
    //  });
    //  this.submitLoading=false;
    //  return;
    //}
    //this.formData.reportFrom = this.hrmdateResize(this.reportDates[0]); 
    //this.formData.reportTo = this.hrmdateResize(this.reportDates[1]);
    this.subscription.push(
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
      }
    })
    )
    
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

    this.subscription.push(
       this.empSubs = delay$.subscribe(data=> {
      this.empReqSub = this.formRecordService.empInfo(data).subscribe({
        next: response=> {
          if(response.success==true) {
            this.processEmpInfo(response);
          } else {
            this.formData.empId = 0;
            this.toastr.warning('',`Employee (${data}) wasn't found`, {
              positionClass: 'toast-top-right'
            });
            this.resetAutofield();
          }
        },
        error: (err)=> {
          this.formData.empId = 0;
          this.resetAutofield();
        }
      })
    })
    )
   

  }

  resetAutofield() {
    function findField(fieldName:string) {
      const compare = (data:any)=> {
        return data.fieldName == fieldName; 
      }

      return compare;
    }
    this.autoSetFields.forEach((field:any)=> {
      const result = this.formData.sections[0].fields.find(findField(field.fieldName))

      if(result!=undefined) {
        result.fieldValue="";
      }
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
        if(field.Transform!=undefined&&field.Transform=="DateFormat"&&fieldValue!=null) {
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

    //this.formData.reportFrom = this.hrmdateResize(this.reportDates[0]);
    //this.formData.reportTo = this.hrmdateResize(this.reportDates[1]);
    
    this.formRecordService.updateFormData(this.formData, this.updateRole).subscribe({
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

  goBack() {
    window.history.back();
  }


 hrmdateResize(formDateValue:any){
   let EntryDate="";
   var month;
   var day;
   var dateObj = new Date(formDateValue);
   var dObj=dateObj.toLocaleDateString().split('/');
   month=parseInt(dObj[0]);
   day=parseInt(dObj[1]);
   if(month<10){
     month='0'+month;
   }
   if(day<10){
     day='0'+day;
   }
 
   EntryDate =dObj[2]+'-'+month+'-'+day;
   return EntryDate;
  }


}
