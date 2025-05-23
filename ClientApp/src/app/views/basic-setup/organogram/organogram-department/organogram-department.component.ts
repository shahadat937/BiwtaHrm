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
    this.isSubDeparmentExtends(this.department.departmentId, 0)
  }

  toggleDesignationExpand(): void {
    this.isDesignationExpanded = !this.isDesignationExpanded;
    this.getEmployeeWithDesignation(this.department.departmentId, 0)
  }
  
  toggleDepartmentExpand(): void {
    this.isDepartmentExpanded = !this.isDepartmentExpanded;
    this.updateSubDepartments(this.department.departmentId)
  }
  
  toggleSectionExpand(): void {
    this.isSectionExpanded = !this.isSectionExpanded;
    this.getSectionByDepartment(this.department.departmentId,0);
  }

  isSubDeparmentExtends(departmentId : number, sectionId: number){
    this.organogramService.getDesiginationDepartmentSectionCount(departmentId, sectionId).subscribe(res=>{
    this.department.subDepartmentCount = res.departmentCount;
    this.department.designationCount = res.designationCount;
    this.department.sectionCount = res.sectionCount;
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
    this.organogramService.getEmployeeWithDesignation(departmentId, sectionId).subscribe((res: any[]) => {
      this.department.designations = res.map((item: any) => ({
        name: item.name,
        employeeInfo: item.employeeInfo
      }));
    });
  }

  getSectionByDepartment(departmentId: number, upperSectionId: any){
    this.organogramService.getSectionByDepartmentId(departmentId, upperSectionId).subscribe(res=>{
     console.log(res);
      this.department.sections = res.map((item: any)=>({        
        name: item.sectionName,
        sectionId : item.sectionId,
        departmentId : item.departmentId
      }))
    })
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
