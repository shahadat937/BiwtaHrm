import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ShiftService } from 'src/app/views/attendance/services/shift.service';
import { EmpShiftAssignService } from '../../service/emp-shift-assign.service';

@Component({
  selector: 'app-update-emp-shift',
  templateUrl: './update-emp-shift.component.html',
  styleUrl: './update-emp-shift.component.scss'
})
export class UpdateEmpShiftComponent implements OnInit, OnDestroy {

  id: number = 0;
  modalOpened: boolean = false;
  
  shifts: SelectedModel[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  
  @ViewChild('EmpShiftAssignForm', { static: true }) EmpShiftAssignForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    public empShiftAssignService: EmpShiftAssignService,
    private ShiftService: ShiftService,
  ) { }


  ngOnInit(): void {
    this.initaialUser();
    this.getById();
    this.getSelectedShift();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  
  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empShiftAssignService.empShiftAssign = {
      id : 0,
      empId : null,
      shiftId : null,
      isActive : true,
      pmisNo :  '',
      empName :  '',
      departmentName :  '',
      designationName :  '',
      shiftName :  '',
    };
  }
  
  getById(){
    this.subscription.push(
      this.empShiftAssignService.findById(this.id).subscribe((res) => {
      this.EmpShiftAssignForm?.form.patchValue(res);
    })
    )
    
  }

  getSelectedShift(){
    this.subscription.push(
      this.ShiftService.getSelectedShift().subscribe((res) => {
      this.shifts = res;
    })
    )
    
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
      }, 500); // duration of the shake animation
    }
  }

  onSubmit(form: NgForm): void{
    this.empShiftAssignService.cachedData = [];
    let action$;
    const id = form.value.id;

    action$ = this.empShiftAssignService.updateEmpShiftAssign(id, form.value);

    // this.subscription = 
    this.subscription.push(
      action$.subscribe((response: any)  => {
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
