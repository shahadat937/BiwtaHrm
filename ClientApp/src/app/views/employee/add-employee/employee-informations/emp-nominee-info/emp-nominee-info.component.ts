import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpNomineeInfoService } from '../../../service/emp-nominee-info.service';
import { EmpPersonalInfoService } from '../../../service/emp-personal-info.service';

@Component({
  selector: 'app-emp-nominee-info',
  templateUrl: './emp-nominee-info.component.html',
  styleUrls: ['./emp-nominee-info.component.scss']
})
export class EmpNomineeInfoComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Input() pNo!: string;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  relationType: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;

  constructor(
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empNomineeInfoService: EmpNomineeInfoService,
    public empPersonalInfoService: EmpPersonalInfoService,
    private fb: FormBuilder) { }

  ngOnInit(): void {
    this.getEmployeeNomineeInfoByEmpId();
    this.getSelectedRelation();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getEmployeeNomineeInfoByEmpId() {
    this.empNomineeInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Update Nominee Information';
        this.btnText = 'Update';
        this.patchNomineeInfo(res);
      } else {
        this.headerText = 'Add Nominee Information';
        this.btnText = 'Submit';
        this.addNominee();
      }
    });
  }

  patchNomineeInfo(nomineeInfoList: any[]) {
    const control = <FormArray>this.EmpNomineeInfoForm.controls['empNomineeList'];
    control.clear();

    nomineeInfoList.forEach(nomineeInfo => {
      control.push(this.fb.group({
        id: [nomineeInfo.id],
        empId: [nomineeInfo.empId],
        nomineeName : [nomineeInfo.nomineeName, Validators.required],
        dateOfBirth: [nomineeInfo.dateOfBirth],
        birthRegNo: [nomineeInfo.birthRegNo],
        nid: [nomineeInfo.nid],
        relationId : [nomineeInfo.relationId, Validators.required],
        percentage : [nomineeInfo.percentage],
        address : [nomineeInfo.address ],
        photoUrl: [nomineeInfo.photoUrl],
        signatureUrl: [nomineeInfo.signatureUrl],
        uniqueIdentity: [nomineeInfo.uniqueIdentity],
        remark: [nomineeInfo.remark],
        isActive: [nomineeInfo.isActive],
        photoFile: new FormControl(undefined),
        signatureFile: new FormControl(undefined),
        photoPreviewUrl: new FormControl(nomineeInfo.photoUrl ? `assets/Images/EmpPhoto/${nomineeInfo.photoUrl}` : ''),
        signaturePreviewUrl: new FormControl(nomineeInfo.signatureUrl ? `assets/Images/EmpSignature/${nomineeInfo.signatureUrl}` : '')
      }));
    });
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  onPhotoSelected(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.empNomineeListArray.controls[index].get('photoPreviewUrl')?.setValue(reader.result);
      };
      reader.readAsDataURL(file);
      this.empNomineeListArray.controls[index].get('photoFile')?.setValue(file);
    }
  }

  onSignatureSelected(event: any, index: number) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = () => {
        this.empNomineeListArray.controls[index].get('signaturePreviewUrl')?.setValue(reader.result);
      };
      reader.readAsDataURL(file);
      this.empNomineeListArray.controls[index].get('signatureFile')?.setValue(file);
    }
  }

  EmpNomineeInfoForm: FormGroup = this.fb.group({
    empNomineeList: this.fb.array([])
  });

  get empNomineeListArray() {
    return this.EmpNomineeInfoForm.controls["empNomineeList"] as FormArray;
  }

  addNominee() {
    this.empNomineeListArray.push(this.fb.group({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      pNo: new FormControl(this.pNo),
      nomineeName: new FormControl("", Validators.required),
      dateOfBirth: new FormControl(""),
      birthRegNo: new FormControl(""),
      nid: new FormControl(""),
      relationId: new FormControl(null),
      percentage: new FormControl(0),
      address: new FormControl(""),
      photoFile: new FormControl(null),
      signatureFile: new FormControl(null),
      uniqueIdentity: new FormControl(""),
      remark: new FormControl(""),
      isActive: new FormControl(true),
      photoPreviewUrl: new FormControl(null),
      signaturePreviewUrl: new FormControl(null),
    }));
  }

  removeNomineeList(index: number, id: number) {
    if (id != 0) {
      this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This Item')
        .subscribe((result) => {
          if (result) {
            this.empNomineeInfoService.deleteEmpNomineeInfo(id).subscribe(
              (res) => {
                this.toastr.warning('Deleted successfully!', ` `, {
                  positionClass: 'toast-top-right',
                });

                if (this.empNomineeListArray.controls.length > 0) {
                  this.empNomineeListArray.removeAt(index);
                }
              },
              (err) => {
                this.toastr.error('Something went wrong!', ` `, {
                  positionClass: 'toast-top-right',
                });
                console.log(err);
              }
            );
          }
        });
    } else if (id == 0) {
      if (this.empNomineeListArray.controls.length > 0) {
        this.empNomineeListArray.removeAt(index);
      }
    }
  }

  getSelectedRelation() {
    this.subscription = this.empPersonalInfoService.getSelectedRelationType().subscribe((data) => {
      this.relationType = data;
    });
  }

  cancel() {
    this.close.emit();
  }

  saveNominee() {
    this.loading = true;
    const formData = this.EmpNomineeInfoForm.get('empNomineeList')?.value;
    console.log("Form Data: ", formData);
    this.empNomineeInfoService.saveEmpNomineeInfo(formData).subscribe((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        // this.cancel();
      this.getEmployeeNomineeInfoByEmpId();
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
    });
  }

  saveNomineeSingle() {
    const empNomineeList = this.EmpNomineeInfoForm.get('empNomineeList')?.value as any[]; // Replace 'any' with the 
    empNomineeList.forEach((nominee) => {
      this.loading = true;
        this.empNomineeInfoService.saveEmpNomineeInfo(nominee).subscribe({
          next: (response:any) => {
            if(response.success) {
              this.toastr.success('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })
            } else {
              this.toastr.warning('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })
            }
          },
          error: (err) => {
            this.loading = false;
          },
          complete: () => {
            this.loading = false;
          }
        })
    });
    // this.empNomineeListArray.controls.forEach(control => {
    //   this.loading = true;
    //   // let formData = this.convertFormGroupToFormData(control as FormGroup);
    //   // this.empNomineeInfoService.saveEmpNomineeInfo(control.value).subscribe({
    //   //   next: (response:any) => {
    //   //     if(response.success) {
    //   //       this.toastr.success('',`${response.message}`, {
    //   //         positionClass: 'toast-top-right'
    //   //       })
    //   //     } else {
    //   //       this.toastr.warning('',`${response.message}`, {
    //   //         positionClass: 'toast-top-right'
    //   //       })
    //   //     }
    //   //   },
    //   //   error: (err) => {
    //   //     this.loading = false;
    //   //   },
    //   //   complete: () => {
    //   //     this.loading = false;
    //   //   }
    //   // })
    // })
    this.getEmployeeNomineeInfoByEmpId();
  }

  private convertFormGroupToFormData(formGroup: FormGroup): FormData {
    let formData = new FormData();
    Object.keys(formGroup.controls).forEach((key) => {
      const control = formGroup.get(key);
      if (control) {
        const value = control.value;
        // Check if the control value is an array (e.g., for file inputs)
        if (Array.isArray(value)) {
          value.forEach((file: File) => {
            formData.append(key, file);
          });
        } else {
          formData.append(key, value);
        }
      }
    });
    return formData;
  }
  
}
