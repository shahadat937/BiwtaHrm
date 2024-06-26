import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmpPhotoSignService } from '../../../service/emp-photo-sign.service';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmpPersonalInfoService } from '../../../service/emp-personal-info.service';

@Component({
  selector: 'app-emp-photo-sign',
  templateUrl: './emp-photo-sign.component.html',
  styleUrl: './emp-photo-sign.component.scss'
})
export class EmpPhotoSignComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Input() pNo!: string;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  subscription: Subscription = new Subscription();
  loading: boolean = false;

  empPhoto: File = null!;
  empSignature: File = null!;

  photoPreviewUrl: string | ArrayBuffer | null = null;
  signaturePreviewUrl: string | ArrayBuffer | null = null;

  @ViewChild('EmpPhotoSignForm', { static: true }) EmpPhotoSignForm!: NgForm;


  constructor(
    public empPhotoSignService: EmpPhotoSignService,
    public empPersonalInfoService: EmpPersonalInfoService,
    public userService: UserService,
    private toastr: ToastrService,) {

  }

  ngOnInit(): void {
    this.getEmployeeByEmpId();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getEmployeeByEmpId() {
    this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
      if (res) {
        this.EmpPhotoSignForm?.form.patchValue(res);
        this.photoPreviewUrl = res.photoUrl ? `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}` : null;
        console.log("Photo  : ", this.photoPreviewUrl);
        this.signaturePreviewUrl = res.signatureUrl ? `${this.empPhotoSignService.imageUrl}EmpSignature/${res.signatureUrl}` : null;
        this.headerText = 'Update Employee Photo and Signature';
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add Employee Photo and Signature';
        this.btnText = 'Submit';
        this.initaialForm();
      }
    })
  }
  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empPhotoSignService.empPhotoSign = {
      id: 0,
      empId: this.empId,
      pNo: this.pNo,
      photoUrl: '',
      signatureUrl: '',
      photoFile: null,
      signatureFile: null,
      uniqueIdentity: '',
      remark: '',
      menuPosition: 0,
      isActive: true
    };
  }

  onPhotoSelected(event: any) {
    this.empPhoto = event.target.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      this.photoPreviewUrl = reader.result;
    };
    reader.readAsDataURL(this.empPhoto);
  }
  onSignatureSelected(event: any) {
    this.empSignature = event.target.files[0];
    const reader = new FileReader();
    reader.onload = () => {
      this.signaturePreviewUrl = reader.result;
    };
    reader.readAsDataURL(this.empSignature);
  }

  cancel() {
    this.close.emit();
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.empPhotoSignService.cachedData = [];
    form.value.photoFile = this.empPhoto;
    form.value.signatureFile = this.empSignature;
    form.value.pNo = this.pNo;
    const id = form.value.id;
    const action$ = id
      ? this.empPhotoSignService.updateEmpPhotoSignInfo(id, form.value)
      : this.empPhotoSignService.saveEmpPhotoSignInfo(form.value);
  
    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.cancel();
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    });
  }
  

}
