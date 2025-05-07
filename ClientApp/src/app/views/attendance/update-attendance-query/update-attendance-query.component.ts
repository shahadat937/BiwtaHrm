import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { AttendanceRecordService } from '../services/attendance-record-service.service';

@Component({
  selector: 'app-update-attendance-query',
  templateUrl: './update-attendance-query.component.html',
  styleUrl: './update-attendance-query.component.scss'
})
export class UpdateAttendanceQueryComponent implements OnInit, OnDestroy {

  subscription: Subscription[] = []
  modalOpened: boolean = false;

  fromDate: Date | null = null;
  toDate: Date | null = null;
  loading: boolean = false;

  constructor(
    private toastr: ToastrService,
    private bsModalRef: BsModalRef,
    private el: ElementRef,
    private renderer: Renderer2,
    public AtdRecordService: AttendanceRecordService,
  ) {

  }

  ngOnInit(): void {
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);

  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs => subs.unsubscribe());
    }
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

  onSubmit(){
    this.loading = true;
    this.subscription.push(
      this.AtdRecordService.updateAttendanceQuery(this.fromDate, this.toDate).subscribe((res: any) => {
        if(res.success){
          this.toastr.success('',`${res.message}`, {
            messageClass:"toast-top-right"
          });
          this.closeModal();
        }
        else {
          this.toastr.warning('',`${res.message}`, {
            messageClass:"toast-top-right"
          });
        }
      })
    )
    this.loading = false;
  }
}

