
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { SharedService } from '../service/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-officer-form-part-6',
  templateUrl: './officer-form-part-6.component.html',
  styleUrl: './officer-form-part-6.component.scss'
})
export class OfficerFormPart6Component implements OnInit, OnDestroy{

  formData: any = {};
  @ViewChild('officerFormPart6', { static: true }) OfficerFormPart6Module!: NgForm;

  loading:boolean=false
 
  constructor( private sharedservice :SharedService,private router: Router ){}

  ngOnInit(): void {
    this.formData=this.sharedservice.getFormData('Part-6')
  }
  ngOnDestroy(): void {
  }

  onSubmit(form: NgForm): void {
    this.sharedservice.setFormData('part-6',this.formData)
    this.router.navigate(['/appraisal/officerFormPart7']);
  }
}
