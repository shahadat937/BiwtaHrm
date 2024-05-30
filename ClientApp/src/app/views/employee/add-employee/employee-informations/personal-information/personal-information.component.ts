import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmployeeService } from '../../../service/employee.service';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrl: './personal-information.component.scss'
})
export class PersonalInformationComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  fullName: string = '';
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  employeeType: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  @ViewChild('PersonalInfoForm', { static: true }) PersonalInfoForm!: NgForm;

  constructor(public bsModalRef: BsModalRef,
    public employeeService: EmployeeService,) { }

  ngOnInit(): void {
    this.getEmployeeByAspNetUserId();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  
  UserFormView(): void{
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getEmployeeByAspNetUserId(){
    this.employeeService.findByEmpId(this.empId).subscribe((res) => {
      if(res.genderId){
        this.headerText = 'Update Personal Information';
        // this.BasicInfoForm?.form.patchValue(res);
        console.log(res)
        this.btnText='Update';
      }
      else{
        this.headerText = 'Add Personal Information';
        this.btnText='Submit';
        // this.resetForm();
      }
    });
  }


}
