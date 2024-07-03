import { OfficerFormPart5Module } from './../model/officer-form-part5.module';
import { OfficerFormPart5ServiceService } from './../service/officer-form-part5-service.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-officer-form-part-5',
  templateUrl: './officer-form-part-5.component.html',
  styleUrl: './officer-form-part-5.component.scss'
})
export class OfficerFormPart5Component  implements OnInit, OnDestroy{
  
  @ViewChild('officerFormPart5', { static: true }) OfficerFormPart5Module!: NgForm;
constructor(public OfficerFormPart5ServiceService: OfficerFormPart5ServiceService){

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
    this.OfficerFormPart5ServiceService.officerFormpart5 = {
      setpeciality :'',
      suitabilityForFurtherEmployment :'',
      recommendMoreServiceTraning :'',
      notEligableForFurtherPromotion :'',
      fasterPromotionIsRecomended :null,
      eligibleForPromotion :null,
      recentlyPromotedNotDueForFurtherPromotion :null,
      notyetEligableForPromotionButWillBeDueCourse :null,
      notEligableForFurtherPromotionMaximumLimitReached :null,
      signature:null
    }
  }
}
