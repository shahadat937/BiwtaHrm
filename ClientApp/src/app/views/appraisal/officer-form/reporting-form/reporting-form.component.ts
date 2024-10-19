import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';

@Component({
  selector: 'app-reporting-form',
  templateUrl: './reporting-form.component.html',
  styleUrl: './reporting-form.component.scss'
})
export class ReportingFormComponent implements OnInit {
  ActiveSection: boolean[] = [false,true,true,true,true,false,false];
  formRecordId: number = 0;
  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const recordId = params.get('formRecordId');
      this.formRecordId = recordId == null? 0 : parseInt(recordId);
    })
  }


}
