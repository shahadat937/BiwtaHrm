import { Component, EventEmitter, Input, Output, SimpleChange } from '@angular/core';
import { EmpEducationInfoModule } from '../../employee/model/emp-education-info.module';
import { Subscription } from 'rxjs';
import { cilX } from '@coreui/icons';
import { EmpTrainingInfoService } from '../../employee/service/emp-training-info.service';
import { EmpEducationInfoService } from '../../employee/service/emp-education-info.service';
import { FormRecordService } from '../services/form-record.service';
import { JobHistoryModel } from '../models/job-history-model';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-field-print',
  templateUrl: './field-print.component.html',
  styleUrl: './field-print.component.scss'
})
export class FieldPrintComponent {
  @Input() fieldData:any;
  @Input() fieldUniqueName: string;
  @Input() Index:any;
  @Input() IsReadonly: boolean;
  @Input() empId: number;
  @Input() IsChild: boolean;

  fieldValue: string = "";
  @Output() fieldChange = new EventEmitter();
  @Output() change = new EventEmitter();

  @Input()
  get field() {
    return this.fieldValue;
  }

  set field(value) {
    this.fieldValue = value;
    this.fieldChange.emit(this.fieldValue);
  }

  onFieldChange(event:any) {
    this.change.emit(event);
  }

  jobHistory: JobHistoryModel[];
  trainingHistory: any[];
  educationInfos: EmpEducationInfoModule[];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  selectedEduInfos : EmpEducationInfoModule[] = [];
  icons = {cilX};

  constructor(
    private empTrainingInfoService: EmpTrainingInfoService,
    private empEducationInfoService: EmpEducationInfoService,
    private formRecordService: FormRecordService, 
  ) {
    this.fieldUniqueName = "default";
    this.Index = "";
    this.IsReadonly = false;
    this.empId = 0;
    this.educationInfos = [];
    this.jobHistory = [];
    this.trainingHistory = [];
    this.IsChild = false;
  }

  ngOnInit(): void {

    this.getEducationInfo(false);
    this.getEmpTrainingInfo();
    this.getJobHistory();
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    } 
  }


  getEducationInfo(reset: boolean) {
    if(this.empId!=0&&this.fieldData.htmlInputType=="educationinfo") {
      this.subscription.push(
      this.empEducationInfoService.findByEmpId(this.empId).subscribe({
        next: response => {
          this.educationInfos = response;
          if(reset&&this.IsReadonly==false) {
            this.resetEducationInfo();
          } else {
            this.getSelectedEducationInfo();
          }
        }
      })
      )
      
    }
  }

  resetEducationInfo() {
    this.selectedEduInfos = this.educationInfos;
    this.fieldValue = this.selectedEduInfos.map(x=>x.id).join(',');
    this.fieldChange.emit(this.fieldValue);
  }

  removeEducationInfo(id:number) {
    this.selectedEduInfos = this.selectedEduInfos.filter(x=>x.id!=id);
    this.fieldValue = this.selectedEduInfos.map(x=>x.id).join(',');
    this.fieldChange.emit(this.fieldValue);
  }

  getSelectedEducationInfo() {
    let ids =this.field.trim().split(',').map(x=>parseInt(x));
    this.selectedEduInfos = this.educationInfos.filter(x=>ids.includes(x.id));
  }

  getJobHistory() {

    if(this.fieldData.htmlTagName!="table"||this.fieldData.htmlInputType!="jobhistory") {
      return;
    }
    let startDate = "2002-01-01";
    let endDate = "2025-01-01";
    this.formRecordService.getJobHistory(this.empId,startDate,endDate).subscribe({
      next: response => {
        this.jobHistory = response;
      }
    })
  }

  getEmpTrainingInfo() {
    if(this.empId !=0 && this.fieldData.htmlInputType=="trainingHistory") {
      let params = new HttpParams;
      params = params.set("empId", this.empId);
      this.subscription.push(
        this.empTrainingInfoService.getEmpTrainingInfo(params).subscribe({
        next: response => {
          if(response) {
            this.trainingHistory = response;
          }
        },
        error: err => {
          console.log(err);
        },
        complete: () => {

        }
      })
      )
      
    }
  }

  indexToAlpha(index:number) {
    let result = '';
    while (index >= 0) {
        result = String.fromCharCode(index % 26 + 97) + result;  
        index = Math.floor(index / 26) - 1;
    }
    return result;
  }
}
