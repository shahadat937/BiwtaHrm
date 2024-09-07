import { AfterViewInit, Component, Injectable, OnDestroy, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { OrganogramDepartmentNameDto, OrganogramOfficeNameDto, OrganogramService } from '../service/organogram.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-organogram',
  templateUrl: './organogram.component.html',
  styleUrl: './organogram.component.scss'
})
export class OrganogramComponent implements OnInit, OnDestroy  {

  subscription: Subscription = new Subscription();
  organograms:any[] = [];
  offices: OrganogramOfficeNameDto[] = [];
  departments: OrganogramDepartmentNameDto[] = [];
  expandedOffices: { [key: string]: boolean } = {};
  expandedDesignations: { [key: string]: boolean } = {};
  expandedDepartments: { [key: string]: boolean } = {};
  expandedSections: { [key: string]: boolean } = {};

  constructor(
    public organogramService: OrganogramService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
  ) {
  }
  ngOnInit(): void {
    this.getOrganogram();
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  getOrganogram(){
    this.subscription=this.organogramService.getOrganogramNamesOnly().subscribe((data: OrganogramDepartmentNameDto[]) => { 
      this.organograms = data;
      this.departments = data;
    });
  }
  
  toggleOfficeExpand(officeName: string): void {
    this.expandedOffices[officeName] = !this.expandedOffices[officeName];
  }

  isOfficeExpanded(officeName: string): boolean {
    return this.expandedOffices[officeName];
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

  isDepartmentExpanded(officeName: string): boolean {
    return this.expandedDepartments[officeName];
  }
  

  toggleSectionExpand(officeName: string): void {
    this.expandedSections[officeName] = !this.expandedSections[officeName];
  }

  isSectionExpanded(officeName: string): boolean {
    return this.expandedSections[officeName];
  }
  
}

