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
    { name: 'i) Significant Knowledge', evaluationValue: '5', Signature: '', professionalKnowledgeRemarks:'',  signatureEnabled: false ,remarksEnabled: false},
    { name: 'ii) Sufficient Knowledge', evaluationValue: '4', Signature: '', professionalKnowledgeRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'iii) Fairly Good Knowledge', evaluationValue: '3', Signature: '', professionalKnowledgeRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'iv) Insufficient', evaluationValue: '2', Signature: '', professionalKnowledgeRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'v) Low Quality', evaluationValue: '1', Signature: '', professionalKnowledgeRemarks:'', signatureEnabled: false ,remarksEnabled: false},
  ];

  qualityOfWorkRows = [
    { name: 'i) Accurate and Complete', evaluationValue: '5', qualityOfWorkSignature: '', qualityofworkRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'ii) High Standard', evaluationValue: '4', qualityOfWorkSignature: '', qualityofworkRemarks:'',  signatureEnabled: false,remarksEnabled: false  },
    { name: 'iii) General', evaluationValue: '3', qualityOfWorkSignature: '', qualityofworkRemarks:'', signatureEnabled: false ,remarksEnabled: false },
    { name: 'iv) Unequal Values', evaluationValue: '2', qualityOfWorkSignature: '', qualityofworkRemarks:'', signatureEnabled: false,remarksEnabled: false  },
    { name: 'v) Low Quality', evaluationValue: '1', qualityOfWorkSignature: '', qualityofworkRemarks:'', signatureEnabled: false,remarksEnabled: false  },
  ];
  amountOfWorkRows = [
    { name: 'i) Too Much', evaluationValue: '5', amountOfWorkSignature: '', amountofWorkRemarks:'', signatureEnabled: false,remarksEnabled: false  },
    { name: 'ii) Sufficient', evaluationValue: '4', amountOfWorkSignature: '', amountofWorkRemarks:'', signatureEnabled: false,remarksEnabled: false  },
    { name: 'iii) Satisfactory', evaluationValue: '3', amountOfWorkSignature: '', amountofWorkRemarks:'', signatureEnabled: false,remarksEnabled: false  },
    { name: 'iv) Not as Expected', evaluationValue: '2', amountOfWorkSignature: '', amountofWorkRemarks:'',signatureEnabled: false,remarksEnabled: false  },
    { name: 'v) Insufficient', evaluationValue: '1', amountOfWorkSignature: '', amountofWorkRemarks:'', signatureEnabled: false,remarksEnabled: false  },
  ];
  punctualityRows = [
    { name: 'i) Never Delay', evaluationValue: '5', punctualitySignature: '', punctuilityRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii) Rarely Late', evaluationValue: '4', punctualitySignature: '', punctuilityRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) Usually Punctual', evaluationValue: '3', punctualitySignature: '',  punctuilityRemarks:'',signatureEnabled: false ,remarksEnabled: false},
    { name: 'iv) Sometimes Delay', evaluationValue: '2', punctualitySignature: '',  punctuilityRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Habitually Late', evaluationValue: '1', punctualitySignature: '', punctuilityRemarks:'', signatureEnabled: false ,remarksEnabled: false},
  ];

  SenceOfResponsibilityRows = [
    { name: 'i) Always Ready to Perform duties', evaluationValue: '5', senceOfResponsibilityAndCommitmentSignature: '', senseofResponsibilityRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii) Very Wiling to take Responsibility', evaluationValue: '4', senceOfResponsibilityAndCommitmentSignature: '', senseofResponsibilityRemarks:'',  signatureEnabled: false ,remarksEnabled: false},
    { name: 'iii) Traditional', evaluationValue: '3', senceOfResponsibilityAndCommitmentSignature: '', senseofResponsibilityRemarks:'',  signatureEnabled: false ,remarksEnabled: false},
    { name: 'iv) Tendency submit  despite  decision-making power', evaluationValue: '2', senceOfResponsibilityAndCommitmentSignature: '', senseofResponsibilityRemarks:'',  signatureEnabled: false ,remarksEnabled: false},
    { name: 'v) Avoid Responssibility', evaluationValue: '1', senceOfResponsibilityAndCommitmentSignature: '', senseofResponsibilityRemarks:'',  signatureEnabled: false ,remarksEnabled: false},
  ];

  PromptnessTakingMeasuresRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', promptnessTakingMeasuresAndFollowingOrdersSignature: '', promptnessRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii) Very Active', evaluationValue: '4', promptnessTakingMeasuresAndFollowingOrdersSignature: '', promptnessRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii)Activity Is Effortful', evaluationValue: '3', promptnessTakingMeasuresAndFollowingOrdersSignature: '', promptnessRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv) Slow', evaluationValue: '2', promptnessTakingMeasuresAndFollowingOrdersSignature: '', promptnessRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Careless', evaluationValue: '1', promptnessTakingMeasuresAndFollowingOrdersSignature: '', promptnessRemarks:'', signatureEnabled: false,remarksEnabled: false },
  ];

  interestOfWorkRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', interestOfWorkSignature: '', interestWorkRemarks:'', signatureEnabled: false,remarksEnabled: false},
    { name: 'ii) Stedy', evaluationValue: '4', interestOfWorkSignature: '', interestWorkRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii)Responsible', evaluationValue: '3', interestOfWorkSignature: '', interestWorkRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv) Low', evaluationValue: '2', interestOfWorkSignature: '', interestWorkRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Lack of Interest', evaluationValue: '1', interestOfWorkSignature: '', interestWorkRemarks:'',signatureEnabled: false ,remarksEnabled: false},
  ];

  superviseandMeasureRows = [
    { name: 'i)A Source of Motivation for subordinates', evaluationValue: '5', superviseAndManageSignature: '', serviceRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii)Good at Management', evaluationValue: '4', superviseAndManageSignature: '', serviceRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'iii)Assistant to Subordinate', evaluationValue: '3', superviseAndManageSignature: '', serviceRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Lack of Control', evaluationValue: '2', superviseAndManageSignature: '', serviceRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'v)Inferior', evaluationValue: '1', superviseAndManageSignature: '', serviceRemarks:'', signatureEnabled: false,remarksEnabled: false },
  ];

  RelationWithColleaguesRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', relationWithColleaguesSignature: '', RelationshipRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii)Highly Respective and Preferred', evaluationValue: '4', relationWithColleaguesSignature: '', RelationshipRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii)Sincere', evaluationValue: '3', relationWithColleaguesSignature: '', RelationshipRemarks:'',signatureEnabled: false ,remarksEnabled: false},
    { name: 'iv)Avoidence Tendency', evaluationValue: '2', relationWithColleaguesSignature: '', RelationshipRemarks:'',signatureEnabled: false ,remarksEnabled: false},
    { name: 'v)Behaviour is Consistent', evaluationValue: '1', relationWithColleaguesSignature: '',RelationshipRemarks:'', signatureEnabled: false ,remarksEnabled: false},
  ];

  abilityToImplementRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', abilityImplementDecisionSignature: '', abilitytoImplentRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii)Highly Very Good', evaluationValue: '4', abilityImplementDecisionSignature: '', abilitytoImplentRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'iii)Good', evaluationValue: '3', abilityImplementDecisionSignature: '', abilitytoImplentRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Weak', evaluationValue: '2', abilityImplementDecisionSignature: '', abilitytoImplentRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'v)Weak', evaluationValue: '1', abilityImplementDecisionSignature: '', abilitytoImplentRemarks:'',  signatureEnabled: false,remarksEnabled: false },
  ];
  expressivePowerWritingRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', expressivePowerWritingSignature: '', expressiveWritingRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii)Clear,Irrefutable,Orderly', evaluationValue: '4', expressivePowerWritingSignature: '', expressiveWritingRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii)Generally clear and concise', evaluationValue: '3', expressivePowerWritingSignature: '', expressiveWritingRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Not So Good', evaluationValue: '2', expressivePowerWritingSignature: '', expressiveWritingRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'v)Not Clear', evaluationValue: '1', expressivePowerWritingSignature: '', expressiveWritingRemarks:'', signatureEnabled: false ,remarksEnabled: false},
  ];

  expressivePowerSpeechRows = [
    { name: 'i)Extraordinary', evaluationValue: '5', expressivePowerSpeechSignature: '', expressiveSpeechRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii)Strong', evaluationValue: '4', expressivePowerSpeechSignature: '',  expressiveSpeechRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'iii)Much', evaluationValue: '3', expressivePowerSpeechSignature: '', expressiveSpeechRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Not Clear', evaluationValue: '2', expressivePowerSpeechSignature: '', expressiveSpeechRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'v)Vegue and Ineffective', evaluationValue: '1', expressivePowerSpeechSignature: '', expressiveSpeechRemarks:'',signatureEnabled: false ,remarksEnabled: false},
  ];

  overallAssessmentRows = [
    { name: 'i)Extraordinary', evaluationValue: '91-100', overallAssessmentSignature: '',    overallAssesmentRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'ii)High Standard', evaluationValue: '81-90', overallAssessmentSignature: '', overallAssesmentRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'iii)Intelligent', evaluationValue: '65-80', overallAssessmentSignature: '', overallAssesmentRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'iv)Current Status', evaluationValue: '45-64', overallAssessmentSignature: '', overallAssesmentRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'v)Below Expected Value', evaluationValue: '31-44', overallAssessmentSignature: '', overallAssesmentRemarks:'', signatureEnabled: false ,remarksEnabled: false},
    { name: 'v)Low Quality', evaluationValue: '20-30', overallAssessmentSignature: '', overallAssesmentRemarks:'', signatureEnabled: false ,remarksEnabled: false},
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
      row.remarksEnabled=false;
    });

    // Enable signature input for the selected row
    this.professionalKnowledgeRows[index].signatureEnabled = true;
    this.professionalKnowledgeRows[index].remarksEnabled = true;
  }

  toggleQualitySignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.qualityOfWorkRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });

    // Enable signature input for the selected row
    this.qualityOfWorkRows[index].signatureEnabled = true;
    this.qualityOfWorkRows[index].remarksEnabled = true;
  }

    toggleAmountSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.amountOfWorkRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.amountOfWorkRows[index].signatureEnabled = true;
      this.amountOfWorkRows[index].remarksEnabled = true;
    }
    togglePunctualitySignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.punctualityRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.punctualityRows[index].signatureEnabled = true;
      this.punctualityRows[index].remarksEnabled = true;
      
    }
    toggleSenseOfResponsibilitySignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.SenceOfResponsibilityRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.SenceOfResponsibilityRows[index].signatureEnabled = true;
      this.SenceOfResponsibilityRows[index].remarksEnabled = true;
      
    }
    togglePromptnessSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.PromptnessTakingMeasuresRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.PromptnessTakingMeasuresRows[index].signatureEnabled = true;
      this.PromptnessTakingMeasuresRows[index].remarksEnabled = true;
      
    }
    toggleinterestofWorkSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.interestOfWorkRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.interestOfWorkRows[index].signatureEnabled = true;
      this.interestOfWorkRows[index].remarksEnabled = true;
      
    }
    toggleSuperviseMeasureSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.superviseandMeasureRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.superviseandMeasureRows[index].signatureEnabled = true;
      this.superviseandMeasureRows[index].remarksEnabled = true;
      
    }
    toggleRelationColleaguesignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.RelationWithColleaguesRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.RelationWithColleaguesRows[index].signatureEnabled = true;
      this.RelationWithColleaguesRows[index].remarksEnabled = true;
      
    }
    toggleAbilityWithImplementSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.abilityToImplementRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.abilityToImplementRows[index].signatureEnabled = true;
      this.abilityToImplementRows[index].remarksEnabled = true;
      
    }
    toggleExessivePowerWritingSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.expressivePowerWritingRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.expressivePowerWritingRows[index].signatureEnabled = true;
      this.expressivePowerWritingRows[index].remarksEnabled = true;
      
    }
    toggleExessivePowerSpeechSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.expressivePowerSpeechRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.expressivePowerSpeechRows[index].signatureEnabled = true;
      this.expressivePowerSpeechRows[index].remarksEnabled = true;
      
    }
    toggleOverallAssessmentSignatureInput(index: number) {
      // Reset all rows to disable signature input
      this.overallAssessmentRows.forEach((row) => {
        row.signatureEnabled = false;
        row.remarksEnabled=false;
      });
  
      // Enable signature input for the selected row
      this.overallAssessmentRows[index].signatureEnabled = true;
      this.overallAssessmentRows[index].remarksEnabled = true;
      
    }
  }

