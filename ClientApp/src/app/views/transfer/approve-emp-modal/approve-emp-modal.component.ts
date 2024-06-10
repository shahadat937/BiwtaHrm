import { EmployeeService } from './../../employee/service/employee.service';
import { AfterViewInit, Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { Employee } from '../../basic-setup/model/employees';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeesModule } from '../../employee/model/employees.module';

@Component({
  selector: 'app-approve-emp-modal',
  templateUrl: './approve-emp-modal.component.html',
  styleUrl: './approve-emp-modal.component.scss'
})
export class ApproveEmpModalComponent implements OnInit, AfterViewInit{
  [x: string]: any;
    position = 'top-end';
    visible = false;
    percentage = 0;
    btnText: string | undefined;
    // employees: Employee[]=[]
  //  @ViewChild('WardForm', { static: true }) WardForm!: NgForm;
    //subscription: Subscription = new Subscription();
    displayedColumns: string[] = ['Action','empId', 'employeeName', 'department', 'designation'];
    dataSource = new MatTableDataSource<Employee>(); // Correct the type of MatTableDataSource
    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;
    @Output() employeeApproved = new EventEmitter<Employee>();
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
   this.dataSource.data = this.employees;
    }
    
    confirm() {
      this.bsModalRef.hide();
    }
    decline() {
      this.bsModalRef.hide();
    }
    approveEmployee(employee: Employee) {
      this.employeeApproved.emit(employee);
      this.bsModalRef.hide();
    }

     employees: Employee[] = [
      { empId: 1, employeeName: 'Rakib', department: 'Doe', designation: 'john@example.com' },
      { empId: 2, employeeName: 'Ariful', department: 'Doe', designation: 'john@example.com' },
      { empId: 3, employeeName: 'shoriful', department: 'Doe', designation: 'john@example.com' },
      { empId: 4, employeeName: 'rasel', department: 'Doe', designation: 'john@example.com' },
      { empId: 5, employeeName: 'jubair', department: 'Doe', designation: 'john@example.com' },
      { empId: 6, employeeName: 'jannatul', department: 'Doe', designation: 'john@example.com' },
      { empId: 7, employeeName: 'naima', department: 'Doe', designation: 'john@example.com' },
      { empId: 8, employeeName: 'shishir', department: 'Doe', designation: 'john@example.com' },
      { empId: 9, employeeName: 'diba', department: 'Doe', designation: 'john@example.com' },
      { empId: 10, employeeName: 'rakib', department: 'Doe', designation: 'john@example.com' },
      { empId: 11, employeeName: 'pavel', department: 'Doe', designation: 'john@example.com' },
      { empId: 12, employeeName: 'sujon', department: 'Doe', designation: 'john@example.com' },
      { empId: 13, employeeName: 'isrita', department: 'Doe', designation: 'john@example.com' },
      { empId: 14, employeeName: 'wamia', department: 'Doe', designation: 'john@example.com' },
      { empId: 15, employeeName: 'dina appu', department: 'Doe', designation: 'john@example.com' },
      { empId: 16, employeeName: 'hasan bhai', department: 'Doe', designation: 'john@example.com' },
  
      // Add more employees here if needed
    ];
  }