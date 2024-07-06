import { OfficerFormPart7ServiceService } from './../service/officer-form-part7-service.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-officer-form-part-7',
  templateUrl: './officer-form-part-7.component.html',
  styleUrl: './officer-form-part-7.component.scss'
})
export class OfficerFormPart7Component implements OnInit, OnDestroy{

  @ViewChild('officerFormPart7', { static: true }) OfficerFormPart7Module!: NgForm;

  loading:boolean=false
  constructor(public OfficerFormPart7ServiceService: OfficerFormPart7ServiceService){
  }
  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    this.loading=true
    console.log("Form Value: ",form.value)
  }

  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.OfficerFormPart7ServiceService.officerFormPart7 = {
      reportingSignatorySubmissionDate:'',
      counterSignatorySubmissionDate:'',
      reportingSignatoryDelayCauses:'',
      counterSignatoryDelayCauses:'',
      applicationEditor:'',
      signature: null
    }
  }
}
