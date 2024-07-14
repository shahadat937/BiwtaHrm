import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
  loadingBulk = false;
  @ViewChild('manualAtdform', { static: true }) manualAtdForm!: NgForm;
  @ViewChild('manualAtdBulkForm', { static: true }) manualAtdBulkForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumnName: string[] = ["Attendance Id", "Attendance Date", "Shift", "Employee Name","InTime","OutTime", "Attendance Type", "Attendance Status"];
  dataSource = new MatTableDataSource<any>();
  HeaderText: string | undefined;
  visible = true;
  buttonIcon:string = '';
  OfficeOption:any;
  DepartmentOption:any;
  ShiftOption: any;
  EmpOption:any;
  AtdStatusOption:any;
  selectedOffice:number;
  selectedShift:number;
  selectedDepartment:number;
  selectedEmp:number;
  atdFile: File | null;


  constructor(
    public manualAtdService: ManualAttendanceService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
      this.selectedOffice = 0;
      this.selectedDepartment=0;
      this.selectedShift = 0;
      this.selectedEmp = 0;
      this.atdFile = null;

      this.HeaderText = "Manual Attendance ";
      this.OfficeOption = [{"value":1,"name":"Office1"},{"value":2,"name":"Office2"}]
      this.DepartmentOption = [{"value":1,"name":"Department1"},{"value":2,"name":"Department2"}]
      this.ShiftOption = [{"value":1,"name":"Shift1"},{"value":2,"name":"Shift2"}]
      this.EmpOption = [{"value":1,"name":"Emp1"},{"value":2,"name":"Emp2"}]
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
    
    this.manualAtdService.getEmpOption().subscribe(
      option=> this.EmpOption = option
    );
    
  }

  ResetForm() {
    //console.log("Reset Form Attempt");
    if(this.manualAtdForm?.form==null) {
      return;
    }
    this.manualAtdForm.reset();
    //console.log("Form Reset");
    //console.log(this.loading);
  }

  ResetBulkForm() {
    if(this.manualAtdBulkForm?.form==null) {
      return;
    } 

    this.atdFile=null;
    this.manualAtdBulkForm.reset();
  }

  onOfficeChange() {
    console.log(this.selectedOffice);
    this.manualAtdService.getDepartmentOption(this.selectedOffice).subscribe(
      option => this.DepartmentOption = option,
    );
  }

  onSubmit(form:NgForm) {
    this.loading = true;
    
    this.subscription=this.manualAtdService.submit(this.manualAtdService.attendances).subscribe((response:any)=> {
      if(response.success) {
        this.ResetForm();
        this.loading = false;
        this.toastr.success('',`${response.message}`, {
          positionClass: 'toast-top-right',
        });
      } else {
        this.loading = false;
        this.toastr.warning('',`${response.message}`, {
          positionClass: 'toast-top-right'
        });
      } 

      this.ResetForm();

      this.loading = false;

    });
  }

  onSubmitBulk(form:NgForm) {

    this.loadingBulk = true;
    this.subscription = this.manualAtdService.submitBulk(this.atdFile).subscribe((response:any)=> {
      if(response.success) {
        this.ResetBulkForm();
        this.loadingBulk=false;
        this.toastr.success('',`${response.message}`,{
          positionClass:'toast-top-right'
        });
      } else {
        this.loadingBulk=false;
        this.toastr.warning('',`${response.message}`, {
          positionClass:'toast-top-right'
        });
      }

      this.ResetBulkForm();
      this.loadingBulk = false;
    });
  }

  onAtdFileChange(event:any) {
    this.atdFile = event.target.files[0];
  }

  ngOnDestroy(): void {
    if(this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  ngAfterViewInit(): void {
    
  }
}
