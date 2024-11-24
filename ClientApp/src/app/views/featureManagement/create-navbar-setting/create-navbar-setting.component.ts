import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { NavbarSettingService } from '../service/navbar-setting.service';
import { NavbarThemService } from '../service/navbar-them.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-create-navbar-setting',
  templateUrl: './create-navbar-setting.component.html',
  styleUrl: './create-navbar-setting.component.scss'
})
export class CreateNavbarSettingComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;
  navbarLogo: File = null!;
  brandLogo: File = null!;
  logoPreviewUrl: string | ArrayBuffer | null = null;
  brandPreviewUrl: string | ArrayBuffer | null = null;
  thems: SelectedModel[] = [];

  @ViewChild('NavbarSettingForm', { static: true }) NavbarSettingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public navbarSettingService: NavbarSettingService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    public navbarThemService: NavbarThemService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    this.initaialModule();
    this.handleText();
    this.getModuleInfo();
    this.getSelectedThem();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }
  
  getSelectedThem(){
    this.subscription = this.navbarThemService.getSelectedNavbarThem().subscribe((res) => {
      if(res){
        this.thems = res;
      }
    });
  }

  handleText(){
    this.heading = this.clickedButton == 'Edit' ? 'Edit Navbar Setting Information' : 'Create Navbar Setting Information';
    this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
    this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
  }

  getModuleInfo() {
    this.subscription = this.navbarSettingService.find(this.id).subscribe((res) => {
      if(res){
        this.NavbarSettingForm?.form.patchValue(res);
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
    this.navbarSettingService.navbarSetting = {
      id : 0,
      navbarLogo : "",
      navbarLogoFile : null,
      brandLogo : "",
      brandLogoFile : null,
      brandName : "",
      showLogo : false,
      themId : null,
      themName : "",
      remark : "",
      isActive : true,
    };
  }

  onPhotoSelected(event: any) {
    this.navbarLogo = event.target.files[0];
    const reader = new FileReader();
    const img = new Image();

    reader.onload = (e: any) => {
      img.src = e.target.result;
      img.onload = () => {
          this.logoPreviewUrl = e.target.result;
      };
    };
    reader.readAsDataURL(this.navbarLogo);
  }
  
  onBrandPhotoSelected(event: any) {
    this.brandLogo = event.target.files[0];
    const reader = new FileReader();
    const img = new Image();

    reader.onload = (e: any) => {
      img.src = e.target.result;
      img.onload = () => {
          this.brandPreviewUrl = e.target.result;
      };
    };
    reader.readAsDataURL(this.brandLogo);
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
    console.log(form.value)
    this.navbarSettingService.cachedData = [];
    form.value.navbarLogoFile = this.navbarLogo;
    form.value.brandLogoFile = this.brandLogo;
    const id = form.value.id;
    const action$ = id
      ? this.navbarSettingService.update(id, form.value)
      : this.navbarSettingService.submit(form.value);

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