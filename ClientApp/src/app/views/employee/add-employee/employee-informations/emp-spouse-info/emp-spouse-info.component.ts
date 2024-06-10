import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { FormArray, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpSpouseInfoService } from '../../../service/emp-spouse-info.service';
import { cilPlus, cilShieldAlt } from '@coreui/icons';
import { EmpSpouseInfoModule } from '../../../model/emp-spouse-info.module';

@Component({
  selector: 'app-emp-spouse-info',
  templateUrl: './emp-spouse-info.component.html',
  styleUrl: './emp-spouse-info.component.scss'
})
export class EmpSpouseInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();visible: boolean = true;
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
    public empSpouseInfoService: EmpSpouseInfoService,){}

  icons = { cilPlus, cilShieldAlt };
    
  ngOnInit(): void {
    this.getEmployeeSpouseInfoByEmpId();
    this.getSelectedOccupation();
    this.addSkill();
    // this.dataSvc.getSkillList().subscribe((result) => {
    //   this.skillList = result;
    // });
    // this.addSkill();
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
  addSkill() {
    this.empSpouseListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      spouseName: new FormControl(undefined, Validators.required),
      spouseNameBangla: new FormControl(undefined),
      dateOfBirth: new FormControl(undefined),
      birthRegNo: new FormControl(undefined),
      nid: new FormControl(undefined),
      occupationId: new FormControl(undefined),
    }));
  }

  removeSkillList(index: number) {
    if (this.empSpouseListArray.controls.length > 0)
      this.empSpouseListArray.removeAt(index);
  }

  
  getEmployeeSpouseInfoByEmpId() {
    this.empSpouseInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        console.log("Spouse Info: ",res)
        this.headerText = 'Update Spouse Information';
        this.btnText = 'Update';
        this.empSpouse = res;
      }
      else {
        this.headerText = 'Add Spouse Information';
        this.btnText = 'Submit';
        // this.initaialForm();
      }
    })
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }
  
  // initaialForm(form?: NgForm) {
  //   if (form != null) form.resetForm();
  //   this.empSpouseInfoService.empSpouseInfo = {
  //     id: 0,
  //     empId: this.empId,
  //     spouseName: '',
  //     spouseNameBangla: '',
  //     dateOfBirth : null,
  //     birthRegNo : null,
  //     nid : null,
  //     occupationId : null,
  //     remark: '',
  //     menuPosition: 0,
  //     isActive: true,
  //   };
  // }

  // resetForm() {
  //   this.EmpSpouseInfoForm.form.reset();
  //   this.EmpSpouseInfoForm.form.patchValue({
  //     empId: this.empId,
  //     spouseName: '',
  //     spouseNameBangla: '',
  //     dateOfBirth : null,
  //     birthRegNo : null,
  //     nid : null,
  //     occupationId : null,
  //     remark: '',
  //     menuPosition: 0,
  //     isActive: true,
  //   });
  // }

  getSelectedOccupation(){
    this.empSpouseInfoService.getSelectedOccupation().subscribe((res) => {
      this.occupations = res;
    })
  }

  cancel() {
    this.close.emit();
  }

  insertSpouse() {
    var formData = new FormData();

    // formData.append("skillStringify", JSON.stringify(this.EmpSpouseInfoForm.get("empSpouseList")?.value));

    console.log("Form Value: ", this.EmpSpouseInfoForm.get("empSpouseList")?.value)

    this.empSpouseInfoService.saveEmpSpouseInfo(this.EmpSpouseInfoForm.get("empSpouseList")?.value).subscribe((res=>{
      // if (response.success) {
      //   this.toastr.success('', `${response.message}`, {
      //     positionClass: 'toast-top-right',
      //   });
      //   this.loading = false;
      // } else {
      //   this.toastr.warning('', `${response.message}`, {
      //     positionClass: 'toast-top-right',
      //   });
      //   this.loading = false;
      // }
      // this.loading = false;
    })

    )
    // this.dataSvc.postCandidateSkill(formData).subscribe({
    //   next: r => {
    //     console.log(r);
    //     this.router.navigate(['/masterDetails']);
    //     this.notifySvc.message('Data inserted successfully!!!', 'DISMISS');
    //   },
    //   error: err => {
    //     console.log(err);
    //     this.notifySvc.message('Data inserted Failed!!!', 'DISMISS');
    //   }
    // })
  }



  onSubmit(form: NgForm): void {
    console.log("Form Value: ", form.value)
  }

}
