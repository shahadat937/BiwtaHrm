import { SharedService } from '../service/shared.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-officer-form-part-5',
  templateUrl: './officer-form-part-5.component.html',
  styleUrl: './officer-form-part-5.component.scss'
})
export class OfficerFormPart5Component  implements OnInit, OnDestroy{

  formData: any = {
  setpeciality:'',
  suitabilityForFurtherEmployment :'',
  recommendMoreServiceTraning:'',
  notEligableForFurtherPromotion:'',
  fasterPromotionIsRecomended : null,
  eligibleForPromotion :null,
  recentlyPromotedNotDueForFurtherPromotion :null,
  notyetEligableForPromotionButWillBeDueCourse : null,
  notEligableForFurtherPromotionMaximumLimitReached :null,
  signature :null
  };
  
  @ViewChild('officerFormPart5', { static: true }) OfficerFormPart5Module!: NgForm;

  loading:boolean=false

  constructor( private sharedservice :SharedService,private router: Router ){}

  ngOnInit(): void {
    this.formData=this.sharedservice.getFormData('Part-5')
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    this.sharedservice.setFormData('part-4',this.formData)
    this.router.navigate(['/appraisal/officerFormPart6']);
  }
}
