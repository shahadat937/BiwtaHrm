import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrl: './personal-information.component.scss'
})
export class PersonalInformationComponent {
    constructor(public bsModalRef: BsModalRef){}
  confirm() {
    this.bsModalRef.hide();
  }

  decline() {
    this.bsModalRef.hide();
  }
}
