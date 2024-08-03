import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpJobDetailsService } from 'src/app/views/employee/service/emp-job-details.service';
import { EmpTransferPosting } from '../../model/emp-transfer-posting';
import { EmpTransferPostingService } from '../../service/emp-transfer-posting.service';

@Component({
  selector: 'app-transfer-posting-approval',
  templateUrl: './transfer-posting-approval.component.html',
  styleUrl: './transfer-posting-approval.component.scss'
})
export class TransferPostingApprovalComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  empTransferPosting: EmpTransferPosting = new EmpTransferPosting();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  modalOpened: boolean = false;

  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    public empJobDetailsService: EmpJobDetailsService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    this.handleText();
    this.getTransferPostingInfo();
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);

    console.log("Id : ", this.id);
    console.log("clickedButton : ", this.clickedButton);
  }

  handleText(){
    this.heading = this.clickedButton == 'Approve' ? 'Approve Transfer Posting Application' :
                 this.clickedButton == 'Reject' ? 'Reject Transfer Posting Application' :
                 this.clickedButton == 'Edit' ? 'Edit Transfer Posting Application' : '';
  }

  getTransferPostingInfo() {
    this.subscription = this.empTransferPostingService.findById(this.id).subscribe((res) => {
      if(res){
        this.empTransferPosting = res;
        this.getEmpJobDetailsByEmpIdOfOrderOfficeBy(res.orderOfficeById || 0);
      }
    });
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getEmpJobDetailsByEmpIdOfOrderOfficeBy(id: number){
    this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
        this.empTransferPosting.orderByDepartmentName = res.departmentName;
        this.empTransferPosting.orderByDesignationName = res.designationName;
        this.empTransferPosting.orderBySectionName = res.sectionName;
      }
    })
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