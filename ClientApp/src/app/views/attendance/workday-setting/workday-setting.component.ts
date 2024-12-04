import { AfterContentInit, AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import {WorkdayService} from  "../services/workday.service"
import { cilTrash, cilPlus, cilX } from '@coreui/icons'
import { get } from 'lodash-es';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';
import { cilZoom } from '@coreui/icons';

@Component({
  selector: 'app-workday-setting',
  templateUrl: './workday-setting.component.html',
  styleUrl: './workday-setting.component.scss'
})
export class WorkdaySettingComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();
  icons = {cilZoom,cilTrash, cilPlus, cilX};
  YearOption: any[] = [];
  DayOption: any[] = [];
  Workday: any[] = [];
  weekendData: any[] = [];
  weekendColumns: any[] = [{header: "Date", field: "date"},{header:'Day', field:'dayName'}, {header: "Status", field: "isActive"}];
  selectedYear:number|null;
  selectedDay: number | null;
  selectedDayForAddition : number | null;
  showAddDay: boolean ;
  yearLoaded : boolean;
  filterForWeekend:string = "";
  loading: boolean = false;
  constructor(
    private workdayService: WorkdayService,
    private toastr : ToastrService,
    private confirmService: ConfirmService,
    private authService: AuthService,        
    public roleFeatureService: RoleFeatureService
  ) {
    this.selectedYear = 0;
    this.selectedDay = null;
    this.selectedDayForAddition = null;
    this.showAddDay = false;
    this.yearLoaded = false;
  }
  ngOnInit(): void {
    this.getYear();
    this.getDay();
  }


  ngOnDestroy(): void {
    if(this.subscription!=null) {
      this.subscription.unsubscribe();
    }
  }

  getYear() {
    this.subscription = this.workdayService.getYear().subscribe({
      next: (option)=> {
        this.YearOption = option;
        this.setDefaultYear();
        this.getWorkday();
        this.getWeekend();
        this.yearLoaded=true;
      },
      error: (err)=> {
        console.error("Got error while retriving Year Option for Selection");
        this.yearLoaded=true;
      }
    })

    this.yearLoaded=true;
  }

  getDay() {
    this.subscription = this.workdayService.getDay().subscribe({
      next: option=> {
        this.DayOption = option;
      },
      error: (err)=> {
        console.log("Got error while retriving Day Option for Selection");
      }
    })
  }


  getWeekend() {
    if(this.selectedYear==null) {
      return;
    }

    this.subscription = this.workdayService.getWeekend(this.selectedYear).subscribe({
      next: response=> {
        this.weekendData = response;
      }
    })
  }


  toggleWeekendStatus(weekend:any) {
    let empId : number = parseInt(this.authService.currentUserValue.empId);
    this.loading = true;
    if(weekend.isActive) {
      this.subscription = this.workdayService.addCancelledWeekend(weekend.date,empId).subscribe({
        next: response => {
          if(response.success) {
            this.toastr.success('',"Deactivated", {
              positionClass: 'toast-top-right'
            })
            weekend.isActive = false;
            weekend.cancelId = {id:response.id};
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right'
            })
          }
        },
        error: err => {
          this.loading = false;
        },
        complete: ()=> {
          this.loading = false;
        }
      })
    } else {
        this.subscription=this.workdayService.deleteCancelledWeekend(weekend.cancelId.id).subscribe({
          next: response => {
            if(response.success) {
              this.toastr.success('', "Activated", {
                positionClass: 'toast-top-right'
              })

              weekend.isActive = true;
              weekend.cancelId = null;
            } else {
              this.toastr.warning('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })
            }
          },
          error: err=> {
            this.loading = false;
          },

          complete: () => {
            this.loading = false;
          }
        })      
    }
  }

  getWorkday() {
    if(this.selectedYear==null) {
      return;
    }
    this.subscription = this.workdayService.getWorkday(this.selectedYear).subscribe({
      next: (option)=> {
        this.Workday = option;
      },
      error: (err)=> {
        console.log("Got error while retriving Workday");
      }
    })
  }

  setDefaultYear() {
    let curDate = new Date();
    let curYear = this.YearOption.find(item=>item.name==curDate.getFullYear());

    if(curYear!=null) {
      this.selectedYear = curYear.id;
    } else {
      this.selectedYear = null;
    }
  }

  togglePlusButton() {
    if(this.showAddDay==false&&this.roleFeatureService.featurePermission.add==false) {
      this.toastr.warning("", "Unauthorized Access", {
        positionClass: 'toast-top-right'
      });
      return;
    }
    this.selectedDayForAddition = null;
    this.showAddDay = this.showAddDay?false:true;
  }

  onDayAddition() {
    
    console.log(this.selectedYear)
    console.log(this.selectedDayForAddition);

    let workday = {yearId: this.selectedYear, weekdayId: this.selectedDayForAddition, isActive: true}
    this.subscription = this.workdayService.addWorkday(workday).subscribe({
      next: (response)=> {
        if(response.success==true) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
          this.togglePlusButton();
          this.getWorkday();
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (error)=> {
        this.toastr.error('',`${error}`, {
          positionClass: 'toast-top-right'
        })
      }
    })
  }

  onWorkdayDelete(workdayId:number) {
    console.log(this.roleFeatureService.featurePermission);
    if(this.roleFeatureService.featurePermission.delete==false) {
      this.toastr.warning("", "Unauthorized Access", {
        positionClass: 'toast-top-right'
      });
      return; 
    }
    this.confirmService.confirm('Confirm Deletion',"Are you sure?").subscribe(response=> {
      if(response) {
        this.workdayService.deleteWorkday(workdayId).subscribe({
          next: (response)=> {
            if(response.success == true) {
              this.toastr.success('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })
              this.getWorkday();
            } else {
              this.toastr.warning('',`${response.message}`, {
                positionClass: 'toast-top-right'
              })
            }
          },
          error: (err)=> {
            this.toastr.error('',`${err}`,{
              positionClass: 'toast-top-right'
            })
          }
        })

      }
      else{
        
      }
    })
  }

  onYearChange() {
    this.getWorkday();
    this.getWeekend();
  }
}
