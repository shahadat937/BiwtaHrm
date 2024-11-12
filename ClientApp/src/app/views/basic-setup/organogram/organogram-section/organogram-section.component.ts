import { Component, Input } from '@angular/core';
import { OrganogramSectionNameDto } from '../../service/organogram.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { EmpProfileComponent } from 'src/app/views/employee/manage-employee/emp-profile/emp-profile.component';

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
    private modalService: BsModalService,
  ) {
  }
  toggleExpand(): void {
    this.isExpanded = !this.isExpanded;
  }

  toggleDesignationExpand(): void {
    this.isDesignationExpanded = !this.isDesignationExpanded;
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
