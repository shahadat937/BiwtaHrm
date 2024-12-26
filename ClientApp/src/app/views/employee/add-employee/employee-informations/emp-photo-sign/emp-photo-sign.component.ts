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
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;

  empPhoto: File = null!;
  empSignature: File = null!;
  
  photoInvalid: boolean = false;
  signatureInvalid: boolean = false;

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
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getEmployeeByEmpId() {
    this.initaialForm();
    this.subscription.push(
      this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
      if (res) {
        this.EmpPhotoSignForm?.form.patchValue(res);
        this.photoPreviewUrl = res.photoUrl ? `${this.empPhotoSignService.imageUrl}EmpPhoto/${res.photoUrl}` : null;
        this.signaturePreviewUrl = res.signatureUrl ? `${this.empPhotoSignService.imageUrl}EmpSignature/${res.signatureUrl}` : null;
        this.headerText = 'Update Employee Photo and Signature';
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add Employee Photo and Signature';
        this.btnText = 'Submit';
      }
    })
    )
    
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
    const file = event.target.files[0];
    if (!file) {
      this.photoInvalid = true;
      return;
    }
    this.empPhoto = file; // Assign the file
  
    const reader = new FileReader();
    const img = new Image();
  
    reader.onload = (e: any) => {
      img.src = e.target.result;
      img.onload = () => {
        const width = img.width;
        const height = img.height;
        if (width !== 300 || height !== 300) {
          this.photoInvalid = true;
          this.photoPreviewUrl = null;
        } else {
          this.photoInvalid = false;
          this.photoPreviewUrl = e.target.result;
        }
      };
    };
    reader.readAsDataURL(file);  // Read file as data URL
  }

  onSignatureSelected(event: any) {
    const file = event.target.files[0];
    if (!file) {
      this.signatureInvalid = true;
      return;
    }
    this.empSignature = file; // Assign the file
  
    const reader = new FileReader();
    const img = new Image();
  
    reader.onload = (e: any) => {
      img.src = e.target.result;
      img.onload = () => {
        const width = img.width;
        const height = img.height;
        if (width !== 300 || height !== 80) {
          this.signatureInvalid = true;
          this.signaturePreviewUrl = null;
        } else {
          this.signatureInvalid = false;
          this.signaturePreviewUrl = e.target.result;
        }
      };
    };
    reader.readAsDataURL(file);  // Read file as data URL
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
  
    // this.subscription = 
    this.subscription.push(
      action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        // this.cancel();
        this.getEmployeeByEmpId();
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
    )
    
  }
  

}
