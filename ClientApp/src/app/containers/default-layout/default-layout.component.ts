import { Component, OnInit } from '@angular/core';

import { navItems } from './_nav';
import { EmpPhotoSignService } from 'src/app/views/employee/service/emp-photo-sign.service';
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { ModuleFeatureByRole } from 'src/app/views/featureManagement/model/module-feature-by-role';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent implements OnInit{

  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta.png`;
  logo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;

  navItems: ModuleFeatureByRole[] = [];
  roleName: string = '';

  constructor(
    public empPhotoSignService: EmpPhotoSignService,
    public roleFeatureService: RoleFeatureService,) {}


  ngOnInit(): void {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.roleName = currentUserJSON.role;
    this.getMenuList();
  }

  getMenuList(){
    this.roleFeatureService.getModuleFeaturesByRole(this.roleName).subscribe((item) => {
      this.navItems = item;
    });
  }
}
