import { Component } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BasicInformationComponent } from '../employee-informations/basic-information/basic-information.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PersonalInformationComponent } from '../employee-informations/personal-information/personal-information.component';
import { config } from 'rxjs';

@Component({
  selector: 'app-view-information-list',
  templateUrl: './view-information-list.component.html',
  styleUrl: './view-information-list.component.scss'
})
export class ViewInformationListComponent {
  bsModelRef!: BsModalRef;
  constructor(public dialog: MatDialog, private modalService: BsModalService) { }
  openDialog(){
    const dialogRef = this.dialog.open(BasicInformationComponent, { disableClose: true, minWidth: '40vw' });
  }
  openModal(){
    this.bsModelRef = this.modalService.show(PersonalInformationComponent);
  }
}
