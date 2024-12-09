import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpJobDetailsService } from 'src/app/views/employee/service/emp-job-details.service';
import { EmpTransferPosting } from '../../model/emp-transfer-posting';
import { EmpTransferPostingService } from '../../service/emp-transfer-posting.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-transfer-posting-approval',
  templateUrl: './transfer-posting-approval.component.html',
  styleUrl: './transfer-posting-approval.component.scss'
})
export class TransferPostingApprovalComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  empTransferPosting: EmpTransferPosting = new EmpTransferPosting();
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  modalOpened: boolean = false;
  loginEmpId : number = 0;

  @ViewChild('EmpTransferPostingForm', { static: true }) EmpTransferPostingForm!: NgForm;

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

    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId;
  }

  handleText(){
    this.heading = this.clickedButton == 'Approve' ? 'Approve Transfer Posting Application' :
                 this.clickedButton == 'Reject' ? 'Reject Transfer Posting Application' :
                 this.clickedButton == 'Edit' ? 'Edit Transfer Posting Application' : '';
  }

  getTransferPostingInfo() {
    this.subscription.push(
    this.empTransferPostingService.findById(this.id).subscribe((res) => {
      if(res){
        this.empTransferPosting = res;
        // this.getEmpJobDetailsByEmpIdOfOrderOfficeBy(res.orderOfficeById || 0);
        this.EmpTransferPostingForm?.form.patchValue(res);
      }
    })
    )
   
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  // getEmpJobDetailsByEmpIdOfOrderOfficeBy(id: number){
  //   this.subscription = this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
  //     if(res){
  //       this.empTransferPosting.orderByDepartmentName = res.departmentName;
  //       this.empTransferPosting.orderByDesignationName = res.designationName;
  //       this.empTransferPosting.orderBySectionName = res.sectionName;
  //     }
  //   })
  // }


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

  onSubmit(transferApproveStatus?: boolean){
    if(this.empTransferPosting.deptApproveStatus!=null){
      this.toastr.warning('', `Already Department Status Updated`, {
        positionClass: 'toast-top-right',
      });
    }
    else{
      if(transferApproveStatus == true || transferApproveStatus == false){
        this.empTransferPosting.transferApproveStatus = transferApproveStatus;
      }
      else{
        this.empTransferPosting.transferApproveStatus = this.empTransferPostingService.empTransferPosting.transferApproveStatus;
      }
      this.empTransferPosting.transferApproveById = this.loginEmpId;
      this.empTransferPosting.transferApproveDate = new Date().toISOString().split('T')[0] as any as Date;
      this.empTransferPosting.approveRemark = this.empTransferPostingService.empTransferPosting.approveRemark;
      console.log("Full Response : ", this.empTransferPosting);

      this.subscription.push(
      this.empTransferPostingService.updateEmpTransferPostingStatus(this.empTransferPosting.id, this.empTransferPosting).subscribe((response: any) => {
        if (response.success) {
          if(transferApproveStatus == true){
            this.toastr.success('', `Application Approved Successfull`, {
              positionClass: 'toast-top-right',
            });
          }
          else if(transferApproveStatus == false){
            this.toastr.error('', `Application Rejected Successfull`, {
              positionClass: 'toast-top-right',
            });
          }
          else{
            this.toastr.success('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
        }
        this.closeModal();
      })
      )
      
    }
  }
}