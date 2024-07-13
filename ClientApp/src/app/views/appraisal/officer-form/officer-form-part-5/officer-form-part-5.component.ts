import { SharedService } from '../service/shared.service';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-officer-form-part-5',
  templateUrl: './officer-form-part-5.component.html',
  styleUrl: './officer-form-part-5.component.scss'
})
export class OfficerFormPart5Component  implements OnInit, OnDestroy{

  formData: any = {};
  
  @ViewChild('officerFormPart5', { static: true }) OfficerFormPart5Module!: NgForm;

  loading:boolean=false

  constructor( private sharedservice :SharedService ){}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    if(form.valid){
      this.sharedservice.setFormData('Part-5',this.formData)
    }
  }
}
