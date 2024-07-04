import { OfficerFormPart6ServiceService } from './../service/officer-form-part6-service.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-officer-form-part-6',
  templateUrl: './officer-form-part-6.component.html',
  styleUrl: './officer-form-part-6.component.scss'
})
export class OfficerFormPart6Component implements OnInit, OnDestroy{

  @ViewChild('officerFormPart6', { static: true }) OfficerFormPart6Module!: NgForm;
  constructor(public OfficerFormPart6ServiceService: OfficerFormPart6ServiceService){
  }

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    console.log("Form Value: ",form.value)
  }

  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.OfficerFormPart6ServiceService.officerFormPart6 = {
      fromDate : new Date(),
      toDate : new Date(),
      additionalComment:'',
      overallAssessment:'',
      recommendation:'',
      signature:null
    }
  }
}
