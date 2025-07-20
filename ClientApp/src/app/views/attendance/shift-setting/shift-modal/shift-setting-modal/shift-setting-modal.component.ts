import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ShiftSettingService } from '../../../services/shift-setting.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { SharedService } from '../../../../../shared/shared.service';

@Component({
  selector: 'app-shift-setting-modal',
  templateUrl: './shift-setting-modal.component.html',
  styleUrl: './shift-setting-modal.component.scss'
})
export class ShiftSettingModalComponent implements OnInit, OnDestroy {

  subscription: Subscription[]=[]
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;
  selectedShiftType: number = 0;

  shiftTypes : SelectedModel[] = [];

  @ViewChild('ShiftSettingForm', { static: true }) ShiftSettingForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public shiftSettingService: ShiftSettingService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    private sharedService: SharedService
  ) {

  }

  
    ngOnInit(): void {
      this.handleText();
      this.getShiftSettingInfo();
      this.getSelectedShiftType();
      setTimeout(() => {
        this.modalOpened = true;
      }, 0);
      this.initaialModule();
    }
  
    handleText(){
      this.heading = this.clickedButton == 'Edit' ? 'Edit Shift Setting' : 'Create Shift Setting';
      this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
      this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
    }
  
    getShiftSettingInfo() {
      this.subscription.push(
      this.shiftSettingService.findShiftSetting(this.id).subscribe((res) => {
        if(res){
          this.ShiftSettingForm?.form.patchValue(res);
          this.shiftSettingService.shiftSetting.startDate = this.sharedService.parseDate(res.startDate);
          this.shiftSettingService.shiftSetting.endDate = this.sharedService.parseDate(res.endDate);
        }
      })
      )
      
    }

    getSelectedShiftType(){
      this.subscription.push(
        this.shiftSettingService.getSelectedShiftType().subscribe((res) => {
          if(res){
            this.shiftTypes = res;
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
      this.shiftSettingService.shiftSetting = {
        id :  0,
        settingName : '',
        shiftTypeId : this.selectedShiftType,
        startTime: null,
        endTime: null,
        bufferTime: null,
        absentTime: null,
        startDate: null,
        endDate: null,
        isActive: true,
        remark: null,
        shiftTypeName: null,
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
      form.value.startDate = this.sharedService.formatDateOnly(form.value.startDate);
      form.value.endDate = this.sharedService.formatDateOnly(form.value.endDate);

      const id = form.value.id;
      const action$ = id
        ? this.shiftSettingService.updateShiftSetting(id, form.value)
        : this.shiftSettingService.submitShiftSetting(form.value);
  
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