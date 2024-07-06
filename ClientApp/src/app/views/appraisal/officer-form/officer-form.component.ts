import { NgForm } from '@angular/forms';
import { OfficerFormService } from './service/officer-form.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-officer-form',
  templateUrl: './officer-form.component.html',
  styleUrl: './officer-form.component.scss'
})
export class OfficerFormComponent implements OnInit, OnDestroy {

  @ViewChild('officerForm', { static: true }) BloodGroupForm!: NgForm;

  loading :boolean=false;

  constructor(public officerservice :OfficerFormService ){}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }
  onSubmit(form: NgForm): void {
    this.loading=true;
    console.log("Form Value: ",form.value)
  }
  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.officerservice.officerModels = {
      division : '',
      yearStartDate : new Date(),
      yearEndDate :new Date(),
      employeeName :'',
      fathersName :'',
      mothersName:'',
      birthRegNo:0,
      dateofBirth:new Date(),
      designation:'',
      workplace:'',
      joiningDate:new Date(),
      presentDesignationJoiningDate:new Date(),
      education:'',
      trainingSpecialTraining:'',
      reportinFromDate:new Date(),
      reportingEndDate:new Date(),
    };
  }
}
