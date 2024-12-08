import { Component, OnDestroy, OnInit } from '@angular/core';
import { FooterComponent } from '@coreui/angular';
import { Subscription } from 'rxjs';
import { EmpPhotoSignService } from 'src/app/views/employee/service/emp-photo-sign.service';
import { SiteSettingService } from 'src/app/views/featureManagement/service/site-setting.service';

@Component({
  selector: 'app-default-footer',
  templateUrl: './default-footer.component.html',
  styleUrls: ['./default-footer.component.scss'],
})
export class DefaultFooterComponent extends FooterComponent implements OnInit, OnDestroy {
  
  siteLogo: string = '';
  footerTitle: string = '';
  subscription: Subscription[]=[];

  constructor(
    public empPhotoSignService: EmpPhotoSignService,
    public siteSettingService: SiteSettingService,) {
    super();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  ngOnInit() {
    this.siteLogo = this.empPhotoSignService.imageUrl + 'TempleteImage/infinity_Logo.png';
    this.subscription.push(
    this.siteSettingService.getActive().subscribe((item) => {
      this.footerTitle = item.footerTitle;
    })
    )
    
  }
}
