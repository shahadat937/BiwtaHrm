import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm, NgModel, FormsModule } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import {ManualAttendanceService} from '../services/manual-attendance.service'
import { HttpParams } from '@angular/common/http';

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
  selectedOffice:number|null;
  selectedShift:number|null;
  selectedDepartment:number|null;
  selectedEmp:number;
  atdFile: File | null;


  constructor(
    public manualAtdService: ManualAttendanceService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
      this.selectedOffice = null;
      this.selectedDepartment=null;
      this.selectedShift = null;
      this.selectedEmp = 0;
      this.atdFile = null;

      this.HeaderText = "Manual Attendance ";
      this.OfficeOption = []
      this.DepartmentOption = []
      this.ShiftOption = []
      this.EmpOption = []
      this.AtdStatusOption = []
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
    this.manualAtdBulkForm.form.patchValue({
      empId:null

    })
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
    //console.log(this.selectedDepartment);

    if(this.selectedOffice!=null)
    this.manualAtdService.getDepartmentOption(this.selectedOffice).subscribe(
      option => this.DepartmentOption = option,
    );

    if(this.selectedOffice==null) {
      this.DepartmentOption=[];
    }

    let params = new HttpParams();
    if(this.selectedDepartment!=0) {
      params = params.set('DepartmentId',this.selectedDepartment==null?"":this.selectedDepartment);
    }
    if(this.selectedOffice!=0) {
      params = params.set("OfficeId",this.selectedOffice==null?"":this.selectedOffice);
    }


    console.log(params.get('officeId'));

    this.manualAtdService.getFilteredEmpOption(params).subscribe(response=> {
      this.EmpOption = response;
      console.log(response);
    });
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
