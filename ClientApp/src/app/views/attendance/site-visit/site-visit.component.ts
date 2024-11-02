import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {SiteVisitModel} from "../models/site-visit-model";
import {SiteVisitService} from "../services/site-visit.service";
import { cilList, cilShieldAlt, cilZoom } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { cilX,cilCheck,cilPencil,cilTrash } from '@coreui/icons';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ConfirmService } from "../../../core/service/confirm.service";
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { update } from 'lodash-es';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { HttpParams } from '@angular/common/http';



@Component({
  selector: 'app-site-visit',
  templateUrl: './site-visit.component.html',
  styleUrl: './site-visit.component.scss'
})
export class SiteVisitComponent implements OnInit, OnDestroy{

  @Input() IsUser: boolean;
  @Input() filter: any;
  icons = {cilX,cilTrash,cilCheck, cilPencil,cilZoom};
  subscription:Subscription = new Subscription();
  EmpOption: any[] = [];
  @ViewChild('siteVisitForm', { static: true }) siteVisitForm!: NgForm;
  tableData: any[] = [];
  btnText = "Add SiteVisit";
  submitBtnText = "Submit"
  isVisible: boolean = false;
  isUpdate: boolean = false;
  loading=false;
  statusOption = ["Approved","Declined"]

  constructor(
    public siteVisitService : SiteVisitService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public roleFeatureService: RoleFeatureService,

  ) {
    this.IsUser = false;
    this.filter = {};
  }


  getInputEventValue(event:Event) {
    return (event.target as HTMLInputElement).value;
  }
  ngOnInit(): void {
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

  ngOnDestroy(): void {
    if(this.subscription!=null) {
      this.subscription.unsubscribe();
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

    if(this.IsUser==false) {
      this.subscription = this.siteVisitService.getSiteVisitAll().subscribe(data=> {
        this.tableData=data;
      }, error=> {
        console.log(error);
      })

    } else {

      let params = new HttpParams();

      for(const key of Object.keys(this.filter)) {
        if(key=="Status") {
          for(const item of this.filter[key]) {
            params = params.append(key, item);
          }
          continue;
        }
        params = params.set(key, this.filter[key]);
      }


      if(!params.get('EmpId')) {
        return;
      }
      this.siteVisitService.getSiteVisitByFilter(params).subscribe({
        next:response => {
          this.tableData = response;
        }
      });
    }
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
        } else {
          this.toastr.error('',`${response.message}`, {
            positionClass: 'toast-top-right'
          });
        }
      })
    })
  }

  onSubmit(form:NgForm) {
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
    this.siteVisitForm?.form.patchValue(element);
    this.siteVisitService.model.empId = element.empId;

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
            var index = this.tableData.findIndex((item)=> item.siteVisitId === siteVisitId);
            delete this.tableData[index];
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
    this.siteVisitService.model = new SiteVisitModel();
    this.siteVisitForm.reset(); 

    if(this.IsUser) {
      this.siteVisitService.model.empId = JSON.parse(JSON.stringify(this.filter)).EmpId;
      this.siteVisitForm.control.patchValue({empId:this.filter.EmpId});
    }

  }

  getEmpOption() {
    this.siteVisitService.getEmpOption().subscribe(option=> {
      this.EmpOption= option;
    })
  }

  onSiteVisitCreate(form:NgForm) {
    this.loading = true;
    console.log(form.value);
    this.siteVisitService.submit(form.value).subscribe({
      next: (result)=> {
        if(result.success==true) {
          this.toastr.success("",`${result.message}`, {
            positionClass:'toast-top-right'
          })
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

  onSiteVisitUpdate(form:NgForm) {
    this.loading = true;
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
}
