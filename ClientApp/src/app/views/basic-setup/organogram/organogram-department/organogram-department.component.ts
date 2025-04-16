import { Component, Input } from '@angular/core';
import { OrganogramDepartmentNameDto, OrganogramService } from '../../service/organogram.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EmpProfileComponent } from '../../../../../../src/app/views/employee/manage-employee/emp-profile/emp-profile.component';

@Component({
  selector: 'app-organogram-department',
  templateUrl: './organogram-department.component.html',
  styleUrls: ['./organogram-department.component.scss']
})
export class OrganogramDepartmentComponent {
  @Input() department!: OrganogramDepartmentNameDto;
  isExpanded: boolean = false;
  isDesignationExpanded: boolean = false;
  isDepartmentExpanded: boolean = false;
  isSectionExpanded: boolean = false;
  
  constructor(
    private modalService: BsModalService, public organogramService: OrganogramService
  ) {
  }

  toggleExpand(): void {
    this.isExpanded = !this.isExpanded;
  }

  toggleDesignationExpand(): void {
    this.isDesignationExpanded = !this.isDesignationExpanded;
  }
  
  toggleDepartmentExpand(): void {
    this.isDepartmentExpanded = !this.isDepartmentExpanded;
  }
  
  toggleSectionExpand(): void {
    this.isSectionExpanded = !this.isSectionExpanded;
  }

  isSubDeparmentExtends(departmentId : number, sectionId: number){

    this.organogramService.getDesiginationDepartmentSectionCount(departmentId, sectionId).subscribe(res=>{
    this.department.subDepartmentCount = res.departmentCount;
    this.department.designationCount = res.designationCount;
    this.department.sectionCount = res.sectionCount;
    console.log(res);
    })

  }
  updateSubDepartments(departmentId: any) {
    this.organogramService.getSubDept(departmentId).subscribe(res => {
      if (res && Array.isArray(res)) {
        this.department.subDepartments = res.map(sub => ({
          departmentId: sub.departmentId,
          name: sub.departmentName,
          designations: [],         // Default value
          subDepartments: [],       // Default value
          sections: [],             // Default value
          subDepartmentCount: 0,    // Default value
          designationCount: 0,      // Default value
          sectionCount: 0           // Default value
        }));
      }
    });
  }

  getEmployeeWithDesignation(departmentId: any, sectionId: any) {
    console.log(this.department);
  
    this.organogramService.getEmployeeWithDesignation(departmentId, sectionId).subscribe((res: any[]) => {
      this.department.designations = res.map((item: any) => ({
        name: item.name,
        employeeInfo: item.employeeInfo
      }));
    });
  }
  
   
  
  



  viewEmployeeProfile(id: number){
    const isModal = true;
    const initialState = {
      id: id,
      isModal: isModal
    };
    const modalRef: BsModalRef = this.modalService.show(EmpProfileComponent, { initialState});
  }
}
