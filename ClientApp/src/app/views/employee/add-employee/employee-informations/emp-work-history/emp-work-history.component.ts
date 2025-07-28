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
import { DesignationService } from 'src/app/views/basic-setup/service/designation.service';
import { SharedService } from '../../../../../shared/shared.service'

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
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  loading: boolean = false;
  sectionView: boolean = false;
  empWorkHistory: EmpWorkHistory[] = [];

  displayedColumns: string[] = [
    'slNo',
    'department',
    'section',
    'designation',
    'workPlace',
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
    public designationService: DesignationService,
    private fb: FormBuilder,
    public sharedService: SharedService
  ) { }


  ngOnInit(): void {
    // this.getAllSelectedDepartments();
    this.getEmployeeWorkHistoryInfoByEmpId();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  getEmployeeWorkHistoryInfoByEmpId() {

    this.subscription.push(
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
    )
    
  }

  patchWorkHistoryInfo(workHistoryInfoList: any[]) {
    const control = <FormArray>this.EmpWorkHistoryInfoForm.controls['empWorkHistoryList'];
    control.clear();
  
    workHistoryInfoList.forEach((workHistoryInfo, index) => {
      // Add the work history form group for each row
      control.push(this.fb.group({
        id: [workHistoryInfo.id],
        empId: [workHistoryInfo.empId],
        departmentName: [workHistoryInfo.departmentName, Validators.required],
        sectionName: [workHistoryInfo.sectionName],
        designationName: [workHistoryInfo.designationName],
        departmentNameBangla: [workHistoryInfo.departmentNameBangla],
        sectionNameBangla: [workHistoryInfo.sectionNameBangla],
        designationNameBangla: [workHistoryInfo.designationNameBangla],
        workPlace: [workHistoryInfo.workPlace],
        workPlaceBangla: [workHistoryInfo.workPlaceBangla],
        joiningDate: [workHistoryInfo.joiningDate],
        releaseDate: [workHistoryInfo.releaseDate],
        remark: [workHistoryInfo.remark],
      }));
    });
  }
  
  
  EmpWorkHistoryInfoForm: FormGroup = new FormGroup({
    empWorkHistoryList: new FormArray([])
  });

  get empWorkHistoryListArray() {
    return this.EmpWorkHistoryInfoForm.controls["empWorkHistoryList"] as FormArray;
  }

  addWorkHistory() {
  
    // Step 2: Push the formGroup into empWorkHistoryListArray
    const formGroup = new FormGroup({
      id: new FormControl(0),
      empId: new FormControl(this.empId),
      departmentName: new FormControl(null, Validators.required),
      sectionName: new FormControl(null),
      designationName: new FormControl(null),
      departmentNameBangla: new FormControl(null),
      sectionNameBangla: new FormControl(null),
      designationNameBangla: new FormControl(null),
      workPlace: new FormControl(null),
      workPlaceBangla: new FormControl(null),
      joiningDate: new FormControl(null),
      releaseDate: new FormControl(null),
      remark: new FormControl(null),
    });
    this.empWorkHistoryListArray.push(formGroup);
  }

  find(id: number){
    this.subscription.push(
      this.empWorkHistoryService.findById(id).subscribe((res) => {
      if(res){
        this.visible = true;
        this.headerBtnText = 'Hide From';
        this.headerText = 'Update Employment History';
        this.btnText = 'Update';
        res.joiningDate = this.sharedService.parseDate(res.joiningDate);
        res.releaseDate = this.sharedService.parseDate( res.releaseDate);
        const index = 0;
        const control = <FormArray>this.EmpWorkHistoryInfoForm.controls['empWorkHistoryList'];
        control.clear();

        control.push(this.fb.group({
          id: [res.id],
          empId: [res.empId],
          departmentName: [res.departmentName],
          sectionName: [res.sectionName],
          designationName: [res.designationName],
          departmentNameBangla: [res.departmentNameBangla],
          sectionNameBangla: [res.sectionNameBangla],
          designationNameBangla: [res.designationNameBangla],
          workPlace: [res.workPlace],
          workPlaceBangla: [res.workPlaceBangla],
          joiningDate: [res.joiningDate],
          releaseDate: [res.releaseDate],
          remark: [res.remark],
        }));

        
      }
    })
    )
    
  }

  removeWorkHistoryList(index: number) {
    this.empWorkHistoryListArray.removeAt(index);
  }

  delete(element: any){
    this.confirmService
        .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
        .subscribe((result) => {
          if (result) {
            this.subscription.push(
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
            )
            )
            
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

  // getAllSelectedDepartments(){
  //   this.departmentService.getSelectedAllDepartment().subscribe((res) => {
  //         this.departments = res;
  //   });
  // }

  // onDepartmentSelect(event: Event, index: number) {
  //   const selectElement = event.target as HTMLSelectElement;
  //   const departmentId = selectElement?.value ? +selectElement.value : null;

  //   this.empWorkHistoryListArray.at(index).get('sectionId')?.setValue(null);
  //   this.empWorkHistoryListArray.at(index).get('designationId')?.setValue(null);
  
  //   if (departmentId) {
  //     this.designationOptions[index] = [];
  //     this.sectionService.getSectionByOfficeDepartment(departmentId).subscribe((res) => {
  //       this.sectionOptions[index] = res;
  //     });
  //     this.empJobDetailsService.getOldDesignationByDepartment(departmentId).subscribe((res) => {
  //       this.designationOptions[index] = res; 
  //     });
  //   } else {
  //     this.sectionOptions[index] = [];
  //     this.designationOptions[index] = [];
  //   }
  // }
  
  // onSectionSelect(event: Event, index: number) {
  //   const selectElement = event.target as HTMLSelectElement;
  //   const sectionId = selectElement?.value ? +selectElement.value : null;
    
  //   this.empWorkHistoryListArray.at(index).get('designationId')?.setValue(null);
  
  //   if (sectionId) {
  //     this.sectionService.find(sectionId).subscribe((res) => {
  //       console.log(res)
  //       if(res.showAllDesignation == true){
  //         this.designationService.getSelectDesignationSetupName().subscribe((data) => { 
  //           this.designationOptions[index] = data;
  //         });
  //       }
  //       else{
  //         this.empJobDetailsService.getOldDesignationBySection(sectionId).subscribe((res) => {
  //           this.designationOptions[index] = res;
  //         });
  //       }
  //     });
  //   } else {
  //     this.designationOptions[index] = [];
  //   }
  // }
  

  saveWorkHistory() {
    this.loading = true;

    
    const formattedJoiningDate = this.sharedService.formatDateOnly(this.empWorkHistoryService.empWorkHistory.joiningDate!);
    
    const formattedReleaseDate = this.sharedService.formatDateOnly(this.empWorkHistoryService.empWorkHistory.releaseDate!);


        const workHistoryList = this.EmpWorkHistoryInfoForm.get("empWorkHistoryList")?.value.map((item: any) => ({
      ...item,
      joiningDate: this.sharedService.formatDateOnly(item.joiningDate),
      releaseDate: this.sharedService.formatDateOnly(item.releaseDate),
      
    }));



    // const payload = {
    // ...this.EmpWorkHistoryInfoForm.get("empWorkHistoryList")?.value,
    // joiningDate : formattedJoiningDate,
    // releaseDate : formattedReleaseDate



    // };

    // console.log(payload)

    this.subscription.push(
       this.empWorkHistoryService.saveEmpWorkHistory(workHistoryList).subscribe(((res: any) => {
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
    )
   
  }



}