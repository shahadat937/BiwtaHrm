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

@Component({
  selector: 'app-officer-form',
  templateUrl: './officer-form.component.html',
  styleUrl: './officer-form.component.scss'
})
export class OfficerFormComponent implements OnInit, OnDestroy {

  formId:number = 1;
  loading: boolean;
  formData: any;
  subscription: Subscription = new Subscription();
  currentSection:number ;
  
  constructor(
    public officerFormService: OfficerFormService,
    private toastr: ToastrService,
    private confirmService: ConfirmService
  ) {
    this.loading = false;
    this.currentSection = 0;
  }

  ngOnInit(): void {
    this.getFormInfo(); 
  }


  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  getFormInfo() {
    this.loading = true;
    this.officerFormService.getFormInfo(this.formId).subscribe({
      next: response => {
        this.formData = response;
        console.log(this.formData);
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
}
