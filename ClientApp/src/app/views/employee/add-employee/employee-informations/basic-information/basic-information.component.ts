import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-basic-information',
  templateUrl: './basic-information.component.html',
  styleUrl: './basic-information.component.scss'
})
export class BasicInformationComponent implements OnInit {

  userId: any;

  constructor(public bsModalRef: BsModalRef){}

  ngOnInit(): void {
    console.log('User ID:', this.userId);
  }
  
  confirm() {
    this.bsModalRef.hide();
  }

  decline() {
    this.bsModalRef.hide();
  }
}
