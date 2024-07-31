import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-holiday-setup',
  templateUrl: './holiday-setup.component.html',
  styleUrl: './holiday-setup.component.scss'
})
export class HolidaySetupComponent implements OnInit, OnDestroy {
  isVisible: boolean
  holidayTypeOption : any[] = [];
  subscription: Subscription = new Subscription();

  constructor(
    private toastr: ToastrService,
    private confirmService: ConfirmService
  ) {
    this.isVisible=true;
  }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    } 
  }

  getHolidayType() {
    return ""; 
  }

}
