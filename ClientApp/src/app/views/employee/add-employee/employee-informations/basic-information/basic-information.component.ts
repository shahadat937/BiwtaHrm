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

@Component({
  selector: 'app-basic-information',
  templateUrl: './basic-information.component.html',
  styleUrl: './basic-information.component.scss'
})
export class BasicInformationComponent implements OnInit, OnDestroy  {

  @Input() userId!: string;
  @Output() close = new EventEmitter<void>();
  firstName: string = '';
  lastName: string = '';
  empId: number = 0;
  visible:boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText:string='';
  employeeType: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  @ViewChild('BasicInfoForm', { static: true }) BasicInfoForm!: NgForm;
  
  constructor(
    public userService: UserService,
    public empBasicInfoService: EmpBasicInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
  ){}

  ngOnInit(): void {
    this.getEmployeeByAspNetUserId();
    this.getSelectedEmployeeType();
    this.getUserDetails();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empBasicInfoService.basicInfo = {
      id: this.empId,
      firstName : this.firstName,
      lastName : this.lastName,
      firstNameBangla : '',
      lastNameBangla : '',
      dateOfBirth: null,
      personalFileNo: '',
      nid: null,
      employeeTypeId: null,
      aspNetUserId: this.userId,
    };
  }

  resetForm(){
      this.BasicInfoForm.form.reset();
      this.BasicInfoForm.form.patchValue({
        id: this.empId,
        firstName : this.firstName,
        lastName : this.firstName,
        firstNameBangla : '',
        lastNameBangla : '',
        dateOfBirth: undefined,
        personalFileNo: '',
        nid: null,
        employeeTypeId: null,
        aspNetUserId: this.userId,
      });
  }
  
  UserFormView(): void{
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getUserDetails(){
    this.userService.find(this.userId).subscribe((res) => {
      this.firstName= res.firstName;
      this.lastName= res.lastName;
      this.BasicInfoForm.form.patchValue({
        firstName: res.firstName,
        lastName: res.lastName,
        aspNetUserId: res.id,
      });
    });
  }
  
  getEmployeeByAspNetUserId(){
    this.empBasicInfoService.findByAspNetUserId(this.userId).subscribe((res) => {
      if(res){
        this.empId = res.id;
        this.headerText = 'Update Basic Information';
        this.BasicInfoForm?.form.patchValue(res);
        this.btnText='Update';
      }
      else{
        this.headerText = 'Add Basic Information';
        this.btnText='Submit';
        this.initaialForm();
      }
    });
  }

  getSelectedEmployeeType(){
    this.subscription=this.empBasicInfoService.getSelectedEmployeeType().subscribe((data) => { 
      this.employeeType = data;
    });
  }

  cancel(){
    this.close.emit();
  }

  onSubmit(form: NgForm): void{
    this.loading = true;
    this.empBasicInfoService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empBasicInfoService.updateEmpBasicInfo(id, form.value)
      : this.empBasicInfoService.saveEmpBasicInfo(form.value);
    
      this.subscription = action$.subscribe((response: any) => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
          this.cancel();
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
        }
        this.loading = false;
      });
  }
}
