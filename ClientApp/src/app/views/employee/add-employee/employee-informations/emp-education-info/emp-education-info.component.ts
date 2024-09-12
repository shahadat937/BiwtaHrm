import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpEducationInfoModule } from '../../../model/emp-education-info.module';
import { EmpEducationInfoService } from '../../../service/emp-education-info.service';

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
  examTypes: SelectedModel[] = [];
  boards: SelectedModel[] = [];
  subGroups: SelectedModel[] = [];
  maritalStatuses: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  empEducation: EmpEducationInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empEducationInfoService: EmpEducationInfoService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeEducationInfoByEmpId();
    this.getSelectedExamTypeStatus();
    this.getSelectedBoard();
    this.getSelectedSubGroups();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  getEmployeeEducationInfoByEmpId() {
    this.empEducationInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        console.log("Education Info: ", res)
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

  patchEducationInfo(educationInfoList: any[]) {
    const control = <FormArray>this.EmpEducationInfoForm.controls['empEducationList'];
    control.clear();

    educationInfoList.forEach(educationInfo => {
      control.push(this.fb.group({
        id: [educationInfo.id],
        empId: [educationInfo.empId],
        examTypeId: [educationInfo.examTypeId, Validators.required],
        boardId: [educationInfo.boardId],
        subGroupId: [educationInfo.subGroupId],
        result: [educationInfo.result, Validators.required],
        courseDuration: [educationInfo.courseDuration],
        passingYear: [educationInfo.passingYear, Validators.required],
        remark: [educationInfo.remark],
      }));
    });
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
      result: new FormControl(undefined, Validators.required),
      courseDuration: new FormControl(undefined),
      passingYear: new FormControl(undefined, Validators.required),
      remark: new FormControl(undefined),
    }));
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
  getSelectedSubGroups(){
    this.subscription=this.empEducationInfoService.getSelectedSubject().subscribe((data) => { 
      this.subGroups = data;
    });
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
        // this.cancel();
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

