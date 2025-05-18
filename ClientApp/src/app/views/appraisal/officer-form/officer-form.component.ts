import { Division } from './../../basic-setup/model/division';
import { ActivatedRoute, Router } from '@angular/router';
import { SharedService } from './service/shared.service';
import { NgForm } from '@angular/forms';
import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {OfficerFormService} from './service/officer-form.service'
import { ToastrService } from 'ngx-toastr';
import {ConfirmService} from '../../../core/service/confirm.service'
import { concat, concatMap, delay, of, Subscription } from 'rxjs';
import { FieldComponent } from '../field/field.component';
import { FormRecordService } from '../services/form-record.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UpdateFormComponent } from '../update-form/update-form.component';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';
import { HttpParams } from '@angular/common/http';
import {AppraisalRole} from '../enum/appraisal-role';
import { environment } from '../../../../environments/environment';
import { cilArrowLeft, cilSearch } from '@coreui/icons';
import {AuthService} from '../../../core/service/auth.service'
import {EmpPhotoSignComponent} from './../../employee/add-employee/employee-informations/emp-photo-sign/emp-photo-sign.component';
import { EmployeeInfoListModalComponent } from '../../employee/employee-info-list-modal/employee-info-list-modal.component';
import { ChangeProfileComponent } from '../../profile/change-profile/change-profile.component';
import { UserNotification } from '../../notifications/models/user-notification';
import { NotificationService } from '../../notifications/service/notification.service';
import { EmpJobDetailsModule } from '../../employee/model/emp-job-details.module';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';




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
  @Input()
  IsUpdate: boolean
  appraisalRole = AppraisalRole;
  IdCardNo:string;
  currentEmp: number;
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
  icons = {cilArrowLeft,cilSearch};

  EmpOption: any[] = []

  reportDates:any[]= [];

  department: string;
  @Input()
  firstSection:number;
  @Input()
  lastSection:number;


  currentEmpSignatureUrl: string | null;
  imageUrl = environment.imageUrl 


  // Reporting Officer And Counter Signatory Officer Name 
  reportingOfficerName: string = "";
  counterSignatoryOfficername: string = "";

  // authentication
  @Input()
  featureName: string;
  featurePermission: FeaturePermission = new FeaturePermission();

  constructor(
    private notificationService: NotificationService,
    private empBasicInfoService: EmpBasicInfoService,
    private empJobDetailService: EmpJobDetailsService,
    private bsModalService: BsModalService,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private roleFeatureService: RoleFeatureService,
    private empService: EmpBasicInfoService,
    private empPhotoSignService: EmpPhotoSignService,
    private formRecordService: FormRecordService,
    public officerFormService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private modalService: BsModalService
  ) {
   
    this.currentEmp = 0;
    this.currentEmpSignatureUrl = null;
    this.IdCardNo = "";
    this.loading = false;
    this.submitLoading = false;
    this.currentSection = 0;
    this.ActiveSection = [true,true,true,true,true,true,true];
    this.formRecordId = 0;
    this.showHeader = false
    this.department = "ICT"
    this.updateRole = AppraisalRole.User
    this.IsUpdate = false;
    this.formId = environment.officerFormId
    this.firstSection = 0;
    this.lastSection = 6;
    this.submitButtonText = "Submit";
    this.featureName = "officerForm";
  }

  ngOnInit(): void {
    this.getPermission();
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


  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission(this.featureName).subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        // To do
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
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
        const subs = this.authService.currentUser.subscribe(user => {
          if(user&&user.empId) {
            let empId = parseInt(user.empId);
            this.empService.findByEmpId(empId).subscribe({
              next: response => {
                this.IdCardNo = response.idCardNo;
                this.getEmpInfo();
                this.getPhotoInfo(response.id);
              }
            })
          }
        })

        this.subscription.push(subs);
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


    if(!this.IsUpdate) {
      this.getFormInfo();
    } else {
      this.getFormData();
    }
    this.reportDates = [];
  }

  saveFormData() {
    this.submitLoading=true;

    if(this.formRecordId!=0) {
      if(this.featurePermission.update==false) {
        this.roleFeatureService.unauthorizeAccress();
        this.submitLoading=false;
        return;
      }
      this.updateFormData();
      return;
    }

    if(this.featurePermission.add==false) {
      this.roleFeatureService.unauthorizeAccress();
      this.submitLoading = false;
      return;
    }


    if(this.formData.reportFrom ==null||this.formData.reportTo==null) {
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
          
          // Send notification to reporting officer
          this.formData.recordId = response.id;
          this.sendOfficerNotification(AppraisalRole.User,this.formData);

          this.formRecordService.cachedData = [];
          this.router.navigate(['/appraisal/MyFormRecord']);
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
        if(response) {
          signature = response.signatureUrl;
          if(signature==null||signature=="")
            return;
          for(let i = 0;i<this.formData.sections.length;i++) {
            for(let j = 0; j<this.formData.sections[i].fields.length;j++) {
              let field = this.formData.sections[i].fields[j];
              if(field.fieldTypeName=="signaturePhoto"&&this.ActiveSection[i]&&(field.fieldValue==null||field.fieldValue=='')) {
                field.fieldValue = signature;
              }
            }
          }

        }
      }
    })

    return signature;
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

          this.sendUserNotification(this.updateRole,this.formData);
          this.sendOfficerNotification(this.updateRole,this.formData);
          
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
        this.authService.currentUser.subscribe(user => {
          if(user&&user.empId) {
            let empId = parseInt(user.empId);
            this.empService.findByEmpId(empId).subscribe({
              next: response => {
                //this.IdCardNo = response.idCardNo;
                //this.getEmpInfo();
                this.getPhotoInfo(response.id);
              }
            })
          }
        })
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

  openReportingOfficerModal() {

    const modalRef: BsModalRef = this.bsModalService.show(EmployeeInfoListModalComponent,{backdrop: 'static', class: 'modal-xl'});

      if (modalRef.content) {
      modalRef.content?.employeeSelected.subscribe((employee: any) => {
        // this.empBasicInfoService.getEmpInfoByCard(idCardNo).subscribe({
        //   next: response => {
        //     if(response) {
        //       this.formData.reportingOfficerId = response.id;
        //       this.reportingOfficerName = [response.firstName,response.lastName].join(' ');
        //     } else {
        //       this.formData.reportingOfficerId = null;
        //       this.reportingOfficerName = "";
        //     }
        //   },
        //   error: (err) => {
        //     this.formData.reportingOfficerId = null;
        //     this.reportingOfficerName = "";
        //   }
        // })
        if (employee) {
          this.formData.reportingOfficerId = employee.id;
          this.reportingOfficerName = [employee.firstName, employee.lastName].join(' ');
          this.formData.reportingOfficerDepartmentId = employee.departmentId;
          this.formData.reportingOfficerSectionId = employee.sectionId;
          this.formData.reportingOfficerDesignationId = employee.designationId;
          this.formData.reportingOfficerResponsibilityTypeId = employee.additionalResponsibilityId;
        } else {
          this.formData.reportingOfficerId = null;
          this.reportingOfficerName = "";
        }
      })
    }
  }

  openCounterSignatoryOfficerModal() {
    const modalRef: BsModalRef = this.bsModalService.show(EmployeeInfoListModalComponent,{backdrop: 'static', class: 'modal-xl'});

    if (modalRef.content) {
      modalRef.content?.employeeSelected.subscribe((employee: any) => {
        // this.empBasicInfoService.getEmpInfoByCard(idCardNo).subscribe({
        //   next: response => {
        //     if(response) {
        //       this.formData.counterSignatoryId = response.id;
        //       this.counterSignatoryOfficername = [response.firstName,response.lastName].join(' ');
        //     } else {
        //       this.formData.counterSignatoryId = null;
        //       this.counterSignatoryOfficername = "";
        //     }
        //   },
        //   error: (err) => {
        //     this.formData.counterSignatoryId = null;
        //     this.counterSignatoryOfficername = "";
        //   }
        // })
        console.log(employee)
        if (employee) {
          this.formData.counterSignatoryId = employee.id;
          this.counterSignatoryOfficername = [employee.firstName, employee.lastName].join(' ');
          this.formData.counterSignatoryDepartmentId = employee.departmentId;
          this.formData.counterSignatorySectionId = employee.sectionId;
          this.formData.counterSignatoryDesignationId = employee.designationId;
          this.formData.counterSignatoryResponsibilityTypeId = employee.additionalResponsibilityId;
        }
        else {
          this.formData.counterSignatoryId = null;
          this.counterSignatoryOfficername = "";
        }
      })
    }
  }


  uploadSignature(): void {

    let empId = parseInt(this.authService.currentUserValue.empId)
    if(empId==null||empId==undefined) {
      return;
    }
    const initialState = {
      id: empId,
      clickedButton: 'ChangeSignature'
    };
    const modalRef: BsModalRef = this.modalService.show(ChangeProfileComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      modalRef.onHide.subscribe(() => {
        this.getPhotoInfo(empId);
      });
    }
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

  sendUserNotification(progress:number,formData:any) {
    const userNotification = new UserNotification();

    if(progress == AppraisalRole.ReportingOfficer) {
      userNotification.fromEmpId=formData.reportingOfficerId;
      userNotification.message = "Reporting Officer submission has been done."
    } else if(progress == AppraisalRole.CounterSignatory) {
      userNotification.fromEmpId=formData.counterSignatoryId;
      userNotification.message = "Counter Signatory submission has been done."
    } else if(progress == AppraisalRole.Receiver) {
      userNotification.fromEmpId = this.authService.currentUserValue.empId || 0;
      userNotification.message = "Appraisal is received by the receiver.";
    }
    userNotification.toEmpId = formData.empId;
    userNotification.forEntryId = formData.recordId;
    userNotification.featurePath = "MyFormRecord"
    userNotification.nevigateLink = "/appraisal/MyFormRecord";
    userNotification.title = "Appraisal";

    this.notificationService.submit(userNotification).subscribe(()=> {});

  }

  sendOfficerNotification(role: number, formData:any) {
    const userNotification = new UserNotification();

    userNotification.fromEmpId = formData.empId;
    userNotification.title = "Appraisal";
    userNotification.message = "New Appraisal Form is available"
    userNotification.forEntryId = formData.recordId;

    if(role == AppraisalRole.User) {
      userNotification.toEmpId = formData.reportingOfficerId;
      userNotification.featurePath = "manageFormOfficerRF";
      userNotification.nevigateLink ="/appraisal/manageFormOfficerRF";
    } else if(role == AppraisalRole.ReportingOfficer) {
      userNotification.toEmpId = formData.counterSignatoryId;
      userNotification.featurePath = "manageFormOfficerCs";
      userNotification.nevigateLink = "/appraisal/manageFormOfficerCs"
    } else if(role == AppraisalRole.CounterSignatory) {
      userNotification.featurePath = "manageFormOfficerR";
      userNotification.nevigateLink = "/appraisal/manageFormOfficerR";
    }

    if(role == AppraisalRole.CounterSignatory) {
      this.empJobDetailService.findByEmpId(formData.empId).subscribe(data => {
        userNotification.toDeptId = data.departmentId;

        this.notificationService.submit(userNotification).subscribe(()=> {});
      })
    } else {
      this.notificationService.submit(userNotification).subscribe(()=> {});
    }
  }
}
