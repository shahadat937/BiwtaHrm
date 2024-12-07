import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { FeatureManagementService } from '../service/feature-management.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-create-feature',
  templateUrl: './create-feature.component.html',
  styleUrl: './create-feature.component.scss'
})
export class CreateFeatureComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modules: SelectedModel[] = [];
  modalOpened: boolean = false;

  @ViewChild('FeatureForm', { static: true }) FeatureForm!: NgForm;

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
    this.getSelectedModule();
    this.getFeatureInfo();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }

  handleText(){
    this.heading = this.clickedButton == 'Edit' ? 'Edit Feature Information' : 'Create Feature Information';
    this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
    this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
  }

  getFeatureInfo() {
    // this.subscription =
    this.subscription.push(
    this.featureManagementService.findFeature(this.id).subscribe((res) => {
      if(res){
        this.FeatureForm?.form.patchValue(res);
      }
    })
    )
    
  }

  getSelectedModule(){
    this.subscription.push(
    this.featureManagementService.getSelectedModule().subscribe((res) => {
      if(res){
        this.modules = res;
      }
    })
    )
    
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  initaialModule(form?: NgForm) {
    if (form != null) form.resetForm();
    this.featureManagementService.features = {
      featureId : 0,
      moduleId : 0,
      featureTypeId : 0,
      featureName : '',
      path : '',
      iconName : '',
      icon : '',
      class : '',
      groupTitle : '',
      isActive : true,
      featureCode : 0,
      orderNo : 0,
      isReport : true,
      moduleName : '',
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
    const id = form.value.featureId;
    const action$ = id
      ? this.featureManagementService.updateFeature(id, form.value)
      : this.featureManagementService.submitFeature(form.value);

    // this.subscription =
    this.subscription.push(
    action$.subscribe((response: any) => {
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
    })
    )
    
  }
}
