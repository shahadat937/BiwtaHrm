import { Component, Input, input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {HolidaySetupService} from '../../services/holiday-setup.service'
import {HolidayModel} from '../../models/holiday-model'
import { NgForm } from '@angular/forms';
import {cilPen, cilPencil, cilTrash} from '@coreui/icons'

@Component({
  selector: 'app-holiday-setup',
  templateUrl: './holiday-setup.component.html',
  styleUrl: './holiday-setup.component.scss'
})
export class HolidaySetupComponent implements OnInit, OnDestroy {
  loading:boolean = false;
  icons = {cilPencil, cilTrash}
  YearOption: any[] = []
  SelectedYear: number | null = null;
  isVisible: boolean
  isUpdate: boolean
  holidayTypeOption : any[] = [];
  Holidays: HolidayModel [];
  subscription: Subscription = new Subscription();
  @ViewChild('holidayForm', {static:true}) holidayForm!:NgForm

  constructor(
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    public holidayService: HolidaySetupService
  ) {
    this.isVisible=false;
    this.isUpdate = false;
    this.Holidays = []
  }

  ngOnInit(): void {
    this.getHolidayType();
    this.getYear();
    this.getHolidays();
    console.log(this.Holidays);
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  toggleAddHoliday() {
    this.isUpdate = false;
    this.isVisible=this.isVisible?false:true;
    this.holidayService.model = new HolidayModel();
    this.holidayForm.reset();
    console.log("Hello World");
  }

  getHolidayType() {
    this.subscription = this.holidayService.getHolidayTypeOption().subscribe({
      next: (option)=> {
        this.holidayTypeOption = option;
      },
      error: (err)=> {
        console.log(err);
      }
    })
  }

  getYear() {
    this.subscription = this.holidayService.getYear().subscribe({
      next: (option)=> {
        this.YearOption = option;
      },
      error: (err)=> {
        console.error("Got error while retriving Year Option for Selection");
      }
    })
  }

  getHolidays() {
    this.subscription = this.holidayService.getHolidays().subscribe({
      next: (response)=> {
        this.Holidays = response;
      },
      error: (err) => {
        console.error("Error While loading holidays data from server");
      }
    })
  }

  onCreate(form:NgForm) {

    let element = form.value;
    element['holidayId']=0;
    console.log(element);
    this.loading=true;
    this.subscription = this.holidayService.createHoliday(element).subscribe({
      next: (response)=> {
        if(response.success==true) {
          this.toastr.success('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })

          this.holidayService.cachedData = [];
          this.getHolidays();
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (err)=> {
        this.toastr.error('',`${err}`,{
          positionClass: 'toast-top-right'
        });
        this.loading=false;
      },
      complete: ()=> {
        this.loading=false;
      }
    })
  }

  onSubmit(form:NgForm) {

    this.isUpdate?this.onUpdate():this.onCreate(form);
  }

  onDelete(holidayId:number) {

    this.confirmService.confirm("Confirm Delete Message","Are you sure?").subscribe((result)=> {
      if(!result) {
        return;
      }
      this.holidayService.deleteHoliday(holidayId).subscribe({
        next: (response)=> {
          if(response.success==true) {
            this.toastr.success('',`${response.message}`,{
              positionClass: 'toast-top-right'
            })
            var index = this.Holidays.findIndex((item)=> item.holidayId==holidayId);
            delete this.Holidays[index];
          } else {
            this.toastr.error('', `${response.message}`, {
              positionClass: 'toast-top-right'
            })
          }
        },
        error: (error)=> {
          this.toastr.error('',error, {
            positionClass: 'toast-top-right'
          })
        }
      });

    })
  }


  toggleUpdate(element:any) {
    let item = JSON.parse(JSON.stringify(element)); 
    this.holidayForm.form.patchValue(item);
    this.holidayService.model = item;
    console.log(this.holidayService.model);
    this.isUpdate = true;
    this.isVisible = true;
    console.log(element);
  }

  onUpdate() {
    this.loading = true;
    this.holidayService.updateHoliday(this.holidayService.model).subscribe({
      next: (response)=> {
        if(response.success==true) {
          this.toastr.success('',`${response.message}`,{
            positionClass: 'toast-top-right'
          })
          this.holidayService.cachedData=[];
          this.getHolidays();
        } else {
          this.toastr.warning('',`${response.message}`, {
            positionClass: 'toast-top-right'
          })
        }
      },
      error: (err)=> {
        this.toastr.error('',`${err}`, {
          positionClass:'toast-top-right'
        })
        this.loading=false;
      },
      complete: ()=> {
        this.loading = false;
      }
    })
  }

}
