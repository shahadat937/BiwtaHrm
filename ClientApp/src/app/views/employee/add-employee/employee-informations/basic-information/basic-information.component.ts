import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmpBasicInfoService } from '../../../service/emp-basic-info.service';
import { NgForm } from '@angular/forms';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { UserModule } from 'src/app/views/usermanagement/model/user.module';
import { ShiftService } from 'src/app/views/attendance/services/shift.service';
import { ShiftSettingService } from '../../../../attendance/services/shift-setting.service';
import { EmpShiftAssign } from '../../../model/emp-shift-assign';
import { EmpShiftAssignService } from '../../../service/emp-shift-assign.service';
import { SiteSettingService } from '../../../../featureManagement/service/site-setting.service';
import {SharedService } from '../../../../../shared/shared.service'

@Component({
  selector: 'app-basic-information',
  templateUrl: './basic-information.component.html',
  styleUrl: './basic-information.component.scss'
})
export class BasicInformationComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  employeeType: SelectedModel[] = [];
  shifts: SelectedModel[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[] = []
  loading: boolean = false;
  userForm: UserModule = new UserModule;
  empShiftForm: EmpShiftAssign = new EmpShiftAssign;
  activeShiftId: number = 0;
  defaultPassword: string = "";
  @ViewChild('BasicInfoForm', { static: true }) BasicInfoForm!: NgForm;

  constructor(
    public userService: UserService,
    public empBasicInfoService: EmpBasicInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public shiftService: ShiftService,
    public empShiftAssignService: EmpShiftAssignService,
    public shiftSettingService: ShiftSettingService,
    public siteSettingService: SiteSettingService,
    public sharedService : SharedService
  ) { }

  ngOnInit(): void {
    this.getActiveShiftType();
    this.getActiveSiteSetting();
    // this.getEmployeeByAspNetUserId();
    this.getSelectedEmployeeType();
    // this.getUserDetails();
    this.getEmployeeByEmpId();
    this.getSelectedShift();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs => subs.unsubscribe());
    }
  }

  getActiveShiftType() {
    this.subscription.push(
      this.shiftSettingService.getActiveShiftType().subscribe((res) => {
        this.activeShiftId = res.id;
      })
    )
  }


  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empBasicInfoService.basicInfo = {
      id: this.empId,
      firstName: '',
      lastName: '',
      firstNameBangla: '',
      lastNameBangla: '',
      dateOfBirth: null,
      personalFileNo: '',
      nid: null,
      employeeTypeId: null,
      aspNetUserId: null,
      userStatus: false,
      employeeTypeName: '',
      idCardNo: '',
      departmentName: '',
      designationName: '',
      sectionName: '',
      shiftId: this.activeShiftId,
      empPhotoName: '',
      empGenderName: '',
    };
  }

  resetForm() {
    this.router.navigate(['/employee/create-new-employee']);
    this.BasicInfoForm.form.reset();
    this.BasicInfoForm.form.patchValue({
      id: this.empId,
      firstName: '',
      lastName: '',
      firstNameBangla: '',
      lastNameBangla: '',
      dateOfBirth: undefined,
      personalFileNo: '',
      nid: null,
      employeeTypeId: null,
      aspNetUserId: null,
      userStatus: false,
      idCardNo: null,
      departmentName: '',
      designationName: '',
      shiftId: this.activeShiftId
    });
  }

  getActiveSiteSetting() {
    this.subscription.push(
      this.siteSettingService.getActive().subscribe((res) => {
        this.defaultPassword = res.defaultPassword;
      })
    )
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  // getUserDetails(){
  //   this.userService.find(this.userId).subscribe((res) => {
  //     this.firstName= res.firstName;
  //     this.lastName= res.lastName;
  //     this.BasicInfoForm.form.patchValue({
  //       firstName: res.firstName,
  //       lastName: res.lastName,
  //       aspNetUserId: res.id,
  //     });
  //   });
  // }

  // getEmployeeByAspNetUserId(){
  //   this.empBasicInfoService.findByAspNetUserId(this.userId).subscribe((res) => {
  //     if(res){
  //       this.empId = res.id;
  //       this.headerText = 'Update Basic Information';
  //       this.BasicInfoForm?.form.patchValue(res);
  //       this.btnText='Update';
  //     }
  //     else{
  //       this.headerText = 'Add Basic Information';
  //       this.btnText='Submit';
  //       this.initaialForm();
  //     }
  //   });
  // }


  getEmployeeByEmpId() {
    this.empBasicInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res) {
        this.headerText = 'Update Basic Information';
      res.dateOfBirth = this.sharedService.parseDate(res.dateOfBirth);
        this.BasicInfoForm?.form.patchValue(res);
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add Basic Information';
        this.btnText = 'Submit';
        this.initaialForm();
      }
    });
  }

  getSelectedEmployeeType() {
    // this.subscription=
    this.subscription.push(
      this.empBasicInfoService.getSelectedEmployeeType().subscribe((data) => {
        this.employeeType = data;
      })
    )

  }
  getSelectedShift() {
    this.subscription.push(
      this.shiftService.getSelectedShift().subscribe((data) => {
        this.shifts = data;
      })
    )
    // this.subscription=
  }

  cancel() {
    this.router.navigate(['/employee/create-new-employee']);
    this.close.emit();
  }

onSubmit(form: NgForm): void {

const formattedDob = this.sharedService.formatDateOnly(this.empBasicInfoService.basicInfo.dateOfBirth!);
const payload = {
  ...form.value,
  dateOfBirth: formattedDob
};

    this.loading = true;
    this.empBasicInfoService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empBasicInfoService.updateEmpBasicInfo(id, payload)
      : this.empBasicInfoService.saveEmpBasicInfo(payload);
// const action$ = id
//       ? this.empBasicInfoService.updateEmpBasicInfo(id, form.value)
//       : this.empBasicInfoService.saveEmpBasicInfo(form.value);


    this.subscription.push(
      action$.subscribe((response: any) => {
        if (response.success) {
        if(!id){
          this.userForm.firstName = form.value.firstName;
          this.userForm.lastName = form.value.lastName;
          this.userForm.userName = form.value.idCardNo;
          this.userForm.password = this.defaultPassword ?? "Admin@123";
          this.userForm.empId = response.id;
          this.userService.submit(this.userForm).subscribe(((res: any) => {
            if(res.success){
              this.empBasicInfoService.updateUserStatus(response.id).subscribe((res) =>{})
            }
          }));
          this.empShiftAssignService.cachedData = []
          this.empShiftForm.empId = response.id;
          this.empShiftForm.shiftId = this.activeShiftId;
          this.empShiftAssignService.saveEmpShiftAssign(this.empShiftForm).subscribe((res) =>{});
        }
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
          this.cancel();
          this.router.navigate(['/employee/update-employee-information/',
            response.id]);
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
        }
        this.loading = false;
      })
    )

  }
}
