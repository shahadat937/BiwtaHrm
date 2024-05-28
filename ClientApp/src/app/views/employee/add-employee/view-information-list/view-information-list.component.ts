import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PersonalInformationComponent } from '../employee-informations/personal-information/personal-information.component';
import { config } from 'rxjs';
import { BasicInformationComponent } from '../employee-informations/basic-information/basic-information.component';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-view-information-list',
  templateUrl: './view-information-list.component.html',
  styleUrl: './view-information-list.component.scss'
})
export class ViewInformationListComponent implements OnInit {

  gettingStatus : boolean = false;
  bsModelRef!: BsModalRef;
  userId : any;
  empId : any;

  constructor(public dialog: MatDialog,
    private modalService: BsModalService,
    private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      this.userId = params.get('id');
    });
  }



  openBasicInfoModal(){
    const initialState = {
      userId: this.userId
    };

    this.bsModelRef = this.modalService.show(BasicInformationComponent, {
      initialState,
      ignoreBackdropClick: true
    });
  }
  openModal(){
    this.bsModelRef = this.modalService.show(PersonalInformationComponent);
  }
}
