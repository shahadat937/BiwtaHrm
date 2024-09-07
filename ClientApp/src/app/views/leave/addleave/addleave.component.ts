import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {AddLeaveService} from '../service/add-leave.service';
import { delay, of, Subscription } from 'rxjs';
import { NgFor } from '@angular/common';
import { NgForm } from '@angular/forms';
import { HttpParams } from '@angular/common/http';
import { AddLeaveModel } from '../models/add-leave-model';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-addleave',
  templateUrl: './addleave.component.html',
  styleUrl: './addleave.component.scss'
})
export class AddleaveComponent  implements OnInit, OnDestroy{
  subcription: Subscription = new Subscription();
  loading: boolean;
  LeaveTypeOption:any[] = [];
  @ViewChild("addLeaveForm",{static:true}) addLeaveForm!: NgForm
  empCardNo:string ;
  employeeName: string;
  empSubs: Subscription = new Subscription();
  empReqSub: Subscription = new Subscription();
  totalLeave:number|null;
  totalDue: number|null;
  CountryOption: any[] = [];
  isValidPMS: boolean;

  constructor(
    public addLeaveService: AddLeaveService, 
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private authService: AuthService
  ) {
    this.loading = false;
    this.empCardNo ="";
    this.employeeName = "";
    this.totalDue = null;
    this.totalLeave = null;
    this.isValidPMS = false;
  }

  ngOnInit(): void {
    this.addLeaveService.getSelectedLeaveType().subscribe({
      next: option => {
        this.LeaveTypeOption = option;
      }
    })


    if(this.authService.currentUserValue.empId!=null) {
      this.addLeaveService.getEmpById(parseInt(this.authService.currentUserValue.empId)).subscribe({
        next: response => {
          this.empCardNo = response.idCardNo;
          this.addLeaveService.addLeaveModel.empId = parseInt(this.authService.currentUserValue.empId);
          this.employeeName = response.firstName + " "+response.lastName;
        }
      })
    }

    this.getCountry();
  }

  onEmpIdChange(event:any) {
    const source$ = of (this.empCardNo);
    const delay$ = source$.pipe(
      delay(800)
    );

    if(this.empSubs) {
      this.empSubs.unsubscribe();
    }

    if(this.empReqSub) {
      this.empReqSub.unsubscribe();
    }

    if(this.empCardNo.trim()=="") {
      this.employeeName = "";
      this.addLeaveService.addLeaveModel.empId = null;
      this.getLeaveAmount();
      return;
    }

    this.empSubs = delay$.subscribe(data=> {
      this.empReqSub = this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response=> {
          if(response!=null) {
            this.employeeName = response.firstName + " "+response.lastName;
            this.addLeaveService.addLeaveModel.empId = response.id;
            this.isValidPMS = true;
            this.getLeaveAmount();
          } else {
            this.employeeName = "";
            this.addLeaveService.addLeaveModel.empId = null;
            this.isValidPMS = false
            this.getLeaveAmount();
          }
        },
        error: err=> {
          this.isValidPMS=false;
          this.employeeName = "";
          this.addLeaveService.addLeaveModel.empId = null;
          this.getLeaveAmount();
        }
      })
    })

  }

  ngOnDestroy(): void {
    if(this.subcription) {
      this.subcription.unsubscribe();
    }
    if(this.empReqSub) {
      this.empReqSub.unsubscribe();
    }

    if(this.empSubs) {
      this.empReqSub.unsubscribe();
    }
    this.addLeaveService.addLeaveModel = new AddLeaveModel();
  }

  onDateChange() {
    this.getLeaveAmount();
    this.getWorkingDays();
  }

  getLeaveAmount() {
    console.log(this.addLeaveService.addLeaveModel);
    if(this.addLeaveService.addLeaveModel.empId==null||this.addLeaveService.addLeaveModel.leaveTypeId==null||this.addLeaveService.addLeaveModel.fromDate==null||this.addLeaveService.addLeaveModel.toDate==null) {
      console.log("Hello World");
      this.totalDue = null;
      return;
    }

    let params = new HttpParams();
    params = params.set("empId",this.addLeaveService.addLeaveModel.empId);
    params = params.set("leaveTypeId", this.addLeaveService.addLeaveModel.leaveTypeId);
    params = params.set("fromDate", this.addLeaveService.addLeaveModel.fromDate);
    params = params.set("toDate", this.addLeaveService.addLeaveModel.toDate);

    this.subcription = this.addLeaveService.getLeaveAmount(params).subscribe({
      next: response=> {
        this.totalDue = response.totalDue;
      },
      error: err=> {
        this.totalDue =null;
      }
    })

    this.getWorkingDays();
  }

  getCountry() {
    this.subcription = this.addLeaveService.getSelectedCountry().subscribe({
      next: response=> {
        this.CountryOption = response;
      }
    })
  }

  getWorkingDays() {

    if(this.addLeaveService.addLeaveModel.fromDate==null||this.addLeaveService.addLeaveModel.toDate==null) {
      return;
      this.totalLeave = null;
    }
    let params = new HttpParams();
    params = params.set("From", this.addLeaveService.addLeaveModel.fromDate);
    params = params.set("To", this.addLeaveService.addLeaveModel.toDate);
    this.subcription = this.addLeaveService.getWorkingDays(params).subscribe({
      next: response => {
        this.totalLeave = response;
      }
    })
  }


  onSubmit() {
    this.loading = true;
    if(this.addLeaveService.addLeaveModel.countryId!=null) {
      this.addLeaveService.addLeaveModel.isForeignLeave = true;
    }
    let formData = this.convertToFormData(this.addLeaveService.addLeaveModel,["AssociatedFiles"]);
    this.addLeaveService.createLeaveRequest(formData).subscribe({
      next: response=> {
        if(response.success==true) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
          this.onReset();
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (error)=> {
        this.loading=false;
        /*this.toastr.warning('',`${error}`,{
          positionClass: 'toast-top-right'
        })*/
      },
      complete: ()=> {
        this.loading=false;
      }
    })
    



    let output = '';
    formData.forEach((value, key) => {
        output += `${key}: ${value}\n`;
    });

    // Print the form data to the console
    console.log(output);
  }

  onReset() {
    this.addLeaveForm.reset();
    this.isValidPMS=false;
    this.addLeaveService.addLeaveModel = new AddLeaveModel();
    this.loading = false;
    this.empCardNo ="";
    this.employeeName = "";
    this.totalDue = null;
    this.totalLeave = null;
  }

  convertToFormData(model: any, fileFields: string[] = []): FormData {
    const formData = new FormData();
    for (const key in model) {
      if (model.hasOwnProperty(key)) {
        if (fileFields.includes(key)) {
          formData.append(key, model[key], model[key]);
        } else if (model[key] != null) {
          formData.append(key, model[key]);
        }
      }
    }

    return formData;
  }

  handleFile(event:any) {
    this.addLeaveService.addLeaveModel.associatedFiles = event.target.files[0];
  }
}
