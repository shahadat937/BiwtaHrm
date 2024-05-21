import { Component, Input } from '@angular/core';
import { OrganogramDepartmentNameDto } from '../../service/organogram.service';


@Component({
  selector: 'app-organogram-department',
  templateUrl: './organogram-department.component.html',
  styleUrl: './organogram-department.component.scss'
})
export class OrganogramDepartmentComponent {
  @Input() department!: OrganogramDepartmentNameDto;
  isExpanded: boolean = false;

  toggleExpand(): void {
    this.isExpanded = !this.isExpanded;
  }
}
