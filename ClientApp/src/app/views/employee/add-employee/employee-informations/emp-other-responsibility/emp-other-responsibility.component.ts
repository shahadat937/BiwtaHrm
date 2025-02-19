import { Component, EventEmitter, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, FormArray, FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { EmpOtherResponsibility } from '../../../model/emp-other-responsibility';
import { EmpJobDetailsService } from '../../../service/emp-job-details.service';
import { EmpOtherResponsibilityService } from '../../../service/emp-other-responsibility.service';
import { ResponsibilityTypeService } from 'src/app/views/basic-setup/service/responsibility-type.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-emp-other-responsibility',
  templateUrl: './emp-other-responsibility.component.html',
  styleUrl: './emp-other-responsibility.component.scss'
})
export class EmpOtherResponsibilityComponent implements OnInit, OnDestroy {
  @Input() empId!: number;
  @Output() close = new EventEmitter<void>();
  visible: boolean = true;
  headerText: string = '';
  headerBtnText: string = 'Hide From';
  btnText: string = '';
  departments: SelectedModel[] = [];
  sections: SelectedModel[] = [];
  designations: SelectedModel[] = [];
  responsibilities: SelectedModel[] = [];
  departmentOptions: SelectedModel[][] = [];
  sectionOptions: SelectedModel[][] = [];
  designationOptions: SelectedModel[][] = [];
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
    this.departmentOptions = [];
    this.sectionOptions = [];
    this.designationOptions = [];
    this.getAllSelectedDepartments();
    this.getSelectedResponsibilityType();
    this.getInActiveEmpOtherResponsibility();
    this.getEmployeeOtherResponsibilityInfoByEmpId();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  getInActiveEmpOtherResponsibility(){
    // this.subscription = 
    this.subscription.push(
      this.empOtherResponsibilityService.findInActiveByEmpId(this.empId).subscribe((res) => {
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


  getEmployeeOtherResponsibilityInfoByEmpId() {
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(this.empId).subscribe((res) => {
      if (res) {
        this.empJobDetailsId = res.id;
      }
    })
    )
    this.subscription.push(
      this.empOtherResponsibilityService.findByEmpId(this.empId).subscribe((res) => {
      if (res.length > 0) {
        this.headerText = 'Update Other Responsibility';
        this.btnText = 'Update';
        this.empOtherResponsibility = res;
        this.patchOtherResponsibilityInfo(res);
      }
      else {
        this.headerText = 'Add Other Responsibility';
        this.btnText = 'Submit';
        this.addOtherResponsibility();
      }
    })
    )
    
  }

  patchOtherResponsibilityInfo(OtherResponsibilityInfoList: any[]) {
    const control = <FormArray>this.EmpOtherResponsibilityInfoForm.controls['empOtherResponsibilityList'];
    control.clear();
  
    OtherResponsibilityInfoList.forEach((OtherResponsibilityInfo, index) => {
      // Add the work history form group for each row
      this.departmentOptions[index] = [...this.departments];
      if (OtherResponsibilityInfo.departmentId) {
        this.sectionService.getSectionByOfficeDepartment(OtherResponsibilityInfo.departmentId).subscribe((sections) => {
          this.sectionOptions[index] = sections; 

          if (OtherResponsibilityInfo.sectionId) {
            this.empJobDetailsService.getDesignationBySectionId(OtherResponsibilityInfo.sectionId, this.empJobDetailsId).subscribe((designations) => {
              this.designationOptions[index] = designations; 
            });
          }
          else {
            this.empJobDetailsService.getDesignationByDepartmentId(OtherResponsibilityInfo.departmentId, this.empJobDetailsId).subscribe((designations) => {
              this.designationOptions[index] = designations; 
            });
          }
        });
      } else {
        this.sectionOptions[index] = [];
        this.designationOptions[index] = [];
      }
      setTimeout(() => {
        control.push(this.fb.group({
          id: [OtherResponsibilityInfo.id],
          empId: [OtherResponsibilityInfo.empId],
          responsibilityTypeId: [OtherResponsibilityInfo.responsibilityTypeId],
          departmentId: [OtherResponsibilityInfo.departmentId],
          sectionId: [OtherResponsibilityInfo.sectionId],
          designationId: [OtherResponsibilityInfo.designationId],
          startDate: [OtherResponsibilityInfo.startDate],
          endDate: [OtherResponsibilityInfo.endDate],
          serviceStatus: [OtherResponsibilityInfo.serviceStatus],
          remark: [OtherResponsibilityInfo.remark],
        }));
      }, 500);
  

    });
  }
  
  
  EmpOtherResponsibilityInfoForm: FormGroup = new FormGroup({
    empOtherResponsibilityList: new FormArray([])
  });

  get empOtherResponsibilityListArray() {
    return this.EmpOtherResponsibilityInfoForm.controls["empOtherResponsibilityList"] as FormArray;
  }

  addOtherResponsibility() {
    
    this.departmentOptions.push([...this.departments]);  // Clone the departments list
    this.sectionOptions.push([]);
    this.designationOptions.push([]);

    const formGroup = new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      responsibilityTypeId: new FormControl(null),
      departmentId: new FormControl(null),
      sectionId: new FormControl(null),
      designationId: new FormControl(null),
      startDate: new FormControl(null),
      endDate: new FormControl(null),
      serviceStatus: new FormControl(true),
      remark: new FormControl(null),
    });
    setTimeout(() => {
      this.empOtherResponsibilityListArray.push(formGroup);
    }, 500);
    
  }

  removeOtherResponsibilityList(index: number, id: number) {
    if (id != 0) {
      this.subscription.push(
        this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item.')
        .subscribe((result) => {
          if (result) {
            this.empOtherResponsibilityService.deleteEmpOtherResponsibility(id).subscribe(
              (res) => {
                this.toastr.warning('Delete sucessfully ! ', ` `, {
                  positionClass: 'toast-top-right',
                });

                // if (this.empOtherResponsibilityListArray.controls.length > 0)
                //   this.empOtherResponsibilityListArray.removeAt(index);
                this.getInActiveEmpOtherResponsibility();
              },
              (err) => {
                this.toastr.error('Somethig Wrong ! ', ` `, {
                  positionClass: 'toast-top-right',
                });
                console.log(err);
              }
            );
          }
        })
      )
      
    }
    else if (id == 0) {
      if (this.empOtherResponsibilityListArray.controls.length > 0)
        this.empOtherResponsibilityListArray.removeAt(index);
    }
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
    // this.subscription = 
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
    )
    
  }

  onDepartmentSelect(event: Event, index: number) {
    const selectElement = event.target as HTMLSelectElement;
    const departmentId = selectElement?.value ? +selectElement.value : null;

    this.empOtherResponsibilityListArray.at(index).get('sectionId')?.setValue(null);
    this.empOtherResponsibilityListArray.at(index).get('designationId')?.setValue(null);
  
    if (departmentId) {
      
      this.designationOptions[index] = [];
      this.subscription.push(
        this.sectionService.getSectionByOfficeDepartment(departmentId).subscribe((res) => {
        this.sectionOptions[index] = res;
      })
      )
      this.subscription.push(
        this.empJobDetailsService.getDesignationByDepartmentId(departmentId, this.empJobDetailsId).subscribe((res) => {
        this.designationOptions[index] = res; 
      })
      )
      
    } else {
      this.sectionOptions[index] = [];
      this.designationOptions[index] = [];
    }
  }
  
  onSectionSelect(event: Event, index: number) {
    const selectElement = event.target as HTMLSelectElement;
    const sectionId = selectElement?.value ? +selectElement.value : null;
    
    this.empOtherResponsibilityListArray.at(index).get('designationId')?.setValue(null);
  
    if (sectionId) {
      this.subscription.push(
        this.empJobDetailsService.getDesignationBySectionId(+sectionId, this.empJobDetailsId).subscribe((res) => {
        this.designationOptions[index] = res; 
      })
      )
      
    } else {
      this.designationOptions[index] = [];
    }
  }

  findById(id: number){
    this.subscription.push(
    this.empOtherResponsibilityService.findById(id).subscribe((res) => {
      if(res){
        this.headerText = 'Update Other Responsibility';
        this.btnText = 'Update';
        const index = 0;
        const control = <FormArray>this.EmpOtherResponsibilityInfoForm.controls['empOtherResponsibilityList'];
        control.clear();

        this.departmentOptions[index] = [...this.departments];
      if (res.departmentId) {
        this.sectionService.getSectionByOfficeDepartment(res.departmentId).subscribe((sections) => {
          this.sectionOptions[index] = sections; 

          if (res.sectionId) {
            this.empJobDetailsService.getDesignationBySectionId(res.sectionId, this.empJobDetailsId).subscribe((designations) => {
              this.designationOptions[index] = designations; 
            });
          }
          else {
            this.empJobDetailsService.getDesignationByDepartmentId(res.departmentId, this.empJobDetailsId).subscribe((designations) => {
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
        responsibilityTypeId: [res.responsibilityTypeId],
        departmentId: [res.departmentId],
        sectionId: [res.sectionId],
        designationId: [res.designationId],
        startDate: [res.startDate],
        endDate: [res.endDate],
        serviceStatus: [res.serviceStatus],
        remark: [res.remark],
      }));
      }
    })
    )
   
  }
  

  saveOtherResponsibility() {
    this.loading = true;
    this.subscription.push(
      this.empOtherResponsibilityService.saveEmpOtherResponsibility(this.EmpOtherResponsibilityInfoForm.get("empOtherResponsibilityList")?.value).subscribe(((res: any) => {
      if (res.success) {
        this.toastr.success('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        // this.cancel();
        // this.getEmployeeOtherResponsibilityInfoByEmpId();
        this.ngOnInit();
      } else {
        this.toastr.warning('', `${res.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
      }
      this.loading = false;
    })
    )
    )
    
  }

  updateOtherResponsibilityStatus(index: number, id: number){
    this.confirmService
    .confirm('Confirm Inactive message', 'Are You Sure In-Active This Responsibility.')
    .subscribe((result) => {
      if (result) {
        this.subscription.push(
          this.empOtherResponsibilityService.updateEmpOtherResponsibilityStatus(id).subscribe(
          (res:any) => {
            if(res.success){
              this.toastr.warning('In-Active sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
  
              if (this.empOtherResponsibilityListArray.controls.length > 0)
                this.empOtherResponsibilityListArray.removeAt(index);
                // this.getEmployeeOtherResponsibilityInfoByEmpId();
                this.getInActiveEmpOtherResponsibility();
            }
          },
          (err) => {
            this.toastr.error('Somethig Wrong ! ', ` `, {
              positionClass: 'toast-top-right',
            });
            console.log(err);
          }
        )
        )
        
      }
    });
  }
}