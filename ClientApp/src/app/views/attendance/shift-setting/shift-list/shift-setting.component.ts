import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilPencil, cilTrash } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { ShiftSettingService } from '../../services/shift-setting.service';
import { TreeShift } from '../../models/tree-shift';
import { FeaturePermission } from 'src/app/views/featureManagement/model/feature-permission';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ShiftTypeModalComponent } from '../shift-modal/shift-type-modal/shift-type-modal.component';
import { ShiftSettingModalComponent } from '../shift-modal/shift-setting-modal/shift-setting-modal.component';

@Component({
  selector: 'app-shift-setting',
  templateUrl: './shift-setting.component.html',
  styleUrl: './shift-setting.component.scss',
})
export class ShiftSettingComponent implements OnInit, OnDestroy {

  icons = { cilArrowLeft, cilPlus, cilPencil, cilTrash };
  subscription: Subscription[]=[];
  expandedRows = {};
  featurePermission : FeaturePermission = new FeaturePermission;

  treeShiftInfo : TreeShift[] = [];

  constructor(
      private route: ActivatedRoute,
      private router: Router,
      private confirmService: ConfirmService,
      private toastr: ToastrService,
      public roleFeatureService: RoleFeatureService,
      public shiftSettingService: ShiftSettingService,
      private modalService: BsModalService,
    ) {
  }
  
  ngOnInit(): void {
    this.getPermission();
  }


  
  getPermission(){
    this.roleFeatureService.getFeaturePermission('shift-setting').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.getTreeShiftInfo();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    });
  }

  getTreeShiftInfo(){
    this.subscription.push(this.shiftSettingService.getTreeShiftType().subscribe((res) => {
      this.treeShiftInfo = res;
    }))
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }
  
  manageShiftTypeModal(id: number, clickedButton: string){
      if(clickedButton == "Create" && this.featurePermission.add == true || clickedButton == "Edit" && this.featurePermission.update == true){
        const initialState = {
          id: id,
          clickedButton: clickedButton
        };
        const modalRef: BsModalRef = this.modalService.show(ShiftTypeModalComponent, { initialState, backdrop: 'static' });
    
        if (modalRef.onHide) {
          modalRef.onHide.subscribe(() => {
            this.getTreeShiftInfo();
          });
        }
      }
      else {
        this.roleFeatureService.unauthorizeAccress();
      }
  }
  
  manageShiftSettingModal(id: number, clickedButton: string){
    if(clickedButton == "Create" && this.featurePermission.add == true || clickedButton == "Edit" && this.featurePermission.update == true){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(ShiftSettingModalComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        modalRef.onHide.subscribe(() => {
          this.getTreeShiftInfo();
        });
      }
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  deleteShiftType(shiftType: TreeShift) {
    if(this.featurePermission.delete == true){
      this.subscription.push(
        this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.shiftSettingService.deleteShiftType(shiftType.id).subscribe(
            (res: any) => {
              if(res.success){
                this.treeShiftInfo = this.treeShiftInfo.filter((val) => val.id !== shiftType.id);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      })
      )
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  // delete(element: any) {
  //     if(this.featurePermission.delete == true){
  //       this.subscription.push(
  //         this.confirmService
  //       .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
  //       .subscribe((result) => {
  //         if (result) {
  //           this.featureManagementService.delete(element.moduleId).subscribe(
  //             (res) => {
  //               const index = this.dataSource.data.indexOf(element);
  //               this.toastr.warning('Delete Successfull', ` `, {
  //                 positionClass: 'toast-top-right',
  //               });
  //               if (index !== -1) {
  //                 this.dataSource.data.splice(index, 1);
  //                 this.dataSource = new MatTableDataSource(this.dataSource.data);
  //               }
  //             },
  //             (err) => {
  //               this.toastr.error('Somethig Wrong ! ', ` `, {
  //                 positionClass: 'toast-top-right',
  //               });
  //             }
  //           );
  //         }
  //       })
  //       )
        
  //     }
  //     else {
  //       this.roleFeatureService.unauthorizeAccress();
  //     }
  //   }

}
