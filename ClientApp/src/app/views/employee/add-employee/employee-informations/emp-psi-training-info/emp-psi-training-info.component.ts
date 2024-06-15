import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpPsiTrainingInfoModule } from '../../../model/emp-psi-training-info.module';
import { EmpPsiTrainingInfoService } from '../../../service/emp-psi-training-info.service';

@Component({
  selector: 'app-emp-psi-training-info',
  templateUrl: './emp-psi-training-info.component.html',
  styleUrl: './emp-psi-training-info.component.scss'
})
export class EmpPsiTrainingInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>(); visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  trainingNames: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  empPsiTraining: EmpPsiTrainingInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empPsiTrainingInfoService: EmpPsiTrainingInfoService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeePsiTrainingInfoByEmpId();
    this.getSelectedTrainingName();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  getEmployeePsiTrainingInfoByEmpId() {
    this.empPsiTrainingInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Update PSI Training Information';
        this.btnText = 'Update';
        this.patchPsiTrainingInfo(res);
      }
      else {
        this.headerText = 'Add PSI Training Information';
        this.btnText = 'Submit';
        this.addPsiTraining();
      }
    })
  }

  patchPsiTrainingInfo(psiTrainingInfoList: any[]) {
    const control = <FormArray>this.EmpPsiTrainingInfoForm.controls['empPsiTrainingList'];
    control.clear();

    psiTrainingInfoList.forEach(psiTrainingInfo => {
      control.push(this.fb.group({
        id: [psiTrainingInfo.id],
        empId: [psiTrainingInfo.empId],
        trainingNameId: [psiTrainingInfo.trainingNameId, Validators.required],
        workPurpose: [psiTrainingInfo.workPurpose],
        fromDate: [psiTrainingInfo.fromDate],
        toDate: [psiTrainingInfo.toDate],
        remark: [psiTrainingInfo.remark],
      }));
    });
  }
  EmpPsiTrainingInfoForm: FormGroup = new FormGroup({
    empPsiTrainingList: new FormArray([])
  });

  get empPsiTrainingListArray() {
    return this.EmpPsiTrainingInfoForm.controls["empPsiTrainingList"] as FormArray;
  }

  addPsiTraining() {
    this.empPsiTrainingListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      trainingNameId: new FormControl(undefined, Validators.required),
      workPurpose: new FormControl(undefined),
      fromDate: new FormControl(undefined),
      toDate: new FormControl(undefined),
      remark: new FormControl(undefined),
    }));
  }

  removePsiTrainingList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empPsiTrainingInfoService.deleteEmpPsiTrainingInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empPsiTrainingListArray.controls.length > 0)
                  this.empPsiTrainingListArray.removeAt(index);
                this.getEmployeePsiTrainingInfoByEmpId();
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
      if (this.empPsiTrainingListArray.controls.length > 0)
        this.empPsiTrainingListArray.removeAt(index);
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }


  getSelectedTrainingName() {
    this.empPsiTrainingInfoService.getSelectedTrainingName().subscribe((res) => {
      this.trainingNames = res;
    })
  }

  cancel() {
    this.close.emit();
  }

  insertPsiTraining() {
    this.loading = true;
    this.empPsiTrainingInfoService.saveEmpPsiTrainingInfo(this.EmpPsiTrainingInfoForm.get("empPsiTrainingList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.cancel();
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


