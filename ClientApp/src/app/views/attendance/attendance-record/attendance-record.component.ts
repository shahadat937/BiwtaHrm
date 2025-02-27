import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { AttendanceRecordService } from '../services/attendance-record-service.service';
import { Router } from '@angular/router';
import { delay, map, of, Subscription, timeInterval } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { MatTableDataSource } from '@angular/material/table';
import { DataSource } from '@angular/cdk/collections';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import {UpdateAttendanceModel} from "../models/update-attendance-model"
import { NgForm } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { cilZoom } from '@coreui/icons';
import { DepartmentService } from '../../basic-setup/service/department.service';
import { SectionService } from '../../basic-setup/service/section.service';
import { HttpParams } from '@angular/common/http';
import { AuthService } from 'src/app/core/service/auth.service';
import { trigger } from '@angular/animations';
import { RealTimeService } from 'src/app/core/service/real-time.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { Feature } from '../../featureManagement/model/feature';


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
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  debounceSubs: Subscription = new Subscription();
  selectedOffice:number|null;
  selectedShift:number|null;
  selectedUpdateShift: any|null;
  selectedEmp:any|null;
  displayedColumns = ["fullName","attendanceDate","inTime","outTime","lateTime","dayTypeName","attendanceStatusName","Action"]
  @ViewChild('paginator')
  paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  updateWindowVisible:boolean = false;
  icons = {cilZoom}

  // Server Side Pagination
  selectedDate: Date | null;
  SectionOption: any[] = [];
  selectedSection: number|null;
  searchKeyword: string;
  pageSize: number;
  pageIndex: number;
  totalRecord: number;
  empId: number | null;
  isUser: boolean

  // Feature Permission
  featurePermission: FeaturePermission = new FeaturePermission();

  constructor(
    private roleFeatureService: RoleFeatureService,
    private realTimeService: RealTimeService,
    private authService: AuthService,
    private sectionService: SectionService,
    private departmentService: DepartmentService,
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
    this.selectedDate = null;
    this.searchKeyword = "";
    this.selectedSection = null;
    this.pageSize = 10;
    this.pageIndex = 0;
    this.totalRecord = 0;
    this.empId = null;
    this.isUser = false;

    this.authService.currentUser.subscribe(data => {
      if(data && data.empId!=null) {
        this.empId = parseInt(data.empId);
      }

      if(data && data.role.toString()!="Master Admin") {
        this.isUser = true;
      }
    })

  }

  ngOnInit(): void {
    this.getPermission();
    this.getFilteredAttendance(true);
    //this.getAllAttendance();
    this.getSelectedDepartment();

    this.AtdRecordService.getAttendanceStatusOption().subscribe(option=> {
      this.AtdStatusOption = option;
    });

    const subs = this.realTimeService.eventBus.getEvent('newAtd').subscribe(data => {
      
      this.getFilteredAttendance(false);
    })

    this.subscription.push(subs);
  }

  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('attendanceRecord').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        // To do
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
  }


  getSelectedDepartment() {
    this.subscription.push(
      this.departmentService.getSelectedAllDepartment().subscribe({
        next: response => {
          this.DepartmentOption = response;
        }
      })
    )
    
  }

  applyFilter(event: Event) {
    let val = (event.target as HTMLInputElement).value
    val = val.trim().toLowerCase();
    this.dataSource.filter=val;
  }

  ngOnDestroy(): void {
    this.subscription.forEach(subs=>subs.unsubscribe())
    
    if(this.debounceSubs) {
      this.debounceSubs.unsubscribe();
    }
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
    this.subscription.push(
      this.sort.sortChange.subscribe(() => {
      this.getFilteredAttendance(false);
    })
    )
   
  }

  logSort() {
    console.log(this.sort.active);
    console.log(this.sort.direction);
  }


  onPageChange(event:PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.getFilteredAttendance(false);
  }
  getFilteredAttendance(resetPage: boolean) {
    let params = new HttpParams();
    let pageSize = this.pageSize;
    let pageIndex = this.pageIndex+1;

    if(resetPage) {
      pageIndex = 1;
    }

    params = this.selectedDepartment==null?params:params.set('departmentId',this.selectedDepartment);
    params = this.selectedSection==null? params: params.set('sectionId', this.selectedSection);
    params = this.isUser && this.empId != null? params.set("empId", this.empId): params;

    
    if(this.sort!=undefined) {
      params = params.set('sortColumn',this.sort.active);
      params = params.set('sortDirection', this.sort.direction);
    }

    if(this.selectedDate!=null) {
      params = params.set('month',this.selectedDate.getMonth()+1);
      params = params.set('year', this.selectedDate.getFullYear());
    }

    params = params.set('pageSize',pageSize);
    params = params.set('pageIndex',pageIndex);

    params = this.searchKeyword.trim()==""?params:params.set('keyword',this.searchKeyword);

    this.subscription.push(
      this.AtdRecordService.getAttendance(params).subscribe({
        next: response => {
          this.dataSource =new MatTableDataSource (response.result.map(x=>({
            ...x,
            fullName: [x.empFirstName,x.empLastName].join(' ')
          })));
          this.totalRecord = response.totalCount;
          this.dataSource.sort = this.sort;
          this.pageIndex = pageIndex -1;
        }
      })
    )

   
  
  }


    delete(element: any){

      if(this.featurePermission.delete==false) {
        this.roleFeatureService.unauthorizeAccress();
        return;
      }

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

    if(this.featurePermission.update==false) {
      this.roleFeatureService.unauthorizeAccress();
      return;
    }

    this.subscription.push(
      this.AtdRecordService.getAttendanceById(element.attendanceId).subscribe(res=> {
      this.AtdRecordService.UpdateAtdModel.attendanceId = res.attendanceId;
      this.AtdRecordService.UpdateAtdModel.attendanceDate = res.attendanceDate;
      this.AtdRecordService.UpdateAtdModel.empId = res.empId;
      this.AtdRecordService.UpdateAtdModel.shiftId = res.shiftId;
      this.AtdRecordService.UpdateAtdModel.inTime = res.inTime;
      this.AtdRecordService.UpdateAtdModel.outTime = res.outTime;
      this.AtdRecordService.UpdateAtdModel.attendanceStatusId = res.attendanceStatusId;
      this.AtdRecordService.UpdateAtdModel.done = res.done;
      this.AtdRecordService.UpdateAtdModel.remark = res.remark;
    })
    )
   

    this.selectedEmp = {id:this.AtdRecordService.UpdateAtdModel.empId, name: element.empFirstName+" "+element.empLastName};

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
        this.getFilteredAttendance(false);
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

  onDepartmentChange() {
    if(this.selectedDepartment!=null) {
      this.subscription.push(
        this.sectionService.getSectionByOfficeDepartment(this.selectedDepartment).subscribe({
          next: response => {
            this.SectionOption = response;
          }
        })
      )
     
    }

    this.getFilteredAttendance(true);
  }

  onSectionChange() {
    this.getFilteredAttendance(true)
  }

  onSearch() {
    if(this.debounceSubs) {
      this.debounceSubs.unsubscribe();
    }

    const source$ = of (this.searchKeyword).pipe(
      delay(700)
    );

    this.debounceSubs =  source$.subscribe(data=> {
        this.getFilteredAttendance(true);
      })
    
  }

}
