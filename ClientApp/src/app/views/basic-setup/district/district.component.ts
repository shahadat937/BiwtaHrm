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
import { DistrictService } from './../service/district.service';

import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrl: './district.component.scss',
})
export class DistrictComponent implements OnInit, OnDestroy {

  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('DistrictForm', { static: true }) DistrictForm!: NgForm;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo','divisionName','districtName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  divisions: SelectedModel[] = [];
  constructor(
    public districtService: DistrictService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('districtId');
      if (id) {

        this.btnText = 'Update';
        this.headerText = 'Update District';
        this.subscription.push(
        this.districtService.find(+id).subscribe((res) => {        
          this.DistrictForm?.form.patchValue(res);
        })
        )
        
      } else {
        this.resetForm();
        this.headerText = 'Add District';
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getALlDistricts();
    this.loaddivisions();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialDistrict(form?: NgForm) {
    if (form != null) form.resetForm();
    this.districtService.districts = {
      districtId: 0,
      districtName: '',
      divisionId: 0,
      //divisionName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.DistrictForm?.form != null) {
      this.DistrictForm.form.reset();
      this.DistrictForm.form.patchValue({
        districtId: 0,
        districtName: '',
        divisionId: 0,
        //  divisionName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/addressSetup/district']);
  }

  loaddivisions() {
    this.subscription.push(
    this.districtService.getDivision().subscribe(data => {
      this.divisions = data;
    })
    )
    
  }

  getALlDistricts() {
    this.subscription.push(
    this.districtService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
   
  }

  // onSubmit(form: NgForm) {
  //   const id = this.DistrictForm.form.get('districtId')?.value;
  //   if (id) {
  //     this.districtService.update(+id, this.DistrictForm.value).subscribe(
  //       (response: any) => {
  //         console.log(response);
  //         if (response.success) {
  //           this.toastr.success('Successfully', 'Update', {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlDistricts();
  //           this.resetForm();
  //           this.router.navigate(['/bascisetup/district']);
  //         } else {
  //           this.toastr.warning('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //         }
  //       },
  //       (err) => {
  //         //console.log(err);
  //         this.toastr.error('Somethig Wrong ! ', ` `, {
  //           positionClass: 'toast-top-right',})
  //       }
  //     );
  //   } else {
  //     this.subscription = this.districtService.submit(form?.value).subscribe(
  //       (response: any) => {
  //         if (response.success) {
  //           this.toastr.success('Successfully', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //           this.getALlDistricts();
  //           this.resetForm();
  //         } else {
  //           this.toastr.warning('', `${response.message}`, {
  //             positionClass: 'toast-top-right',
  //           });
  //         }
  //       },
  //       (err) => {
  //        // console.log(err);
  //        this.toastr.error('Somethig Wrong ! ', ` `, {
  //         positionClass: 'toast-top-right',})
  //       }
  //     );
  //   }
  // }
  onSubmit(form: NgForm): void {
    this.districtService.cachedData = [];
    const id = form.value.districtId;
    const action$ = id
      ? this.districtService.update(id, form.value)
      : this.districtService.submit(form.value);

   this.subscription.push(
action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlDistricts();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/addressSetup/district']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    })
   )
      
  }
  delete(element: any) {
    this.subscription.push(
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.subscription.push(
          this.districtService.delete(element.districtId).subscribe(
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
          )
          )
          
        }
      })
    )
    
  }
}
