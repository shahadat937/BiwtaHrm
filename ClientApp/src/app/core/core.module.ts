import { ConfirmService } from './service/confirm.service';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RightSidebarService } from './service/rightsidebar.service';
import { AuthGuard } from './guard/auth.guard';
import { AuthService } from './service/auth.service';
import { DynamicScriptLoaderService } from './service/dynamic-script-loader.service';
import { SharedCustomModule } from '../shared/shared.module';

@NgModule({
  declarations: [],
  imports: [CommonModule,SharedCustomModule],
  providers: [
    RightSidebarService,
    AuthGuard,
    AuthService,
    DynamicScriptLoaderService,
    ConfirmService
   
  ],
})
export class CoreModule {
  constructor() {
 
  }
}
