import { Component, OnInit, ViewChild,ElementRef } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { BloodGroupService } from '../../service/BloodGroup.service';
import { SelectionModel } from '@angular/cdk/collections';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BloodGroup } from '../../model/BloodGroup';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
 

@Component({
  selector: 'app-BloodGroup',
  templateUrl: './bloodgroup-list.component.html',
  styleUrls: ['./bloodgroup-list.component.sass']
})
export class BloodGroupListComponent implements OnInit {
  @ViewChild(MatSort, { static: true })
  sort!: MatSort;
  @ViewChild(MatPaginator, { static: true })
  paginator!: MatPaginator;
  dataSource = new MatTableDataSource();
  loading = false;
  ELEMENT_DATA: BloodGroup[] = [];
  isLoading = false;
  

  searchText="";

  displayedColumns: string[] = [ 'ser', 'bloodGroupName', 'isActive', 'actions'];
  constructor(
    private snackBar: MatSnackBar,
    private BloodGroupService: BloodGroupService,
    private router: Router
    ) { }
  
  ngOnInit() {
  this.getAll()
  }
 
  getAll(){ 
    this.BloodGroupService.getAll().subscribe(item=>{
    //  console.log(item)
    //  this.dataSource=new MatTableDataSource(item);
      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
    });
   
  }



 
}
