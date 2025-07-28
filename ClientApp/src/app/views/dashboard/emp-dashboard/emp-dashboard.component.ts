import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/service/auth.service';
import { AttendanceReportService } from '../../attendance/services/attendance-report.service';
import { HttpParams } from '@angular/common/http';

@Component({
  selector: 'app-emp-dashboard',
  templateUrl: './emp-dashboard.component.html',
  styleUrl: './emp-dashboard.component.scss'
})
export class EmpDashboardComponent implements OnInit, OnDestroy {
  
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]

  fromDate = new Date();
  toDate = new Date();
  empId: any;
  PresentText:any="N/A";
  AbsentText: any ="N/A";
  LateText:any = "N/A";
  WorkingDayText: any = "N/A";
  SiteVisitText: any = "N/A";
  OnLeaveText: any = "N/A";
  monthName: any;
  yearName: any;

  constructor(
    private attendanceReportService: AttendanceReportService,
    private authService: AuthService,){
  }

  ngOnInit(): void {
    this.subscription.push(
    this.authService.currentUser.subscribe(data => {
      if(data==null) {
        return;
      }
      if(data.empId!=null) {
        this.empId = parseInt(data.empId);
      }
    })
    )
     

    const startOfMonth = new Date(this.toDate.getFullYear(), this.toDate.getMonth(), 1);
    this.fromDate = startOfMonth;
    this.yearName = this.toDate.getFullYear();
    this.monthName = this.toDate.toLocaleString('default', { month: 'long' });
    this.getEmpAttendanceInfo();
  }

  ngOnDestroy(): void {
    if(this.subscription!=null) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  hrmdateResize(formDateValue: any) {
    if (!formDateValue) return "";

    const dateObj = new Date(formDateValue);

    const year = dateObj.getFullYear();
    const month = (dateObj.getMonth() + 1).toString().padStart(2, '0');
    const day = dateObj.getDate().toString().padStart(2, '0');

    return `${year}-${month}-${day}`;
  }


  getEmpAttendanceInfo(){
    let filter = new HttpParams();
    filter = filter.set("EmpId",this.empId);
    filter = filter.set("From", this.hrmdateResize(this.fromDate));
    filter = filter.set("To", this.hrmdateResize(this.toDate));

    this.subscription.push(
    this.attendanceReportService.getEmpSummary(filter).subscribe(response=> {
      this.PresentText = response.totalPresent;
      this.AbsentText = response.totalAbsent;
      this.LateText = response.totalLate;
      this.WorkingDayText = response.totalWorkingDay;
      this.SiteVisitText = response.totalSiteVisit;
      this.OnLeaveText = response.totalOnLeave;
    })
    )
    
  }

}
