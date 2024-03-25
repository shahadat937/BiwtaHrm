import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ModalModule } from 'ngx-bootstrap/modal';
import { MaterialModule } from './material.module';
import { ButtonGroupModule, ButtonModule, CardModule, DropdownModule, GridModule, SharedModule, TabsModule } from '@coreui/angular';
import { IconModule } from '@coreui/icons-angular';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    NgbModule,
    NgxSpinnerModule,
    ModalModule.forRoot()
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,
    NgbModule,
    NgxSpinnerModule,
    MaterialModule,
    ButtonModule,
    IconModule,
    ButtonGroupModule,
    TabsModule,
    CardModule,
    SharedModule,
    GridModule,
    DropdownModule,
  ],
})
export class SharedCustomModule {}
