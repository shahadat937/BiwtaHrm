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
    intelligenceAndHumanActivity: '',
    intelligenceAndHumanActivityRadio: 0,
    professionalKnowledge: '',
    professionalKnowledgeRadio: 0,
    expressivePowerWriting: '',
    expressivePowerWritingRadio: 0,
    expressivePowerSpeaking:  '',
    expressivePowerSpeakingRadio: 0,
    initiative:  '',
    initiativeRadio: 0,
    qualityAndQuantityOfWork:  '',
    qualityAndQuantityOfWorkRadio: 0,
    cooperationAndPrudence:  '',
    cooperationAndPrudenceRadio: 0,
    interestAndHardWork:  '',
    interestAndHardWorkRadio: 0,
    responsibilityGeneral: '',
    responsibilityGeneralRadio: 0,
    responsibilityFinancialMatters:  '',
    responsibilityFinancialMattersRadio: 0,
    integrityAndReputation:  '',
    integrityAndReputationRadio: 0,
    personalityAndCharacter:  '',
    personalityAndCharacterRadio: 0,
    health:  '',
    healthRadio: 0,
    punctuality:  '',
    punctualityRadio: 0,
    senseOfDiscipline: '',
    senseOfDisciplineRadio: 0,
  }
}
}
