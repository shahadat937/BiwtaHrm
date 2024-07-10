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
  
  professionalKnowledgeRows = [
    { name: 'i) Significant Knowledge', evaluationValue: '5', Signature: '',  signatureEnabled: false },
    { name: 'ii) Sufficient Knowledge', evaluationValue: '4', Signature: '', signatureEnabled: false },
    { name: 'iii) Fairly Good Knowledge', evaluationValue: '3', Signature: '', signatureEnabled: false },
    { name: 'iv) Insufficient', evaluationValue: '2', Signature: '', signatureEnabled: false },
    { name: 'v) Low Quality', evaluationValue: '1', Signature: '', signatureEnabled: false },
  ];

  qualityOfWorkRows = [
    { name: 'i) Accurate and Complete', evaluationValue: '5', qualityOfWorkSignature: '',  signatureEnabled: false },
    { name: 'ii) High Standard', evaluationValue: '4', qualityOfWorkSignature: '', signatureEnabled: false },
    { name: 'iii) General', evaluationValue: '3', qualityOfWorkSignature: '', signatureEnabled: false },
    { name: 'iv) Unequal Values', evaluationValue: '2', qualityOfWorkSignature: '', signatureEnabled: false },
    { name: 'v) Low Quality', evaluationValue: '1', qualityOfWorkSignature: '', signatureEnabled: false },
  ];
  amountOfWorkRows = [
    { name: 'i) Too Much', evaluationValue: '5', amountOfWorkSignature: '', signatureEnabled: false },
    { name: 'ii) Sufficient', evaluationValue: '4', amountOfWorkSignature: '', signatureEnabled: false },
    { name: 'iii) Satisfactory', evaluationValue: '3', amountOfWorkSignature: '', signatureEnabled: false },
    { name: 'iv) Not as Expected', evaluationValue: '2', amountOfWorkSignature: '', signatureEnabled: false },
    { name: 'v) Insufficient', evaluationValue: '1', amountOfWorkSignature: '', signatureEnabled: false },
  ];
  punctualityRows = [
    { name: 'i) Never Delay', evaluationValue: '5', punctualitySignature: '', signatureEnabled: false },
    { name: 'ii) Rarely Late', evaluationValue: '4', punctualitySignature: '', signatureEnabled: false },
    { name: 'iii) Usually Punctual', evaluationValue: '3', punctualitySignature: '', signatureEnabled: false },
    { name: 'iv) Sometimes Delay', evaluationValue: '2', punctualitySignature: '', signatureEnabled: false },
    { name: 'v) Habitually Late', evaluationValue: '1', punctualitySignature: '', signatureEnabled: false },
  ];

  SenceOfResponsibilityRows = [
    { name: 'i) Always Ready to Perform duties', evaluationValue: '5', senceOfResponsibilityAndCommitmentSignature: '', signatureEnabled: false },
    { name: 'ii) Very Wiling to take Responsibility', evaluationValue: '4', senceOfResponsibilityAndCommitmentSignature: '', signatureEnabled: false },
    { name: 'iii) Traditional', evaluationValue: '3', senceOfResponsibilityAndCommitmentSignature: '', signatureEnabled: false },
    { name: 'iv) Tendency submit  despite  decision-making power', evaluationValue: '2', senceOfResponsibilityAndCommitmentSignature: '', signatureEnabled: false },
    { name: 'v) Avoid Responssibility', evaluationValue: '1', senceOfResponsibilityAndCommitmentSignature: '', signatureEnabled: false },
  ];

  PromptnessTakingMeasuresRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', promptnessTakingMeasuresAndFollowingOrdersSignature: '', signatureEnabled: false },
    { name: 'ii) Very Active', evaluationValue: '4', promptnessTakingMeasuresAndFollowingOrdersSignature: '', signatureEnabled: false },
    { name: 'iii)Activity Is Effortful', evaluationValue: '3', promptnessTakingMeasuresAndFollowingOrdersSignature: '', signatureEnabled: false },
    { name: 'iv) Slow', evaluationValue: '2', promptnessTakingMeasuresAndFollowingOrdersSignature: '', signatureEnabled: false },
    { name: 'v) Careless', evaluationValue: '1', promptnessTakingMeasuresAndFollowingOrdersSignature: '', signatureEnabled: false },
  ];

  interestOfWorkRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', interestOfWorkSignature: '', signatureEnabled: false },
    { name: 'ii) Stedy', evaluationValue: '4', interestOfWorkSignature: '', signatureEnabled: false },
    { name: 'iii)Responsible', evaluationValue: '3', interestOfWorkSignature: '', signatureEnabled: false },
    { name: 'iv) Low', evaluationValue: '2', interestOfWorkSignature: '', signatureEnabled: false },
    { name: 'v) Lack of Interest', evaluationValue: '1', interestOfWorkSignature: '', signatureEnabled: false },
  ];

  superviseandMeasureRows = [
    { name: 'i)A Source of Motivation for subordinates', evaluationValue: '5', superviseAndManageSignature: '', signatureEnabled: false },
    { name: 'ii)Good at Management', evaluationValue: '4', superviseAndManageSignature: '', signatureEnabled: false },
    { name: 'iii)Assistant to Subordinate', evaluationValue: '3', superviseAndManageSignature: '', signatureEnabled: false },
    { name: 'iv)Lack of Control', evaluationValue: '2', superviseAndManageSignature: '', signatureEnabled: false },
    { name: 'v)Inferior', evaluationValue: '1', superviseAndManageSignature: '', signatureEnabled: false },
  ];

  RelationWithColleaguesRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', relationWithColleaguesSignature: '', signatureEnabled: false },
    { name: 'ii)Highly Respective and Preferred', evaluationValue: '4', relationWithColleaguesSignature: '', signatureEnabled: false },
    { name: 'iii)Sincere', evaluationValue: '3', relationWithColleaguesSignature: '', signatureEnabled: false },
    { name: 'iv)Avoidence Tendency', evaluationValue: '2', relationWithColleaguesSignature: '', signatureEnabled: false },
    { name: 'v)Behaviour is Consistent', evaluationValue: '1', relationWithColleaguesSignature: '', signatureEnabled: false },
  ];

  abilityToImplementRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', abilityImplementDecisionSignature: '', signatureEnabled: false },
    { name: 'ii)Highly Very Good', evaluationValue: '4', abilityImplementDecisionSignature: '', signatureEnabled: false },
    { name: 'iii)Good', evaluationValue: '3', abilityImplementDecisionSignature: '', signatureEnabled: false },
    { name: 'iv)Weak', evaluationValue: '2', abilityImplementDecisionSignature: '', signatureEnabled: false },
    { name: 'v)Weak', evaluationValue: '1', abilityImplementDecisionSignature: '', signatureEnabled: false },
  ];
  expressivePowerWritingRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', expressivePowerWritingSignature: '', signatureEnabled: false },
    { name: 'ii)Clear,Irrefutable,Orderly', evaluationValue: '4', expressivePowerWritingSignature: '', signatureEnabled: false },
    { name: 'iii)Generally clear and concise', evaluationValue: '3', expressivePowerWritingSignature: '', signatureEnabled: false },
    { name: 'iv)Not So Good', evaluationValue: '2', expressivePowerWritingSignature: '', signatureEnabled: false },
    { name: 'v)Not Clear', evaluationValue: '1', expressivePowerWritingSignature: '', signatureEnabled: false },
  ];

  expressivePowerSpeechRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', expressivePowerSpeechSignature: '', signatureEnabled: false },
    { name: 'ii)Strong', evaluationValue: '4', expressivePowerSpeechSignature: '', signatureEnabled: false },
    { name: 'iii)Much', evaluationValue: '3', expressivePowerSpeechSignature: '', signatureEnabled: false },
    { name: 'iv)Not Clear', evaluationValue: '2', expressivePowerSpeechSignature: '', signatureEnabled: false },
    { name: 'v)Vegue and Ineffective', evaluationValue: '1', expressivePowerSpeechSignature: '', signatureEnabled: false },
  ];

  overallAssessmentRows = [
    { name: 'i)Extraordinary', evaluationValue: '91-100', overallAssessmentSignature: '', signatureEnabled: false },
    { name: 'ii)High Standard', evaluationValue: '81-90', overallAssessmentSignature: '', signatureEnabled: false },
    { name: 'iii)Intelligent', evaluationValue: '65-80', overallAssessmentSignature: '', signatureEnabled: false },
    { name: 'iv)Current Status', evaluationValue: '45-64', overallAssessmentSignature: '', signatureEnabled: false },
    { name: 'v)Below Expected Value', evaluationValue: '31-44', overallAssessmentSignature: '', signatureEnabled: false },
    { name: 'v)Low Quality', evaluationValue: '20-30', overallAssessmentSignature: '', signatureEnabled: false },
  ];

  selectedRow: any; // Variable to store the selected row
  selectedQualityRow: any; // Variable to store the selected row
  selectedAmountRow:any;
  selectedPunctualityRow:any;
  selectedSenseofResponsibilityRow:any;
  selectedPromtnessTakingRow:any;
  selectedinterstofworkRow:any;
  selectedsuperviseAndMeasureRow:any;
  selectedRelationColleaguesRow:any;
  abilityWithImplementRow:any;
  exessivePowerWritingRow:any;
  exessivePowerspeechRow:any;
  overallAssessmentRow:any;

  toggleSignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.professionalKnowledgeRows.forEach((row) => {
      row.signatureEnabled = false;
    });

    // Enable signature input for the selected row
    this.professionalKnowledgeRows[index].signatureEnabled = true;
  }

  toggleQualitySignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.qualityOfWorkRows.forEach((row) => {
      row.signatureEnabled = false;
    });

    // Enable signature input for the selected row
    this.qualityOfWorkRows[index].signatureEnabled = true;
  }

    toggleAmountSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.amountOfWorkRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.amountOfWorkRows[index].signatureEnabled = true;
    }
    togglePunctualitySignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.punctualityRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.punctualityRows[index].signatureEnabled = true;
      
    }
    toggleSenseOfResponsibilitySignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.SenceOfResponsibilityRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.SenceOfResponsibilityRows[index].signatureEnabled = true;
      
    }
    togglePromptnessSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.PromptnessTakingMeasuresRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.PromptnessTakingMeasuresRows[index].signatureEnabled = true;
      
    }
    toggleinterestofWorkSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.interestOfWorkRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.interestOfWorkRows[index].signatureEnabled = true;
      
    }
    toggleSuperviseMeasureSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.superviseandMeasureRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.superviseandMeasureRows[index].signatureEnabled = true;
      
    }
    toggleRelationColleaguesignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.RelationWithColleaguesRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.RelationWithColleaguesRows[index].signatureEnabled = true;
      
    }
    toggleAbilityWithImplementSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.abilityToImplementRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.abilityToImplementRows[index].signatureEnabled = true;
      
    }
    toggleExessivePowerWritingSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.expressivePowerWritingRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.expressivePowerWritingRows[index].signatureEnabled = true;
      
    }
    toggleExessivePowerSpeechSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.expressivePowerSpeechRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.expressivePowerSpeechRows[index].signatureEnabled = true;
      
    }
    toggleOverallAssessmentSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.overallAssessmentRows.forEach((row) => {
        row.signatureEnabled = false;
      });
  
      // Enable signature input for the selected row
      this.overallAssessmentRows[index].signatureEnabled = true;
      
    }
  }

