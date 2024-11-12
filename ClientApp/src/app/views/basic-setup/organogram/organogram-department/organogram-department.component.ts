import { Component, Input } from '@angular/core';
import { OrganogramDepartmentNameDto } from '../../service/organogram.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EmpProfileComponent } from 'src/app/views/employee/manage-employee/emp-profile/emp-profile.component';

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
    private modalService: BsModalService,
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

  viewEmployeeProfile(id: number){
    const isModal = true;
    const initialState = {
      id: id,
      isModal: isModal
    };
    const modalRef: BsModalRef = this.modalService.show(EmpProfileComponent, { initialState});
  }
}
