import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { EmpEducationInfoService } from '../../employee/service/emp-education-info.service';
import { EmpEducationInfoModule } from '../../employee/model/emp-education-info.module';
import { Subscription } from 'rxjs';
import { cilX } from '@coreui/icons';
import { FormRecordService } from '../services/form-record.service';
import { JobHistoryModel } from '../models/job-history-model';
import { ThanaService } from '../../basic-setup/service/thana.service';
import { EmpTrainingInfoService } from '../../employee/service/emp-training-info.service';
import { HttpParams } from '@angular/common/http';
import {InputFieldSyncService} from '../services/input-field-sync.service'
import { EmpWorkHistoryService } from '../../employee/service/emp-work-history.service';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  styleUrl: './field.component.scss'
})
export class FieldComponent implements OnInit, OnChanges, OnDestroy {
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
    this.inputFieldSyncService.emitValueChange(this.fieldUniqueName,this.fieldData);
  }

  onFieldChange(event:any) {
    this.change.emit(event);
    this.inputFieldSyncService.emitValueChange(this.fieldUniqueName,this.fieldData);
  }

  jobHistory: JobHistoryModel[];
  trainingHistory: any[];
  educationInfos: EmpEducationInfoModule[];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  selectedEduInfos : EmpEducationInfoModule[] = [];
  icons = {cilX};

  constructor(
    private inputFieldSyncService: InputFieldSyncService, 
    private empTrainingInfoService: EmpTrainingInfoService,
    private empEducationInfoService: EmpEducationInfoService,
    private empWorkHistoryService: EmpWorkHistoryService,
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
    this.getJobHistory(false);
    const subs = this.inputFieldSyncService.valueChange$.subscribe(data => {
      if(data==null||data.value==null||data.value.associateFieldId==null||data.value.associateFieldId!=this.fieldData.fieldId) {
        return;
      }
      this.getJobHistory(data);
    })

    this.subscription.push(subs);
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    } 
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes["empId"]) {
      this.getEducationInfo(true);
      this.getEmpTrainingInfo();
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

  getJobHistory(data:any) {

    if(this.fieldData.htmlTagName!="table"||this.fieldData.htmlInputType!="jobhistory") {
      return;
    }

    if(data==null||data.value==null||data.value.childFields==null||data.value.childFields.length<2) {
      return;
    }

    let startDate = data.value.childFields[0].fieldValue;
    let endDate = data.value.childFields[1].fieldValue;

    if(startDate==null||startDate==''||endDate==null||endDate==''||this.empId==0) {
      return;
    }
    let params = new HttpParams();
    params = params.set('id', this.empId);
    params = params.set('startDate',startDate);
    params = params.set('endDate', endDate);
    const subs = this.empWorkHistoryService.findCombinedDateRangeEmpHistory(params).subscribe({
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
