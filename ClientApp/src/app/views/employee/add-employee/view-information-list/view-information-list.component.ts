import { Component } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PersonalInformationComponent } from '../employee-informations/personal-information/personal-information.component';
import { config } from 'rxjs';
import { BasicInformationComponent } from '../employee-informations/basic-information/basic-information.component';

@Component({
  selector: 'app-view-information-list',
  templateUrl: './view-information-list.component.html',
  styleUrl: './view-information-list.component.scss'
})
export class ViewInformationListComponent {

  box_background : string = 'bg-primary';
  bsModelRef!: BsModalRef;

  constructor(public dialog: MatDialog, private modalService: BsModalService) { }
  openBasicInfoModal(){
    this.bsModelRef = this.modalService.show(BasicInformationComponent);
  }
  openModal(){
    this.bsModelRef = this.modalService.show(PersonalInformationComponent);
  }
}
