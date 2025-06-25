import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from '../../../../../src/app/core/service/confirm.service';
import { AddLeaveService } from '../service/add-leave.service';
import { delay, of, Subscription } from 'rxjs';
import { NgFor } from '@angular/common';
import { NgForm } from '@angular/forms';
import { HttpParams } from '@angular/common/http';
import { AddLeaveModel } from '../models/add-leave-model';
import { AuthService } from '../../../../../src/app/core/service/auth.service';
import { LeaveBalanceService } from '../service/leave-balance.service';
import { environment } from '../../../../../src/environments/environment';
import { forEach } from 'lodash-es';
import { cilSearch } from '@coreui/icons';
import { EmployeeInfoListModalComponent } from '../../employee/employee-info-list-modal/employee-info-list-modal.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ManageLeaveService } from '../service/manage-leave.service';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';
import { LeaveStatus } from '../enum/leave-status';
import { Router } from '@angular/router';
import { NotificationService } from '../../notifications/service/notification.service';
import { UserNotification } from '../../notifications/models/user-notification';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { ResponsibilityTypeService } from '../../../../../src/app/views/basic-setup/service/responsibility-type.service';

@Component({
  selector: 'app-addleave',
  templateUrl: './addleave.component.html',
  styleUrl: './addleave.component.scss'
})
export class AddleaveComponent implements OnInit, OnDestroy {
  baseImageUrl: string;
  subcription: Subscription = new Subscription();
  loading: boolean;
  LeaveTypeOption: any[] = [];
  @ViewChild("addLeaveForm", { static: true }) addLeaveForm!: NgForm
  empCardNo: string;
  employeeName: string;
  defaultPhoto: string;
  employeePhoto: string;
  department: string;
  otherResponsibilityType: any;
  designation: string;
  empSubs: Subscription = new Subscription();
  empReqSub: Subscription = new Subscription();
  totalLeave: number | null;
  totalDue: number | null;
  CountryOption: any[] = [];
  isValidPMS: boolean;
  leaveBalances: any[] = [];
  leaveFiles: any[] = [];
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
  icons = { cilSearch }
  constructor(
    private empBasicInfoService: EmpBasicInfoService,
    private modalService: BsModalService,
    private manageLeaveService: ManageLeaveService,
    private router: Router,
    public addLeaveService: AddLeaveService,
    private leaveBalanceService: LeaveBalanceService,
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    private authService: AuthService,
    private departmentService: DepartmentService,
    private responsibilityTypeService: ResponsibilityTypeService,
    public notificationService: NotificationService,
  ) {
    this.loading = false;
    this.empCardNo = "";
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
    this.otherResponsibilityType = ""
  }

  ngOnInit(): void {
    if (this.IsReadonly) {
      console.log("A");
      this.addLeaveService.addLeaveModel = this.FillLeaveDataToAddLeaveModel(this.addLeaveService.addLeaveModel);
      this.filterLeaveBalance();
      if (this.leaveData.reviewedBy) {
        this.empBasicInfoService.findByEmpId(this.leaveData.reviewedBy).subscribe({
          next: response => {
            this.reviewerPMIS = response.idCardNo
            this.reviewerName = [response.firstName, response.lastName].join(' ');
          }
        })
        console.log("B");
      }

      if (this.leaveData.approvedBy) {
        this.empBasicInfoService.findByEmpId(this.leaveData.approvedBy).subscribe({
          next: response => {
            this.approverPMIS = response.idCardNo
            this.approverName = [response.firstName, response.lastName].join(' ');
          }
        })
        console.log("C");
      }

      this.getLeaveFiles();

    }
    this.addLeaveService.getSelectedLeaveType().subscribe({
      next: option => {
        this.LeaveTypeOption = option;
      }
    })

    if (this.IsReadonly) {
      this.buttonTitle = "Update";
      console.log("D");
    }

    if (this.authService.currentUserValue.empId != null && this.IsReadonly == false) {
      this.addLeaveService.getEmpById(parseInt(this.authService.currentUserValue.empId)).subscribe({
        next: response => {
          this.empCardNo = response.idCardNo;
          this.isValidPMS = true;
          this.addLeaveService.addLeaveModel.empId = parseInt(this.authService.currentUserValue.empId);

          // this.addLeaveService.addLeaveModel.empCurrentDepartmentId = this.authService.currentUserValue.departmentId ? parseInt(this.authService.currentUserValue.departmentId) : null;

          // this.addLeaveService.addLeaveModel.empCurrentSectionId = this.authService.currentUserValue.sectionId ? parseInt(this.authService.currentUserValue.sectionId) : null;

          // this.addLeaveService.addLeaveModel.empCurrentDesignationId = this.authService.currentUserValue.designationId ? parseInt(this.authService.currentUserValue.designationId) : null;

          // this.addLeaveService.addLeaveModel.empCurrentResponsibilityTypeId = this.authService.currentUserValue.responsibilityTypeId ? parseInt(this.authService.currentUserValue.responsibilityTypeId) : null;

          this.employeeName = [response.firstName, response.lastName].join(' ');
          // this.department = response.departmentName;
          this.getDepartmentById(Number(this.authService.currentUserValue.departmentId))
          this.getDesignationByIdDesignationId(Number(this.authService.currentUserValue.designationId));
          if (this.authService.currentUserValue.responsibilityTypeId) {
            this.getResponsibilityTypeById(Number(this.authService.currentUserValue.responsibilityTypeId))
          }

          this.designation = response.designationName;

          if (response.empPhotoName != "") {
            this.employeePhoto = this.imageUrl + "EmpPhoto/" + response.empPhotoName;
          } else {
            this.employeePhoto = this.defaultPhoto;
          }
          this.getLeaveBalanceForAllType(response.id);
        }
      })
      console.log("E");
    }

    this.getCountry();
  }

  onEmpIdChange() {
    this.otherResponsibilityType = '';
    const source$ = of(this.empCardNo);
    const delay$ = source$.pipe(
      delay(800)
    );

    if (this.empSubs) {
      this.empSubs.unsubscribe();
    }

    if (this.empReqSub) {
      this.empReqSub.unsubscribe();
    }

    if (this.empCardNo.trim() == "") {
      this.employeeName = "";
      this.department = "";
      this.designation = "";
      this.otherResponsibilityType = ""
      this.addLeaveService.addLeaveModel.empId = null;
      this.getLeaveAmount();
      this.leaveBalances = [];
      this.employeePhoto = this.defaultPhoto;
      return;
    }

    this.empSubs = delay$.subscribe(data => {
      this.empReqSub = this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response => {
          if (response != null) {
            this.employeeName = [response.firstName, response.lastName].join(' ');
            this.addLeaveService.addLeaveModel.empId = response.id;
            this.isValidPMS = true;
            if (response.empPhotoName != "") {
              this.employeePhoto = this.imageUrl + "EmpPhoto/" + response.empPhotoName;
            } else {
              this.employeePhoto = this.defaultPhoto;
            }
            this.getLeaveAmount();
            this.getLeaveBalanceForAllType(response.id);
            this.department =   this.addLeaveService.addLeaveModel.empCurrentDepartmentId? this.getDepartmentById(this.addLeaveService.addLeaveModel.empCurrentDepartmentId) :  response.departmentName;

            this.designation =  this.addLeaveService.addLeaveModel.empCurrentDesignationId? this.getDesignationByIdDesignationId(this.addLeaveService.addLeaveModel.empCurrentDesignationId) : response.designationName;

            this.otherResponsibilityType = this.addLeaveService.addLeaveModel.empCurrentResponsibilityTypeId? this.getResponsibilityTypeById(this.addLeaveService.addLeaveModel.empCurrentResponsibilityTypeId) : "";

            
            this.addLeaveService.addLeaveModel.empCurrentDepartmentId = response.departmentId
            this.addLeaveService.addLeaveModel.empCurrentSectionId = response.sectionId
            this.addLeaveService.addLeaveModel.empCurrentDesignationId = response.designationId
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
        error: err => {
          this.isValidPMS = false;
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
    if (this.subcription) {
      this.subcription.unsubscribe();
    }
    if (this.empReqSub) {
      this.empReqSub.unsubscribe();
    }

    if (this.empSubs) {
      this.empReqSub.unsubscribe();
    }
    this.addLeaveService.addLeaveModel = new AddLeaveModel();
  }

  onDateChange() {
    this.getLeaveAmount();
    this.getWorkingDays();
    if (this.addLeaveService.addLeaveModel.empId != null) {
      this.getLeaveBalanceForAllType(this.addLeaveService.addLeaveModel.empId);
    }
  }

  getLeaveAmount() {
    this.filterLeaveBalance();
    this.IsForeignLeave();
    console.log(this.addLeaveService.addLeaveModel);
    if (this.addLeaveService.addLeaveModel.empId == null || this.addLeaveService.addLeaveModel.leaveTypeId == null || this.addLeaveService.addLeaveModel.fromDate == null || this.addLeaveService.addLeaveModel.toDate == null) {
      this.totalDue = null;
      return;
    }

    let params = new HttpParams();
    params = params.set("empId", this.addLeaveService.addLeaveModel.empId);
    params = params.set("leaveTypeId", this.addLeaveService.addLeaveModel.leaveTypeId);
    params = params.set("fromDate", this.addLeaveService.addLeaveModel.fromDate);
    params = params.set("toDate", this.addLeaveService.addLeaveModel.toDate);

    this.subcription = this.addLeaveService.getLeaveAmount(params).subscribe({
      next: response => {
        this.totalDue = response.totalDue;
      },
      error: err => {
        this.totalDue = null;
      }
    })

    this.getWorkingDays();
  }

  getCountry() {
    this.subcription = this.addLeaveService.getSelectedCountry().subscribe({
      next: response => {
        this.CountryOption = response;
      }
    })
  }

  getWorkingDays() {

    if (this.addLeaveService.addLeaveModel.fromDate == null || this.addLeaveService.addLeaveModel.toDate == null) {
      return;
      this.totalLeave = null;
    }
    let params = new HttpParams();
    params = params.set("From", this.addLeaveService.addLeaveModel.fromDate);
    params = params.set("To", this.addLeaveService.addLeaveModel.toDate);
    if (this.addLeaveService.addLeaveModel.leaveTypeId != null) {
      params = params.set("leaveTypeId", this.addLeaveService.addLeaveModel.leaveTypeId);
    }
    this.subcription = this.addLeaveService.getWorkingDays(params).subscribe({
      next: response => {
        this.totalLeave = response;
      }
    })
  }


  onSubmit() {
    this.loading = true;

    if (this.IsReadonly) {
      this.onUpdate();
      return;
    }

    if (this.addLeaveService.addLeaveModel.empId != Number(this.authService.currentUserValue.empId)) {
      this.addLeaveService.addLeaveModel.applicationById = Number(this.authService.currentUserValue.empId);
    }

    if (this.addLeaveService.addLeaveModel.countryId != null) {
      this.addLeaveService.addLeaveModel.isForeignLeave = true;
    }

    if (this.addLeaveService.addLeaveModel.empId === Number(this.authService.currentUserValue.empId)) {
      if (!this.addLeaveService.addLeaveModel.empCurrentDepartmentId && this.authService.currentUserValue.departmentId) {
        this.addLeaveService.addLeaveModel.empCurrentDepartmentId = Number(this.authService.currentUserValue.departmentId);
      }

      if (!this.addLeaveService.addLeaveModel.empCurrentSectionId && this.authService.currentUserValue.sectionId) {
        this.addLeaveService.addLeaveModel.empCurrentSectionId = Number(this.authService.currentUserValue.sectionId);
      }

      if (!this.addLeaveService.addLeaveModel.empCurrentDesignationId && this.authService.currentUserValue.designationId) {
        this.addLeaveService.addLeaveModel.empCurrentDesignationId = Number(this.authService.currentUserValue.designationId);
      }

      if (!this.addLeaveService.addLeaveModel.empCurrentResponsibilityTypeId && this.authService.currentUserValue.responsibilityTypeId) {
        this.addLeaveService.addLeaveModel.empCurrentResponsibilityTypeId = Number(this.authService.currentUserValue.responsibilityTypeId);
      }
    }

    let formData = this.convertToFormData(this.addLeaveService.addLeaveModel, ["AssociatedFiles"]);
    this.addLeaveService.createLeaveRequest(formData).subscribe({
      next: response => {
        if (response.success == true) {
          this.toastr.success('', `${response.message}`, {
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
          this.notificationService.submit(userNotification).subscribe((res) => { });


          this.onReset();
          this.router.navigate(['/leave/personalleave']);
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (error) => {
        this.loading = false;
        /*this.toastr.warning('',`${error}`,{
          positionClass: 'toast-top-right'
        })*/
      },
      complete: () => {
        this.loading = false;
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
    this.isValidPMS = false;
    this.addLeaveService.addLeaveModel = new AddLeaveModel();
    this.loading = false;
    this.empCardNo = "";
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
      if (model.hasOwnProperty(key) && model[key] != null) {
        if (key == "associatedFiles") {
          model[key].forEach((data: any) => {
            formData.append(key, data);
          })
          continue;
        }

        formData.append(key, model[key]);
      }
    }

    return formData;
  }

  handleFile(event: any) {
    const files: FileList = event.target.files;
    this.addLeaveService.addLeaveModel.associatedFiles = Array.from(files);
  }

  IsForeignLeave() {

    let leaveName = this.LeaveTypeOption.find(x => x.id == this.addLeaveService.addLeaveModel.leaveTypeId);
    if (leaveName == undefined) {
      this.addLeaveService.addLeaveModel.isForeignLeave = false;
      return;
    }

    if (leaveName.name.toLocaleLowerCase().includes("foreign")) {
      this.addLeaveService.addLeaveModel.isForeignLeave = true;
    } else {
      this.addLeaveService.addLeaveModel.isForeignLeave = false;
    }
  }

  getLeaveBalanceForAllType(empId: number) {
    let params = new HttpParams();
    params = params.set('empId', empId);

    if (this.addLeaveService.addLeaveModel.fromDate != null) {
      params = params.set('leaveStartDate', this.addLeaveService.addLeaveModel.fromDate);
    }

    if (this.addLeaveService.addLeaveModel.toDate != null) {
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
    if (this.reviewerPMIS.trim() == "") {
      this.addLeaveService.addLeaveModel.reviewedBy = null;
      return;
    }

    const source$ = of(this.reviewerPMIS).pipe(
      delay(700)
    );

    if (this.subcription) {
      this.subcription.unsubscribe();
    }

    this.subcription = source$.subscribe(data => {
      this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response => {
          if (response != null) {
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

  getDepartmentById(departmentid: number) {
    this.departmentService.getById(departmentid).subscribe({
      next: response => {
        this.department = response.departmentName
      }
    });
  }
  getDesignationByIdDesignationId(designationId: number) {
    this.departmentService.getDesignationByDesignationId(designationId).subscribe({
      next: response => {
        this.designation = response.designationName
      }
    });
  }
  getResponsibilityTypeById(responsibiltyTypeId: number) {
    this.responsibilityTypeService.find(responsibiltyTypeId).subscribe({
      next: response => {
        if (response) {
          this.otherResponsibilityType = "(" + response.name + ")";
        }

      }
    });
  }



  onApproverChange() {
    if (this.reviewerPMIS.trim() == "") {
      this.addLeaveService.addLeaveModel.approvedBy = null;
      return;
    }

    const source$ = of(this.approverPMIS).pipe(
      delay(700)
    );

    if (this.subcription) {
      this.subcription.unsubscribe();
    }

    this.subcription = source$.subscribe(data => {
      this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response => {
          if (response != null) {
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

  // onImageError(event:any) {
  //   const target = event.target as HTMLImageElement;
  //   target.src = this.defaultPhoto;
  // }
  onImageError(event: any): void {
    const target = event.target as HTMLImageElement;

    if (!target.dataset['fallbackUsed']) {
      target.src = this.defaultPhoto;
      target.dataset['fallbackUsed'] = 'true';
    }
  }
  filterLeaveBalance() {
    this.filteredLeaveBalances = this.leaveBalances.filter(data => {
      return data.leaveTypeId == this.addLeaveService.addLeaveModel.leaveTypeId || this.addLeaveService.addLeaveModel.leaveTypeId == null;
    })
  }

  openEmployeeModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeInfoListModalComponent, { backdrop: 'static', class: 'modal-xl' });

    modalRef.content.employeeSelected.subscribe((employee: any) => {
      console.log(employee)
      if (employee) {
        this.empCardNo = employee.idCardNo;
        this.addLeaveService.addLeaveModel.empCurrentDepartmentId = employee.departmentId;
        this.addLeaveService.addLeaveModel.empCurrentSectionId = employee.sectionId;
        this.addLeaveService.addLeaveModel.empCurrentDesignationId = employee.designationId;
        this.addLeaveService.addLeaveModel.empCurrentResponsibilityTypeId = employee.additionalResponsibilityId;
        this.addLeaveService.addLeaveModel.empId = employee.id;

        if (this.empSubs) {
          this.empSubs.unsubscribe();
        }

        if (this.empReqSub) {
          this.empReqSub.unsubscribe();
        }
        this.designation = employee.designationName;
        this.department = employee.departmentName;
        this.otherResponsibilityType = ""
        if (employee.empPhotoName) {
          this.employeePhoto = this.employeePhoto = this.imageUrl + "EmpPhoto/" + employee.empPhotoName;
        }
        else {
          this.employeePhoto = ""
        }

        this.employeeName = this.employeeName = [employee.firstName, employee.lastName].join(' ');
      }
    });
  }

  openReviewerModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeInfoListModalComponent, { backdrop: 'static', class: 'modal-xl' });

    modalRef.content.employeeSelected.subscribe((reviewer: any) => {

      if (typeof reviewer === 'object') {
        console.log(reviewer)
        this.addLeaveService.addLeaveModel.reviewerCurrentDepartmentId = reviewer.departmentId;
        this.addLeaveService.addLeaveModel.reviewerCurrentSectionId = reviewer.sectionId;
        this.addLeaveService.addLeaveModel.reviewerCurrentDesignationId = reviewer.designationId;
        this.addLeaveService.addLeaveModel.reviewerCurrentResponsibilityTypeId = reviewer.additionalResponsibilityId;
        this.addLeaveService.addLeaveModel.reviewedBy = reviewer.id;

        console.log(reviewer.additionalResponsibilityId);

        this.reviewerPMIS = reviewer.idCardNo
        this.reviewerName = [reviewer.firstName, reviewer.lastName].join(' ');
      }
      else {
        this.approverPMIS = reviewer;
        this.onReviewerChange();
      }
    })
  }

  openApproverModal() {
    const modalRef: BsModalRef = this.modalService.show(EmployeeInfoListModalComponent, { backdrop: 'static', class: 'modal-xl' });

    modalRef.content.employeeSelected.subscribe((approver: any) => {

      if (typeof approver === 'object') {
        console.log("_A", approver)
        this.approverPMIS = approver.idCardNo;
        this.addLeaveService.addLeaveModel.approverCurrentDepartmentId = approver.departmentId;
        this.addLeaveService.addLeaveModel.approverCurrentSectionId = approver.sectionId;
        this.addLeaveService.addLeaveModel.approverCurrentDesignationId = approver.designationId;
        this.addLeaveService.addLeaveModel.approverCurrentResponsibilityTypeId = approver.additionalResponsibilityId;
        this.addLeaveService.addLeaveModel.approvedBy = approver.id;

        this.approverPMIS = approver.idCardNo
        this.approverName = [approver.firstName, approver.lastName].join(' ');
      }
      else {
        this.approverPMIS = approver;
        this.onApproverChange();
      }
    })
  }

  FillLeaveDataToAddLeaveModel(leave: any) {
    if (this.leaveData == null) {
      return leave;
    }
    for (const key in leave) {
      leave[key] = this.leaveData[key];
    }

    if (this.leaveData.fromDate != null)
      leave.fromDate = this.leaveData.fromDate.split('T')[0];

    if (this.leaveData.toDate != null)
      leave.toDate = this.leaveData.toDate.split('T')[0];

    this.empCardNo = this.leaveData.idCardNo;
    this.onEmpIdChange();
    return leave;
  }

  onUpdate() {
    this.loading = true;
    let formData = this.convertToFormData(this.addLeaveService.addLeaveModel, ["AssociatedFiles"]);
    this.manageLeaveService.updateLeaveRequest(formData).subscribe({
      next: response => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right'
          })
        } else {
          this.toastr.warning('', `${response.message}`, {
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

  test(event: any) {
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
