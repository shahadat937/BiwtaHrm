import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
import { forEach } from 'lodash-es';
import { cilSearch } from '@coreui/icons';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ManageLeaveService } from '../service/manage-leave.service';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';
import { LeaveStatus } from '../enum/leave-status';
import { Router } from '@angular/router';
import { NotificationService } from '../../notifications/service/notification.service';
import { UserNotification } from '../../notifications/models/user-notification';

@Component({
  selector: 'app-addleave',
  templateUrl: './addleave.component.html',
  styleUrl: './addleave.component.scss'
})
export class AddleaveComponent  implements OnInit, OnDestroy{
  baseImageUrl: string;
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
  leaveFiles : any[] = [];
  @Input()
  leaveData: any;
  filteredLeaveBalances: any[] = [];
  imageUrl: string;
  uploadedFiles: any[] = [];
  @Input()
  IsReadonly: boolean
  buttonTitle: string;

  leaveStatus = LeaveStatus;
  reviewerPMIS: string;
  approverPMIS: string;
  reviewerName: string;
  approverName: string;
  icons = {cilSearch}
  constructor(
    private empBasicInfoService: EmpBasicInfoService,
    private modalService: BsModalService,
    private manageLeaveService: ManageLeaveService,
    private router : Router,
    public addLeaveService: AddLeaveService,
    private leaveBalanceService: LeaveBalanceService, 
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private authService: AuthService,
    public notificationService: NotificationService,
  ) {
    this.loading = false;
    this.empCardNo ="";
    this.employeeName = "";
    this.totalDue = null;
    this.totalLeave = null;
    this.isValidPMS = false;
    this.reviewerPMIS = "";
    this.approverPMIS = "";
    this.reviewerName = "";
    this.approverName = "";
    this.imageUrl = environment.imageUrl;
    this.defaultPhoto = environment.imageUrl + "EmpPhoto/default.jpg";
    this.employeePhoto = this.defaultPhoto;
    this.designation = "";
    this.department = "";
    this.IsReadonly = false;
    this.leaveData = null;
    this.buttonTitle = "Submit";
    this.baseImageUrl = environment.imageUrl;
  }

  ngOnInit(): void {
    if(this.IsReadonly) {
      this.addLeaveService.addLeaveModel=this.FillLeaveDataToAddLeaveModel(this.addLeaveService.addLeaveModel);
      this.filterLeaveBalance();

      if(this.leaveData.reviewedBy) {
        this.empBasicInfoService.findByEmpId(this.leaveData.reviewedBy).subscribe({
          next: response => {
            this.reviewerPMIS = response.idCardNo
            this.reviewerName = [response.firstName, response.lastName].join(' ');
          }
        })
      }

      if(this.leaveData.approvedBy) {
        this.empBasicInfoService.findByEmpId(this.leaveData.approvedBy).subscribe({
          next: response => {
            this.approverPMIS = response.idCardNo
            this.approverName = [response.firstName, response.lastName].join(' ');
          }
        })
      }

      this.getLeaveFiles();

    }
    this.addLeaveService.getSelectedLeaveType().subscribe({
      next: option => {
        this.LeaveTypeOption = option;
      }
    })

    if(this.IsReadonly) {
      this.buttonTitle = "Update";
    }

    if(this.authService.currentUserValue.empId!=null&&this.IsReadonly==false) {
      this.addLeaveService.getEmpById(parseInt(this.authService.currentUserValue.empId)).subscribe({
        next: response => {
          this.empCardNo = response.idCardNo;
          this.isValidPMS = true;
          this.addLeaveService.addLeaveModel.empId = parseInt(this.authService.currentUserValue.empId);
          this.employeeName = [response.firstName,response.lastName].join(' ');
          this.department = response.departmentName;
          this.designation = response.designationName;

          if(response.empPhotoName!="") {
              this.employeePhoto = this.imageUrl + "EmpPhoto/"+response.empPhotoName;
          } else {
              this.employeePhoto = this.defaultPhoto;
          }
          this.getLeaveBalanceForAllType(response.id);
        }
      })
    }

    this.getCountry();
  }

  onEmpIdChange() {
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
      this.department = "";
      this.designation = "";
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
            this.employeeName = [response.firstName,response.lastName].join(' ');
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
    if(this.addLeaveService.addLeaveModel.empId!=null) {
      this.getLeaveBalanceForAllType(this.addLeaveService.addLeaveModel.empId);
    }
  }

  getLeaveAmount() {
    this.filterLeaveBalance();
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
    if(this.addLeaveService.addLeaveModel.leaveTypeId!=null) {
      params = params.set("leaveTypeId",this.addLeaveService.addLeaveModel.leaveTypeId);
    }
    this.subcription = this.addLeaveService.getWorkingDays(params).subscribe({
      next: response => {
        this.totalLeave = response;
      }
    })
  }


  onSubmit() {
    this.loading = true;

    if(this.IsReadonly) {
      this.onUpdate();
      return;
    }

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

          // For User Notification
          const userNotification = new UserNotification();
          userNotification.fromEmpId = this.addLeaveService.addLeaveModel.empId;
          userNotification.toEmpId = this.addLeaveService.addLeaveModel.reviewedBy;
          userNotification.featurePath = 'reviewleave';
          userNotification.nevigateLink = '/leave/reviewleave';
          userNotification.forEntryId = response.id;
          userNotification.title = 'Leave Application';
          userNotification.message = 'submitted leave application, Review pending.';
          this.notificationService.submit(userNotification).subscribe((res) => {});

          
          this.onReset();
          this.router.navigate(['/leave/personalleave']);
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
    this.filteredLeaveBalances = [];
  }

  convertToFormData(model: any, fileFields: string[] = []): FormData {
    const formData = new FormData();
    for (const key in model) {
      if (model.hasOwnProperty(key)&&model[key]!=null) {
        if(key=="associatedFiles") {
          model[key].forEach((data:any)=> {
            formData.append(key,data);
          })
          continue;
        }

        formData.append(key, model[key]);
      }
    }

    return formData;
  }

  handleFile(event:any) {
    const files: FileList = event.target.files;
    this.addLeaveService.addLeaveModel.associatedFiles = Array.from(files); 
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
    let params = new HttpParams();
    params = params.set('empId',empId);

    if(this.addLeaveService.addLeaveModel.fromDate!=null) {
      params = params.set('leaveStartDate',this.addLeaveService.addLeaveModel.fromDate);
    }

    if(this.addLeaveService.addLeaveModel.toDate!=null) {
      params = params.set('leaveEndDate', this.addLeaveService.addLeaveModel.toDate);
    }

    this.leaveBalanceService.getLeaveBalance(params).subscribe({
      next: response => {
        this.leaveBalances = response;
        this.filteredLeaveBalances = this.leaveBalances;
        this.filterLeaveBalance();
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
            this.reviewerName = [response.firstName, response.lastName].join(' ');
          } else {
            this.addLeaveService.addLeaveModel.reviewedBy = null;
            this.reviewerName = "";
          }
        },
        error: (err) => {
          this.addLeaveService.addLeaveModel.reviewedBy = null;
          this.reviewerName = ""
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
            this.approverName = [response.firstName, response.lastName].join(' ');
          } else {
            this.addLeaveService.addLeaveModel.approvedBy = null;
            this.approverName = "";
          }
        },
        error: (err) => {
          this.addLeaveService.addLeaveModel.approvedBy = null;
          this.approverName = "";
        }
      })
    })
  }

  onImageError(event:any) {
    const target = event.target as HTMLImageElement;
    target.src = this.defaultPhoto;
  }

  filterLeaveBalance() {
    this.filteredLeaveBalances = this.leaveBalances.filter(data => {
      return data.leaveTypeId == this.addLeaveService.addLeaveModel.leaveTypeId || this.addLeaveService.addLeaveModel.leaveTypeId == null;
    })
  }

  openEmployeeModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });

    modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
      if(idCardNo){
          this.empCardNo = idCardNo;
          this.onEmpIdChange();
      }
    });
  }

  openReviewerModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, {backdrop: 'static', class: 'modal-xl'});

    modalRef.content.employeeSelected.subscribe((idCardNo:string) => {
      if(idCardNo) {
        this.reviewerPMIS = idCardNo;
        this.onReviewerChange();
      }
    })
  }

  openApproverModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, {backdrop: 'static', class: 'modal-xl'});

    modalRef.content.employeeSelected.subscribe((idCardNo:string) => {
      if(idCardNo) {
        this.approverPMIS = idCardNo;
        this.onApproverChange();
      }
    })
  }

  FillLeaveDataToAddLeaveModel(leave:any) {
    if(this.leaveData==null) {
      return leave;
    }
    for(const key in leave) {
      leave[key] = this.leaveData[key];
    }

    if(this.leaveData.fromDate!=null)
    leave.fromDate = this.leaveData.fromDate.split('T')[0];

    if(this.leaveData.toDate!=null)
    leave.toDate = this.leaveData.toDate.split('T')[0];

    this.empCardNo = this.leaveData.idCardNo;
    this.onEmpIdChange();
    return leave;
  }

  onUpdate() {
    this.loading = true;
    let formData = this.convertToFormData(this.addLeaveService.addLeaveModel,["AssociatedFiles"]);
    this.manageLeaveService.updateLeaveRequest(formData).subscribe({
      next: response => {
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
      error: err => {
        this.loading = false;
      },
      complete: () => {
        this.loading = false;
      }
    })
  }

  test(event:any) {
    event.preventDefault();

  }

  getLeaveFiles() {
    this.manageLeaveService.getLeaveFiles(this.leaveData.leaveRequestId).subscribe({
      next: response => {
        this.leaveFiles = response;
        console.log(this.leaveFiles);
      }
    })
  }
}
