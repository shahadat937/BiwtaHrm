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
selectedValue: any;

  constructor(public officerForm2service :OfficerFormPart2ServiceService ){}

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
    this.officerForm2service.officerForm2Module={
      senseOfDiscipline1 :'',
      senseOfDiscipline2 :'',
      senseOfDiscipline3 :'',
      senseOfDiscipline4 :'',
      senseOfDiscipline5 :'',
      senseOfDisciplineRadio :0,
      intelegentAndJudgment1 :'',
      intelegentAndJudgment2 :'',
      intelegentAndJudgment3 :'',
      intelegentAndJudgment4 :'',
      intelegentAndJudgment5 :'',
      intelegentAndJudgmentRadio :0,
      intelegence1 :'',
      intelegence2 :'',
      intelegence3 :'',
      intelegence4 :'',
      intelegence5 :'',
      intelegenceRadio :0,
      energyAndEnthusisam1 :'',
      energyAndEnthusisam2 :'',
      energyAndEnthusisam3 :'',
      energyAndEnthusisam4 :'',
      energyAndEnthusisam5 :'',
      energyAndEnthusisamRadio :0,
       publicRelation1 :'',
       publicRelation2 :'',
       publicRelation3 :'',
       publicRelation4 :'',
       publicRelation5 :'',
       publicRelationRadio :0,
       cooperation1 :'',
       cooperation2 :'',
       cooperation3 :'',
       cooperation4 :'',
       cooperation5 :'',
       cooperationRadio :0,
       personality1 :'',
       personality2 :'',
       personality3 :'',
       personality4 :'',
       personality5 :'',
       personalityRadio :0,
       securityAwareness1 :'',
       securityAwareness2 :'',
       securityAwareness3 :'',
       securityAwareness4 :'',
       securityAwareness5 :'',
       securityAwarenessRadio :0,
       totalMarks : 0,
       totalMarksInWords: 0,
       totalMarksSignature :'',
       totalMarksInWordsSignature :'',
   }
  }
}
