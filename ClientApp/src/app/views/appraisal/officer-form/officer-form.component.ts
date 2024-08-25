import { Division } from './../../basic-setup/model/division';
import { Router } from '@angular/router';
import { SharedService } from './service/shared.service';
import { NgForm } from '@angular/forms';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import {OfficerFormService} from './service/officer-form.service'
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { Subscription } from 'rxjs';
import { FieldComponent } from '../field/field.component';
import { FormRecordService } from '../services/form-record.service';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UpdateFormComponent } from '../update-form/update-form.component';

@Component({
  selector: 'app-officer-form',
  templateUrl: './officer-form.component.html',
  styleUrl: './officer-form.component.scss'
})
export class OfficerFormComponent implements OnInit, OnDestroy {

  formId:number = 1;
  loading: boolean;
  submitLoading: boolean;
  formData: any;
  subscription: Subscription = new Subscription();
  currentSection:number ;
  
  constructor(
    private formRecordService: FormRecordService,
    public officerFormService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private modalService: BsModalService
  ) {
    
    this.loading = false;
    this.submitLoading = false;
    this.currentSection = 0;
  }

  ngOnInit(): void {
    this.loading=true;
    this.getFormInfo(); 
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
}
