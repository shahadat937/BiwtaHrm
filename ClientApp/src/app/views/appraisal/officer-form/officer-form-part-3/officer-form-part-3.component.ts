import { NgForm } from '@angular/forms';
import { OfficerFormPart3ServiceService } from './../service/officer-form-part3-service.service';
import { Component, OnDestroy, OnInit } from '@angular/core';

@Component({
  selector: 'app-officer-form-part-3',
  templateUrl: './officer-form-part-3.component.html',
  styleUrl: './officer-form-part-3.component.scss'
})
export class OfficerFormPart3Component  implements OnInit, OnDestroy{

  loading:boolean=false

  constructor(public officerForm3service :OfficerFormPart3ServiceService ){}
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
  this.officerForm3service.officerForm3Module={
  professionalKnowledge1:'',
  professionalKnowledge2:'',
  professionalKnowledge3:'',
  professionalKnowledge4:'',
  professionalKnowledge5:'',
  professionalKnowledgeRadio : 0,
  qualityOfWork1 :'',
  qualityOfWork2 :'',
  qualityOfWork3 :'',
  qualityOfWork4 :'',
  qualityOfWork5 :'',
  qualityOfWorkRadio:0,
  amountOfWork1 :'',
  amountOfWork2 :'',
  amountOfWork3 :'',
  amountOfWork4 :'',
  amountOfWork5 :'',
  amountOfWorkRadio: 0,
  punctuality1 :'',
  punctuality2 :'', 
  punctuality3 :'',
  punctuality4 :'',
  punctuality5 :'',
  punctualityRadio: 0,
  senceOfResponsibilityAndCommitment1 :'',
  senceOfResponsibilityAndCommitment2 :'',
  senceOfResponsibilityAndCommitment3 :'',
  senceOfResponsibilityAndCommitment4 :'',
  senceOfResponsibilityAndCommitment5 :'',
  senceOfResponsibilityAndCommitmentRadio: 0,
  promptnessTakingMeasuresAndFollowingOrders1 :'',
  promptnessTakingMeasuresAndFollowingOrders2 :'',
  promptnessTakingMeasuresAndFollowingOrders3 :'',
  promptnessTakingMeasuresAndFollowingOrders4 :'',
  promptnessTakingMeasuresAndFollowingOrders5 :'',
  promptnessTakingMeasuresAndFollowingOrdersRadio: 0,
  interestOfWork1 :'',
  interestOfWork2 :'',
  interestOfWork3 :'',
  interestOfWork4 :'',
  interestOfWork5 :'',
  interestOfWorkRadio: 0,
  superviseAndManage1 :'',
  superviseAndManage2 :'',
  superviseAndManage3 :'',
  superviseAndManage4 :'',
  superviseAndManage5 :'',
  superviseAndManageRadio: 0,
  relationWithColleagues1 :'',
  relationWithColleagues2 :'',
  relationWithColleagues3 :'',
  relationWithColleagues4 :'',
  relationWithColleagues5 :'',
  relationWithColleaguesRadio: 0,
  abilityImplementDecision1 :'',
  abilityImplementDecision2 :'',
  abilityImplementDecision3 :'',
  abilityImplementDecision4 :'',
  abilityImplementDecision5 :'',
  abilityImplementDecisionRadio: 0,
  expressivePowerWriting1 :'',
  expressivePowerWriting2 :'',
  expressivePowerWriting3 :'',
  expressivePowerWriting4 :'',
  expressivePowerWriting5 :'',
  expressivePowerWritingRadio: 0,
  expressivePowerSpeech1 :'',
  expressivePowerSpeech2 :'',
  expressivePowerSpeech3 :'',
  expressivePowerSpeech4 :'',
  expressivePowerSpeech5 :'',
  expressivePowerSpeechRadio: 0,
  overallAssessment1 :'',
  overallAssessment2 :'',
  overallAssessment3 :'',
  overallAssessment4 :'',
  overallAssessment5 :'',
  overallAssessment6 :'',
  overallAssessmentRadio: 0,
  totalMarks3rdPart : 0,
  totalMarksInWords3rdPart : 0,
  totalMarksSignature3rdPart  :'',
  totalMarksInWordsSignature3rdPart :'',
  totalMarks2ndAnd3rdPart : 0,
  totalMarksInWordstotalMarks2ndAnd3rdPart  : 0,
  totalMarksSignaturetotalMarks2ndAnd3rdPart   :'', 
  totalMarksInWordsSignaturetotalMarks2ndAnd3rdPart :'',
  signature: null

    }
  }
}
