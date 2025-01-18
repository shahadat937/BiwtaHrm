import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NotificationService } from '../service/notification.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell, cilSearch } from '@coreui/icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmployeeListModalComponent } from '../../employee/employee-list-modal/employee-list-modal.component';
import { EmpJobDetailsService } from '../../employee/service/emp-job-details.service';
import { EmpTransferPostingService } from '../../transferPosting/service/emp-transfer-posting.service';
import { SelectedModel } from '../../../core/models/selectedModel';

@Component({
  selector: 'app-add-notice',
  templateUrl: './add-notice.component.html',
  styleUrl: './add-notice.component.scss'
})
export class AddNoticeComponent  implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  modalOpened: boolean = false;
  
  isValidEmp: boolean = false;
  loginEmpId: number = 0;

  departments: SelectedModel[] = [];
  toEmployee: boolean = false;
  toDepartment: boolean = false;
  

  @ViewChild('UserNotificationForm', { static: true }) UserNotificationForm!: NgForm;

  constructor (
    private toastr: ToastrService,
    public notificationService: NotificationService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    public empTransferPostingService: EmpTransferPostingService,
    public empJobDetailsService: EmpJobDetailsService,
    private modalService: BsModalService,
  ) {

  }
  
    icons = { cilArrowLeft, cilPlus, cilBell, cilSearch };
  
    ngOnInit(): void {
      this.getAllDepartment();
      this.initaialModule();
      this.handleText();
      this.getModuleInfo();
      setTimeout(() => {
        this.modalOpened = true;
      }, 0);
    }
  
    handleText(){
      this.heading = this.clickedButton == 'Edit' ? 'Edit Notice Information' : 'Create New Notice Information';
      this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
      this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
    }
  
    getModuleInfo() {
      this.subscription.push(
      // this.featureManagementService.find(this.id).subscribe((res) => {
      //   if(res){
      //     this.ModuleForm?.form.patchValue(res);
      //   }
      // })
      )
      
    }
    
    ngOnDestroy(): void {
      if (this.subscription) {
        this.subscription.forEach(subs=>subs.unsubscribe());
      }
    }
  
    initaialModule(form?: NgForm) {
      if (form != null) form.resetForm();
      this.notificationService.userNotification = {
        id : 0,
        fromEmpId : null,
        toEmpId : null,
        toDeptId : null,
        featureId : null,
        featurePath : '',
        UnreadCount : 0,
        isNotice : false,
        forAllUsers : false,
        title : '',
        message : '',
        nevigateLink : '',
        forEntryId : null,
        readStatus : false,
        fromEmplName : '',
        dateCreated : '',
        isActive : true,
      };
    }
    

    EmployeeListModal() {
        const modalRef: BsModalRef = this.modalService.show(EmployeeListModalComponent, { backdrop: 'static', class: 'modal-xl'  });
    
        modalRef.content.employeeSelected.subscribe((idCardNo: string) => {
          if(idCardNo){
            this.getEmpInfoByIdCardNo(idCardNo);
          }
        });
    }
    
  getEmpInfoByIdCardNo(idCardNo: string) {
    this.subscription.push(
      this.empTransferPostingService.getEmpBasicInfoByIdCardNo(idCardNo).subscribe((res) => {
      if (res) {
        this.getEmpJobDetailsNewByEmpId(res.id);  
      }
      else {
        this.isValidEmp = false;
        this.toastr.warning('', 'Invalid Employee PMS No', {
                positionClass: 'toast-top-right',
        });
      }
    })    
    )
  }

  getEmpJobDetailsNewByEmpId(id: number){
    // this.subscription = 
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(id).subscribe((res) => {
      if(res){
      }
    }))
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

    
  getAllDepartment() {
    this.subscription.push(
    this.empJobDetailsService.getAllDepartment().subscribe((res) => {
      this.departments = res;
    })
    )
  }

    selectToNotice(value: string){

    }
  
    onSubmit(form: NgForm): void {
      // this.notificationService.cachedData = [];
      // const id = form.value.moduleId;
      // const action$ = id
      //   ? this.notificationService.update(id, form.value)
      //   : this.notificationService.submit(form.value);
  
      // // this.subscription = 
      // this.subscription.push(
      //   action$.subscribe((response: any) => {
      //     if (response.success) {
      //       this.toastr.success('', `${response.message}`, {
      //         positionClass: 'toast-top-right',
      //       });
      //       this.closeModal();
      //     } else {
      //       this.toastr.warning('', `${response.message}`, {
      //         positionClass: 'toast-top-right',
      //       });
      //     }
      //   })
      // )
      
    }
  }
