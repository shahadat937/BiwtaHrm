
import { OfficerFormpart4Service } from './../service/officer-formpart4.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-officer-form-part-4',
  templateUrl: './officer-form-part-4.component.html',
  styleUrl: './officer-form-part-4.component.scss'
})
export class OfficerFormPart4Component  implements OnInit, OnDestroy{
  
  @ViewChild('officerFormPart4', { static: true }) OfficerFormModule!: NgForm;

  loading:boolean=false
  
  constructor(public officerform4service:OfficerFormpart4Service){
  }

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
    this.officerform4service.officerFormpart4 = {
      personalCharacteristics :'',
      professionalSkill:'',
      loyalityAndRelaiability:'',
      otherMatters:'',
      annualConfidentialReportWritingAndCountersigningActivates:'',
      adviceToOfficers:'',
      proficencyAndInterestBengaliLanguage:'',
      signatureOfReportingOfficer:null
    }
  }
}
