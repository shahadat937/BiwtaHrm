import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmployeeService } from '../../../service/employee.service';
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
  fullName: string = '';
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
    public employeeService: EmployeeService,
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


  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.employeeService.basicInfo = {
      empId: 0,
      empEngName: this.fullName,
      empBdName: '',
      dateOfBirth: null,
      personalFileNo: '',
      nid: null,
      employeeTypeId: null,
      AspNetUserId: this.userId,
    };
  }

  resetForm(){
      this.BasicInfoForm.form.reset();
      this.BasicInfoForm.form.patchValue({
        empId: this.empId,
        empEngName: this.fullName,
        empBdName: '',
        dateOfBirth: undefined,
        personalFileNo: '',
        nid: null,
        employeeTypeId: null,
        AspNetUserId: this.userId,
      });
  }
  
  UserFormView(): void{
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getUserDetails(){
    this.userService.find(this.userId).subscribe((res) => {
      this.fullName = res.firstName+" "+res.lastName;
      this.BasicInfoForm.form.patchValue({
        empEngName: res.firstName+" "+res.lastName,
        AspNetUserId: res.id,
      });
    });
  }
  
  getEmployeeByAspNetUserId(){
    this.employeeService.findByAspNetUserId(this.userId).subscribe((res) => {
      if(res){
        this.empId = res.empId;
        this.headerText = 'Update Basic Information';
        this.BasicInfoForm?.form.patchValue(res);
        console.log(res)
        this.btnText='Update';
      }
      else{
        this.headerText = 'Add Basic Information';
        this.btnText='Submit';
        this.resetForm();
      }
    });
  }

  getSelectedEmployeeType(){
    this.subscription=this.employeeService.getSelectedEmployeeType().subscribe((data) => { 
      this.employeeType = data;
    });
  }

  cancel(){
    this.visible = false;
  }

  onSubmit(form: NgForm): void{
    this.loading = true;
    console.log(form.value)
    this.employeeService.cachedData = [];
    const id = form.value.empId;
    const action$ = id
      ? this.employeeService.updateBasicInfo(id, form.value)
      : this.employeeService.saveBasicInfo(form.value);
    
      this.subscription = action$.subscribe((response: any) => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
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
