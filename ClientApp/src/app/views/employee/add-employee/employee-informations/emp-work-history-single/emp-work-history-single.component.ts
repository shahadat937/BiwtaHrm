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
import { DesignationService } from 'src/app/views/basic-setup/service/designation.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { EmpWorkHistory } from '../../../model/emp-work-history';
import { EmpJobDetailsService } from '../../../service/emp-job-details.service';
import { EmpWorkHistoryService } from '../../../service/emp-work-history.service';

@Component({
  selector: 'app-emp-work-history-single',
  templateUrl: './emp-work-history-single.component.html',
  styleUrl: './emp-work-history-single.component.scss'
})
export class EmpWorkHistorySingleComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = false;
  headerText: string = '';
  headerBtnText: string = 'Add New';
  btnText: string = '';
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  showAllDesination = false;
  subscription: Subscription = new Subscription();
  loading: boolean = false;
  sectionView: boolean = false;
  empWorkHistory: EmpWorkHistory[] = [];

  displayedColumns: string[] = [
    'slNo',
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
  @ViewChild('EmpWorkHistoryForm', { static: true }) EmpWorkHistoryForm!: NgForm;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empWorkHistoryService: EmpWorkHistoryService,
    public empJobDetailsService: EmpJobDetailsService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    public designationService: DesignationService,
    private fb: FormBuilder) { }

    ngOnInit(): void {
      // this.getAllSelectedDepartments();
      // this.getEmployeeWorkHistoryInfoByEmpId();
    }
  
    ngOnDestroy(): void {
      // if (this.subscription) {
      //   this.subscription.unsubscribe();
      // }
    }
    
//   applyFilter(filterValue: string) {
//     filterValue = filterValue.trim();
//     filterValue = filterValue.toLowerCase();
//     this.dataSource.filter = filterValue;
//   }
//   UserFormView(): void {
//     this.visible = !this.visible;
//     this.headerBtnText = this.visible ? 'Hide Form' : 'Add New';
//   }

//     getAllSelectedDepartments(){
//       this.departmentService.getSelectedAllDepartment().subscribe((res) => {
//             this.departments = res;
//       });
//     }

    
//   onDepartmntSelectGetSection(departmentId: any){
//     this.sections = [];
//     this.designations = [];
//     this.empWorkHistoryService.empWorkHistory.sectionId = null;
//     this.empWorkHistoryService.empWorkHistory.designationId = null;
//     this.empWorkHistoryService.empWorkHistory.designationSetupId = null;
//     if(departmentId){
//       this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
//         this.sections = res;
//       });
//     }
//   }
//   onDepartmntSelectGetDesignation(departmentId: any) {
//     this.designations = [];
//     this.empWorkHistoryService.empWorkHistory.designationId = null;
//     this.empWorkHistoryService.empWorkHistory.designationSetupId = null;
//     if(departmentId){
//       this.empJobDetailsService.getOldDesignationByDepartment(+departmentId).subscribe((res) => {
//         this.designations = res;
//       });
//     }
//   }
//   onSectionSelectGetDesignation(sectionId: any){
//     this.designations = [];
//     this.empWorkHistoryService.empWorkHistory.designationId = null;
//     this.empWorkHistoryService.empWorkHistory.designationSetupId = null;
//     if(sectionId){
//       this.sectionService.find(sectionId).subscribe((res) => {
//         if(res.showAllDesignation == true){
//           this.designationService.getSelectDesignationSetupName().subscribe((data) => { 
//             this.designations = data;
//             this.showAllDesination = true;
//           });
//         }
//         else{
//           this.empJobDetailsService.getOldDesignationBySection(+sectionId).subscribe((res) => {
//             this.designations = res;
//             this.showAllDesination = false;
//           });
//         }
//     })}
//   }

//     initaialForm(form?: NgForm) {
//       if (form != null) form.resetForm();
//       this.empWorkHistoryService.empWorkHistory = {
//         id : 0,
//         empId : this.empId,
//         officeId : null,
//         departmentId : null,
//         sectionId : null,
//         designationId : null,
//         designationSetupId : null,
//         joiningDate : null,
//         releaseDate : null,
//         remark : '',
//         IsActive : true,
//         officeName : '',
//         departmentName : '',
//         sectionName : '',
//         designationName : '',
//         designationSetupName : '',
//       };
//     }
  
//     resetForm() {
//       this.headerText = 'Add Employment History';
//       this.btnText = 'Submit';
//       this.EmpWorkHistoryForm.form.reset();
//       this.EmpWorkHistoryForm.form.patchValue({
//         id : 0,
//         empId : this.empId,
//         officeId : null,
//         departmentId : null,
//         sectionId : null,
//         designationId : null,
//         designationSetupId : null,
//         joiningDate : null,
//         releaseDate : null,
//         remark : '',
//         IsActive : true,
//         officeName : '',
//         departmentName : '',
//         sectionName : '',
//         designationName : '',
//       });
//     }

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

//     onSubmit(form: NgForm): void {
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
