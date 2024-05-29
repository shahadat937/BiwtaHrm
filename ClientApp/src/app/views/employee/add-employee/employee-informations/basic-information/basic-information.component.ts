import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmployeeService } from '../../../service/employee.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-basic-information',
  templateUrl: './basic-information.component.html',
  styleUrl: './basic-information.component.scss'
})
export class BasicInformationComponent implements OnInit {

  @Input() userId!: string;
  visible:boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText:string='';
  @ViewChild('BasicInfoForm', { static: true }) BasicInfoForm!: NgForm;
  
  constructor(
    public userService: UserService,
    public employeeService: EmployeeService,
  ){}

  ngOnInit(): void {
    this.getEmployeeByAspNetUserId();
    this.getUserDetails();
  }

  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.employeeService.basicInfo = {
      empId: 0,
      empEngName: '',
      empBdName: '',
      dateOfBirth: undefined,
      personalFileNo: '',
      nId: 0,
      AspNetUserId: this.userId,
    };
  }

  resetForm(){
    if (this.BasicInfoForm?.form != null) {
      this.BasicInfoForm.form.reset();
      this.BasicInfoForm.form.patchValue({
        empId: 0,
        empEngName: '',
        empBdName: '',
        dateOfBirth: undefined,
        personalFileNo: '',
        nId: 0,
        AspNetUserId: this.userId,
      });
    }
  }
  
  UserFormView(): void{
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getUserDetails(){
    this.userService.find(this.userId).subscribe((res) => {
      this.BasicInfoForm.form.patchValue({
        empEngName: res.firstName+" "+res.lastName,
      });
    });
  }
  
  getEmployeeByAspNetUserId(){
    this.employeeService.findByAspNetUserId(this.userId).subscribe((res) => {
      if(res){
        this.headerText = 'Update Basic Information';
        this.BasicInfoForm.form.patchValue(res);
      }
      else{
        this.headerText = 'Add Basic Information';
      }
    });
  }

  onSubmit(form: NgForm): void{

  }
}
