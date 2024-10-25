import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { EmpEducationInfoService } from '../../employee/service/emp-education-info.service';
import { EmpEducationInfoModule } from '../../employee/model/emp-education-info.module';
import { Subscription } from 'rxjs';
import { cilX } from '@coreui/icons';
import { FormRecordService } from '../services/form-record.service';
import { JobHistoryModel } from '../models/job-history-model';
import { ThanaService } from '../../basic-setup/service/thana.service';

@Component({
  selector: 'app-field',
  templateUrl: './field.component.html',
  styleUrl: './field.component.scss'
})
export class FieldComponent implements OnInit, OnChanges, OnDestroy {
  @Input() fieldData:any;
  @Input() fieldUniqueName: string;
  @Input() Index:any;
  @Input() IsReadonly: boolean
  @Input() empId: number

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
  educationInfos: EmpEducationInfoModule[];
  subscription: Subscription = new Subscription();
  selectedEduInfos : EmpEducationInfoModule[] = [];
  icons = {cilX};

  constructor(
    private empEducationInfoService: EmpEducationInfoService,
    private formRecordService: FormRecordService, 
  ) {
    this.fieldUniqueName = "default";
    this.Index = "";
    this.IsReadonly = false;
    this.empId = 0;
    this.educationInfos = [];
    this.jobHistory = [];
  }

  ngOnInit(): void {

    this.getEducationInfo(false);
    this.getJobHistory();
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes["empId"])
    this.getEducationInfo(true);
  }

  getEducationInfo(reset: boolean) {
    if(this.empId!=0&&this.fieldData.htmlInputType=="educationinfo") {
      this.subscription = this.empEducationInfoService.findByEmpId(this.empId).subscribe({
        next: response => {
          this.educationInfos = response;
          if(reset&&this.IsReadonly==false) {
            this.resetEducationInfo();
          } else {
            this.getSelectedEducationInfo();
          }
        }
      })
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
}
