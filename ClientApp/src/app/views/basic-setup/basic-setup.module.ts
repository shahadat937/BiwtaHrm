import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BasicSetupRoutingModule } from './basic-setup-routing.module';
import { NewAccountTypeComponent } from './accounttype/new-accounttype/new-accounttype.component';


@NgModule({
  declarations: [NewAccountTypeComponent],
  imports: [
    CommonModule,
    BasicSetupRoutingModule
  ]
})
export class BasicSetupModule { }
