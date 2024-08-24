import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { ManageEmployeeService } from '../../service/manage-employee.service';
import { cilZoom, cilPlus } from '@coreui/icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { EmployeeInformationComponent } from '../employee-information/employee-information.component';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.scss'
})
export class EmployeeListComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo', 
    'idNo', 
    'fullName', 
    'department', 
    'designation', 
    // 'fullNameBangla', 
    // 'email', 
    // 'isActive', 
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;

  constructor(
    public manageEmployeeService: ManageEmployeeService,
    private modalService: BsModalService,
  ) {
  }

  icons = { cilZoom, cilPlus };

  ngOnInit(): void {
    this.getAllEmpBasicInfo();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }


  getAllEmpBasicInfo(){
    this.subscription = this.manageEmployeeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  viewEmployeeInformation(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(EmployeeInformationComponent, { initialState, backdrop: 'static' });
  }

}
