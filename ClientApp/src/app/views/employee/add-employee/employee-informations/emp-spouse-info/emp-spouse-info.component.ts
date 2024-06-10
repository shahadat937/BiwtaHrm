import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpSpouseInfoService } from '../../../service/emp-spouse-info.service';
import { cilPlus, cilShieldAlt } from '@coreui/icons';

@Component({
  selector: 'app-emp-spouse-info',
  templateUrl: './emp-spouse-info.component.html',
  styleUrl: './emp-spouse-info.component.scss'
})
export class EmpSpouseInfoComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  occupations: SelectedModel[] = [];
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  @ViewChild('EmpSpouseInfoForm', { static: true }) EmpSpouseInfoForm!: NgForm;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empSpouseInfoService: EmpSpouseInfoService,){}

  icons = { cilPlus, cilShieldAlt };
    
  ngOnInit(): void {
    this.getEmployeeSpouseInfoByEmpId();
    this.getSelectedOccupation();
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  
  getEmployeeSpouseInfoByEmpId() {
    this.empSpouseInfoService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Update Spouse Information';
        this.btnText = 'Update';
      }
      else {
        this.headerText = 'Add Spouse Information';
        this.btnText = 'Submit';
        this.initaialForm();
      }
    })
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }
  
  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empSpouseInfoService.empSpouseInfo = {
      id: 0,
      empId: this.empId,
      spouseName: '',
      spouseNameBangla: '',
      dateOfBirth : null,
      birthRegNo : null,
      nid : null,
      occupationId : null,
      remark: '',
      menuPosition: 0,
      isActive: true,
    };
  }

  resetForm() {
    this.EmpSpouseInfoForm.form.reset();
    this.EmpSpouseInfoForm.form.patchValue({
      empId: this.empId,
      spouseName: '',
      spouseNameBangla: '',
      dateOfBirth : null,
      birthRegNo : null,
      nid : null,
      occupationId : null,
      remark: '',
      menuPosition: 0,
      isActive: true,
    });
  }

  getSelectedOccupation(){
    this.empSpouseInfoService.getSelectedOccupation().subscribe((res) => {
      this.occupations = res;
    })
  }

  cancel() {
    this.close.emit();
  }

  onSubmit(form: NgForm): void {

  }

}
