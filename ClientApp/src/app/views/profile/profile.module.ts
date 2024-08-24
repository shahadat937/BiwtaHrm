import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {
  AvatarModule,
  ButtonGroupModule,
  ButtonModule,
  CardModule,
  CollapseDirective,
  FormModule,
  GridModule,
  NavModule,
  ProgressModule,
  SharedModule,
  SpinnerModule,
  TableModule,
  TabsModule
} from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { ChartjsModule } from '@coreui/angular-chartjs';
import { ProfileRoutingModule } from './profile-routing.module';
import { ChangeProfileComponent } from './change-profile/change-profile.component';
import { BrowserModule } from '@angular/platform-browser';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { MatIconModule } from '@angular/material/icon';


@NgModule({
  imports: [
    ProfileRoutingModule,
    CardModule,
    NavModule,
    IconModule,
    TabsModule,
    CommonModule,
    GridModule,
    ProgressModule,
    ReactiveFormsModule,
    ButtonModule,
    FormModule,
    ButtonModule,
    ButtonGroupModule,
    ChartjsModule,
    AvatarModule,
    TableModule,
    CollapseDirective,
    FormsModule,
    BrowserModule,
    SharedCustomModule,
    SpinnerModule,
    SharedModule,
    MatIconModule,
  ],
  declarations: [ChangeProfileComponent]
})
export class ProfileModule {
}
