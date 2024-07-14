import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AttendanceRecordService } from '../services/attendance-record-service.service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { DataSource } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import {UpdateAttendanceModel} from "../models/update-attendance-model"


@Component({
  selector: 'app-attendance-record',
  templateUrl: './attendance-record.component.html',
  styleUrl: './attendance-record.component.scss'
})
export class AttendanceRecordComponent implements OnInit, OnDestroy, AfterViewInit {
  headerText = "Attendance Records"
  loading:Boolean=false;
  dataSource = new MatTableDataSource<any>();
  OfficeOption:any[]=[];
  DepartmentOption:any[]=[];
  ShiftOption:any[]=[]
  AtdStatusOption:any[]=[];
  selectedDepartment:number|null;
  selectedOffice:number|null;
  selectedShift:number|null;
  selectedUpateShift: any|null;
  selectedEmp:any|null;
  displayedColumns = ["attendanceId","empId","empFirstName","empLastName","inTime","outTime","dayTypeName","attendanceStatusName","Action"]
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;

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
    this.selectedEmp = {"id":null,name:"No Employee Selected"};
    this.selectedUpateShift = {"id":null,name:"No Shift Selected"};
  }

  ngOnInit(): void {
    this.getAllAttendance(); 

    this.AtdRecordService.getAttendanceStatusOption().subscribe(option=> {
      this.AtdStatusOption = option;
    });
  }

  ngOnDestroy(): void {
    
  }

  ngAfterViewInit(): void {
    
  }

  getAllAttendance() {
    this.AtdRecordService.getAll().subscribe(item=> {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
    })
  }

    delete(element: any){
      this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.AtdRecordService.delete(element.attendanceId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }

  update(id:number) {
    console.log(id);
  }


}
