
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SharedService } from '../service/shared.service';

@Component({
  selector: 'app-officer-form-part-6',
  templateUrl: './officer-form-part-6.component.html',
  styleUrl: './officer-form-part-6.component.scss'
})
export class OfficerFormPart6Component implements OnInit, OnDestroy{

  formData: any = {};
  @ViewChild('officerFormPart6', { static: true }) OfficerFormPart6Module!: NgForm;

  loading:boolean=false
 
  constructor( private sharedservice :SharedService ){}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    if(form.valid){
      this.sharedservice.setFormData('Part-6',this.formData)
    }
  }
}
