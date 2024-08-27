import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormRecordService } from '../services/form-record.service';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-update-form',
  templateUrl: './update-form.component.html',
  styleUrl: './update-form.component.scss'
})
export class UpdateFormComponent implements OnInit, OnDestroy {
  @Input() formRecordId: number;
  loading:boolean;
  updateLoading:boolean
  subscription: Subscription = new Subscription();
  formData:any;

  constructor(
    public formRecordService: FormRecordService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private modalService: BsModalService,
    private modalRef: BsModalRef
  ) {
    this.loading=false;
    this.updateLoading=false;
    this.formRecordId=0;
  }

  ngOnInit(): void {
    this.loading=true;
    this.getFormData();
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  onHide() {
    this.modalRef.hide();
  }

  getFormData() {
    this.formRecordService.getFormData(this.formRecordId).subscribe({
      next: (response)=> {
        this.formData = response;
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


  updateFormData() {
    this.updateLoading=true;
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
        this.updateLoading=false;
      },
      complete: () => {
        this.updateLoading=false;
      }
    })
  }
  onReset() {

  }
}
