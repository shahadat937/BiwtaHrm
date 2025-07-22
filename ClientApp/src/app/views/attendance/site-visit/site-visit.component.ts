import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {SiteVisitModel} from "../models/site-visit-model";
import {SiteVisitService} from "../services/site-visit.service";
import { cilList, cilSearch, cilShieldAlt, cilZoom } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { cilX,cilCheck,cilPencil,cilTrash } from '@coreui/icons';
import { delay, of, Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ConfirmService } from "../../../core/service/confirm.service";
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { update } from 'lodash-es';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { HttpParams } from '@angular/common/http';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';
import { UserNotification } from '../../notifications/models/user-notification';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { NotificationService } from '../../notifications/service/notification.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { Feature } from '../../featureManagement/model/feature';
import { SharedService } from '../../../shared/shared.service';



@Component({
  selector: 'app-site-visit',
  templateUrl: './site-visit.component.html',
  styleUrl: './site-visit.component.scss'
})
export class SiteVisitComponent implements OnInit, OnDestroy{

  @Input() IsUser: boolean;
  @Input() filter: any;
  icons = {cilX,cilTrash,cilCheck, cilPencil,cilZoom, cilSearch};
  // subscription:Subscription = new Subscription();
  subscription: Subscription[]=[]
  EmpOption: any[] = [];
  @ViewChild('siteVisitForm', { static: true }) siteVisitForm!: NgForm;
  tableData: any[] = [];
  btnText = "Add SiteVisit";
  submitBtnText = "Submit"
  isVisible: boolean = false;
  isUpdate: boolean = false;
  loading=false;
  statusOption = ["Approved","Declined"]
  empIdCardNo: string;
  empName: string;
  isValidPMIS: boolean;
  routelink : string = "";

  @Input()
  featureName: string;
  featurePermission: FeaturePermission = new FeaturePermission();

  constructor(
    public siteVisitService : SiteVisitService,
    private authService : AuthService,
    private notificationService: NotificationService,
    private empJobDetailService: EmpJobDetailsService,
    private modalService: BsModalService,
    private empBasicInfoService: EmpBasicInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public roleFeatureService: RoleFeatureService,
    private sharedService: SharedService

  ) {
    this.IsUser = false;
    this.filter = {};
    this.empIdCardNo = "";
    this.empName = "";
    this.isValidPMIS = false;
    this.featureName = "siteVisit";
  }

  resetEmp() {
    this.empName = "";
    this.isValidPMIS = false;
    this.siteVisitService.model.empId = null;  
  }


  getInputEventValue(event:Event) {
    return (event.target as HTMLInputElement).value;
  }
  ngOnInit(): void {

    this.getPermission();

    this.route.queryParams.subscribe(data => {
      if(data['forNotificationId']) {
        this.filter['siteVisitId'] = data['forNotificationId'];
        this.routelink = this.router.url.split('?')[0];
        this.getSiteVisit();
      } else {
        this.filter['siteVisitId'] = null;
        this.routelink = "";
        this.getSiteVisit();
      }
    });

    this.getSiteVisit();
    this.getEmpOption();

    if(this.IsUser) {
      this.roleFeatureService.getFeaturePermission("manageSiteVisit").subscribe(response=> {
      })

      this.siteVisitService.model.empId = JSON.parse(JSON.stringify(this.filter)).EmpId; 
    } else {
      this.roleFeatureService.getFeaturePermission("siteVisit").subscribe(response=> {
      })
    }
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission(this.featureName).subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == false){
        this.router.navigate(['/dashboard']);
        this.roleFeatureService.unauthorizeAccress();
      }
    })
    )
  }

  ngOnDestroy(): void {
    if(this.subscription!=null) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }
  getBadgeColor(status:string) {
    switch(status) {
      case 'Approved':
        return "success";
      case 'Pending':
        return "warning";
      case 'Declined':
        return "danger";

    }

    return "primary";
  }

  getSiteVisit() {
    let params = new HttpParams();

    for(const key of Object.keys(this.filter)) {
      if(this.filter[key]==null) {
        continue;
      }
      if(key=="Status") {
        for(const item of this.filter[key]) {
          params = params.append(key, item);
        }
        continue;
      }
      params = params.set(key, this.filter[key]);
    }


    if(!params.get('EmpId')&&this.IsUser) {
      return;
    }
    this.siteVisitService.getSiteVisitByFilter(params).subscribe({
      next:response => {
        this.tableData = response;
      }
    });
  }

  onApprove(siteVisitId:number) {
    console.log(siteVisitId);

    this.confirmService.confirm("Confirm Approval", "Are you sure?").subscribe((result)=> {
      if(!result) {
        return;
      }

      this.siteVisitService.approveSiteVisit(siteVisitId).subscribe(response=> {
        if(response.success==true) {
          this.toastr.success('',`${response.message}`,{
            positionClass: 'toast-top-right'
          });

          var index = this.tableData.findIndex(item=>item.siteVisitId===siteVisitId);
          this.tableData[index].status = "Approved";
          this.notifyUser(true,index);

        } else {
          this.toastr.error('',`${response.message}`, {
            positionClass: 'toast-top-right'
          });
        }
      })

    })
    
  }

  onDecline(siteVisitId:number) {

    this.confirmService.confirm("Confirm Decline","Are you sure?").subscribe((result)=> {
      if(!result) {
        return;
      }
      this.siteVisitService.declineSiteVisit(siteVisitId).subscribe(response=> {
        if(response.success==true) {
          this.toastr.success('',`${response.message}`,{
            positionClass: 'toast-top-right'
          });
          var index = this.tableData.findIndex(item=>item.siteVisitId===siteVisitId);
          this.tableData[index].status = "Declined";
          this.notifyUser(false,index);
        } else {
          this.toastr.error('',`${response.message}`, {
            positionClass: 'toast-top-right'
          });
        }
      })
    })
  }

  onSubmit(form:NgForm) {
    form.control.removeControl("empIdCardNo");
    form.control.removeControl("empName");
    this.isUpdate?this.onSiteVisitUpdate(form):this.onSiteVisitCreate(form);
    
  }

  toggleSubmit() {
    if(this.roleFeatureService.featurePermission.add == true){
      this.isVisible=true;
    }
    else{
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  toggleUpdate(element:any) {
    this.isUpdate = true;
    this.isVisible = true;
    this.isValidPMIS = true;
    element.fromDate = this.sharedService.parseDate(element.fromDate);
    element.toDate = this.sharedService.parseDate(element.toDate);
    this.siteVisitForm?.form.patchValue(element);
    this.siteVisitService.model.empId = element.empId;
    this.empName = [element.firstName,element.lastName].join(' ');
    this.empIdCardNo = element.idCardNo;

  }

  onDelete(siteVisitId:number) {

    this.confirmService.confirm("Confirm Delete Message","Are you sure?").subscribe((result)=> {
      if(!result) {
        return;
      }
      this.siteVisitService.delete(siteVisitId).subscribe({
        next: (response)=> {
          if(response.success==true) {
            this.toastr.success('',`${response.message}`,{
              positionClass: 'toast-top-right'
            })
            //var index = this.tableData.findIndex((item)=> item.siteVisitId === siteVisitId);
            this.tableData = this.tableData.filter((item)=>item.siteVisitId != siteVisitId);
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right'
            })
          }
        },
        error: (error)=> {
          this.toastr.error('',error, {
            positionClass: 'toast-top-right'
          })
        }
      });

    })
  }

  onCancel() {
    this.isVisible = false;
    this.isUpdate = false;
    this.siteVisitForm.reset(); 
    this.siteVisitService.model = new SiteVisitModel();
    this.resetEmp();

    if(this.IsUser) {
      this.siteVisitService.model.empId = JSON.parse(JSON.stringify(this.filter)).EmpId;
      this.onEmpIdChange(false);
    }

  }

  getEmpOption() {
    this.siteVisitService.getEmpOption().subscribe(option=> {
      this.EmpOption= option;
    })
  }

  onSiteVisitCreate(form:NgForm) {
    this.loading = true;
    
    form.value.fromDate = this.sharedService.formatDateOnly(form.value.fromDate);
    form.value.toDate = this.sharedService.formatDateOnly(form.value.toDate);

    const formData = {
      ...form.value,
      "empId": this.siteVisitService.model.empId
    }

    console.log(formData)

    this.siteVisitService.submit(formData).subscribe({
      next: (result)=> {
        if(result.success==true) {
          this.toastr.success("",`${result.message}`, {
            positionClass:'toast-top-right'
          })

          //Notify the approver
          if(this.siteVisitService.model.empId)
          this.notifyApprover(this.siteVisitService.model.empId,result.id);

          this.siteVisitService.cachedData = [];
          this.getSiteVisit();
          this.siteVisitForm.reset();
        } else {
          this.toastr.warning('',`${result.message}`, {
            positionClass: 'toast-top-right'
          })
          
        }
      },
      error: err=> {
        this.toastr.error('',err,{
          positionClass: 'toast-top-right'
        })
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    });
  }

  notifyApprover(empId:number,responseId:number) {
    const userNotification = new UserNotification;
    userNotification.fromEmpId = empId;
    userNotification.toDeptId = 0;
    userNotification.featurePath = "siteVisit";
    userNotification.nevigateLink = "/attendance/siteVisit";
    userNotification.forEntryId = responseId;
    userNotification.title = "Site Visit"
    userNotification.message = "requested sitevisit, Approval pending.";
    console.log(userNotification);
    this.empJobDetailService.findByEmpId(empId).subscribe(response => {
      userNotification.toDeptId = response.departmentId;
      this.notificationService.submit(userNotification).subscribe(()=> {});
    })

  }

  notifyUser(IsApproved:boolean,index:number) {
    const data = this.tableData[index];
    let empId = this.authService.currentUserValue.empId || 0;

    const userNotification = new UserNotification();
    userNotification.fromEmpId = empId;
    userNotification.toEmpId = data.empId;
    userNotification.forEntryId = data.siteVisitId;
    userNotification.featurePath = "manageSiteVisit";
    userNotification.nevigateLink = "/attendance/manageSiteVisit";
    userNotification.title = "Site Visit";
    userNotification.message = IsApproved==true?"Your sitevisit is approved.":"Your sitevisit is denied";

    this.notificationService.submit(userNotification).subscribe(()=>{});
  }




  onSiteVisitUpdate(form:NgForm) {
    this.loading = true;
    
    form.value.fromDate = this.sharedService.formatDateOnly(form.value.fromDate);
    form.value.toDate = this.sharedService.formatDateOnly(form.value.toDate);

    let updateValue= form.value;
    updateValue['empId'] = this.siteVisitService.model.empId;
    this.siteVisitService.update(updateValue).subscribe({
      next: (response)=> {
        if(response.success==true) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
          
          let index = this.tableData.findIndex(item=>item.siteVisitId == form.value.siteVisitId);
          this.siteVisitService.getSiteVisitById(updateValue.siteVisitId).subscribe(data=> {
            this.tableData[index].fromDate = data.fromDate;
            this.tableData[index].toDate = data.toDate;
            this.tableData[index].visitPlace = data.visitPlace;
            this.tableData[index].visitPurpose = data.visitPurpose;
            this.tableData[index].status = data.status;
            this.tableData[index].remark = data.remark;
          });

          
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (err)=> {
        this.toastr.error('',err, {
          positionClass: 'toast-top-right'
        })
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    })
  }

  onEmpIdChange(delayedRequest:boolean = true) {
    console.log("Emp Id changed");
    const delayTime = delayedRequest ?700:0;
    const source$ = of (this.empIdCardNo).pipe(
      delay(delayTime)
    );

    if(this.empIdCardNo.trim()=="") {
      this.resetEmp();
      return;
    }

    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }

   this.subscription.push(
    source$.subscribe(data => {
      this.empBasicInfoService.getEmpInfoByCard(data).subscribe({
        next: (response:any) => {

          if(response) {
            this.empName = [response.firstName,response.lastName].join(' ');
            this.siteVisitService.model.empId = response.id;
            this.isValidPMIS = true;
          } else {
            this.resetEmp();
          }
        },
        error: err => {
          this.resetEmp();
        }
      })
    })
   )
    
    
  }

  openEmployeeModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });

    modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
      if(idCardNo){
          this.empIdCardNo = idCardNo;
          this.onEmpIdChange(false);
      }
    });
  }
}
