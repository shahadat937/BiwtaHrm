import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.sass']
})
export class ConfirmDialogComponent implements OnInit {
  loading = false;
  title!: string;
  message!: string;
  btnOkText!: string;
  btnCancelText!: string;
  result!: boolean;

  constructor(public bsModalRef: BsModalRef) { 

  }

  ngOnInit(): void {
  }

  confirm() {
    this.loading = true;
    this.result = true;
    this.bsModalRef.hide();
  }

  decline() {
    this.result = false;
    this.bsModalRef.hide();
  }

}

