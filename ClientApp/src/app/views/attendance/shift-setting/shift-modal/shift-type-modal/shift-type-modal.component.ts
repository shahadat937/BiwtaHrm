import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ShiftSettingService } from '../../../services/shift-setting.service';

@Component({
  selector: 'app-shift-type-modal',
  templateUrl: './shift-type-modal.component.html',
  styleUrl: './shift-type-modal.component.scss'
})
export class ShiftTypeModalComponent implements OnInit, OnDestroy {

  subscription: Subscription[]=[]
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;

  @ViewChild('ShiftTypeForm', { static: true }) ShiftTypeForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public shiftSettingService: ShiftSettingService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {

  }

  
    ngOnInit(): void {
      this.initaialModule();
      this.handleText();
      this.getModuleInfo();
      setTimeout(() => {
        this.modalOpened = true;
      }, 0);
    }
  
    handleText(){
      this.heading = this.clickedButton == 'Edit' ? 'Edit Shift Type' : 'Create Shift Type';
      this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
      this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
    }
  
    getModuleInfo() {
      this.subscription.push(
      this.shiftSettingService.findShiftType(this.id).subscribe((res) => {
        if(res){
          this.ShiftTypeForm?.form.patchValue(res);
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
      this.shiftSettingService.shiftType = {
        id :  0,
        shiftName : '',
        remark : '',
        isDefault : false,
        isActive : true,
        menuPosition : null,
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
      const id = form.value.id;
      const action$ = id
        ? this.shiftSettingService.updateShiftType(id, form.value)
        : this.shiftSettingService.submitShiftType(form.value);
  
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