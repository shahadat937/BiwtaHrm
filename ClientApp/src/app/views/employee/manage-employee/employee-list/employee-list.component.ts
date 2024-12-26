import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ManageEmployeeService } from '../../service/manage-employee.service';
import { cilZoom, cilPlus } from '@coreui/icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmployeeInformationComponent } from '../employee-information/employee-information.component';
import { EmpProfileComponent } from '../emp-profile/emp-profile.component';
import { PaginatorModel } from 'src/app/core/models/paginator-model';
import { DepartmentService } from 'src/app/views/basic-setup/service/department.service';
import { SectionService } from 'src/app/views/basic-setup/service/section.service';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    'idNo',

    'fullName', 
    'department', 
    'section', 
    'designation', 
    // 'fullNameBangla', 
    // 'email', 
    // 'isActive', 
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  pagination: PaginatorModel = new PaginatorModel();
  departments: any;
  sections: any;
  selectedDepartment: number = 0;
  selectedSection: number = 0;

  constructor(
    public manageEmployeeService: ManageEmployeeService,
    private modalService: BsModalService,
    public departmentService: DepartmentService,
    public sectionService : SectionService,
    public empBasicInfoService: EmpBasicInfoService,
  ) {
  }

  icons = { cilZoom, cilPlus };

  ngOnInit(): void {
    // this.getAllEmpBasicInfo();
    this.getAllEmpBasicInfoQueryPerams(this.pagination);
    this.getAllSelectedDepartments();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  getAllEmpBasicInfo(){
    // this.subscription = 
    this.subscription.push(
     this.manageEmployeeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
    })
    )
   

  }

  
  getAllSelectedDepartments(){
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe((res) => {
          this.departments = res;
    })
    )
  }

  onDepartmentSelectGetSection(departmentId : number){
    this.sections = [];
    this.sectionService.getSectionByOfficeDepartment(+departmentId).subscribe((res) => {
      this.sections = res;
    });
    this.selectedDepartment = departmentId;
    this.getAllEmpBasicInfoQueryPerams(this.pagination);
  }
  onSectionChange(sectionId: number){
    this.selectedSection = sectionId;
    this.getAllEmpBasicInfoQueryPerams(this.pagination);
  }

  viewEmployeeInformation(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(EmployeeInformationComponent, { initialState, backdrop: 'static' });
  }
  
  viewEmployeeProfile(id: number){
    const isModal = true;
    const initialState = {
      id: id,
      isModal: isModal
    };
    const modalRef: BsModalRef = this.modalService.show(EmpProfileComponent, { initialState});
  }

  
  
  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.searchText = filterValue;
    this.getAllEmpBasicInfoQueryPerams(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.pageSize;
    event.pageIndex = event.pageIndex + 1;
    this.getAllEmpBasicInfoQueryPerams(event);
  }

  getAllEmpBasicInfoQueryPerams(queryParams: any){
    // this.subscription = 
    this.subscription.push(
      this.empBasicInfoService.getAllPagination(queryParams, this.selectedDepartment, this.selectedSection).subscribe((employees: any) => {
      this.dataSource.data = employees.items;
      // this.dataSource.paginator = this.paginator;
      this.pagination.length = employees.totalItemsCount;
    })
    )
    
  }

}

