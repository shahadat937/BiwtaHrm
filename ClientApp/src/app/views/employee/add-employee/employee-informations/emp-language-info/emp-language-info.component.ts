import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpLanguageInfoModule } from '../../../model/emp-language-info.module';
import { EmpLanguageInfoService } from '../../../service/emp-language-info.service';

@Component({
  selector: 'app-emp-language-info',
  templateUrl: './emp-language-info.component.html',
  styleUrl: './emp-language-info.component.scss'
})
export class EmpLanguageInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>(); 
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  languageNames: SelectedModel[] = [];
  competences: SelectedModel[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[];
  loading: boolean = false;
  empLanguage: EmpLanguageInfoModule[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empLanguageInfoService: EmpLanguageInfoService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getEmployeeLanguageInfoByEmpId();
    this.getSelectedCompetence();
    this.getSelectedLanguageName();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }


  getEmployeeLanguageInfoByEmpId() {
    this.subscription.push(
      this.empLanguageInfoService.findByEmpId(this.empId).subscribe((res) => {
        if (res.length > 0) {
          this.headerText = 'Update Language Information';
          this.btnText = 'Update';
          this.patchLanguageInfo(res);
        }
        else {
          this.headerText = 'Add Language Information';
          this.btnText = 'Submit';
          this.addLanguage();
        }
      })
    )
   
  }

  patchLanguageInfo(languageInfoList: any[]) {
    const control = <FormArray>this.EmpLanguageInfoForm.controls['empLanguageList'];
    control.clear();

    languageInfoList.forEach(languageInfo => {
      control.push(this.fb.group({
        id: [languageInfo.id],
        empId: [languageInfo.empId],
        languageId: [languageInfo.languageId, Validators.required],
        competenceId: [languageInfo.competenceId, Validators.required],
        remark: [languageInfo.remark],
      }));
    });
  }
  EmpLanguageInfoForm: FormGroup = new FormGroup({
    empLanguageList: new FormArray([])
  });

  get empLanguageListArray() {
    return this.EmpLanguageInfoForm.controls["empLanguageList"] as FormArray;
  }

  addLanguage() {
    this.empLanguageListArray.push(new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      languageId: new FormControl(undefined, Validators.required),
      competenceId: new FormControl(undefined, Validators.required),
      remark: new FormControl(undefined),
    }));
  }

  removeLanguageList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empLanguageInfoService.deleteEmpLanguageInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empLanguageListArray.controls.length > 0)
                  this.empLanguageListArray.removeAt(index);
                this.getEmployeeLanguageInfoByEmpId();
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
      if (this.empLanguageListArray.controls.length > 0)
        this.empLanguageListArray.removeAt(index);
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getSelectedLanguageName() {
    this.subscription.push(
      this.empLanguageInfoService.getSelectedLanguageName().subscribe((res) => {
      this.languageNames = res;
    })
    )
    
  }
  getSelectedCompetence() {
    this.subscription.push(
      this.empLanguageInfoService.getSelectedCompetency().subscribe((res) => {
      this.competences = res;
    })
    )
    
  }

  cancel() {
    this.close.emit();
  }

  insertLanguage() {
    this.loading = true;
    this.subscription.push(
       this.empLanguageInfoService.saveEmpLanguageInfo(this.EmpLanguageInfoForm.get("empLanguageList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        // this.cancel();
        this.getEmployeeLanguageInfoByEmpId();
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
    )
   
    )
  }
}


