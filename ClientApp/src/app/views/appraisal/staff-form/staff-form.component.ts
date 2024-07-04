
import { OfficerFormserviceService } from './service/officer-formservice.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-staff-form',
  templateUrl: './staff-form.component.html',
  styleUrl: './staff-form.component.scss'
})
export class StaffFormComponent implements OnInit, OnDestroy{

  @ViewChild('staffForm', { static: true }) StaffFormModule!: NgForm;

  loading:boolean=false
  constructor(public staffservice:OfficerFormserviceService ){}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }
  onSubmit(form: NgForm): void {
    this.loading=true;
    console.log("Form Value: ", form.value)
  }

  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.staffservice.StaffModels = {
      division : '',
      yearStartDate : new Date(),
      yearEndDate :new Date(),
      name:'',
      employeeCode:0,
      fathersName :'',
      mothersName:'',
      dateofBirth:new Date(),
      designation:'',
      presentSalary:0,
      scaleOfpay:'',
      joiningDate:new Date(),
      presentDesignationJoiningDate:new Date(),
      education:'',
      lingutstics:'',
      trainingSpecialTraining:'',
      reportinFromDate:new Date(),
      reportingEndDate:new Date(),
    }
  }
}
