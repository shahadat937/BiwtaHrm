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

  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.officerForm2service.officerForm2Module={
      senseOfDisciplineSignature :'',
      senseOfDisciplineEvaluation :0,
      intelegentAndJudgmentSignature :'',
      intelegentAndJudgmentEvaluation :0,
      intelegenceSignature :'',
      intelegenceEvaluation :0,
      energyAndEnthusisamSignature :'',
      energyAndEnthusisamEvaluation :0,
       publicRelationSignature :'',
       publicRelationEvaluation :0,
       cooperationSignature :'',
       cooperationEvaluation :0,
       personalitySignature :'',
       personalityEvaluation :0,
       securityAwarenessSignature :'',
       securityAwarenessEvaluation :0,
       totalMarks : 0,
       totalMarksInWords: 0,
       totalMarksSignature :'',
       totalMarksInWordsSignature :'',
   }
  }
}
