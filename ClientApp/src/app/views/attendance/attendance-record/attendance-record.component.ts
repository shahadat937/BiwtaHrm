import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { AttendanceRecordService } from '../services/attendance-record-service.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { DataSource } from '@angular/cdk/collections';


@Component({
  selector: 'app-attendance-record',
  templateUrl: './attendance-record.component.html',
  styleUrl: './attendance-record.component.scss'
})
export class AttendanceRecordComponent implements OnInit, OnDestroy, AfterViewInit {
  loading:Boolean=false;
  dataSource = new MatTableDataSource<any>();
  OfficeOption:any[]=[];
  DepartmentOption:any[]=[];
  ShiftOption:any[]=[]
  selectedDepartment:number|null;
  selectedOffice:number|null;
  selectedShift:number|null;
  displayedColumns = ["attendanceId","empId","empFirstName","empLastName","inTime","outTime","dayTypeName","attendanceStatusName","Action"]

  constructor(
    public AtdRecordService: AttendanceRecordService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.selectedOffice=null;
    this.selectedDepartment=null;
    this.selectedShift=null;
  }

  ngOnInit(): void {
    this.getAllAttendance(); 
  }

  ngOnDestroy(): void {
    
  }

  ngAfterViewInit(): void {
    
  }

  getAllAttendance() {
    this.AtdRecordService.getAll().subscribe(item=> {
      this.dataSource = new MatTableDataSource(item);
    })
  }

}
