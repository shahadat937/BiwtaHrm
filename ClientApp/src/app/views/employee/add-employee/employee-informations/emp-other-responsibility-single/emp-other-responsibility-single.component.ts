import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { ResponsibilityTypeService } from 'src/app/views/basic-setup/service/responsibility-type.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { EmpOtherResponsibility } from '../../../model/emp-other-responsibility';
import { EmpJobDetailsService } from '../../../service/emp-job-details.service';
import { EmpOtherResponsibilityService } from '../../../service/emp-other-responsibility.service';

@Component({
  selector: 'app-emp-other-responsibility-single',
  templateUrl: './emp-other-responsibility-single.component.html',
  styleUrl: './emp-other-responsibility-single.component.scss'
})
export class EmpOtherResponsibilitySingleComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = false;
  headerText: string = '';
  headerBtnText: string = 'Show From';
  btnText: string = '';
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  responsibilities: SelectedModel[] = [];
  // subscription: Subscription = new Subscription();
  subscription: Subscription[] = []
  loading: boolean = false;
  sectionView: boolean = false;
  empJobDetailsId = 0;
  empOtherResponsibility: EmpOtherResponsibility[] = [];
  
  displayedColumns: string[] = [
    'slNo',
    'responsibilityName',
    'department',
    'section',
    'designation',
    'joiningDate',
    'releaseDate',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  @ViewChild('EmpOtherResponsibilityForm', { static: true }) EmpOtherResponsibilityForm!: NgForm;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empOtherResponsibilityService: EmpOtherResponsibilityService,
    public empJobDetailsService: EmpJobDetailsService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    public responsibilityTypeService : ResponsibilityTypeService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    // this.getAllSelectedDepartments();
    // this.getSelectedResponsibilityType();
    this.getAllEmpOtherResponsibilityByEmpId();
    // this.getEmployeeOtherResponsibilityInfoByEmpId();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  
  getAllEmpOtherResponsibilityByEmpId(){
    // this.subscription = 
    this.subscription.push(
      this.empOtherResponsibilityService.findAllByEmpId(this.empId).subscribe((res) => {
      this.dataSource = new MatTableDataSource(res);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Show Form';
  }


  cancel() {
    // this.close.emit();
    this.ngOnInit();
  }

  getSelectedResponsibilityType(){
    this.subscription.push(
      this.responsibilityTypeService.getSelectedResponsibilityType().subscribe((res) => {
      this.responsibilities = res;
    })
    )
    
  }

  getAllSelectedDepartments(){
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
  }

  
    initaialForm(form?: NgForm) {
      if (form != null) form.resetForm();
      this.empOtherResponsibilityService.empOtherResponsibility = {
        id : 0,
        empId : this.empId,
        orderNo : '',
        orderDate: null,
        responsibilityTypeId: null,
        officeId : null,
        departmentId : null,
        sectionId : null,
        designationId : null,
        startDate : null,
        endDate : null,
        serviceStatus: false,
        remark : '',
        isActive : true,
        responsibilityName: '',
        officeName : '',
        departmentName : '',
        sectionName : '',
        designationName : '',
      };
    }
  
    resetForm() {
      this.headerText = 'Add Employment History';
      this.btnText = 'Submit';
      this.EmpOtherResponsibilityForm.form.reset();
      this.EmpOtherResponsibilityForm.form.patchValue({
        id : 0,
        empId : this.empId,
        orderNo : '',
        orderDate: null,
        responsibilityTypeId: null,
        officeId : null,
        departmentId : null,
        sectionId : null,
        designationId : null,
        startDate : null,
        endDate : null,
        serviceStatus: false,
        remark : '',
        isActive : true,
        responsibilityName: '',
        officeName : '',
        departmentName : '',
        sectionName : '',
        designationName : '',
      });
    }

//     getEmployeeWorkHistoryInfoByEmpId() {
//       this.initaialForm();
//       this.empWorkHistoryService.findByEmpId(this.empId).subscribe((res) => {
//           this.headerText = 'Add Employment History';
//           this.btnText = 'Submit';
//           this.headerBtnText = 'Add New';
//           this.empWorkHistory = res;
//           this.dataSource = new MatTableDataSource(res);
//           this.dataSource.paginator = this.paginator;
//           this.dataSource.sort = this.matSort;
//       })
//     }

//     find(id: number){
//       this.empWorkHistoryService.findById(id).subscribe((res) => {
//         if(res){
//           this.visible = true;
//           this.headerBtnText = 'Hide From';
//           this.headerText = 'Update Employment History';
//           this.btnText = 'Update';

//           if(res.departmentId){
//             this.onDepartmntSelectGetSection(res.departmentId);
//           }
//           if(res.sectionId){
//             this.onSectionSelectGetDesignation(res.sectionId);
//           }
//           else{
//             this.onDepartmntSelectGetDesignation(res.departmentId);
//           }
//           setTimeout(() => {
//             this.EmpWorkHistoryForm?.form.patchValue(res);
//           }, 500);
//         }
//       })
//     }

    onSubmit(form: NgForm): void {}
//       this.loading = true;
//       this.empJobDetailsService.cachedData = [];
//       this.loading = true;
//       this.empWorkHistoryService.saveEmpWorkHistory(form.value).subscribe(((res: any) => {
//       if (res.success) {
//         this.toastr.success('', `${res.message}`, {
//           positionClass: 'toast-top-right',
//         });
//         this.loading = false;
//         // this.cancel();
//         this.getEmployeeWorkHistoryInfoByEmpId();
//       } else {
//         this.toastr.warning('', `${res.message}`, {
//           positionClass: 'toast-top-right',
//         });
//         this.loading = false;
//       }
//       this.loading = false;
//     })
//     )
//   }
  
//     delete(element: any){
//       this.confirmService
//           .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
//           .subscribe((result) => {
//             if (result) {
//               this.empWorkHistoryService.deleteEmpWorkHistory(element.id).subscribe(
//                 (res) => {
//                   this.toastr.warning('Delete sucessfully ! ', ` `, {
//                     positionClass: 'toast-top-right',
//                   });
  
//                   const index = this.dataSource.data.indexOf(element);
//                   if (index !== -1) {
//                     this.dataSource.data.splice(index, 1);
//                     this.dataSource = new MatTableDataSource(this.dataSource.data);
//                   }
//                 },
//                 (err) => {
//                   this.toastr.error('Somethig Wrong ! ', ` `, {
//                     positionClass: 'toast-top-right',
//                   });
//                   console.log(err);
//                 }
//               );
//             }
//           });
//     }


}
