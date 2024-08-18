import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpSpouseInfoService } from '../../../service/emp-spouse-info.service';
import { EmpSpouseInfoModule } from '../../../model/emp-spouse-info.module';

@Component({
  selector: 'app-emp-spouse-info',
  templateUrl: './emp-spouse-info.component.html',
  styleUrl: './emp-spouse-info.component.scss'
})
export class EmpSpouseInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>(); visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  occupations: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  empSpouse: EmpSpouseInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empSpouseInfoService: EmpSpouseInfoService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeSpouseInfoByEmpId();
    this.getSelectedOccupation();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }


  EmpSpouseInfoForm: FormGroup = new FormGroup({
    empSpouseList: new FormArray([])
  });

  get empSpouseListArray() {
    return this.EmpSpouseInfoForm.controls["empSpouseList"] as FormArray;
  }
  
  addSpouse() {
    this.empSpouseListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      spouseName: new FormControl(undefined, Validators.required),
      spouseNameBangla: new FormControl(undefined),
      dateOfBirth: new FormControl(undefined),
      birthRegNo: new FormControl(undefined),
      nid: new FormControl(undefined),
      occupationId: new FormControl(undefined),
      remark: new FormControl(undefined),
    }));
  }

  removeSpouselList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empSpouseInfoService.deleteEmpSpouseInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empSpouseListArray.controls.length > 0)
                  this.empSpouseListArray.removeAt(index);
                this.getEmployeeSpouseInfoByEmpId();
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
      if (this.empSpouseListArray.controls.length > 0)
        this.empSpouseListArray.removeAt(index);
    }
  }


  getEmployeeSpouseInfoByEmpId() {
    this.empSpouseInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        console.log("Spouse Info: ", res)
        this.headerText = 'Update Spouse Information';
        this.btnText = 'Update';
        this.empSpouse = res;
        this.patchSpouseInfo(res);
      }
      else {
        this.headerText = 'Add Spouse Information';
        this.btnText = 'Submit';
        this.addSpouse();
      }
    })
  }

  patchSpouseInfo(spouseInfoList: any[]) {
    const control = <FormArray>this.EmpSpouseInfoForm.controls['empSpouseList'];
    control.clear();

    spouseInfoList.forEach(spouseInfo => {
      control.push(this.fb.group({
        id: [spouseInfo.id],
        empId: [spouseInfo.empId],
        spouseName: [spouseInfo.spouseName, Validators.required],
        spouseNameBangla: [spouseInfo.spouseNameBangla],
        dateOfBirth: [spouseInfo.dateOfBirth],
        birthRegNo: [spouseInfo.birthRegNo],
        nid: [spouseInfo.nid],
        occupationId: [spouseInfo.occupationId],
        remark: [spouseInfo.remark],
      }));
    });
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }


  getSelectedOccupation() {
    this.empSpouseInfoService.getSelectedOccupation().subscribe((res) => {
      this.occupations = res;
    })
  }

  cancel() {
    this.close.emit();
  }

  insertSpouse() {
    this.loading = true;
    this.empSpouseInfoService.saveEmpSpouseInfo(this.EmpSpouseInfoForm.get("empSpouseList")?.value).subscribe(((res: any) => {
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
