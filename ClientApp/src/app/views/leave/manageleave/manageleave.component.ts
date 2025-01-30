import { Component, Input, OnChanges, OnDestroy, OnInit, SimpleChanges } from '@angular/core';
import {ManageLeaveService} from '../service/manage-leave.service'
import { Subscription } from 'rxjs';
import { deepObjectsMerge } from '@coreui/utils';
import { LeaveModel } from '../models/leave-model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { LeaveDetailViewComponent } from './leave-detail-view/leave-detail-view.component';
import { HttpParams } from '@angular/common/http';
import { cilZoom } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { LeaveStatus } from '../enum/leave-status';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-manageleave',
  templateUrl: './manageleave.component.html',
  styleUrl: './manageleave.component.scss'
})
export class ManageleaveComponent implements OnInit, OnDestroy, OnChanges {
  icons = {cilZoom}
  loading: boolean ;
  DepartmentOption: any[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  selectedDepartment: number|null;
  leaves: any[] = [];
  leaveStatusOptions: any [] = [];
  selectedLeave: LeaveModel;
  @Input() LeaveFilterParams: any;
  @Input() CanApprove: boolean;
  @Input() Role: string = "Reviewer"
  @Input() refreshLink : string|null;

  @Input()
  featureName: string;

  featurePermission: FeaturePermission = new FeaturePermission();

  leaveStatus = LeaveStatus 

  constructor(
    public leaveService: ManageLeaveService,
    private route: ActivatedRoute,
    private router: Router,
    private roleFeatureService: RoleFeatureService,
    private modalService: BsModalService,
    private confirmService: ConfirmService,
    private toastr : ToastrService
  ) {
    this.loading = false;
    this.selectedDepartment = null;
    this.selectedLeave = new LeaveModel();
    this.LeaveFilterParams = {};
    this.CanApprove = true;
    this.refreshLink = null;
    this.featureName = "manageleave";
  }


  ngOnInit(): void {
    this.getPermission();
    this.getDepartmentOption();
    this.getLeaves();

    this.subscription.push(
       this.leaveService.getLeaveStatusOption().subscribe( {
      next: option =>  {
        this.leaveStatusOptions = option;
      }
    })
    )
   
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

  ngOnChanges(changes: SimpleChanges): void {
    this.getLeaves();
  }

  getInputEventValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  getDepartmentOption() {
    this.subscription.push(
      this.leaveService.getSelectedDepartment().subscribe({
      next: option => {
        this.DepartmentOption = option;
      }
    }) 
    )
    
  }

  getLeaves() {
    let params = new HttpParams();

    for(const key of Object.keys(this.LeaveFilterParams)) {
      if(key=="Status") {
        for(const item of this.LeaveFilterParams[key]) {
          params = params.append(key, item);
        }
        continue;
      }
      params = params.set(key, this.LeaveFilterParams[key]);
    }

    this.subscription.push(
      this.leaveService.getLeaveByFilter(params).subscribe({
      next: response=> {
        this.leaves = response;
      
      },
      error: error=> {
        console.log(error);
      }
    })
    )
    
  }

  selectLeave(leave:LeaveModel) {
    this.selectedLeave = leave;
  }


  onViewDetail(leaveRequestId:number) {

    interface LeaveDetailViewModalConfig {
      leaveRequestId: number;
      CanApprove: boolean;
      Role: string
    }
    const initialState:LeaveDetailViewModalConfig = {
      leaveRequestId : leaveRequestId,
      CanApprove: this.CanApprove&&this.featurePermission.update,
      Role: this.Role
    };
    const modalRef: BsModalRef = this.modalService.show(LeaveDetailViewComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      modalRef.onHide.subscribe(() => {
        this.getLeaves();
      });
    }
  }

  onDelete(leaveRequestId:number) {

    if(this.featurePermission.delete==false) {
      this.roleFeatureService.unauthorizeAccress();
      return;
    }

   this.subscription.push(
     this.confirmService.confirm('Delete Confirmation','Are you sure?').subscribe({
      next: response => {
        if(response) {
          this.loading = true;
          this.subscription.push(
            this.leaveService.deleteLeaveRequest(leaveRequestId).subscribe({
            next: (response) => {
              if(response.success) {
                this.toastr.success('',`${response.message}`, {
                  positionClass: 'toast-top-right'
                })

                this.leaves = this.leaves.filter(item => item.leaveRequestId != leaveRequestId);
              } else {
                this.toastr.warning('',`${response.message}`, {
                  positionClass: 'toast-top-right'
                })
              }
            },
            error: (err)=> {
              console.log(err);
              this.loading = false;
            },
            complete: () => {
              this.loading = false;
            }
          })
          )
           

        }
      }
    })
   )
   

  }
}
