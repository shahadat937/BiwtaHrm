import { SharedService } from './service/shared.service';
import { NgForm } from '@angular/forms';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-officer-form',
  templateUrl: './officer-form.component.html',
  styleUrl: './officer-form.component.scss'
})
export class OfficerFormComponent implements OnInit, OnDestroy {

  formData: any = {}; // Your form data model

  @ViewChild('officerForm', { static: true }) BloodGroupForm!: NgForm;

  loading :boolean=false;

  constructor(private sharedService  :SharedService ){}

  ngOnInit(): void {
  }
  ngOnDestroy(): void {
  }
  onSubmit(form: NgForm): void {
    if(form.valid){
      this.sharedService.setFormData('Part-1',this.formData)
    }
  }
}
