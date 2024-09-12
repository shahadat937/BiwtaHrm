import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpChildInfoModule } from '../../../model/emp-child-info.module';
import { EmpChildInfoService } from '../../../service/emp-child-info.service';
import { EmpSpouseInfoService } from '../../../service/emp-spouse-info.service';
import { EmpPersonalInfoService } from '../../../service/emp-personal-info.service';

@Component({
  selector: 'app-emp-child-info',
  templateUrl: './emp-child-info.component.html',
  styleUrl: './emp-child-info.component.scss'
})
export class EmpChildInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  occupations: SelectedModel[] = [];
  childStatuses: SelectedModel[] = [];
  genders: SelectedModel[] = [];
  maritalStatuses: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  empChild: EmpChildInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empChildInfoService: EmpChildInfoService,
    public empSpouseInfoService: EmpSpouseInfoService,
    public empPersonalInfoService: EmpPersonalInfoService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeChildInfoByEmpId();
    this.getSelectedChildStatus();
    this.getSelectedMaritalStatuses();
    this.getSelectedOccupation();
    this.getSelectedGenders();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  getEmployeeChildInfoByEmpId() {
    this.empChildInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        console.log("Child Info: ", res)
        this.headerText = 'Update Child Information';
        this.btnText = 'Update';
        this.empChild = res;
        this.patchChildInfo(res);
      }
      else {
        this.headerText = 'Add Child Information';
        this.btnText = 'Submit';
        this.addChild();
      }
    })
  }

  patchChildInfo(childInfoList: any[]) {
    const control = <FormArray>this.EmpChildInfoForm.controls['empChildList'];
    control.clear();

    childInfoList.forEach(childInfo => {
      control.push(this.fb.group({
        id: [childInfo.id],
        empId: [childInfo.empId],
        childName: [childInfo.childName, Validators.required],
        childNameBangla: [childInfo.childNameBangla],
        dateOfBirth: [childInfo.dateOfBirth],
        birthRegNo: [childInfo.birthRegNo],
        nid: [childInfo.nid],
        occupationId: [childInfo.occupationId],
        genderId: [childInfo.genderId],
        maritalStatusId: [childInfo.maritalStatusId],
        childStatusId: [childInfo.childStatusId],
        remark: [childInfo.remark],
      }));
    });
  }
  
  EmpChildInfoForm: FormGroup = new FormGroup({
    empChildList: new FormArray([])
  });

  get empChildListArray() {
    return this.EmpChildInfoForm.controls["empChildList"] as FormArray;
  }

  addChild() {
    this.empChildListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      childName: new FormControl(undefined, Validators.required),
      childNameBangla: new FormControl(undefined),
      dateOfBirth: new FormControl(undefined),
      birthRegNo: new FormControl(undefined),
      nid: new FormControl(undefined),
      occupationId: new FormControl(undefined),
      genderId: new FormControl(undefined),
      maritalStatusId: new FormControl(undefined),
      childStatusId: new FormControl(undefined),
      remark: new FormControl(undefined),
    }));
  }

  removeChildList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empChildInfoService.deleteEmpChildInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empChildListArray.controls.length > 0)
                  this.empChildListArray.removeAt(index);
                this.getEmployeeChildInfoByEmpId();
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
      if (this.empChildListArray.controls.length > 0)
        this.empChildListArray.removeAt(index);
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }


  getSelectedChildStatus() {
    this.empChildInfoService.getSelectedChildStatus().subscribe((res) => {
      this.childStatuses = res;
    })
  }
  getSelectedOccupation() {
    this.empSpouseInfoService.getSelectedOccupation().subscribe((res) => {
      this.occupations = res;
    })
  }
  getSelectedGenders(){
    this.subscription=this.empPersonalInfoService.getSelectedGender().subscribe((data) => { 
      this.genders = data;
    });
  }
  getSelectedMaritalStatuses(){
    this.subscription=this.empPersonalInfoService.getSelectedMaritalStatus().subscribe((data) => { 
      this.maritalStatuses = data;
    });
  }

  cancel() {
    this.close.emit();
  }

  saveChild() {
    this.loading = true;
    this.empChildInfoService.saveEmpChildInfo(this.EmpChildInfoForm.get("empChildList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        // this.cancel();
        this.getEmployeeChildInfoByEmpId();
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
