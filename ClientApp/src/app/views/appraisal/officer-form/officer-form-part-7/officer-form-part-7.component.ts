import { SharedService } from './../service/shared.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-officer-form-part-7',
  templateUrl: './officer-form-part-7.component.html',
  styleUrl: './officer-form-part-7.component.scss'
})
export class OfficerFormPart7Component implements OnInit, OnDestroy{

  allFormData: any;

  @ViewChild('officerFormPart7', { static: true }) OfficerFormPart7Module!: NgForm;

  loading:boolean=false

  constructor( private sharedservice :SharedService ){}

  ngOnInit(): void {
    
  }
  ngOnDestroy(): void {
  }
  onSubmit(form: NgForm) {
  }
}
