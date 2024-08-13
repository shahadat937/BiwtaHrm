import { Component, ElementRef, HostListener, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { UserRoles } from '../model/user-roles';
import { UserService } from '../service/user.service';
import { SelectedStringModel } from 'src/app/core/models/selectedStringModel';

@Component({
  selector: 'app-update-role',
  templateUrl: './update-role.component.html',
  styleUrl: './update-role.component.scss'
})
export class UpdateRoleComponent  implements OnInit, OnDestroy {
  
  subscription: Subscription = new Subscription();
  userRoles: UserRoles = new UserRoles();
  id: string = '';
  oldRole: string = '';
  selectedRoles: SelectedStringModel[] = [];
  modalOpened: boolean = false;
  loginEmpId : number = 0;
  
  @ViewChild('UserRoleForm', { static: true }) UserRoleForm!: NgForm;

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
    this.initaialUser();
    this.getSelectedRoles()
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
    this.userService.getUserRoleDetails(this.id).subscribe((res) => {
      this.UserRoleForm?.form.patchValue(res);
      this.oldRole = res.roleId;
    });
  }

  getSelectedRoles(){
    this.userService.getSelectedUserRoles().subscribe((res) => {
      this.selectedRoles = res;
    });
  }
  
  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.userService.userRole = {
      userId: '',
      roleId: '',
      oldRoleId: '',
      newRoleId: '',
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

  onSubmit(form: NgForm): void{
    this.userService.cachedData = [];
    let action$;
    const id = form.value.id;

    form.value.oldRoleId = this.oldRole;
    form.value.newRoleId = form.value.roleId;

    action$ = this.userService.updateUserRoles(id, form.value);

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