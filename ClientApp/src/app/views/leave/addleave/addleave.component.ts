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
import { LeaveBalanceService } from '../service/leave-balance.service';
import { environment } from 'src/environments/environment';

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
  defaultPhoto: string;
  employeePhoto: string;
  department: string;
  designation: string;
  empSubs: Subscription = new Subscription();
  empReqSub: Subscription = new Subscription();
  totalLeave:number|null;
  totalDue: number|null;
  CountryOption: any[] = [];
  isValidPMS: boolean;
  leaveBalances: any[] = [];
  imageUrl: string;

  reviewerPMIS: string;
  approverPMIS: string;
  constructor(
    public addLeaveService: AddLeaveService,
    private leaveBalanceService: LeaveBalanceService, 
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
    this.reviewerPMIS = "";
    this.approverPMIS = "";
    this.imageUrl = environment.imageUrl;
    this.defaultPhoto = environment.imageUrl + "EmpPhoto/default.jpg";
    this.employeePhoto = this.defaultPhoto;
    this.designation = "";
    this.department = "";
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
          this.isValidPMS = true;
          this.addLeaveService.addLeaveModel.empId = parseInt(this.authService.currentUserValue.empId);
          this.employeeName = response.firstName + " "+response.lastName;
          this.department = response.departmentName;
          this.designation = response.designationName;
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
      this.leaveBalances = [];
      this.employeePhoto = this.defaultPhoto;
      return;
    }

    this.empSubs = delay$.subscribe(data=> {
      this.empReqSub = this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response=> {
          if(response!=null) {
            this.employeeName = response.firstName + " "+response.lastName;
            this.addLeaveService.addLeaveModel.empId = response.id;
            this.isValidPMS = true;
            if(response.empPhotoName!="") {
              this.employeePhoto = this.imageUrl + "EmpPhoto/"+response.empPhotoName;
            } else {
              this.employeePhoto = this.defaultPhoto;
            }
            this.getLeaveAmount();
            this.getLeaveBalanceForAllType(response.id);
            this.department = response.departmentName;
            this.designation = response.designationName;
          } else {
            this.employeeName = "";
            this.addLeaveService.addLeaveModel.empId = null;
            this.isValidPMS = false
            this.getLeaveAmount();
            this.employeePhoto = this.defaultPhoto;
            this.department = "";
            this.designation = "";
          }
        },
        error: err=> {
          this.isValidPMS=false;
          this.employeeName = "";
          this.addLeaveService.addLeaveModel.empId = null;
          this.getLeaveAmount();
          this.employeePhoto = this.defaultPhoto;
          this.department = "";
          this.designation = "";
        }
      });
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
    this.IsForeignLeave();
    console.log(this.addLeaveService.addLeaveModel);
    if(this.addLeaveService.addLeaveModel.empId==null||this.addLeaveService.addLeaveModel.leaveTypeId==null||this.addLeaveService.addLeaveModel.fromDate==null||this.addLeaveService.addLeaveModel.toDate==null) {
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
    this.employeePhoto = this.defaultPhoto;
    this.totalDue = null;
    this.totalLeave = null;
    this.department = "";
    this.designation = "";

    this.leaveBalances = [];
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

  IsForeignLeave() {

    let leaveName = this.LeaveTypeOption.find(x=>x.id == this.addLeaveService.addLeaveModel.leaveTypeId);
    if(leaveName == undefined) {
      this.addLeaveService.addLeaveModel.isForeignLeave = false;
      return;
    }

    if(leaveName.name.toLocaleLowerCase().includes("foreign")) {
      this.addLeaveService.addLeaveModel.isForeignLeave = true;
    } else {
      this.addLeaveService.addLeaveModel.isForeignLeave = false;
    }
  }

  getLeaveBalanceForAllType(empId:number) {
    this.leaveBalanceService.getLeaveBalance(empId).subscribe({
      next: response => {
        this.leaveBalances = response;
      }
    })
  }

  onReviewerChange() {
    if(this.reviewerPMIS.trim()=="") {
      this.addLeaveService.addLeaveModel.reviewedBy = null;
      return;
    }

    const source$ = of (this.reviewerPMIS).pipe(
      delay(700)
    );

    if(this.subcription) {
      this.subcription.unsubscribe();
    }

    this.subcription = source$.subscribe(data => {
      this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response => {
          if(response!=null) {
            this.addLeaveService.addLeaveModel.reviewedBy = response.id;
          } else {
            this.addLeaveService.addLeaveModel.reviewedBy = null;
          }
        },
        error: (err) => {
          this.addLeaveService.addLeaveModel.reviewedBy = null;
        }
      })
    })
  }
  onApproverChange() {
    if(this.reviewerPMIS.trim()=="") {
      this.addLeaveService.addLeaveModel.approvedBy = null;
      return;
    }

    const source$ = of (this.approverPMIS).pipe(
      delay(700)
    );

    if(this.subcription) {
      this.subcription.unsubscribe();
    }

    this.subcription = source$.subscribe(data => {
      this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response => {
          if(response!=null) {
            this.addLeaveService.addLeaveModel.approvedBy = response.id;
          } else {
            this.addLeaveService.addLeaveModel.approvedBy = null;
          }
        },
        error: (err) => {
          this.addLeaveService.addLeaveModel.approvedBy = null;
        }
      })
    })
  }

  onImageError(event:any) {
    const target = event.target as HTMLImageElement;
    target.src = this.defaultPhoto;
  }

}
