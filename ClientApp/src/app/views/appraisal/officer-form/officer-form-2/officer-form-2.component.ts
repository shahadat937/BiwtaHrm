import { OfficerFormPart2ServiceService } from './../service/officer-form-part2-service.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-officer-form-2',
  templateUrl: './officer-form-2.component.html',
  styleUrl: './officer-form-2.component.scss'
})
export class OfficerForm2Component  implements OnInit, OnDestroy{

  loading:boolean=false

  constructor(public officerForm2service :OfficerFormPart2ServiceService ){}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    this.loading=true;
    console.log("Form Value: ", form.value)
  }

  SenseOfDisciplineRows = [
    { name: 'i) Extraordinary Standard', evaluationValue: '5', senseOfDisciplineSignature: '', senseOfDisciplineRemarks: '',  signatureEnabled: false,remarksEnabled: false },
    { name: 'ii) High Standard', evaluationValue: '4', senseOfDisciplineSignature: '', senseOfDisciplineRemarks: '', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) Intelligent', evaluationValue: '3', senseOfDisciplineSignature: '', senseOfDisciplineRemarks: '', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv) Below Expected Value', evaluationValue: '2', senseOfDisciplineSignature: '', senseOfDisciplineRemarks: '', signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Low Quality', evaluationValue: '1', senseOfDisciplineSignature: '', senseOfDisciplineRemarks: '', signatureEnabled: false,remarksEnabled: false },
  ];

  intelegentAndJudgmentRows = [
    { name: 'i) Extraordinary Standard', evaluationValue: '5', intelegentAndJudgmentSignature: '', intelegentandjudmentRemarks:'',  signatureEnabled: false,remarksEnabled: false},
    { name: 'ii) High Standard', evaluationValue: '4', intelegentAndJudgmentSignature: '', intelegentandjudmentRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) Reasonable', evaluationValue: '3', intelegentAndJudgmentSignature: '', intelegentandjudmentRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'iv) Flawed and Biased', evaluationValue: '2', intelegentAndJudgmentSignature: '', intelegentandjudmentRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Low Quality', evaluationValue: '1', intelegentAndJudgmentSignature: '', intelegentandjudmentRemarks:'',  signatureEnabled: false,remarksEnabled: false },
  ];

  intelegenceRows = [
    { name: 'i) Great Strength', evaluationValue: '5', intelegenceSignature: '', intelegenceRemarks:'',  signatureEnabled: false ,remarksEnabled: false},
    { name: 'ii) High Strength', evaluationValue: '4', intelegenceSignature: '', intelegenceRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) Intelligent', evaluationValue: '3', intelegenceSignature: '', intelegenceRemarks:'', signatureEnabled: false,remarksEnabled: false},
    { name: 'iv) Below Expected Value', evaluationValue: '2', intelegenceSignature: '', signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Low Quality', evaluationValue: '1', intelegenceSignature: '', intelegenceRemarks:'', signatureEnabled: false,remarksEnabled: false },
  ];

  energyAndEnthuisamRows = [
    { name: 'i) Extraordinary', evaluationValue: '5', energyAndEnthusisamSignature: '', energyandEntuismRemarks:'',  signatureEnabled: false,remarksEnabled: false},
    { name: 'ii) Admirable', evaluationValue: '4', energyAndEnthusisamSignature: '', energyandEntuismRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) Enough', evaluationValue: '3', energyAndEnthusisamSignature: '', energyandEntuismRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Awating Instruction require constant supervision', evaluationValue: '2', energyAndEnthusisamSignature: '', energyandEntuismRemarks:'',signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Shortage', evaluationValue: '1', energyAndEnthusisamSignature: '', energyandEntuismRemarks:'', signatureEnabled: false,remarksEnabled: false },
  ];
  publicRelationRows = [
    { name: 'i) Extraordinary', evaluationValue: '5', publicRelationSignature: '', publicReationRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii) Strong', evaluationValue: '4', publicRelationSignature: '', publicReationRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) Save the opportunity', evaluationValue: '3', publicRelationSignature: '', publicReationRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Friendless', evaluationValue: '2', publicRelationSignature: '', publicReationRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'v) Low Quality', evaluationValue: '1', publicRelationSignature: '', publicReationRemarks:'', signatureEnabled: false,remarksEnabled: false },
  ];
  CooperationRows = [
    { name: 'i) Extraordinary', evaluationValue: '5', cooperationSignature: '', cooperationRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'ii) High Standard', evaluationValue: '4', cooperationSignature: '', cooperationRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) General', evaluationValue: '3', cooperationSignature: '', cooperationRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Sometimes', evaluationValue: '2', cooperationSignature: '', cooperationRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'v)Rarely', evaluationValue: '1', cooperationSignature: '', cooperationRemarks:'',  signatureEnabled: false,remarksEnabled: false },
  ];
  PersonalityRows = [
    { name: 'i) Very Efficent', evaluationValue: '5', personalitySignature: '', personalityRemarks:'',  signatureEnabled: false,remarksEnabled: false  },
    { name: 'ii) Able to gain Lowality', evaluationValue: '4', personalitySignature: '', personalityRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) General', evaluationValue: '3', personalitySignature: '', personalityRemarks:'', signatureEnabled: false,remarksEnabled: false  },
    { name: 'iv)Partially Active', evaluationValue: '2', personalitySignature: '', personalityRemarks:'', signatureEnabled: false,remarksEnabled: false  },
    { name: 'v)Weak', evaluationValue: '1', personalitySignature: '', personalityRemarks:'', signatureEnabled: false,remarksEnabled: false  },
  ];
  securityRows = [
    { name: 'i) Very Concious', evaluationValue: '5', securityAwarenessSignature: '', securityRemarks:'',  signatureEnabled: false,remarksEnabled: false },
    { name: 'ii)Very Aware', evaluationValue: '4', securityAwarenessSignature: '', securityRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iii) General', evaluationValue: '3', securityAwarenessSignature: '', securityRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'iv)Not So Aware', evaluationValue: '2', securityAwarenessSignature: '', securityRemarks:'', signatureEnabled: false,remarksEnabled: false },
    { name: 'v)Unaware of Rules and Regulation', evaluationValue: '1', securityAwarenessSignature: '', securityRemarks:'', signatureEnabled: false,remarksEnabled: false },
  ];
  selectedSenseOfDiscipllineRow: any; 
  selectedIntelegentAndJudgment:any;
  selectedIntelegenceRow:any;
  selectedEnergyEnthuisim:any;
  selectedPublicRelation:any;
  cooperationRow:any;
  personalityRow:any;
  securityRow:any;

  toggleSenseofDisciplineSignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.SenseOfDisciplineRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled = false;
    });

    // Enable signature input for the selected row
    this.SenseOfDisciplineRows[index].signatureEnabled = true;
    this.SenseOfDisciplineRows[index].remarksEnabled = true;
  }
  toggleIntelegentSignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.intelegentAndJudgmentRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });

    // Enable signature input for the selected row
    this.intelegentAndJudgmentRows[index].signatureEnabled = true;
    this.intelegentAndJudgmentRows[index].remarksEnabled=true;
  }
  toggleIntelegenceSignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.intelegenceRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });

    // Enable signature input for the selected row
    this.intelegenceRows[index].signatureEnabled = true;
    this.intelegenceRows[index].remarksEnabled=true;
  }

  toggleEnergySignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.energyAndEnthuisamRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });

    // Enable signature input for the selected row
    this.energyAndEnthuisamRows[index].signatureEnabled = true;
    this.energyAndEnthuisamRows[index].remarksEnabled = true;
  }
  togglePublicRelationSignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.publicRelationRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });

    // Enable signature input for the selected row
    this.publicRelationRows[index].signatureEnabled = true;
    this.publicRelationRows[index].remarksEnabled = true;
  }
  toggleCooperationSignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.CooperationRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });

    // Enable signature input for the selected row
    this.CooperationRows[index].signatureEnabled = true;
    this.CooperationRows[index].remarksEnabled = true;
  }
  togglePersonalitySignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.PersonalityRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });
    this.PersonalityRows[index].signatureEnabled = true;
    this.PersonalityRows[index].remarksEnabled = true;
  }

  toggleSecuritySignatureInput(index: any) {
    // Reset all rows to disable signature input
    this.securityRows.forEach((row) => {
      row.signatureEnabled = false;
      row.remarksEnabled=false;
    });
    this.securityRows[index].signatureEnabled = true;
    this.securityRows[index].remarksEnabled = true;
  }
}
