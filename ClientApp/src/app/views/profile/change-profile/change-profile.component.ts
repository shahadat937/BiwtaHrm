import { Component, ElementRef, HostListener, Input, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpPersonalInfoService } from '../../employee/service/emp-personal-info.service';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';
import { UserService } from '../../usermanagement/service/user.service';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { EmpBasicInfoService } from '../../employee/service/emp-basic-info.service';

@Component({
  selector: 'app-change-profile',
  templateUrl: './change-profile.component.html',
  styleUrl: './change-profile.component.scss'
})

export class ChangeProfileComponent implements OnInit, OnDestroy {

  id: number = 0;
  clickedButton: string = '';
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  pNo: string = '';
  headerText: string = '';
  modalOpened: boolean = false;
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
    public empBasicInfoService: EmpBasicInfoService,
    private toastr: ToastrService,
    private bsModalRef: BsModalRef,
    private el: ElementRef,
    private renderer: Renderer2) {

  }

  ngOnInit(): void {
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
    this.getEmployeeByEmpId();
    this.headerText = this.clickedButton == 'ChangeProfile' ? 'Change Profile Picture' :
      this.clickedButton == 'ChangeSignature' ? 'Change Signature' : '';
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  getEmployeeByEmpId() {
    this.initaialForm();
    this.subscription.push(
      this.empBasicInfoService.findByEmpId(this.id).subscribe((res) => {
      this.pNo = res.idCardNo;
    })
    )
    this.subscription.push(
      this.empPhotoSignService.findByEmpId(this.id).subscribe((res) => {
      if (res) {
        this.EmpPhotoSignForm?.form.patchValue(res);
        this.photoPreviewUrl = res.photoUrl ? `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}` : null;
        this.signaturePreviewUrl = res.signatureUrl ? `${this.empPhotoSignService.imageUrl}EmpSignature/${res.signatureUrl}` : null;
      }
    })
    )
    
  }
  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empPhotoSignService.empPhotoSign = {
      id: 0,
      empId: this.id,
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
    reader.readAsDataURL(this.empPhoto);
  }

  onSignatureSelected(event: any) {
    this.empSignature = event.target.files[0];
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
    reader.readAsDataURL(this.empSignature);
  }

  closeModal(): void {
    this.bsModalRef.hide();
  }

  @HostListener('document:click', ['$event'])
  onClickOutside(event: MouseEvent): void {
    if (this.modalOpened) {
      const modalElement = this.el.nativeElement.querySelector('.modal-content');
      if (modalElement && !modalElement.contains(event.target as Node)) {
        this.shakeModal();
      }
    }
  }

  shakeModal(): void {
    const modalElement = this.el.nativeElement.querySelector('.modal-content');
    if (modalElement) {
      this.renderer.addClass(modalElement, 'shake');
      setTimeout(() => {
        this.renderer.removeClass(modalElement, 'shake');
      }, 500);
    }
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
        this.closeModal();
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
