import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { OfficeOrderService } from '../service/office-order.service';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { OfficeOrder } from '../models/office-order';
import { OrderTypeService } from '../../basic-setup/service/order-type.service';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { SectionService } from '../../basic-setup/service/section.service';
import { DesignationService } from '../../basic-setup/service/designation.service';
import { AuthService } from 'src/app/core/service/auth.service';

@Component({
  selector: 'app-office-order-modal',
  templateUrl: './office-order-modal.component.html',
  styleUrl: './office-order-modal.component.scss'
})
export class OfficeOrderModalComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  id: number = 0;
  clickedButton: string = '';
  heading: string = '';
  btnText: string = '';
  btnIcon: string = '';
  orderTypes: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  modalOpened: boolean = false;
  officeOrderForm: OfficeOrder = new OfficeOrder();
  
  associatefile: File = null!;

  constructor(
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2,
    private authService: AuthService,
    public orderTypeService: OrderTypeService,
    public officeOrderService: OfficeOrderService,
    public departmentService: DepartmentService,
    public sectionService: SectionService,
    public designationService: DesignationService,
  ) {
    
  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    this.initaialForm();
    this.handleText();
    this.getSelectedOrderType();
    this.getSelectedDepartment();
    this.getSelectedDesignations();
    this.getOfficeOrderInfo();
    this.officeOrderForm.empId = this.authService.userInformation.empId;
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
  }

  handleText(){
    this.heading = this.clickedButton == 'Edit' ? 'Edit Office Order Information' : 'Create New Office Order';
    this.btnText = this.clickedButton == 'Edit' ? 'Update' : 'Submit';
    this.btnIcon = this.clickedButton == 'Edit' ? 'update' : 'save';
  }

  getOfficeOrderInfo() {
    this.subscription.push(
    this.officeOrderService.find(this.id).subscribe((res) => {
      if(res){
        this.getSelectedSection(res.departmentId || 0);
        this.officeOrderForm = res;
      }
    })
    )
    
  }

  getSelectedOrderType(){
    this.subscription.push(
    this.orderTypeService.getSelectedOrderType().subscribe((res) => {
      if(res){
        this.orderTypes = res;
      }
    })
    )
  }
  
  getSelectedDepartment(){
    this.subscription.push(
    this.departmentService.getSelectedAllDepartment().subscribe((res) => {
      if(res){
        this.departments = res;
      }
    })
    )
  }

  getSelectedSection(id: number){
    this.subscription.push(
      this.sectionService.getSectionByOfficeDepartment(id).subscribe((res) => {
        if(res){
          this.sections = res;
        }
      })
    )
  }

  getSelectedDesignations(){
    this.subscription.push(
    this.designationService.getSelectDesignationSetupName().subscribe((res) => {
      if(res){
        this.designations = res;
      }
    })
    )
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  onPhotoSelected(event: any) {
    this.associatefile = event.target.files[0];
    const reader = new FileReader();
    const img = new Image();

    reader.onload = (e: any) => {
      img.src = e.target.result;
    };
    reader.readAsDataURL(this.associatefile);
  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.officeOrderForm = {
      id: 0,
      empId: null,
      orderTypeId: null,
      officeId: null,
      departmentId: null,
      sectionId: null,
      designationId: null,
      orderDate: new Date,
      orderNo: "",
      orderFile: null,
      fileUrl: "",
      remark: "",
      isActive: true,
      menuPosition: null,

      orderTypeName: "",
      officeName: "",
      departmentName: "",
      sectionName: "",
      designationName: "",
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
    form.value.orderFile = this.associatefile;
    const action$ = id
      ? this.officeOrderService.update(id, form.value)
      : this.officeOrderService.save(form.value);
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
