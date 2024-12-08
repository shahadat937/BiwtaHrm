import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { IconSetService } from '@coreui/icons-angular';
import { iconSubset } from './icons/icon-subset';
import { Title } from '@angular/platform-browser';
import { SiteSettingService } from './views/featureManagement/service/site-setting.service';
import { EmpPhotoSignService } from './views/employee/service/emp-photo-sign.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  template: '<router-outlet></router-outlet>',
})
export class AppComponent implements OnInit, OnDestroy {
  
  siteLogo: string = '';
  title = '';
  subscription: Subscription[]=[];

  constructor(
    private router: Router,
    private titleService: Title,
    private iconSetService: IconSetService,
    public siteSettingService: SiteSettingService,
    public empPhotoSignService: EmpPhotoSignService,
  ) {
    
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  ngOnInit(): void {
    this.getSiteSetting();
    this.subscription.push(
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
    })
    )
    
      // this.titleService.setTitle(this.title);
      // // iconSet singleton
      this.iconSetService.icons = { ...iconSubset };
  }

  getSiteSetting(){
    this.subscription.push(
    this.siteSettingService.getActive().subscribe((item) => {
      // this.title = item.siteTitle;
      this.titleService.setTitle(item.siteTitle);
      this.siteLogo = this.empPhotoSignService.imageUrl + 'TempleteImage/' + item.siteLogo;
      this.updateFavicon(this.siteLogo);
      // iconSet singleton
      // this.iconSetService.icons = { ...iconSubset };
    })
    )
    
  }
  updateFavicon(logoUrl: string) {
    const link: HTMLLinkElement | null = document.querySelector("link[rel*='icon']");
    
    if (link) {
      link.href = logoUrl; // Update the href attribute with the new logo URL
    } else {
      // If the link element doesn't exist, create it
      const newLink: HTMLLinkElement = document.createElement('link');
      newLink.rel = 'icon';
      newLink.href = logoUrl;
      document.head.appendChild(newLink);
    }
  }

}
