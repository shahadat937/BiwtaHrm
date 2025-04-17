import { Component, Input } from '@angular/core';
import { OrganogramSectionNameDto, OrganogramService } from '../../service/organogram.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EmpProfileComponent } from '../../../../../../src/app/views/employee/manage-employee/emp-profile/emp-profile.component';

@Component({
  selector: 'app-organogram-section',
  templateUrl: './organogram-section.component.html',
  styleUrl: './organogram-section.component.scss'
})
export class OrganogramSectionComponent {
  @Input() section!: OrganogramSectionNameDto;
  isExpanded: boolean = false;
  isDesignationExpanded: boolean = false;
  isSectionExpanded: boolean = false;

  constructor(
    private modalService: BsModalService, public organogramService: OrganogramService,
  ) {
  }
  toggleExpand(): void {
    console.log(this.section)
    this.isExpanded = !this.isExpanded;
    this.deparmentDesignationSectionCountAvaialable(this.section.departmentId, this.section.sectionId);
    console.log(this.section.sectionId);
    
  }

  toggleDesignationExpand(): void {
    this.isDesignationExpanded = !this.isDesignationExpanded;
    this.getEmployeeWithDesignation(this.section.departmentId, this.section.sectionId);
  }
  
  toggleSectionExpand(): void {
    this.isSectionExpanded = !this.isSectionExpanded;
    this.getSubSection(this.section.departmentId, this.section.sectionId)
  }
  
  viewEmployeeProfile(id: number){
    const isModal = true;
    const initialState = {
      id: id,
      isModal: isModal
    };
    const modalRef: BsModalRef = this.modalService.show(EmpProfileComponent, { initialState});
  }

  deparmentDesignationSectionCountAvaialable(departmentId: number, sectionId: number) {
  
    this.organogramService.getDesiginationDepartmentSectionCount(departmentId, sectionId).subscribe(res => {
      console.log(res);
      this.section.designationCount = res.designationCount,
      this.section.subDepartmentCount = res.departmentCount,
      this.section.sectionCount = res.sectionCount
    });  
}

getEmployeeWithDesignation(departmentId: any, sectionId: any) { 
  this.organogramService.getEmployeeWithDesignation(departmentId, sectionId).subscribe((res: any[]) => {
    this.section.designations = res.map((item: any) => ({
      name: item.name,
      employeeInfo: item.employeeInfo
    }));
  });
}

getSubSection(departmentId: number, sectionId: any){
  this.organogramService.getSectionByDepartmentId(departmentId,sectionId).subscribe(res=>{
    this.section.subSections = res.map((item: any)=>({        
      name: item.sectionName,
      sectionId : item.sectionId,
      departmentId : item.departmentId
    }))
  })
}
}
