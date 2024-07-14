
import { OfficerFormpart4Service } from './../service/officer-formpart4.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SharedService } from '../service/shared.service';

@Component({
  selector: 'app-officer-form-part-4',
  templateUrl: './officer-form-part-4.component.html',
  styleUrl: './officer-form-part-4.component.scss'
})
export class OfficerFormPart4Component  implements OnInit, OnDestroy{

  formData: any = {};
  
  @ViewChild('officerFormPart4', { static: true }) OfficerFormModule!: NgForm;

  loading:boolean=false
  
  constructor( private sharedservice :SharedService ){}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    if(form.valid){
      this.sharedservice.setFormData('Part-4',this.formData)
    }
  }
}
