import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AttendanceRecordService } from '../services/attendance-record-service.service';
import { Router } from '@angular/router';
import { Subscription, timeInterval } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { DataSource } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import {UpdateAttendanceModel} from "../models/update-attendance-model"
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-attendance-record',
  templateUrl: './attendance-record.component.html',
  styleUrl: './attendance-record.component.scss'
})
export class AttendanceRecordComponent implements OnInit, OnDestroy, AfterViewInit {
  headerText = "Attendance Records"
  loading:Boolean=false;
  @ViewChild('updateAtdForm', { static: true }) updateAtdForm!: NgForm;
  dataSource = new MatTableDataSource<any>();
  OfficeOption:any[]=[];
  DepartmentOption:any[]=[];
  ShiftOption:any[]=[]
  AtdStatusOption:any[]=[];
  selectedDepartment:number|null;
  subscription: Subscription = new Subscription();
  selectedOffice:number|null;
  selectedShift:number|null;
  selectedUpdateShift: any|null;
  selectedEmp:any|null;
  displayedColumns = ["attendanceId","idCardNo","fullName","attendanceDate","inTime","outTime","dayTypeName","attendanceStatusName","Action"]
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  updateWindowVisible:boolean = false;

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
    this.selectedUpdateShift = {"id":null,name:"No Shift Selected"};
  }

  ngOnInit(): void {
    this.getAllAttendance(); 

    this.AtdRecordService.getAttendanceStatusOption().subscribe(option=> {
      this.AtdStatusOption = option;
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  ngAfterViewInit(): void {
    
  }

  getAllAttendance() {
    this.subscription = this.AtdRecordService.getAll().subscribe(item=> {
      this.dataSource = new MatTableDataSource(item.map(x=>({
        ...x,
        fullName: `${x.empFirstName} ${x.empLastName}`
      })));
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

  update(element:any) {
    this.subscription = this.AtdRecordService.getAttendanceById(element.attendanceId).subscribe(res=> {
      this.AtdRecordService.UpdateAtdModel.attendanceId = res.attendanceId;
      this.AtdRecordService.UpdateAtdModel.attendanceDate = res.attendanceDate;
      this.AtdRecordService.UpdateAtdModel.empId = res.empId;
      this.AtdRecordService.UpdateAtdModel.shiftId = res.shiftId;
      this.AtdRecordService.UpdateAtdModel.inTime = res.inTime;
      this.AtdRecordService.UpdateAtdModel.outTime = res.outTime;
      this.AtdRecordService.UpdateAtdModel.attendanceStatusId = res.attendanceStatusId;
      this.AtdRecordService.UpdateAtdModel.done = res.done;
      this.AtdRecordService.UpdateAtdModel.remark = res.remark;
    });

    this.selectedEmp = {id:this.AtdRecordService.UpdateAtdModel.empId, name: element.empFirstName+" "+element.empLastName};
    console.log(element);

    this.selectedUpdateShift = {id:element.shiftId, name:element.shiftName};
    //this.updateAtdForm?.form.patchValue(this.AtdRecordService.UpdateAtdModel);
    this.updateWindowVisible = true;
    
  }


  onSubmit() {
    this.loading = true;
    this.AtdRecordService.UpdateAtdModel.inTime = this.fixedTimeFormat(this.AtdRecordService.UpdateAtdModel.inTime)
    this.AtdRecordService.UpdateAtdModel.outTime = this.fixedTimeFormat(this.AtdRecordService.UpdateAtdModel.outTime);

    this.AtdRecordService.update(this.AtdRecordService.UpdateAtdModel).subscribe((response:any)=> {
      if(response.success) {
        this.AtdRecordService.cachedData = [];
        this.getAllAttendance();
        this.toastr.success('',`${response.message}`, {
          messageClass:"toast-top-right"
        });

        this.onCollapse();
      } else {
        this.toastr.error('',`${response.message}`, {
          messageClass: "toast-top-right"
        });
      }
      this.loading = false;
    }, error=> {
      this.loading = false;
      console.log(error);
      console.log(typeof(error));
    });
  }

  onCollapse() {
    this.loading = false;
    this.updateWindowVisible=false;
    this.AtdRecordService.UpdateAtdModel = new UpdateAttendanceModel();
    this.selectedEmp = {id:null,name:"No Name Selected"};
    this.selectedUpdateShift = {id:null,name:"No Shift Selected"};
  }

  fixedTimeFormat(data:string|null) {
    if(data==null||data=="") {
      return "";
    }

    const dataList = data.split(':');
    if(dataList.length>=3) {
      return data;
    }

    let fixedFormat = data+":00";
    return fixedFormat;
  }

}
