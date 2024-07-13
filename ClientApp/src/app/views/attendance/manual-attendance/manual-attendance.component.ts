import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { NgForm, NgModel, FormsModule } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {ManualAttendanceService} from '../services/manual-attendance.service'

@Component({
  selector: 'app-manual-attendance',
  templateUrl: './manual-attendance.component.html',
  styleUrl: './manual-attendance.component.scss'
})
export class ManualAttendanceComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  loading = false;
  subscription: Subscription = new Subscription();
  displayedColumnName: string[] = ["Attendance Id", "Attendance Date", "Shift", "Employee Name","InTime","OutTime", "Attendance Type", "Attendance Status"];
  dataSource = new MatTableDataSource<any>();
  HeaderText: string | undefined;
  visible = true;
  buttonIcon:string = '';
  OfficeOption:any;
  DepartmentOption:any;
  ShiftOption: any;
  EmployeeOption:any;
  AtdStatusOption:any;
  selectedOffice:number;


  constructor(
    private manualAtdService: ManualAttendanceService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
      this.selectedOffice = 4;
      this.HeaderText = "Manual Attendance";
      this.OfficeOption = [{"value":1,"name":"Office1"},{"value":2,"name":"Office2"}]
      this.DepartmentOption = [{"value":1,"name":"Department1"},{"value":2,"name":"Department2"}]
      this.ShiftOption = [{"value":1,"name":"Shift1"},{"value":2,"name":"Shift2"}]
      this.EmployeeOption = [{"value":1,"name":"Emp1"},{"value":2,"name":"Emp2"}]
      this.AtdStatusOption = [{"value":1,"name":"Status 1"},{"value":2,"name":"Status 2"}]
  }

  ngOnInit(): void {
    this.btnText = "Add Attendance"; 
    this.buttonIcon = "cilPencil"

    // Get selection option for offices
    this.manualAtdService.getOfficeOption().subscribe(
      option => this.OfficeOption = option
    );

    // Get selection option for Shift
    this.manualAtdService.getShiftOption().subscribe(
      option => this.ShiftOption = option
    );

    // Get attendance status option
    this.manualAtdService.getAttendanceStatusOption().subscribe(
      option => this.AtdStatusOption = option
    );
    
  }

  onOfficeChange() {
    console.log(this.selectedOffice);
    this.manualAtdService.getDepartmentOption(this.selectedOffice).subscribe(
      option => this.DepartmentOption = option,
    );
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  ngAfterViewInit(): void {
    
  }
}
