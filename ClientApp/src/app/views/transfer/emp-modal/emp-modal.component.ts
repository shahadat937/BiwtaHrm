import { Subscription } from 'rxjs';
import { Employee } from './../../basic-setup/model/employees';

import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { PostingComponent } from '../posting/posting.component';
import { EmployeesService } from '../service/employees.service';
import { EmployeesModule } from '../../employee/model/employees.module';
import { EmployeeService } from '../../employee/service/employee.service';


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
  displayedColumns: string[] = ['Action','empId', 'empEngName', 'department', 'designation'];
  dataSource = new MatTableDataSource<EmployeesModule>(); // Correct the type of MatTableDataSource

  subscription: Subscription = new Subscription();
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  //OutPut Decorator
  @Output() employeeSelected = new EventEmitter<EmployeesModule>();
  //OutPut Decorator

  matSort!: MatSort;
  employee: EmployeesModule[]=[];
  constructor(
    //private employeeservice:EmployeesService,
    public bsModalRef: BsModalRef, 
    private dialog: MatDialog,
    private router: Router,
    private route: ActivatedRoute,
    public employeeService:EmployeeService,


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
    this.dataSource.data = this.employee;
    this.getEmployeeDetails();
  }
  
  confirm() {
    this.bsModalRef.hide();
  }
  decline() {
    this.bsModalRef.hide();
  }
  //OutPut Decorator
  selectEmployee(employee: EmployeesModule) {
    this.employeeSelected.emit(employee);
    this.bsModalRef.hide();
  }

 getEmployeeDetails() {
    this.subscription = this.employeeService.getAll().subscribe((data) => {
      this.employee = data;
      console.log(data)
      this.dataSource = new MatTableDataSource(data);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  // employees: Employee[] = [
  //   { empId: 1, employeeName: 'John', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 2, employeeName: 'Ariful', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 3, employeeName: 'shoriful', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 4, employeeName: 'rasel', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 5, employeeName: 'jubair', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 6, employeeName: 'jannatul', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 7, employeeName: 'naima', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 8, employeeName: 'shishir', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 9, employeeName: 'diba', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 10, employeeName: 'rakib', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 11, employeeName: 'pavel', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 12, employeeName: 'sujon', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 13, employeeName: 'isrita', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 14, employeeName: 'wamia', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 15, employeeName: 'dina appu', department: 'Doe', designation: 'john@example.com' },
  //   { empId: 16, employeeName: 'hasan bhai', department: 'Doe', designation: 'john@example.com' },
  //   // Add more employees here if needed
  // ];
}
