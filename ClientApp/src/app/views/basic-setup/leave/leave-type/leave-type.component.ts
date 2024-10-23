import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {LeaveTypeService} from '../../service/leave-type.service'
import { ToastrService } from 'ngx-toastr';
import { Route, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { cilPencil, cilTrash} from "@coreui/icons"
import { LeaveType } from '../../model/leave-type';
import { NgForm } from '@angular/forms';
import { TimeScale } from 'chart.js';
import { Leave } from '../../model/Leave';

@Component({
  selector: 'app-leave-type',
  templateUrl: './leave-type.component.html',
  styleUrl: './leave-type.component.scss'
})
export class LeaveTypeComponent implements OnInit, OnDestroy {
 
  @ViewChild('leaveTypeForm', { static: true }) leaveTypeForm!: NgForm;
  loading: boolean;
  isUpdate: boolean;
  icons = {cilPencil, cilTrash}
  updateIndex: number;

  leaveTypes: any[] = [];
  
  constructor(public leaveTypeService:LeaveTypeService,
    private toastr: ToastrService,
    private router: Router,
    private confirmSerice: ConfirmService
   ) {
    this.loading = false;
    this.isUpdate = false;
    this.updateIndex = -1;
  }

  ngOnInit(): void {
    this.leaveTypeService.getLeaveTypes().subscribe({
      next: data => {
        this.leaveTypes = data;
      }
    })
  }

  ngOnDestroy(): void {
    
  }

  onSubmit() {
    this.isUpdate?this.onUpdate():this.saveLeaveType();
  }

  onUpdate() {
    this.loading = true;
    this.leaveTypeService.updateLeaveType(this.leaveTypeService.leaveTypes).subscribe({
      next: response => {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })

          this.leaveTypes[this.updateIndex].leaveTypeId = this.leaveTypeService.leaveTypes.leaveTypeId;
          this.leaveTypes[this.updateIndex].leaveTypeName = this.leaveTypeService.leaveTypes.leaveTypeName;
          this.leaveTypes[this.updateIndex].remark = this.leaveTypeService.leaveTypes.remark;
          this.leaveTypes[this.updateIndex].isActive = this.leaveTypeService.leaveTypes.isActive;

          this.onReset();
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },

      error: (err)=> {
        this.loading = false;
      },

      complete: ()=> {
        this.loading = false;
      }
    }) 
  }

  onDelete(id:number) {
    this.confirmSerice.confirm('Delete Confirmation','Are you sure?').subscribe(response=> {
      if(response) {
        this.leaveTypeService.deleteLeaveType(id).subscribe({
          next: response=> {
            if(response.success == true) {
              this.toastr.success('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })

              delete this.leaveTypes[this.leaveTypes.findIndex(x=>x.leaveTypeId == response.id)];
            } else {
              this.toastr.warning('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })
            }
          },
          error: err=> {
            console.log(err);
          }
        });
      }
    })
  }

  onReset() {
    this.updateIndex = -1;
    this.isUpdate = false;
    this.leaveTypeForm.reset();
    this.leaveTypeService.leaveTypes = new LeaveType();
    this.leaveTypeForm.form.patchValue(this.leaveTypeService.leaveTypes);
    this.leaveTypeService.leaveTypes.eLWorkDayCal = true;
  }

  saveLeaveType() {

    this.loading = true;
    this.leaveTypeService.createLeaveType(this.leaveTypeService.leaveTypes).subscribe({
      next: response => {
        if(response.success) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })

          this.leaveTypeService.cachedData = [];
          this.leaveTypeService.leaveTypes.leaveTypeId = response.id;

          let curData:LeaveType = new LeaveType();

          curData.leaveTypeId = response.id;
          curData.leaveTypeName = this.leaveTypeService.leaveTypes.leaveTypeName;
          curData.remark = this.leaveTypeService.leaveTypes.remark;
          curData.isActive = this.leaveTypeService.leaveTypes.isActive;

          this.leaveTypes.push(curData)
          this.onReset();
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (err)=> {
        this.loading = false;
      },
      complete: ()=> {
        this.loading = false;
      }
    })
  }

  updateButtonAction(data:any,index:number) {
    
    this.isUpdate = true;
    this.leaveTypeForm.form.patchValue(data);
    this.leaveTypeService.leaveTypes.leaveTypeId = data.leaveTypeId;
    this.updateIndex = index;
  }
}
