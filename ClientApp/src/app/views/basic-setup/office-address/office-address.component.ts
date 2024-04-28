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
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { UnionService } from './../service/union.service';
import { ThanaService } from './../service/thana.service';
import { UapzilaService } from './../service/uapzila.service';
import { DistrictService } from './../service/district.service';
import { CountryService } from '../service/country.service';
import { WardService } from './../service/ward.service';
import { OfficeService} from './../service/office.service';

import {OfficeAddressService} from '../service/office-address.service';
@Component({
  selector: 'app-office-address',
  templateUrl: './office-address.component.html',
  styleUrl: './office-address.component.scss'
})
export class OfficeAddressComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  offices :SelectedModel[]=[];
  countris: SelectedModel[] = [];
  divisions: SelectedModel[] = [];
  districts: SelectedModel[] = [];
  upazilas: SelectedModel[] = [];
  thanas: SelectedModel[] = [];
  unions: SelectedModel[] = [];
  wards: SelectedModel[] = [];
  btnText: string | undefined;
  @ViewChild('OfficeAddressForm', { static: true }) OfficeAddressForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'officeAddressName', 'isActive', 'Action'];
  loading = false;

  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public officeAddressService: OfficeAddressService,
    public  officeService : OfficeService,
    public countryService :CountryService,
    public districtService :DistrictService,
    public uapzilaService :UapzilaService,
    public thanaService :ThanaService,
    public unionService :UnionService,
    public wardService :WardService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getALlOfficeAddresss();
    this.handleRouteParams();
    this.loadoffices();
    this.loadcountris();
    //this.loadedivisions();
    this.loadedistricts();
    this.loadeupazila();
    this.loadethana();
    this.loadunions();
    this.loadewards(); 
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('officeAddressId');
      if (id) {
        this.btnText = 'Update';
        this.officeAddressService.getById(+id).subscribe((res) => {
          this.OfficeAddressForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
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


  onTimerChange($event: number) {
    this.percentage = $event * 25;
  }
  initaialUpazila(form?: NgForm) {
    if (form != null) form.resetForm();
    this.officeAddressService.officeAddresses = {
      officeAddressId: 0,
      officeAddressName: '',
      officeId:0,
      countryId: 0,
      divisionId: 0,
      districtId: 0,
      upazilaId: 0,
      thanaId: 0,
      unionId: 0,
      wardId: 0,
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.OfficeAddressForm?.form.value);
    this.btnText = 'Submit';
    if (this.OfficeAddressForm?.form != null) {
      // console.log(this.OfficeAddressForm?.form);
      this.OfficeAddressForm.form.reset();
      this.OfficeAddressForm.form.patchValue({
        officeAddressId: 0,
        officeAddressName: '',
        officeId:0,
        countryId: 0,
        divisionId: 0,
        districtId: 0,
        upazilaId: 0,
        thanaId: 0,
        unionId: 0,
        wardId: 0,
        //districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  getALlOfficeAddresss() {
    this.subscription = this.officeAddressService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }


  loadoffices() { 
    this.officeService.selectGetoffice().subscribe((data) => { 
      this.offices = data;
    });
  }
  divisionNameByCounterId(counterId:number){
       console.log(counterId)
       this.districtService.getDivisionByCountryId(counterId).subscribe((data) => { 
        this.divisions = data;
        console.log(this.divisions)
      });
  }

  loadcountris() { 
    this.countryService.selectGetCountry().subscribe((data) => { 
      this.countris = data;
    });
  }
  loadedivisions() { 
    this.districtService.getDivision().subscribe((data) => { 
      this.divisions = data;
    });
  }



    
  loadedistricts() { 
    this.uapzilaService.getdistrict().subscribe((data) => { 
      this.districts = data;
    });
  }


  loadeupazila() { 
    this.thanaService.getupazila().subscribe((data) => { 
      this.upazilas = data;
    });
  }



  loadethana() { 
    this.unionService.getThana().subscribe((data) => { 
      this.thanas = data;
    });
  }




  loadunions() { 
    this.wardService.getUnion().subscribe((data) => { 
      this.unions = data;
    });
  }
  loadewards() { 
    this.wardService.getward().subscribe((data) => { 
      this.wards = data;
    });
  
  }



  onSubmit(form: NgForm): void {
    this.loading = false;
    this.officeAddressService.cachedData = [];
    const id = form.value.officeAddressId;
    const action$ = id
      ? this.officeAddressService.update(id, form.value)
      : this.officeAddressService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlOfficeAddresss();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/officeAddress']);
        }
        this.loading = false;

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
          this.officeAddressService.delete(element.officeAddressId).subscribe(
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
