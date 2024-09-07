import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';

import {OfficeService} from '../service/office.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { CountryService } from '../service/country.service';
import { DistrictService } from '../service/district.service';
import { DivisionService } from '../service/division.service';
import { ThanaService } from '../service/thana.service';
import { UapzilaService } from '../service/uapzila.service';
import { UnionService } from '../service/union.service';
import { WardService } from '../service/ward.service';

@Component({
  selector: 'app-office',
  templateUrl: './office.component.html',
  styleUrl: './office.component.scss'
})
export class OfficeComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  BtnText: string | undefined;
  btnText: string | undefined;
  HeaderText : string | undefined;
  buttonIcon : string | undefined;
  countris: SelectedModel[] = [];
  divisions: SelectedModel[] = [];
  districts: SelectedModel[] = [];
  upazilas: SelectedModel[] = [];
  thanas: SelectedModel[] = [];
  unions: SelectedModel[] = [];
  wards: SelectedModel[] = [];
  @ViewChild('OfficeForm', { static: true }) OfficeForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo', 
    'officeName', 
    // 'officeNameBangla', 
    'officeCode', 
    'isActive', 
    'Action'];
  loading = false;

  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public officeService: OfficeService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    public countryService :CountryService,
    public districtService :DistrictService,
    public uapzilaService :UapzilaService,
    public thanaService :ThanaService,
    public unionService :UnionService,
    public wardService :WardService,
    public divisionService :DivisionService,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getALlOffices();
    // this.handleRouteParams();
    this.loadcountris();
    this.getOneOfficeInfo();
  }

  getOneOfficeInfo(){
    this.subscription = this.officeService.getOneOffice().subscribe((res) =>{
      if(res){
        this.btnText = 'Update';
        this.HeaderText = "Update Office";
        this.visible = true;
        this.OfficeForm?.form.patchValue(res);
      }
      else{
        this.btnText = 'Submit';
        this.HeaderText = "Add Office";
        this.visible = true;
      }
    })
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('officeId');
      if (id) {
        this.visible = true;
        this.btnText = 'Update';
        this.HeaderText = "Update Office";
        this.BtnText = " Hide Form";
        this.buttonIcon = "cilTrash";
        this.officeService.getById(+id).subscribe((res) => {
          this.onDivisionNamesChangeByCounterId(res.countryId);
          this.onDistrictNamesChangeByDivisionId(res.divisionId);
          this.onUpazilaNamesChangeByDistrictId(res.districtId);
          this.onThanaNamesChangeByUpazilaId(res.upazilaId);
          this.onUnionNamesChangeByThanaId(res.thanaId);
          this.onWardNamesChangeByUnionId(res.unionId);
          this.OfficeForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
        this.visible = false;
        this.HeaderText = "Add Office"
        this.buttonIcon = "cilPencil";
        this.BtnText = " Add Office";
      }
    });
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialOffice(form?: NgForm) {
    if (form != null) form.resetForm();
    this.officeService.offices = {
      officeId:  0,
      officeName:  "",
      officeNameBangla:  "",
      countryId: null,
      divisionId:  null,
      districtId:  null,
      upazilaId:  null,
      thanaId:  null,
      unionId :  null,
      wardId:  null,
      address:  "",
      phone:  "",
      mobile:  "",
      fax:  "",
      email:  "",
      officeWebsite:  "",
      officeCode:  "",
      remark:  "",
      menuPosition:  0,
      isActive:  true
    };
  }
  resetForm() {
    this.BtnText = 'Submit';
    if (this.OfficeForm?.form != null) {
      this.OfficeForm.form.reset();
      this.OfficeForm.form.patchValue({
        officeId:  0,
        officeName:  "",
        officeNameBangla:  "",
        countryId: null,
        divisionId:  null,
        districtId:  null,
        upazilaId:  null,
        thanaId:  null,
        unionId :  null,
        wardId:  null,
        address:  "",
        phone:  "",
        mobile:  "",
        fax:  "",
        email:  "",
        officeWebsite:  "",
        officeCode:  "",
        remark:  "",
        menuPosition:  0,
        isActive:  true
      });
    }
    this.router.navigate(['/officeSetup/office']);
  }

  getALlOffices() {
    this.subscription = this.officeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  
  UserFormView() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('officeId');
      if(id){
        if (this.BtnText == " View Form") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.HeaderText = "Update Office";
          this.visible = true;
        }
        else {
          this.BtnText = " View Form";
          this.buttonIcon = "cilPencil";
          this.HeaderText = "Office List";
          this.visible = false;
        }
      }
      else {
        if (this.BtnText == " Add Office") {
          this.BtnText = " Hide Form";
          this.buttonIcon = "cilTrash";
          this.HeaderText = "Add New Office";
          this.visible = true;
        }
        else {
          this.BtnText = " Add Office";
          this.buttonIcon = "cilPencil";
          this.HeaderText = "Office List";
          this.visible = false;
        }
      }
    });
  }
  
  toggleCollapse(){
    this.handleRouteParams();
    this.HeaderText = "Update User";
    this.visible = true;
  }

  cancelUpdate(){
    this.router.navigate(['/bascisetup/office']);
    this.resetForm();
  }

  onDivisionNamesChangeByCounterId(counterId:number){
    this.subscription=this.divisionService.getDivisionByCountryId(counterId).subscribe((data) => { 
        this.divisions = data;
      });
  }
  onDistrictNamesChangeByDivisionId(divisionId:number){
    this.subscription=this.districtService.getDistrictByDivisionId(divisionId).subscribe((data) => { 
        this.districts = data;
 
      });
  }
  onUpazilaNamesChangeByDistrictId(districtId:number){
    this.subscription=this.uapzilaService.getUpapzilaByDistrictId(districtId).subscribe((data) => { 
        this.upazilas = data;
      });
  }
  onThanaNamesChangeByUpazilaId(upazilaId:number){
 this.subscription=this.thanaService.getthanaNamesByUpazilaId(upazilaId).subscribe((data) => { 
     this.thanas = data;
    
   });
}
onUnionNamesChangeByThanaId(thanaId:number){
  this.subscription=this.unionService.getUnionNamesByThanaId(thanaId).subscribe((data) => { 
      this.unions = data;
        });
 }
 onWardNamesChangeByUnionId(unionId:number){
  this.subscription=this.wardService.getWardNamesByUnionId(unionId).subscribe((data) => { 
      this.wards = data;
      
        });
 }
  loadcountris() { 
    this.subscription=this.countryService.selectGetCountry().subscribe((data) => { 
      this.countris = data;
    });
  }

  onSubmit(form: NgForm): void {
    this.loading = true;
    this.officeService.cachedData = [];
    const id = form.value.officeId;
    const action$ = id
      ? this.officeService.update(id, form.value)
      : this.officeService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.loading = false;
        this.getALlOffices();
        // this.resetForm();
        // this.router.navigate(['/officeSetup/office']);
        this.getOneOfficeInfo();
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      this.loading = false;

    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          console.log('office id ' + element.officeId);
          this.officeService.delete(element.officeId).subscribe(
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
              // console.log(err);

              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }
}
