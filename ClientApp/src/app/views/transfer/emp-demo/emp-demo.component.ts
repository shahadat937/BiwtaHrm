import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { Component, OnInit } from '@angular/core';
import { SharedModule } from '@coreui/angular';
import { BsModalRef } from 'ngx-bootstrap/modal';
@Component({
  selector: 'app-emp-demo',
  templateUrl: './emp-demo.component.html',
  styleUrl: './emp-demo.component.scss'
  
})
export class EmpDemoComponent implements OnInit{
  loading = false;
  title!: string;
  message!: string;
  btnOkText!: string;
  btnCancelText!: string;
  result!: boolean;
  displayedColumns: string[] = ['slNo', 'firstName','Action'];
  dataSource = new MatTableDataSource<any>();
  constructor(public bsModalRef: BsModalRef,private dialog: MatDialog) { 

  }
  openDialog(firstName: string) {
    const dialogRef = this.dialog.open(EmpDemoComponent, {
      width: '250px',
      data: { firstName: firstName }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('The dialog was closed');
    });
  }
  ngOnInit(): void {
    
  }
  public visible = false;


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