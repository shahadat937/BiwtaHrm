import { Component, Input } from '@angular/core';
import { OrganogramSectionNameDto } from '../../service/organogram.service';

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

  toggleExpand(): void {
    this.isExpanded = !this.isExpanded;
  }

  toggleDesignationExpand(): void {
    this.isDesignationExpanded = !this.isDesignationExpanded;
  }
  
  toggleSectionExpand(): void {
    this.isSectionExpanded = !this.isSectionExpanded;
  }
}
