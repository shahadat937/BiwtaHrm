import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-basic-information',
  templateUrl: './basic-information.component.html',
  styleUrl: './basic-information.component.scss'
})
export class BasicInformationComponent {
  constructor(public bsModalRef: BsModalRef){}
  confirm() {
    this.bsModalRef.hide();
  }

  decline() {
    this.bsModalRef.hide();
  }
}
