import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
 

@Component({
  selector: 'app-AccountType',
  templateUrl: './AccountType-list.component.html',
  styleUrls: ['./AccountType-list.component.sass']
})
export class AccountTypeListComponent implements OnInit {


  isLoading = false;
  

  searchText="";

  displayedColumns: string[] = [ 'ser', 'accoutType', 'isActive', 'actions'];

  
  constructor(private snackBar: MatSnackBar) { }
  
  ngOnInit() {
    
  }
 



  
}
