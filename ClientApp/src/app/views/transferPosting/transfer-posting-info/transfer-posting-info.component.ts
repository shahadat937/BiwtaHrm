import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { EmpTransferPosting } from '../model/emp-transfer-posting';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpTransferPostingService } from '../service/emp-transfer-posting.service';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-transfer-posting-info',
  templateUrl: './transfer-posting-info.component.html',
  styleUrl: './transfer-posting-info.component.scss'
})
export class TransferPostingInfoComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  empTransferPosting: EmpTransferPosting = new EmpTransferPosting();
  id: number = 0;
  modalOpened: boolean = false;

  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {

  }


  ngOnInit(): void {
    this.getAllTransferPostingInfo();
    setTimeout(() => {
      this.modalOpened = true; // Delay setting the modal as initialized to avoid shaking on open
    }, 0);
  }

  getAllTransferPostingInfo() {
    this.subscription = this.empTransferPostingService.findById(this.id).subscribe((item) => {
      this.empTransferPosting = item;
      console.log(this.empTransferPosting)
    });
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
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
      }, 500); // duration of the shake animation
    }
  }

}
