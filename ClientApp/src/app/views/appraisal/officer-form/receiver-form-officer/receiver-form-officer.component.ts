import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppraisalRole } from '../../enum/appraisal-role';

@Component({
  selector: 'app-receiver-form-officer',
  templateUrl: './receiver-form-officer.component.html',
  styleUrl: './receiver-form-officer.component.scss'
})
export class ReceiverFormOfficerComponent implements OnInit {
  ActiveSection: boolean[] = [false,false,false,false,false,false,true]
  formRecordId: number = 0
  appraisalRole = AppraisalRole

  constructor(
    private router: Router,
    private route: ActivatedRoute
  ) {

  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const recordId = params.get('formRecordId');
      this.formRecordId = recordId == null? 0: parseInt(recordId);
    })
  }
}
