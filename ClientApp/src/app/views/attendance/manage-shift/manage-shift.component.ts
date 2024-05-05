import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ShiftService } from '../services/shift.service';

@Component({
  selector: 'app-manage-shift',
  templateUrl: './manage-shift.component.html',
  styleUrl: './manage-shift.component.scss'
})
export class ManageShiftComponent implements OnInit, OnDestroy, AfterViewInit {

  btnText: string | undefined;
  @ViewChild('ShiftForm', { static: true }) ShiftForm!: NgForm;
  loading = false;
  subscription: Subscription = new Subscription();
  // displayedColumns: string[] = ['slNo', 'pNo', 'fullName', 'userName', 'email', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  BtnText : string | undefined;
  HeaderText : string | undefined;
  buttonIcon : string = '';
  visible : boolean | undefined;

  constructor(
    public shiftService: ShiftService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
  }


  ngOnInit(): void {
    this.buttonIcon = "cilPencil";
    this.handleRouteParams();
    this.getAllUsers();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.HeaderText = "Update Shift";
        this.BtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        // this.userService.find(id).subscribe((res) => {
        //   this.UserForm?.form.patchValue(res);
        // });
      } else {
        this.btnText = 'Submit';
        this.visible = false;
        this.HeaderText = "Shift List"
        this.BtnText = " Add Shift";
      }
    });
  }

  initaialUser(form?: NgForm) {
    if (form != null) form.resetForm();
    this.shiftService.shifts = {
      id :  0,
      shiftName: '',
      startTime: '',
      endTime: '',
      startDate : null,
      endDate : null,
      bufferTime: '',
      absentTime : '',
      remark : '',
      isActive : true,
    };
  }

  resetForm(){
    // this.btnText = 'Submit';
    if (this.ShiftForm?.form != null) {
      this.ShiftForm.form.reset();
      this.ShiftForm.form.patchValue({
        id :  0,
        shiftName: '',
        startTime: '',
        endTime: '',
        startDate : null,
        endDate : null,
        bufferTime: '',
        absentTime : '',
        remark : '',
        isActive : true,
      });
    }
    // this.router.navigate(['/usermanagement/user']);
  }

  getAllUsers(){
    this.subscription = this.shiftService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;

      console.log(item);
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  UserFormView(){
    if(this.BtnText == " Add Shift"){
      this.BtnText = " Hide Form";
      this.buttonIcon = "cilTrash";
      this.HeaderText = "Add New Shift";
      this.visible = true;
    }
    else {
      this.BtnText = " Add Shift";
      this.buttonIcon = "cilPencil";
      this.HeaderText = "Shift List";
      this.visible = false;
    }
  }

  cancelUpdate(){

  }

  onSubmit(form: NgForm): void{

    this.loading = true;
    this.shiftService.cachedData = [];

    this.route.paramMap.subscribe((params) => {
      const id = form.value.id;
      const action$ = id
        ? this.shiftService.update(id, form.value)
        : this.shiftService.submit(form.value);
  
        this.subscription =action$.subscribe((response: any)  => {
          if (response.success) {
            this.toastr.success('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.loading = false;
            // this.getAllUsers();
            this.resetForm();
            this.router.navigate(['/attendance/manageShift']);
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
          this.loading = false;
        });
      });
    }
}
