import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppraisalRole } from '../../enum/appraisal-role';

@Component({
  selector: 'app-counter-signature-form-officer',
  templateUrl: './counter-signature-form-officer.component.html',
  styleUrl: './counter-signature-form-officer.component.scss'
})
export class CounterSignatureFormOfficerComponent implements OnInit{
  ActiveSection: boolean[] = [false,true,true,true,true,true,false]

  formRecordId: number = 0;
  appraisalRole = AppraisalRole;

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {

  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const recordId = params.get('formRecordId');
      this.formRecordId = recordId == null?0:parseInt(recordId);
    }) 
  }

}
