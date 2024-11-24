import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { delay, of, Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { environment } from 'src/environments/environment';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';
import { AddLeaveService } from '../service/add-leave.service';
import { LeaveBalanceService } from '../service/leave-balance.service';
import { ManageLeaveService } from '../service/manage-leave.service';
import { cilSearch } from '@coreui/icons';
import { LeaveStatus } from '../enum/leave-status';
import { AddLeaveModel } from '../models/add-leave-model';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-old-leave-entry',
  templateUrl: './old-leave-entry.component.html',
  styleUrl: './old-leave-entry.component.scss'
})
export class OldLeaveEntryComponent  implements OnInit, OnDestroy{
  baseImageUrl: string;
  subcription: Subscription = new Subscription();
  loading: boolean;
  LeaveTypeOption:any[] = [];
  @ViewChild("addLeaveForm",{static:true}) addLeaveForm!: NgForm;
  empCardNo:string ;
  employeeName: string;
  defaultPhoto: string;
  employeePhoto: string;
  department: string;
  designation: string;
  empSubs: Subscription = new Subscription();
  empReqSub: Subscription = new Subscription();
  totalLeave:number|null;
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
    public addLeaveService: AddLeaveService,
    private leaveBalanceService: LeaveBalanceService, 
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private authService: AuthService
  ) {
    this.loading = false;
    this.empCardNo ="";
    this.employeeName = "";
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
    this.addLeaveService.getSelectedLeaveType().subscribe({
      next: option => {
      this.LeaveTypeOption = option;
      }
    });
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
      // this.getLeaveAmount();
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
            // this.getLeaveAmount();
            // this.getLeaveBalanceForAllType(response.id);
            this.department = response.departmentName;
            this.designation = response.designationName;
          } else {
            this.employeeName = "";
            this.addLeaveService.addLeaveModel.empId = null;
            this.isValidPMS = false
            // this.getLeaveAmount();
            this.employeePhoto = this.defaultPhoto;
            this.department = "";
            this.designation = "";
          }
        },
        error: err=> {
          this.isValidPMS=false;
          this.employeeName = "";
          this.addLeaveService.addLeaveModel.empId = null;
          // this.getLeaveAmount();
          this.employeePhoto = this.defaultPhoto;
          this.department = "";
          this.designation = "";
        }
      });
    })

  }

  
  onImageError(event:any) {
    const target = event.target as HTMLImageElement;
    target.src = this.defaultPhoto;
  }
  handleFile(event:any) {
    const files: FileList = event.target.files;
    this.addLeaveService.addLeaveModel.associatedFiles = Array.from(files); 
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
  
  onDateChange() {
    this.getWorkingDays();
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
  onReset() {
    this.addLeaveForm.reset();
    this.isValidPMS=false;
    this.addLeaveService.addLeaveModel = new AddLeaveModel();
    this.loading = false;
    this.empCardNo ="";
    this.employeeName = "";
    this.employeePhoto = this.defaultPhoto;
    this.totalLeave = null;
    this.department = "";
    this.designation = "";

    this.leaveBalances = [];
    this.filteredLeaveBalances = [];
  }

  onSubmit() {
    this.loading = true;
    let formData = this.convertToFormData(this.addLeaveService.addLeaveModel,["AssociatedFiles"]);
    formData.set('status', '3');
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
    });
    
    let output = '';
    formData.forEach((value, key) => {
        output += `${key}: ${value}\n`;
    });

    // Print the form data to the console
    console.log(output);
  }

}
