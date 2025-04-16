import { AfterViewInit, Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from '../../../../../src/app/core/service/confirm.service';
import { OrganogramDepartmentNameDto, OrganogramOfficeNameDto, OrganogramService } from '../service/organogram.service';
import { Subscription } from 'rxjs';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EmpProfileComponent } from '../../employee/manage-employee/emp-profile/emp-profile.component';
import { SubDepartment } from '../model/sub-department';

@Component({
  selector: 'app-organogram',
  templateUrl: './organogram.component.html',
  styleUrl: './organogram.component.scss'
})
export class OrganogramComponent implements OnInit, OnDestroy  {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  organograms:any[] = [];
  offices: OrganogramOfficeNameDto[] = [];
  // departments: OrganogramDepartmentNameDto[] = [];
  departments: any[] = [];
  expandedOffices: { [key: string]: boolean } = {};
  expandedDesignations: { [key: string]: boolean } = {};
  expandedDepartments: { [key: string]: boolean } = {};
  expandedSections: { [key: string]: boolean } = {};
  isApiCalled : boolean = false;
  toggleIcon = "+";
  departmentToggleIcon = "+";
  designationToggleIcon = "+"

  constructor(
    public organogramService: OrganogramService,
    private modalService: BsModalService,
  ) {
  }
  ngOnInit(): void {
    // this.getOrganogram();
    this.getTopLavelDept(0);
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  getOrganogram(){
    this.subscription.push(
    this.organogramService.getOrganogramNamesOnly().subscribe((data: OrganogramDepartmentNameDto[]) => { 
      this.organograms = data;
      this.departments = data;
    })
    )
    
  }
  
  toggleOfficeExpand(officeName: string): void {
    this.expandedOffices[officeName] = !this.expandedOffices[officeName];
  }

  isOfficeExpanded(officeName: string, departmentId: number, sectionId: number){
   if(this.toggleIcon === '+'){
    this.toggleIcon = '-'
    this.deparmentDesignationSectionCountAvaialable(departmentId, sectionId);
   }
   else{
    this.toggleIcon = '+'
    this.resetDepartmentCountById(departmentId);
   }

  }
  
  toggleDesignationExpand(officeName: string): void {
    this.expandedDesignations[officeName] = !this.expandedDesignations[officeName];
  }

  isDesignationExpanded(officeName: string): boolean {
    return this.expandedDesignations[officeName];
  }

  toggleDepartmentExpand(officeName: string): void {
    this.expandedDepartments[officeName] = !this.expandedDepartments[officeName];
  }

  isDepartmentExpanded(officeName: string, departmentId : any) {
    if(this.departmentToggleIcon === '+'){
      this.getSubDepartment(departmentId);
      this.departmentToggleIcon = '-'
    }
    else{
      this.resetSubDepartments(departmentId);
      this.departmentToggleIcon = '+'
    }
    return this.expandedDepartments[officeName];
  }
  

  toggleSectionExpand(officeName: string): void {
    this.expandedSections[officeName] = !this.expandedSections[officeName];
  }

  isSectionExpanded(officeName: string): boolean {
    return this.expandedSections[officeName];
  }
  
  
  viewEmployeeProfile(id: number){
    const isModal = true;
    const initialState = {
      id: id,
      isModal: isModal
    };
    const modalRef: BsModalRef = this.modalService.show(EmpProfileComponent, { initialState});
  }


  //--------------------------------

  getTopLavelDept(deptId :any) {
    this.organogramService.getTopLavelDept(deptId).subscribe(res => {

      if (res && Array.isArray(res)) {
        this.departments = res.map(dept => ({
          departmentId: dept.departmentId,
          departmentName: dept.departmentName,
          designationCount: 0, // Initialize
          departmentCount: 0,  // Initialize
          sectionCount : 0,
          subDepartments : [],
          designations : []
        }));
      } 
    });
  }

  getSubDepartment(deptId: any) {
    this.organogramService.getSubDept(deptId).subscribe(res => {
      if (res && Array.isArray(res)) {
        const index = this.departments.findIndex(d => d.departmentId === deptId);
        if (index !== -1) {
          this.departments[index].subDepartments = res.map(sub => ({
            departmentId: sub.departmentId,
            name: sub.departmentName
          }));

        }
      }
    });
  }
  

  deparmentDesignationSectionCountAvaialable(departmentId: number, sectionId: number) {
  
      this.organogramService.getDesiginationDepartmentSectionCount(departmentId, sectionId).subscribe(res => {
        const index = this.departments.findIndex(d => d.departmentId === departmentId);
  
        if (index !== -1) {
          this.departments[index] = {
            ...this.departments[index],
            designationCount: res.designationCount,
            departmentCount: res.departmentCount
          };
        }

        console.log(this.departments);
      });
    
  }

  resetDepartmentCountById(departmentId: number) {
    const index = this.departments.findIndex(d => d.departmentId === departmentId);
    if (index !== -1) {
      this.departments[index] = {
        ...this.departments[index],
        designationCount: 0,
        departmentCount: 0
      };
    }
  }

  resetSubDepartments(deptId: number) {
    const index = this.departments.findIndex(dept => dept.departmentId === deptId);
    console.log(index);
    if (index !== -1) {
      this.departments[index].subDepartments = [];
    }
  }
  
  getEmployeeWithDesignation(departmentId: any, sectionId: any) {
    if(this.designationToggleIcon === '+'){
      this.organogramService.getEmployeeWithDesignation(departmentId, sectionId).subscribe((res: any[]) => {
        const index = this.departments.findIndex(d => d.departmentId === departmentId);
        if (index !== -1) {
          this.departments[index].designations = res.map((item: any) => ({
            name: item.name,
            employeeInfo: item.employeeInfo
          }));
        }
        this.designationToggleIcon = "-"
      });
    }
    else{
      this.designationToggleIcon = "+"
      this.resetDesignations(departmentId);
    }
  }
  
  resetDesignations(departmentId: any) {
    const index = this.departments.findIndex(d => d.departmentId === departmentId);
    if (index !== -1) {
      this.departments[index].designations = [];
    }
  }
  
  
  
    
}

