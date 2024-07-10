import { StaffForm2ServiceService } from './../service/staff-form2-service.service';
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
    intelligenceAndHumanRemarks: '',
    intelligenceAndHumanActivityValue: 0,
    professionalKnowledgeRemarks: '',
    professionalKnowledgeValue: 0,
    expressivePowerWritingRemarks: '',
    expressivePowerWritingValue: 0,
    expressivePowerSpeakingRemarks: '',
    expressivePowerSpeakingValue: 0,
    initiativeRemarks: '',
    initiativeValue: 0,
    qualityAndQuantityOfWorkRemarks: '',
    qualityAndQuantityOfWorkValue: 0,
    cooperationAndPrudenceRemarks: '',
    cooperationAndPrudenceValue: 0,
    interestAndHardWorkRemarks: '',
    interestAndHardWorkValue: 0,
    responsibilityGeneralRemarks: '',
    responsibilityGeneralValue: 0,
    responsibilityFinancialMattersRemarks: '',
    responsibilityFinancialMattersValue: 0,
    integrityAndReputationRemarks: '',
    integrityAndReputationValue: 0,
    personalityAndCharacterRemarks: '',
    personalityAndCharacterValue: 0,
    healthRemarks: '',
    healthValue: 0,
    punctualityRemarks: '',
    punctualityValue: 0,
    senseOfDisciplineRemarks: '',
    senseOfDisciplineValue: 0,
  }
}
}
