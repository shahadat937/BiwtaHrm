import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { from, Subscription } from 'rxjs';
import { SiteSettingService } from '../service/site-setting.service';

@Component({
  selector: 'app-create-site-setting',
  templateUrl: './create-site-setting.component.html',
  styleUrl: './create-site-setting.component.scss'
})
export class CreateSiteSettingComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;
  siteLogo: File = null!;
  logoPreviewUrl: string | ArrayBuffer | null = null;

  @ViewChild('SiteSettingForm', { static: true }) SiteSettingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public siteSettingService: SiteSettingService,
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
    this.heading = this.clickedButton == 'Edit' ? 'Edit Site Setting Information' : 'Create Site Setting Information';
    this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
    this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
  }

  getModuleInfo() {
    // this.subscription =
    this.subscription.push(
    this.siteSettingService.find(this.id).subscribe((res) => {
      if(res){
        this.SiteSettingForm?.form.patchValue(res);
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
    this.siteSettingService.siteSetting = {
      id :  0,
      siteName :  '',
      siteTitle: '',
      siteLogo :  '',
      footerTitle :  '',
      defaultPassword : '',
      remark :  '',
      siteLogoFile: null,
      isActive :  true,
    };
  }

  onPhotoSelected(event: any) {
    this.siteLogo = event.target.files[0];
    const reader = new FileReader();
    const img = new Image();

    reader.onload = (e: any) => {
      img.src = e.target.result;
      img.onload = () => {
          this.logoPreviewUrl = e.target.result;
      };
    };
    reader.readAsDataURL(this.siteLogo);
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
    this.siteSettingService.cachedData = [];
    form.value.siteLogoFile = this.siteLogo;
    const id = form.value.id;
    const action$ = id
      ? this.siteSettingService.update(id, form.value)
      : this.siteSettingService.submit(form.value);

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