import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { EmpPromotionIncrementService } from '../service/emp-promotion-increment.service';
import { EmpPromotionIncrement } from '../model/emp-promotion-increment';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';

@Component({
  selector: 'app-promotion-increment-info',
  templateUrl: './promotion-increment-info.component.html',
  styleUrl: './promotion-increment-info.component.scss'
})
export class PromotionIncrementInfoComponent  implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  empPromotionIncrement: EmpPromotionIncrement = new EmpPromotionIncrement();
  id: number = 0;
  modalOpened: boolean = false;

  constructor(
    private toastr: ToastrService,
    public empPromotionIncrementService: EmpPromotionIncrementService,
    public empJobDetailsService: EmpJobDetailsService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    this.getAllPromotionIncrementInfo();
    setTimeout(() => {
      this.modalOpened = true; // Delay setting the modal as initialized to avoid shaking on open
    }, 0);
  }

  
  getAllPromotionIncrementInfo() {
    this.subscription = this.empPromotionIncrementService.findById(this.id).subscribe((item) => {
      this.empPromotionIncrement = item;
      this.getEmpJobDetailsByEmpIdOfApproveBy(item.approveById || 0);
    });
  }

  getEmpJobDetailsByEmpIdOfApproveBy(id: number){
    this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empPromotionIncrement.approveByDepartmentName = res.departmentName;
        this.empPromotionIncrement.approveByDesignationName = res.designationName;
      }
    })
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
