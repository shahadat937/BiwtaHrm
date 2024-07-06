import { StaffForm2ServiceService } from './../service/staff-form2-service.service';
import { StaffForm3ServicesService } from '../service/staff-form3-services.service';
import { StaffFormpart2Module } from './../model/staff-formpart2.module';
import { StaffFormServiceService } from './../service/staff-form-service.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-staff-form-2',
  templateUrl: './staff-form-2.component.html',
  styleUrl: './staff-form-2.component.scss'
})
export class StaffForm2Component implements OnInit, OnDestroy{

  
  loading:boolean=false

  constructor(public staffForm2Service :StaffForm2ServiceService ){}


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
  this.staffForm2Service.staffForm2module = {
    intelligenceAndHumanActivity: '',
    intelligenceAndHumanActivityRadio: 0,
    professionalKnowledge: '',
    expressivePowerWriting: '',
    expressivePowerSpeaking:  '',
    initiative:  '',
    qualityAndQuantityOfWork:  '',
    cooperationAndPrudence:  '',
    interestAndHardWork:  '',
    responsibilityGeneral: '',
    responsibilityFinancialMatters:  '',
    integrityAndReputation:  '',
    personalityAndCharacter:  '',
    health:  '',
    punctuality:  '',
    senseOfDiscipline: '',
  }
}
}
