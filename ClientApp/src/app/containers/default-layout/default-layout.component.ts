import { Component, OnInit } from '@angular/core';

import { navItems } from './_nav';
import { EmpPhotoSignService } from 'src/app/views/employee/service/emp-photo-sign.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent {

  biwtaLogo : string = `${this.empPhotoSignService.imageUrl}TempleteImage/biwta.png`;

  public navItems = navItems;

  constructor(
    public empPhotoSignService: EmpPhotoSignService,) {}
}
