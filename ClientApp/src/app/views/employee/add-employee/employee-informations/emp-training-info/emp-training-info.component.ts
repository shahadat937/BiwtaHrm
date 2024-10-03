import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpTrainingInfoService } from '../../../service/emp-training-info.service';
import { EmpTrainingInfo } from '../../../model/emp-training-info';
import { CountryService } from 'src/app/views/basic-setup/service/Country.service';

@Component({
  selector: 'app-emp-training-info',
  templateUrl: './emp-training-info.component.html',
  styleUrl: './emp-training-info.component.scss'
})
export class EmpTrainingInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>(); visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  empTrainingInfo: EmpTrainingInfo[] = [];
  trainingTypes: SelectedModel[] = [];
  trainingNames: SelectedModel[] = [];
  institute: SelectedModel[] = [];
  courseDuration: SelectedModel[] = [];
  countris: SelectedModel[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empTrainingInfoService: EmpTrainingInfoService,
    public countryService: CountryService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeTrainingInfoByEmpId();
    this.getSelectedTrainingType();
    this.getSelectedTrainingName();
    this.getSelectedInstitute();
    this.getSelectedCourseDuration();
    this.getSelectedCountries();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  EmpTrainingInfoForm: FormGroup = new FormGroup({
    empTrainingInfoList: new FormArray([])
  });

  get empTrainingListArray() {
    return this.EmpTrainingInfoForm.controls["empTrainingInfoList"] as FormArray;
  }
  
  addTraining() {
    this.empTrainingListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      trainingTypeId: new FormControl(undefined, Validators.required),
      trainingNameId: new FormControl(undefined),
      instituteId: new FormControl(undefined),
      fromDate: new FormControl(undefined),
      toDate: new FormControl(undefined),
      trainingDurationId: new FormControl(undefined),
      countryId: new FormControl(undefined),
      remark: new FormControl(undefined),
    }));
  }

  removeTrainingList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empTrainingInfoService.delete(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empTrainingListArray.controls.length > 0)
                  this.empTrainingListArray.removeAt(index);
                this.getEmployeeTrainingInfoByEmpId();
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
      if (this.empTrainingListArray.controls.length > 0)
        this.empTrainingListArray.removeAt(index);
    }
  }


  getEmployeeTrainingInfoByEmpId() {
    this.empTrainingInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Update Training Information';
        this.btnText = 'Update';
        this.empTrainingInfo = res;
        this.patchTrainingInfo(res);
      }
      else {
        this.headerText = 'Add Training Information';
        this.btnText = 'Submit';
        this.addTraining();
      }
    })
  }

  getSelectedTrainingType(){
    this.subscription = this.empTrainingInfoService.getSelectedTrainingType().subscribe((res) =>{
      this.trainingTypes = res;
    })
  }
  
  getSelectedTrainingName(){
    this.subscription = this.empTrainingInfoService.getSelectedTrainingName().subscribe((res) =>{
      this.trainingNames = res;
    })
  }
  
  getSelectedInstitute(){
    this.subscription = this.empTrainingInfoService.getSelectedInstitute().subscribe((res) =>{
      this.institute = res;
    })
  }
  
  getSelectedCourseDuration(){
    this.subscription = this.empTrainingInfoService.getSelectedCourseDuration().subscribe((res) =>{
      this.courseDuration = res;
    })
  }
  getSelectedCountries() {
    this.subscription = this.countryService.selectGetCountry().subscribe((data) => {
      this.countris = data;
    });
  }

  patchTrainingInfo(trainingInfoList: any[]) {
    const control = <FormArray>this.EmpTrainingInfoForm.controls['empTrainingInfoList'];
    control.clear();

    trainingInfoList.forEach(trainingInfo => {
      control.push(this.fb.group({
        id: [trainingInfo.id],
        empId: [trainingInfo.empId],
        trainingTypeId: [trainingInfo.trainingTypeId, Validators.required],
        trainingNameId: [trainingInfo.trainingNameId],
        instituteId: [trainingInfo.instituteId],
        fromDate: [trainingInfo.fromDate],
        toDate: [trainingInfo.toDate],
        trainingDurationId: [trainingInfo.trainingDurationId],
        countryId: [trainingInfo.countryId],
        remark: [trainingInfo.remark],
      }));
    });
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }


  cancel() {
    this.close.emit();
  }

  insertTraining() {
    this.loading = true;
    this.empTrainingInfoService.save(this.EmpTrainingInfoForm.get("empTrainingInfoList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.getEmployeeTrainingInfoByEmpId();
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
