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
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';

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
  displayedColumns: string[] = ['slNo', 'shiftId', 'shiftName', 'startTime', 'endTime', 'bufferTime', 'absentTime', 'isActive', 'Action'];
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
    private toastr: ToastrService,
    public roleFeatureService: RoleFeatureService,
  ) {
  }


  ngOnInit(): void {
    this.getPermission();
  }


  
  getPermission(){
    this.roleFeatureService.getFeaturePermission('manageShift').subscribe((item) => {
      if(item.viewStatus == true){
        this.buttonIcon = "cilPencil";
        this.handleRouteParams();
        this.getAllUsers();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    });
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('shiftId');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.HeaderText = "Update Shift";
        this.BtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.shiftService.find(id).subscribe((res) => {
          this.ShiftForm?.form.patchValue(res);
        });
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
      shiftId :  0,
      shiftName: '',
      startTime: null,
      endTime: null,
      startDate : null,
      endDate : null,
      bufferTime: null,
      absentTime : null,
      remark : '',
      isActive : true,
    };
  }

  resetForm(){
    // this.btnText = 'Submit';
    if (this.ShiftForm?.form != null) {
      this.ShiftForm.form.reset();
      this.ShiftForm.form.patchValue({
        shiftId :  0,
        shiftName: '',
        startTime: null,
        endTime: null,
        startDate : null,
        endDate : null,
        bufferTime: null,
        absentTime : null,
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

  
  addButton(){
    if(this.roleFeatureService.featurePermission.add == true){
      this.UserFormView();
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
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

  toggleCollapse(){
    this.handleRouteParams();
    this.HeaderText = "Update User";
    this.visible = true;
  }

  cancelUpdate(){
    this.router.navigate(['/attendance/manageShift']);
    this.resetForm();
  }

  updateFunction(id: number){
    if(this.roleFeatureService.featurePermission.update == true){
      this.router.navigate(['/attendance/update-shift/', id]);
      this.toggleCollapse();
    }
    else{
      this.roleFeatureService.unauthorizeAccress();
    }
  }
  

  onSubmit(form: NgForm): void{

    this.loading = true;
    this.shiftService.cachedData = [];

    this.route.paramMap.subscribe((params) => {
      const id = form.value.shiftId;

      const action$ = id
        ? this.shiftService.update(id, form.value)
        : this.shiftService.submit(form.value);
  
        this.subscription =action$.subscribe((response: any)  => {
          console.log(response)
          if (response.success) {
            this.toastr.success('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.loading = false;
            this.getAllUsers();
            this.resetForm();
            this.router.navigate(['/attendance/manageShift']);
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.loading = false;
          }
          this.loading = false;
        });
      });
    }

    delete(element: any){
      if(this.roleFeatureService.featurePermission.delete == true){
        this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.shiftService.delete(element.shiftId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
      }
  }
}