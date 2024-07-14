import { Router } from '@angular/router';
import { SharedService } from './service/shared.service';
import { NgForm } from '@angular/forms';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-officer-form',
  templateUrl: './officer-form.component.html',
  styleUrl: './officer-form.component.scss'
})
export class OfficerFormComponent implements OnInit, OnDestroy {

  formData: any = { 
      division: '',
      fromdate: '',
      todate: '',
      employeeName: '',
      fathersName: '',
      mothersName: '',
      birthRegNo: '',
      dateofBirth: '',
      designation: '',
      workplace: '',
      joiningDate: '',
      presentDesignationJoiningDate: '',
      education: '',
      trainingSpecialTraining: '',
      reportinFromDate: '',
      reportingEndDate: ''
};

  @ViewChild('officerForm', { static: true }) BloodGroupForm!: NgForm;

  constructor(private sharedService  :SharedService,private router: Router ){}

  ngOnInit(): void {
    this.formData=this.sharedService.getFormData('Part-1')
  }
  ngOnDestroy(): void {
  }
  onSubmit(form: NgForm): void {
    this.sharedService.setFormData('part-1',this.formData)
    this.router.navigate(['/appraisal/officerForm2']);

  }
}
