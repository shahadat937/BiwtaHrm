import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { FeatureManagementService } from '../service/feature-management.service';

@Component({
  selector: 'app-create-module',
  templateUrl: './create-module.component.html',
  styleUrl: './create-module.component.scss'
})
export class CreateModuleComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;

  @ViewChild('ModuleForm', { static: true }) ModuleForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public featureManagementService: FeatureManagementService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    this.initaialModule();
    this.handleText();
    this.getModuleInfo();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }

  handleText(){
    this.heading = this.clickedButton == 'Edit' ? 'Edit Module Information' : 'Create Module Information';
    this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
    this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
  }

  getModuleInfo() {
    this.subscription = this.featureManagementService.find(this.id).subscribe((res) => {
      if(res){
        this.ModuleForm?.form.patchValue(res);
      }
    });
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  initaialModule(form?: NgForm) {
    if (form != null) form.resetForm();
    this.featureManagementService.modules = {
      moduleId :  0,
      title :  '',
      moduleName :  '',
      iconName :  '',
      icon :  '',
      class :  '',
      groupTitle :  '',
      status :  0,
      menuPosition :  0,
      isActive :  true,
    };
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
    this.featureManagementService.cachedData = [];
    const id = form.value.moduleId;
    const action$ = id
      ? this.featureManagementService.update(id, form.value)
      : this.featureManagementService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.closeModal();
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
  }
}