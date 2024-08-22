import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PagesRoutingModule } from './pages-routing.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { Page404Component } from './page404/page404.component';
import { Page500Component } from './page500/page500.component';
import { ButtonModule, CardModule, FormModule, GridModule, SpinnerModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
import { SharedCustomModule } from 'src/app/shared/shared.module';
import { AuthService } from 'src/app/core/service/auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { MatIconModule } from '@angular/material/icon';
import { NgxParticlesModule } from '@tsparticles/angular';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    Page404Component,
    Page500Component,
  ],
  imports: [
    CommonModule,
    PagesRoutingModule,
    CardModule,
    ButtonModule,
    GridModule,
    SharedCustomModule,
    FormModule,
    IconModule,
    SpinnerModule,
    BrowserModule,
    ReactiveFormsModule,
    NgxParticlesModule 
  ],
  providers: [
    
    AuthService
  ],
})
export class PagesModule {
  
}
