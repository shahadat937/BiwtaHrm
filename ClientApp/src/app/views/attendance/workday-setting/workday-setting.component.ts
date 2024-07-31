import { AfterContentInit, AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import {WorkdayService} from  "../services/workday.service"
import { cilTrash, cilPlus, cilX } from '@coreui/icons'
import { get } from 'lodash-es';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-workday-setting',
  templateUrl: './workday-setting.component.html',
  styleUrl: './workday-setting.component.scss'
})
export class WorkdaySettingComponent implements OnInit, OnDestroy {
  subscription: Subscription = new Subscription();
  icons = {cilTrash, cilPlus, cilX};
  YearOption: any[] = [];
  DayOption: any[] = [];
  Workday: any[] = []
  selectedYear:number|null;
  selectedDay: number | null;
  selectedDayForAddition : number | null;
  showAddDay: boolean ;
  constructor(
    private workdayService: WorkdayService,
    private toastr : ToastrService,
    private confirmService: ConfirmService
  ) {
    this.selectedYear = 0;
    this.selectedDay = null;
    this.selectedDayForAddition = null;
    this.showAddDay = false;
    
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
      },
      error: (err)=> {
        console.error("Got error while retriving Year Option for Selection");
      }
    })
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
    })
  }
}
