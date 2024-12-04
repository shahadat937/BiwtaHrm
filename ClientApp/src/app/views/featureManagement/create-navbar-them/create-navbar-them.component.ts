import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { NavbarThemService } from '../service/navbar-them.service';

@Component({
  selector: 'app-create-navbar-them',
  templateUrl: './create-navbar-them.component.html',
  styleUrl: './create-navbar-them.component.scss'
})
export class CreateNavbarThemComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;

  @ViewChild('NavbarThemForm', { static: true }) NavbarThemForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public navbarThemService: NavbarThemService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    this.initaialNavbarThem();
    this.handleText();
    this.getNavbarThemInfo();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }

  handleText(){
    this.heading = this.clickedButton == 'Edit' ? 'Edit Them Information' : 'Create New Them';
    this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
    this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
  }

  getNavbarThemInfo() {
    this.subscription = this.navbarThemService.find(this.id).subscribe((res) => {
      if(res){
        this.NavbarThemForm?.form.patchValue(res);
      }
    });
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  initaialNavbarThem(form?: NgForm) {
    if (form != null) form.resetForm();
    this.navbarThemService.navbarThem = {
      id: 0,
      themName: "",
      bgColor: "",
      brandBg: "",
      togglerBg: "",
      togglerHoverBg: "",
      linkColor: "",
      linkActiveColor: "",
      linkActiveBg: "",
      linkHoverColor: "",
      linkHoverBg: "",
      linkIconColor: "",
      linkIconHoverColor: "",
      groupBg: "",
      groupToggleColor: "",
      width: 0,
      remark: "",
      isActive: true,
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
    this.navbarThemService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.navbarThemService.update(id, form.value)
      : this.navbarThemService.submit(form.value);

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