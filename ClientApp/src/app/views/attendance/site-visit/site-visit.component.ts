import { Component, OnDestroy, OnInit } from '@angular/core';
import {SiteVisitModel} from "../models/site-visit-model";
import {SiteVisitService} from "../services/site-visit.service";
import { cilList, cilShieldAlt } from '@coreui/icons';
import { IconDirective } from '@coreui/icons-angular';
import { cilX,cilCheck,cilPencil,cilTrash } from '@coreui/icons';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { ConfirmService } from "../../../core/service/confirm.service";
import { ToastrService } from 'ngx-toastr';



@Component({
  selector: 'app-site-visit',
  templateUrl: './site-visit.component.html',
  styleUrl: './site-visit.component.scss'
})
export class SiteVisitComponent implements OnInit, OnDestroy{

  
  icons = {cilX,cilTrash,cilCheck, cilPencil}
  subscription:Subscription = new Subscription();
  EmpOption: any[] = [];

  tableData: any[] = [];
  btnText = "Add SiteVisit";
  submitBtnText = "Submit"
  isVisible: boolean = true;
  loading=false;

  constructor(
    public siteVisitService : SiteVisitService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {

  }


  ngOnInit(): void {
    this.getSiteVisit();
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
    this.subscription = this.siteVisitService.getSiteVisitAll().subscribe(data=> {
      this.tableData=data;
    }, error=> {
      console.log(error);
    })
  }

  onApprove(siteVisitId:number) {
    console.log(siteVisitId);
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
  }

  onDecline(siteVisitId:number) {
    console.log(siteVisitId);
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
  }

  onSubmit() {
    console.log("Hello World");
  }

  toggleSubmit() {
    this.isVisible=true;
  }

  toggleUpdate(element:any) {
    
  }

  onDelete(siteVisitId:number) {

  }

  onCancel() {
    this.isVisible = false;
  }
}
