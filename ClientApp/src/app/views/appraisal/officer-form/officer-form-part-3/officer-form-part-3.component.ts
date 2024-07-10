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
  
  formRows = [
    { name: 'i) Significant Knowledge', evaluationValue: '5', professionalKnowledgeSignature: '', signatureEnabled: false },
    { name: 'ii) Sufficient Knowledge', evaluationValue: '4', professionalKnowledgeSignature: '', signatureEnabled: false },
    { name: 'iii) Fairly Good Knowledge', evaluationValue: '3', professionalKnowledgeSignature: '', signatureEnabled: false },
    { name: 'iv) Insufficient', evaluationValue: '2', professionalKnowledgeSignature: '', signatureEnabled: false },
    { name: 'v) Low Quality', evaluationValue: '1', professionalKnowledgeSignature: '', signatureEnabled: false },
  ];

  qualityOfWorkRows = [
    { name: 'i) Accurate and Complete', evaluationValue: '5', qualityOfWorkSignature: '', signatureEnabled: false },
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

  selectedRow: any; // Variable to store the selected row
  selectedQualityRow: any; // Variable to store the selected row
  selectedAmountRow:any;


  toggleSignatureInput(index: number) {
    // Reset all rows to disable signature input
    this.formRows.forEach((row, i) => {
      row.signatureEnabled = false;
    });

    // Enable signature input for the selected row
    this.formRows[index].signatureEnabled = true;
  }

  toggleQualitySignatureInput(index: number) {
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
  }

