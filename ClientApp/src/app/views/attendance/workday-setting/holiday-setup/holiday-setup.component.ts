import { Component, Input, input, model, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {HolidaySetupService} from '../../services/holiday-setup.service'
import {HolidayModel} from '../../models/holiday-model'
import { NgForm } from '@angular/forms';
import {cilPen, cilPencil, cilTrash} from '@coreui/icons'
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { FeaturePermission } from 'src/app/views/featureManagement/model/feature-permission';
import { Feature } from 'src/app/views/featureManagement/model/feature';
import { group } from '@angular/animations';

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
  @ViewChild('holidayForm', {static:true}) holidayForm!:NgForm;

  constructor(
    private toastr: ToastrService,
    private confirmService: ConfirmService,
    public holidayService: HolidaySetupService,
    public roleFeatureService: RoleFeatureService
  ) {
    this.isVisible=false;
    this.isUpdate = false;
    this.Holidays = []
  }

  ngOnInit(): void {
    this.getHolidayType();
    this.getYear();
    this.getHolidays();
    this.roleFeatureService.getFeaturePermission("workdaySetting").subscribe(data=> {

    });
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  toggleAddHoliday() {
    if(this.roleFeatureService.featurePermission.add == true){
      this.isUpdate = false;
      this.isVisible=this.isVisible?false:true;
      this.holidayForm.reset();
      this.holidayService.model = new HolidayModel();
      this.holidayForm.form.patchValue(this.holidayService.model);
    }
    else{
      this.roleFeatureService.unauthorizeAccress();
    }
   
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
        this.Holidays =  this.getGroupedData(response);
      },
      error: (err) => {
        console.error("Error While loading holidays data from server");
      }
    })
  }

  getGroupedData(data:HolidayModel[]) {
    let reducedData : HolidayModel []=[];

    let currentGroup = -1;

    const minDate = (date1:Date|null, date2:Date|null) =>  {
      if(date1!=null&&date2!=null&&date1<date2) {
        return date1;
      } else {
        return date2;
      }
    }
    const maxDate = (date1:Date|null, date2:Date|null) => {
      if(date1!=null&&date2!=null&&date1>date2) {
        return date1;
      } else {
        return date2;
      }
    }


    data.forEach(x=> {
      if(currentGroup!=x.groupId) {
        x.holidayFrom = x.holidayDate;
        x.holidayTo = x.holidayDate;
        reducedData.push(x);
        currentGroup = x.groupId;
      } else {
        let curDateFrom = reducedData[reducedData.length-1].holidayFrom;
        let curDateTo = reducedData[reducedData.length-1].holidayTo;

        reducedData[reducedData.length-1].holidayFrom = minDate(curDateFrom,x.holidayDate);
        reducedData[reducedData.length-1].holidayTo = maxDate(curDateTo, x.holidayDate);
      }
    })



    return reducedData;

  }

  onCreate(form:NgForm) {

    let element = form.value;
    element['holidayId']=0;
    element.holidayDate = element.holidayFrom;
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

  onDelete(groupId:number) {

    this.confirmService.confirm("Confirm Delete Message","Are you sure?").subscribe((result)=> {
      if(!result) {
        return;
      }
      this.holidayService.deleteHolidayByGroupId(groupId).subscribe({
        next: (response)=> {
          if(response.success==true) {
            this.toastr.success('',`${response.message}`,{
              positionClass: 'toast-top-right'
            })
            var index = this.Holidays.findIndex((item)=> item.groupId==groupId);
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
    this.holidayService.updateHolidayGroup(this.holidayService.model).subscribe({
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
