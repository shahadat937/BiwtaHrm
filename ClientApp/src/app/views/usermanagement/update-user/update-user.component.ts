import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpPromotionIncrement } from '../../promotion/model/emp-promotion-increment';
import { UserService } from '../service/user.service';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrl: './update-user.component.scss'
})
export class UpdateUserComponent  implements OnInit, OnDestroy {
  
  subscription: Subscription = new Subscription();
  empPromotionIncrement: EmpPromotionIncrement = new EmpPromotionIncrement();
  id: string = '';
  clickedButton: string = '';
  heading: string = '';
  modalOpened: boolean = false;
  loginEmpId : number = 0;
  
  @ViewChild('UserForm', { static: true }) UserForm!: NgForm;

  constructor(
    public userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) {
  }

  icons = { cilArrowLeft, cilPlus, cilBell };

  ngOnInit(): void {
    setTimeout(() => {
      this.modalOpened = true;
    }, 0);
    this.getById();
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId;
  }

  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getById(){
    if(this.clickedButton == 'UpdateUser'){
      this.heading = "Update User";
    }
    else {
      this.heading = "Change Password";
    }
    this.userService.find(this.id).subscribe((res) => {
      this.UserForm?.form.patchValue(res);
    });
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

  onSubmit(form: NgForm): void{
    this.userService.cachedData = [];
    let action$;
    const id = form.value.id;

    action$ = this.clickedButton == 'UpdateUser' ? this.userService.update(id, form.value) :  this.userService.updatePassword(id, form.value);

    this.subscription = action$.subscribe((response: any)  => {
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
    });
  }
}
