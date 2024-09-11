import { Component, Input } from '@angular/core';
import { OrganogramDepartmentNameDto } from '../../service/organogram.service';

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
}
