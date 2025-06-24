import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EmpJobDetailsService } from '../../../service/emp-job-details.service';
import { OfficeService } from 'src/app/views/basic-setup/service/office.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { GradeService } from 'src/app/views/basic-setup/service/Grade.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { EmpBasicInfoService } from '../../../service/emp-basic-info.service';
import { JobDetailsSetupService } from '../../../../basic-setup/service/job-details-setup.service';
import { RetiredReasonService } from 'src/app/views/basic-setup/service/retired-reason.service';

@Component({
  selector: 'app-emp-job-details',
  templateUrl: './emp-job-details.component.html',
  styleUrl: './emp-job-details.component.scss'
})
export class EmpJobDetailsComponent implements OnInit, OnDestroy {

  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  offices: SelectedModel[] = [];
  departments: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  firstDepartments: SelectedModel[] = [];
  firstSections: SelectedModel[] = [];
  firstDesignations: SelectedModel[] = [];
  firstScale: SelectedModel[] = [];
  countris: SelectedModel[] = [];
  grades: SelectedModel[] = [];
  scales: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  retiredReasons: SelectedModel[] = [];
  isRetiredReasonNeeded: boolean = true;
  isRetirementIdNeeded: boolean = true;
  empJobDetailsId: number = 0;
  sectionView: boolean = false;
  firstSectionView: boolean = false;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[] = []
  loading: boolean = false;
  prlDate: string = '';
  retirementDate: string = '';
  @ViewChild('EmpJobDetailsForm', { static: true }) EmpJobDetailsForm!: NgForm;

  constructor(
    private toastr: ToastrService,
    public empJobDetailsService: EmpJobDetailsService,
    public empBasicInfoService: EmpBasicInfoService,
    public jobDetailsSetupService: JobDetailsSetupService,
    public officeService: OfficeService,
    public departmentService: DepartmentService,
    private gradeService: GradeService,
    public sectionService: SectionService,
    public retiredReasonService: RetiredReasonService,
  ) {

  }

  ngOnInit(): void {
    this.loadOffice();
    this.SelectModelGrade();
    this.getAllDepartment();
    this.getAllSelectedDepartments();
    this.getSelectedRetirmentReason();
    this.getPrlAndRetirmentDate();
    this.getEmployeeByEmpId();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs => subs.unsubscribe());
    }
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }

  getEmployeeByEmpId() {
    this.empJobDetailsService.findByEmpId(this.empId).subscribe((res) => {
      if (res) {
        this.empJobDetailsId = res.id;
        if (res.sectionId) {
          this.onSectionSelectGetDesignation(res.sectionId, res.id);
        }
        else if (res.departmentId) {
          this.onDepartmentSelectGetDesignation(res.departmentId, res.id);
        }
        else {
          this.onOfficeSelectGetDesignation(res.officeId, res.id);
        }
        this.onChangeGradeGetScale(res.presentGradeId);
        if (res.firstGradeId) {
          this.onChangeFirstGradeGetFirstScale(res.firstGradeId);
        }
        if (res.firstSectionId) {
          this.getOldDesignationBySection(res.firstSectionId);
        }
        else if (res.firstDepartmentId) {
          this.getOldDesignationByDepartment(res.firstDepartmentId);
        }
        this.getRetirmentReasonDetails(res.retiredReasonId ?? 0);
        this.onOfficeAndDepartmentSelect(res.departmentId);
        this.onOfficeAndDepartmentSelectFirstSection(res.firstDepartmentId);
        this.EmpJobDetailsForm?.form.patchValue(res);
        this.onServiceStatusSelect();
        this.headerText = 'Update Job Details';
        this.btnText = 'Update';
        console.log(res)
        this.getAllSelectedDepartments();
      }
      else {
        this.headerText = 'Add Job Details';
        this.btnText = 'Submit';
        this.initaialForm();
      }
    })
  }

  getSelectedRetirmentReason(){
    this.subscription.push(
      this.retiredReasonService.getSelectedRetiredReason().subscribe((res) => {
        this.retiredReasons = res;
      })
    )
  }

  getRetirmentReasonDetails(id: number){
    this.subscription.push(
      this.retiredReasonService.find(id).subscribe((res) => {
        if(res){
          this.isRetirementIdNeeded = res.idNeeded;
        }
        else {
          this.isRetirementIdNeeded = false;
        }
      })
    )
  }

  onServiceStatusSelect(){
    if(this.empJobDetailsService.empJobDetails.serviceStatus){
      this.isRetiredReasonNeeded = false;
      this.isRetirementIdNeeded = false;
    }
    else {
      this.isRetiredReasonNeeded = true;
    }
  }

  getPrlAndRetirmentDate() {
    // this.subscription = 
    this.subscription.push(
      this.empBasicInfoService.findByEmpId(this.empId).subscribe((res) => {

        this.jobDetailsSetupService.getActive().subscribe((response) => {
          if (res.dateOfBirth && response) {
            let prlDate = new Date(res.dateOfBirth);
            prlDate.setFullYear(prlDate.getFullYear() + (response.prlAge || 0));
            this.prlDate = prlDate.toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' });

            let retirementDate = new Date(res.dateOfBirth);
            retirementDate.setFullYear(retirementDate.getFullYear() + (response.retirmentAge || 0));
            this.retirementDate = retirementDate.toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' });
          }
        })
      })
    )

  }

  initaialForm(form?: NgForm) {
    if (form != null) form.resetForm();
    this.empJobDetailsService.empJobDetails = {
      id: 0,
      empId: this.empId,
      officeId: null,
      departmentId: null,
      designationId: null,
      sectionId: null,
      presentGradeId: null,
      presentScaleId: null,
      basicPay: 0,
      joiningDate: null,
      currentPositionJoinDate: null,
      codeNo: '',
      confirmationDate: null,
      firstGradeId: null,
      firstScaleId: null,
      firstDepartmentId: null,
      firstSectionId: null,
      firstDesignationId: null,
      firstGradeInput: null,
      firstScaleInput: null,
      firstDepartmentInput: null,
      firstSectionInput: null,
      firstDesignationInput: null,
      prlDate: null,
      retirementDate: null,
      serviceStatus: true,
      remark: '',
      menuPosition: 0,
      isActive: true,
      retiredReasonId: null,
      retirmentId: '',

      officeName: '',
      departmentName: '',
      designationName: '',
      designationNameBangla: '',
      sectionName: '',
      presentGradeName: '',
      presentScaleName: '',
      firstDepartmentName: '',
      firstSectionName: '',
      firstDesignationName: '',
      firstGradeName: '',
      firstScaleName: '',
      retiredReasonName: '',
    };
  }

  resetForm() {
    this.EmpJobDetailsForm.form.reset();
    this.EmpJobDetailsForm.form.patchValue({
      empId: this.empId,
      officeId: null,
      departmentId: null,
      designationId: null,
      sectionId: null,
      presentGradeId: null,
      presentScaleId: null,
      basicPay: 0,
      joiningDate: null,
      codeNo: '',
      confirmationDate: null,
      firstGradeId: null,
      firstScaleId: null,
      firstDepartmentId: null,
      firstSectionId: null,
      firstDesignationId: null,
      firstGradeInput: "",
      firstScaleInput: "",
      firstDepartmentInput: "",
      firstSectionInput: "",
      firstDesignationInput: "",
      prlDate: null,
      retirementDate: null,
      serviceStatus: true,
      remark: '',
      menuPosition: 0,
      isActive: true,
      retiredReasonId: null,
      retirmentId: '',
    });
  }

  loadOffice() {
    // this.subscription = 
    this.subscription.push(
      this.officeService.selectGetoffice().subscribe((data) => {
        this.offices = data;
      })
    )

  }

  onOfficeAndDepartmentSelect(departmentId: number) {
    this.empJobDetailsService.empJobDetails.sectionId = null;
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
      if (res.length > 0) {
        this.sectionView = true;
      }
      else {
        this.sectionView = false;
      }
    });
  }

  onOfficeAndDepartmentSelectFirstSection(departmentId: number) {
    this.empJobDetailsService.empJobDetails.sectionId = null;
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.firstSections = res;
      if (res.length > 0) {
        this.firstSectionView = true;
      }
      else {
        this.firstSectionView = false;
      }
    });
  }


  getAllSelectedDepartments() {
    // this.subscription = 
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
        this.departments = res;
      })
    )

  }

  onOfficeSelect(officeId: number) {
    this.empJobDetailsService.empJobDetails.departmentId = null;
    this.departmentService.getSelectedDepartmentByOfficeId(+officeId).subscribe((res) => {
      this.departments = res;
    });
  }
  onOfficeSelectGetDesignation(officeId: number, empJobDetailsId: number) {
    this.designations = [];
    this.empJobDetailsService.empJobDetails.designationId = null;
    this.empJobDetailsService.getDesignationByOfficeId(officeId, empJobDetailsId).subscribe((res) => {
      this.designations = res;
    });
  }

  onDepartmentSelectGetDesignation(departmentId: number, empJobDetailsId: number) {
    this.designations = [];
    this.empJobDetailsService.empJobDetails.designationId = null;
    if (departmentId == null) {
      this.onOfficeSelectGetDesignation(this.empJobDetailsService.empJobDetails.officeId, this.empJobDetailsId);
    }
    else {
      this.empJobDetailsService.getDesignationByDepartmentId(departmentId, empJobDetailsId).subscribe((res) => {
        this.designations = res;
      });
    }
  }

  onSectionSelectGetDesignation(sectionId: number, empJobDetailsId: number) {
    this.designations = [];
    this.empJobDetailsService.empJobDetails.designationId = null;
    if (sectionId == null) {
      this.onOfficeSelectGetDesignation(this.empJobDetailsService.empJobDetails.departmentId, this.empJobDetailsId);
    }
    else {
      this.empJobDetailsService.getDesignationBySectionId(+sectionId, +empJobDetailsId).subscribe((res) => {
        this.designations = res;
      });
    }
  }

  getAllDepartment() {
    this.empJobDetailsService.getAllDepartment().subscribe((res) => {
      this.firstDepartments = res;
    });
  }
  getOldDesignationByDepartment(departmentId: number) {
    this.firstDesignations = [];
    this.empJobDetailsService.empJobDetails.firstDesignationId = null;
    this.empJobDetailsService.empJobDetails.firstSectionId = null;
    this.empJobDetailsService.getOldDesignationByDepartment(departmentId).subscribe((res) => {
      this.firstDesignations = res;
    });
  }
  getOldDesignationBySection(sectionId: number) {
    this.firstDesignations = [];
    this.empJobDetailsService.empJobDetails.firstDesignationId = null;
    this.empJobDetailsService.getOldDesignationBySection(sectionId).subscribe((res) => {
      this.firstDesignations = res;
    });
  }

  SelectModelGrade() {
    this.gradeService.selectModelGrade().subscribe((data) => {
      this.grades = data;
    });
  }

  onChangeGradeGetScale(gradeId: number) {
    this.empJobDetailsService.empJobDetails.presentScaleId = null;
    this.empJobDetailsService.getScaleByGradeId(+gradeId).subscribe((res) => {
      this.scales = res;
    })
  }
  onChangeScaleGetBasicPay(scaleId: number) {
    this.empJobDetailsService.getBasicPayByScale(scaleId).subscribe((res) => {
      this.empJobDetailsService.empJobDetails.basicPay = res.basicPay;
    })
  }

  onChangeFirstGradeGetFirstScale(gradeId: number) {
    this.empJobDetailsService.empJobDetails.firstScaleId = null;
    this.empJobDetailsService.getScaleByGradeId(+gradeId).subscribe((res) => {
      this.firstScale = res;
    })
  }

  cancel() {
    this.close.emit();
  }


  onSubmit(form: NgForm): void {
    console.log(form.value)
    this.loading = true;
    this.empJobDetailsService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.empJobDetailsService.updateEmpJobDetails(id, form.value)
      : this.empJobDetailsService.saveEmpJobDetails(form.value);

    // this.subscription = 
    this.subscription.push(
      action$.subscribe((response: any) => {
        if (response.success) {
          this.toastr.success('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
          // this.cancel();
          this.getEmployeeByEmpId();
        } else {
          this.toastr.warning('', `${response.message}`, {
            positionClass: 'toast-top-right',
          });
          this.loading = false;
        }
        this.loading = false;
      })
    )

  }

}
