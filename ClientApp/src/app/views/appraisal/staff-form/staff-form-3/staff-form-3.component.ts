import { StaffForm3ServicesService } from './../service/staff-form3-services.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-staff-form-3',
  templateUrl: './staff-form-3.component.html',
  styleUrl: './staff-form-3.component.scss'
})
export class StaffForm3Component  implements OnInit, OnDestroy{

  @ViewChild('staffForm3', { static: true }) StaffFormpart3Module!: NgForm;

  loading:boolean=false

  constructor(public staffForm3 :StaffForm3ServicesService ){}
  

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
    this.staffForm3.staffForm3Module = {
      interestBengaliLanguage :'',
      overalEvaluationAndPromotion :'',
      date : new Date(),
      reportingOfficeSignature : null,
      remarks :'',
      remarksDate : new Date(),
      countersigningOfficeSignature : null
    }
  }
}

