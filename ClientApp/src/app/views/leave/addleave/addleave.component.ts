import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {AddLeaveService} from '../service/add-leave.service';
import { delay, of, Subscription } from 'rxjs';
import { NgFor } from '@angular/common';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-addleave',
  templateUrl: './addleave.component.html',
  styleUrl: './addleave.component.scss'
})
export class AddleaveComponent  implements OnInit, OnDestroy{
  loading: boolean;
  LeaveTypeOption:any[] = [];
  @ViewChild("addLeaveForm",{static:true}) addLeaveForm!: NgForm
  empCardNo:string ;
  employeeName: string;
  empSubs: Subscription = new Subscription();
  empReqSub: Subscription = new Subscription();
  inc: number = 0;

  constructor(
    public addLeaveService: AddLeaveService, 
    private toastr: ToastrService,
    private confirmService: ConfirmService
  ) {
    this.loading = false;
    this.empCardNo ="";
    this.employeeName = "";
  }

  ngOnInit(): void {
    this.addLeaveService.getSelectedLeaveType().subscribe({
      next: option => {
        this.LeaveTypeOption = option;
      }
    }) 
  }

  onEmpIdChange(event:any) {
    const source$ = of (this.empCardNo);
    const delay$ = source$.pipe(
      delay(700)
    );

    if(this.empSubs) {
      this.empSubs.unsubscribe();
    }

    if(this.empReqSub) {
      this.empReqSub.unsubscribe();
    }

    if(this.empCardNo.trim()=="") {
      this.employeeName = "";
      return;
    }

    this.empSubs = delay$.subscribe(data=> {
      this.empReqSub = this.addLeaveService.getEmpInfoByCard(data).subscribe({
        next: response=> {
          console.log(response);
          if(response!=null) {
            this.employeeName = response.firstName + " "+response.lastName;
          } else {
            this.employeeName = "";
          }
        },
        error: err=> {

        },
        complete:()=> {
          console.log("Complete");
        }
      })
    })
  }

  ngOnDestroy(): void {
    
  }

  getLeaveAmount() {

  }

}
