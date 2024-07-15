
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SharedService } from '../service/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-officer-form-part-4',
  templateUrl: './officer-form-part-4.component.html',
  styleUrl: './officer-form-part-4.component.scss'
})
export class OfficerFormPart4Component  implements OnInit, OnDestroy{

  formData: any = {
  personalCharacteristics: '',
  professionalSkill: '',
  loyalityAndRelaiability:'' ,
  otherMatters :'',
  annualConfidentialReportWritingAndCountersigningActivates :'',
  adviceToOfficers :'',
  proficencyAndInterestBengaliLanguage :'',
  signatureOfReportingOfficer : null,
  };
  
  @ViewChild('officerFormPart4', { static: true }) OfficerFormModule!: NgForm;

  loading:boolean=false
  
  constructor( private sharedservice :SharedService,private router: Router ){}

  ngOnInit(): void {
    this.formData=this.sharedservice.getFormData('Part-4')
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    this.sharedservice.setFormData('part-4',this.formData)
    this.router.navigate(['/appraisal/officerFormPart5']);
  }
}
