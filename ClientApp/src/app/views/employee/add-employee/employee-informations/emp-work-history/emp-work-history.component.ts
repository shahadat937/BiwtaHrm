import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormArray, Validators, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { EmpWorkHistory } from '../../../model/emp-work-history';
import { EmpWorkHistoryService } from '../../../service/emp-work-history.service';
import { EmpPersonalInfoService } from '../../../service/emp-personal-info.service';
import { EmpSpouseInfoService } from '../../../service/emp-spouse-info.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { EmpJobDetailsService } from '../../../service/emp-job-details.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-emp-work-history',
  templateUrl: './emp-work-history.component.html',
  styleUrl: './emp-work-history.component.scss'
})
export class EmpWorkHistoryComponent  implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = false;
  headerText: string = '';
  headerBtnText: string = 'Add New';
  btnText: string = '';
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  departmentOptions: SelectedModel[][] = [];
  sectionOptions: SelectedModel[][] = [];
  designationOptions: SelectedModel[][] = [];
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empWorkHistoryService: EmpWorkHistoryService,
    public empJobDetailsService: EmpJobDetailsService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    private fb: FormBuilder) { }


  ngOnInit(): void {
    this.getAllSelectedDepartments();
    this.getEmployeeWorkHistoryInfoByEmpId();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  getEmployeeWorkHistoryInfoByEmpId() {
    this.empWorkHistoryService.findByEmpId(this.empId).subscribe((res) => {
      // if (res.length > 0) {
      //   this.headerText = 'Update Work History';
      //   this.btnText = 'Update';
      //   this.empWorkHistory = res;
      //   // this.patchWorkHistoryInfo(res);
      //   this.dataSource = new MatTableDataSource(res);
      //   this.dataSource.paginator = this.paginator;
      //   this.dataSource.sort = this.matSort;
      // }
      // else {
        const control = <FormArray>this.EmpWorkHistoryInfoForm.controls['empWorkHistoryList'];
        control.clear();
        this.visible = false;
        this.headerText = 'Add Employment History';
        this.btnText = 'Submit';
        this.headerBtnText = 'Add New';
        this.addWorkHistory();
        this.empWorkHistory = res;
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.matSort;
    })
  }

  patchWorkHistoryInfo(workHistoryInfoList: any[]) {
    const control = <FormArray>this.EmpWorkHistoryInfoForm.controls['empWorkHistoryList'];
    control.clear();
  
    workHistoryInfoList.forEach((workHistoryInfo, index) => {
      // Add the work history form group for each row
      control.push(this.fb.group({
        id: [workHistoryInfo.id],
        empId: [workHistoryInfo.empId],
        departmentId: [workHistoryInfo.departmentId],
        sectionId: [workHistoryInfo.sectionId],
        designationId: [workHistoryInfo.designationId],
        joiningDate: [workHistoryInfo.joiningDate],
        releaseDate: [workHistoryInfo.releaseDate],
        remark: [workHistoryInfo.remark],
      }));
  
      this.departmentOptions[index] = [...this.departments];

      if (workHistoryInfo.departmentId) {
        this.sectionService.getSectionByOfficeDepartment(workHistoryInfo.departmentId).subscribe((sections) => {
          this.sectionOptions[index] = sections; 

          if (workHistoryInfo.sectionId) {
            this.empJobDetailsService.getOldDesignationBySection(workHistoryInfo.sectionId).subscribe((designations) => {
              this.designationOptions[index] = designations; 
            });
          }
          else {
            this.empJobDetailsService.getOldDesignationByDepartment(workHistoryInfo.departmentId).subscribe((designations) => {
              this.designationOptions[index] = designations; 
            });
          }
        });
      } else {
        this.sectionOptions[index] = [];
        this.designationOptions[index] = [];
      }
    });
  }
  
  
  EmpWorkHistoryInfoForm: FormGroup = new FormGroup({
    empWorkHistoryList: new FormArray([])
  });

  get empWorkHistoryListArray() {
    return this.EmpWorkHistoryInfoForm.controls["empWorkHistoryList"] as FormArray;
  }

  addWorkHistory() {
    const formGroup = new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      departmentId: new FormControl(null),
      sectionId: new FormControl(null),
      designationId: new FormControl(null),
      joiningDate: new FormControl(null),
      releaseDate: new FormControl(null),
      remark: new FormControl(null),
    });

    this.empWorkHistoryListArray.push(formGroup);
    
    // Initialize department, section, and designation options for the new row
    this.departmentOptions.push([...this.departments]);  // Clone the departments list
    this.sectionOptions.push([]);
    this.designationOptions.push([]);
  }

  find(id: number){
    this.empWorkHistoryService.findById(id).subscribe((res) => {
      console.log(res)
      if(res){
        this.visible = true;
        this.headerBtnText = 'Hide From';
        this.headerText = 'Update Employment History';
        this.btnText = 'Update';
        const index = 0;
        const control = <FormArray>this.EmpWorkHistoryInfoForm.controls['empWorkHistoryList'];
        control.clear();

        this.departmentOptions[index] = [...this.departments];if (res.departmentId) {
        this.sectionService.getSectionByOfficeDepartment(res.departmentId).subscribe((sections) => {
          this.sectionOptions[index] = sections; 

          if (res.sectionId) {
            this.empJobDetailsService.getOldDesignationBySection(res.sectionId).subscribe((designations) => {
              this.designationOptions[index] = designations; 
            });
          }
          else {
            this.empJobDetailsService.getOldDesignationByDepartment(res.departmentId || 0).subscribe((designations) => {
              this.designationOptions[index] = designations; 
            });
          }
        });
      } else {
        this.sectionOptions[index] = [];
        this.designationOptions[index] = [];
      }

        control.push(this.fb.group({
          id: [res.id],
          empId: [res.empId],
          departmentId: [res.departmentId],
          sectionId: [res.sectionId],
          designationId: [res.designationId],
          joiningDate: [res.joiningDate],
          releaseDate: [res.releaseDate],
          remark: [res.remark],
        }));

        
      }
    })
  }

  removeWorkHistoryList(index: number) {
    this.empWorkHistoryListArray.removeAt(index);
  }

  delete(element: any){
    this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.empWorkHistoryService.deleteEmpWorkHistory(element.id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                const index = this.dataSource.data.indexOf(element);
                if (index !== -1) {
                  this.dataSource.data.splice(index, 1);
                  this.dataSource = new MatTableDataSource(this.dataSource.data);
                }
              },
              (err) => {
                this.toastr.error('Somethig Wrong ! ', ` `, {
                  positionClass: 'toast-top-right',
                });
                console.log(err);
              }
            );
          }
        });
  }

  UserFormView(): void {
    this.visible = !this.visible;
    this.headerBtnText = this.visible ? 'Hide Form' : 'Add New';
  }


  cancel() {
    this.getEmployeeWorkHistoryInfoByEmpId();
    // this.close.emit();
  }

  getAllSelectedDepartments(){
    this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    });
  }

  onDepartmentSelect(event: Event, index: number) {
    const selectElement = event.target as HTMLSelectElement;
    const departmentId = selectElement?.value ? +selectElement.value : null;

    this.empWorkHistoryListArray.at(index).get('sectionId')?.setValue(null);
    this.empWorkHistoryListArray.at(index).get('designationId')?.setValue(null);
  
    if (departmentId) {
      this.designationOptions[index] = [];
      this.sectionService.getSectionByOfficeDepartment(departmentId).subscribe((res) => {
        this.sectionOptions[index] = res;
      });
      this.empJobDetailsService.getOldDesignationByDepartment(departmentId).subscribe((res) => {
        this.designationOptions[index] = res; 
      });
    } else {
      this.sectionOptions[index] = [];
      this.designationOptions[index] = [];
    }
  }
  
  onSectionSelect(event: Event, index: number) {
    const selectElement = event.target as HTMLSelectElement;
    const sectionId = selectElement?.value ? +selectElement.value : null;
    
    this.empWorkHistoryListArray.at(index).get('designationId')?.setValue(null);
  
    if (sectionId) {
      this.empJobDetailsService.getOldDesignationBySection(sectionId).subscribe((res) => {
        this.designationOptions[index] = res; 
      });
    } else {
      this.designationOptions[index] = [];
    }
  }
  

  saveWorkHistory() {
    this.loading = true;
    this.empWorkHistoryService.saveEmpWorkHistory(this.EmpWorkHistoryInfoForm.get("empWorkHistoryList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        // this.cancel();
        this.getEmployeeWorkHistoryInfoByEmpId();
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
    )
  }



}