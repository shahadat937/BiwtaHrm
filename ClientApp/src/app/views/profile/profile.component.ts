import { Component, OnInit } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup } from '@angular/forms';



@Component({
  templateUrl: 'profile.component.html',
  styleUrls: ['profile.component.scss']
})
export class ProfileComponent implements OnInit {

  empId : number = 0;

  constructor() {
  }

  ngOnInit(): void {

  }

}
