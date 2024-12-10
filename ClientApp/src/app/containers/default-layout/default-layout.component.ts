import { Component, OnDestroy, OnInit } from '@angular/core';

import { navItems } from './_nav';
import { EmpPhotoSignService } from 'src/app/views/employee/service/emp-photo-sign.service';
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { ModuleFeatureByRole } from 'src/app/views/featureManagement/model/module-feature-by-role';
import { NavbarSettingService } from 'src/app/views/featureManagement/service/navbar-setting.service';
import { NavbarSetting } from 'src/app/views/featureManagement/model/navbar-setting';
import { NavbarThemService } from 'src/app/views/featureManagement/service/navbar-them.service';
import { NavbarThem } from 'src/app/views/featureManagement/model/navbar-them';
import { Subscription } from 'rxjs';
import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent implements OnInit, OnDestroy{

  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta.png`;
  logo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta-logo.png`;

  navItems: ModuleFeatureByRole[] = [];
  roleName: string = '';

  navbarSetting: NavbarSetting = new NavbarSetting();
  navbarThem: NavbarThem = new NavbarThem();
  templatePath: string = `${this.empPhotoSignService.imageUrl}TempleteImage/`;
  subscription: Subscription[]=[];

  constructor(
    public empPhotoSignService: EmpPhotoSignService,
    public roleFeatureService: RoleFeatureService,
    public navbarSettingService: NavbarSettingService,
    public navbarThemService: NavbarThemService) {}
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }


  ngOnInit(): void {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    // this.roleName = currentUserJSON.role;
    this.getDecryptedUser();
    this.getActiveNavbarSetting();
  }

  getMenuList(){
    this.subscription.push(
    this.roleFeatureService.getModuleFeaturesByRole(this.roleName).subscribe((item) => {
      this.navItems = item;
    })
    )
    
  }

  getDecryptedUser() {
    const encryptedUser = localStorage.getItem('encryptedUser');
    if (encryptedUser) {
      const bytes = CryptoJS.AES.decrypt(encryptedUser, 'secret-key');
      // console.log(JSON.parse(bytes.toString(CryptoJS.enc.Utf8)));
      this.roleName = JSON.parse(bytes.toString(CryptoJS.enc.Utf8)).role;
    }
    this.getMenuList();
  }

  getActiveNavbarSetting(){
    this.subscription.push(
      this.navbarSettingService.getActive().subscribe((item) => {
      if(item){
        this.navbarSetting = item;
        this.navbarSetting.navbarLogo = this.templatePath + item.navbarLogo;
        this.navbarSetting.brandLogo = this.templatePath + item.brandLogo;
        if(item.themId){
          this.getThemInformation(item.themId);
        }
      }
    })
    )
    
  }

  getThemInformation(id: number){
    this.subscription.push(
    this.navbarThemService.find(id).subscribe((res) => {
      if(res){
        this.navbarThem = res;

        const css = `
        .sidebar {
          --cui-sidebar-bg: ${res['bgColor']};
          --cui-sidebar-brand-bg: ${res['brandBg']};
          --cui-sidebar-toggler-bg: ${res['togglerBg']};
          --cui-sidebar-toggler-hover-bg: ${res['togglerHoverBg']};
          --cui-sidebar-nav-link-color: ${res['linkColor']};
          --cui-sidebar-nav-link-active-color: ${res['linkActiveColor']};
          --cui-sidebar-nav-link-active-bg: ${res['linkActiveBg']};
          --cui-sidebar-nav-link-hover-color: ${res['linkHoverColor']};
          --cui-sidebar-nav-link-hover-bg: ${res['linkHoverBg']};
          --cui-sidebar-nav-link-icon-color: ${res['linkIconColor']};
          --cui-sidebar-nav-link-hover-icon-color: ${res['linkIconHoverColor']};
          --cui-sidebar-nav-group-bg: ${res['groupBg']};
          --cui-sidebar-nav-group-toggle-show-color: ${res['groupToggleColor']};
        }
      `;

        const styleElement = document.createElement('style');
        styleElement.type = 'text/css';
        styleElement.innerHTML = css;
        document.head.appendChild(styleElement);
      }
    })
    )
    
  }
}
