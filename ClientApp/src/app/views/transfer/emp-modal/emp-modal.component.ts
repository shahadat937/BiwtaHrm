import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { PostingComponent } from '../posting/posting.component';
import { Employee, EmployeesService } from '../service/employees.service';


@Component({
  selector: 'app-emp-modal',
  templateUrl: './emp-modal.component.html',
  styleUrl: './emp-modal.component.scss'
})
export class EmpModalComponent implements OnInit, AfterViewInit{
[x: string]: any;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
//  @ViewChild('WardForm', { static: true }) WardForm!: NgForm;
  //subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['Action','employeePMSNo', 'employeeName', 'department', 'designation'];
  dataSource = new MatTableDataSource<Employee>(); // Correct the type of MatTableDataSource
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @Output() employeeSelected = new EventEmitter<Employee>();
  constructor(
    //private employeeservice:EmployeesService,
    public bsModalRef: BsModalRef, 
    private dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute,


  ) {

    this.route.paramMap.subscribe((params) => {
      const id = params.get('Id');
      if (id) {
        this.btnText = 'Id';
      } 
    });
  }


  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  selectRow(employee: Employee) {
    // Logic for selecting a row
    console.log('Selected employee:', employee);
  }
  ngOnInit(): void {
    this.dataSource.data = this.employees;
  }
  
  confirm() {
    this.bsModalRef.hide();
  }
  decline() {
    this.bsModalRef.hide();
  }
  selectEmployee(employee: Employee) {
    this.employeeSelected.emit(employee);
    console.log(employee)
  }


  employees: Employee[] = [
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'Rakib', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'Sujon', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'Saje', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'Pavel', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'Rahim', department: 'Doe', designation: 'john@example.com' },
    { employeePMSNo: 1, employeeName: 'Ariful', department: 'IT', designation: 'john@example.com' },



    // Add more employees here if needed
  ];
}
// export interface Employee {
//   employeePMSNo: number;
//   employeeName: string;
//   department: string;
//   designation: string;
// }