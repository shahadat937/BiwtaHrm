import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Subscription } from 'rxjs';
import { EmployeeInformationComponent } from '../../manage-employee/employee-information/employee-information.component';
import { EmpShiftAssignService } from '../../service/emp-shift-assign.service';
import { UpdateEmpShiftComponent } from '../update-emp-shift/update-emp-shift.component';

@Component({
  selector: 'app-emp-shift-list',
  templateUrl: './emp-shift-list.component.html',
  styleUrl: './emp-shift-list.component.scss'
})
export class EmpShiftListComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo', 
    'idNo', 
    'fullName', 
    'department', 
    'designation',
    'shift',
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
    public empShiftAssignService: EmpShiftAssignService,
    private modalService: BsModalService,
  ) {
  }


  ngOnInit(): void {
    this.getAllEmpShiftAssignedList();
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


  getAllEmpShiftAssignedList(){
    this.subscription = this.empShiftAssignService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  updateEmpShift(id: number){
    const initialState = {
      id: id,
    };
    const modalRef: BsModalRef = this.modalService.show(UpdateEmpShiftComponent, { initialState, backdrop: 'static' });
    
    if (modalRef.onHide) {
      modalRef.onHide.subscribe(() => {
        this.getAllEmpShiftAssignedList();
      });
    }
  }

}
