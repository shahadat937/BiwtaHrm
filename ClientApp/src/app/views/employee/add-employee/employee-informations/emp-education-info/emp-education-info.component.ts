import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpEducationInfoModule } from '../../../model/emp-education-info.module';
import { EmpEducationInfoService } from '../../../service/emp-education-info.service';
import { EmpTrainingInfoService } from '../../../service/emp-training-info.service';
import { ResultService } from 'src/app/views/basic-setup/service/result.service';
import { Result } from 'src/app/views/basic-setup/model/result';

@Component({
  selector: 'app-emp-education-info',
  templateUrl: './emp-education-info.component.html',
  styleUrl: './emp-education-info.component.scss'
})
export class EmpEducationInfoComponent  implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>(); visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  examTypesOptions: SelectedModel[][] = [];
  examTypes: SelectedModel[] = [];
  boards: SelectedModel[] = [];
  courseDurations: SelectedModel[] = [];
  results: SelectedModel[] = [];
  subGroups: SelectedModel[][] = [];
  pointOptions: Result[] = [];
  maritalStatuses: SelectedModel[] = [];
  yearOptions: number[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[];
  loading: boolean = false;
  empEducation: EmpEducationInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empEducationInfoService: EmpEducationInfoService,
    public empTrainingInfoService: EmpTrainingInfoService,
    public ResultService: ResultService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeEducationInfoByEmpId();
    this.getSelectedExamTypeStatus();
    this.getSelectedBoard();
    this.getSelectedCourseDuration();
    this.getSelectedResults();
    this.getOnlyYears();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }


  getEmployeeEducationInfoByEmpId() {
    this.empEducationInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Update Education Information';
        this.btnText = 'Update';
        this.empEducation = res;
        this.patchEducationInfo(res);
      }
      else {
        this.headerText = 'Add Education Information';
        this.btnText = 'Submit';
        this.addEducation();
      }
    })
  }


  async patchEducationInfo(educationInfoList: any[]) {
    const control = <FormArray>this.EmpEducationInfoForm.controls['empEducationList'];
    control.clear();
    
    for (const [index, educationInfo] of educationInfoList.entries()) {
      this.examTypesOptions[index] = [...this.examTypes];

      // Process examTypeId if it exists
      if (educationInfo.examTypeId) {
        await new Promise<void>((resolve) => {
          this.subscription.push(
            this.empEducationInfoService.getSelectedSubject(educationInfo.examTypeId).subscribe((data) => {
              this.subGroups[index] = data;
              resolve();
            })
          );
        });
      }

      // Process resultId if it exists
      if (educationInfo.resultId) {
        await new Promise<void>((resolve) => {
          this.subscription.push(
            this.ResultService.find(educationInfo.resultId).subscribe((data) => {
              this.pointOptions[index] = data;
              if (data.havePoint) {
                this.empEducationListArray.at(index)?.get('point')?.enable();
                this.empEducationListArray.at(index)?.get('point')?.setValidators([
                  Validators.required, 
                  Validators.max(data.maxPoint || 100)
                ]);
              } else {
                this.empEducationListArray.at(index)?.get('point')?.disable();
              }
              resolve();
            })
          );
        });
      }
      
      // Push the form group after all async operations complete
      control.push(this.fb.group({
        id: [educationInfo.id],
        empId: [educationInfo.empId],
        examTypeId: [educationInfo.examTypeId, Validators.required],
        boardId: [educationInfo.boardId],
        subGroupId: [educationInfo.subGroupId],
        resultId: [educationInfo.resultId, Validators.required],
        point: [educationInfo.point],
        courseDurationId: [educationInfo.courseDurationId],
        passingYear: [educationInfo.passingYear, Validators.required],
        remark: [educationInfo.remark],
      }));
    }
  }
  
  EmpEducationInfoForm: FormGroup = new FormGroup({
    empEducationList: new FormArray([])
  });

  get empEducationListArray() {
    return this.EmpEducationInfoForm.controls["empEducationList"] as FormArray;
  }

  addEducation() {
    this.empEducationListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      examTypeId: new FormControl(undefined, Validators.required),
      boardId: new FormControl(undefined),
      subGroupId: new FormControl(undefined),
      courseDurationId: new FormControl(undefined),
      point: new FormControl(undefined),
      resultId: new FormControl(undefined, Validators.required),
      passingYear: new FormControl(undefined, Validators.required),
      remark: new FormControl(undefined),
    }));
    this.examTypesOptions.push([...this.examTypes]);  // Clone the departments list
    this.subGroups.push([]);
  }

  removeEducationList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empEducationInfoService.deleteEmpEducationInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empEducationListArray.controls.length > 0)
                  this.empEducationListArray.removeAt(index);
                this.getEmployeeEducationInfoByEmpId();
              },
              (err) => {
                this.toastr.error('Somethig Wrong ! ', ` `, {
                  positionClass: 'toast-top-right',
                });
                console.log(err);
              }
            );
          }
        });
    }
    else if (id == 0) {
      if (this.empEducationListArray.controls.length > 0)
        this.empEducationListArray.removeAt(index);
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }


  getSelectedExamTypeStatus() {
    this.empEducationInfoService.getSelectedExamType().subscribe((res) => {
      this.examTypes = res;
    })
  }
  getSelectedBoard() {
    this.empEducationInfoService.getSelectedBoard().subscribe((res) => {
      this.boards = res;
    })
  }
  getSelectedSubGroups(event: Event, index: number){
    const selectElement = event.target as HTMLSelectElement;
    const id = selectElement?.value ? +selectElement.value : null;
    this.empEducationListArray.at(index).get('subGroupId')?.setValue(null);

    if(id){
      this.subGroups[index] = [];
      // this.subscription=
      this.subscription.push(
        this.empEducationInfoService.getSelectedSubject(+id).subscribe((data) => {
        this.subGroups[index] = data; 
      })
      )
      
    }
  }
  
  getSelectedCourseDuration() {
    this.empTrainingInfoService.getSelectedCourseDuration().subscribe((res) => {
      this.courseDurations = res;
    })
  }
  
  getSelectedResults() {
    this.empEducationInfoService.getSelectedResult().subscribe((res) => {
      this.results = res;
    })
  }
  getPointStatusByResult(event: Event, index: number){
    const selectElement = event.target as HTMLSelectElement;
    const id = selectElement?.value ? +selectElement.value : null;
    this.empEducationListArray.at(index).get('point')?.setValue(null);

    if(id){
      // this.subscription=
      this.subscription.push(
        this.ResultService.find(+id).subscribe((data) => {
        this.pointOptions[index] = data; 
        if (data.havePoint) {
          this.empEducationListArray.at(index).get('point')?.enable();
          this.empEducationListArray.at(index).get('point')?.setValidators([
            Validators.required, Validators.max(data.maxPoint || 100)
          ]);
        } else {
          this.empEducationListArray.at(index).get('point')?.disable();
        }
      })
      )
      
    }
  }

  getOnlyYears(){
    const currentYear = new Date().getFullYear();
    for (let year = currentYear; year >= 1970; year--) {
      this.yearOptions.push(year);
    } 
  }

  cancel() {
    this.close.emit();
  }

  insertEducation() {
    this.loading = true;
    this.empEducationInfoService.saveEmpEducationInfo(this.EmpEducationInfoForm.get("empEducationList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.getEmployeeEducationInfoByEmpId();
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
    )
  }
}

