import { DistrictService } from './../service/district.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {
  AfterViewInit,
  Component,
  OnInit,
  ViewChild,
  OnDestroy,
} from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { Subscription } from 'rxjs';
 
import { NgForm } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { SelectedModel } from 'src/app/core/models/selectedModel';

@Component({
  selector: 'app-district',
  templateUrl: './district.component.html',
  styleUrl: './district.component.scss',
})
export class DistrictComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('DistrictForm', { static: true }) DistrictForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'districtName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  divisions: SelectedModel[] = [];
  constructor(
    public districtService: DistrictService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) 
  { 

    this.route.paramMap.subscribe((params) => {
      const id = params.get('districtId');
      if (id) {
        this.btnText = 'Update';
        this.districtService.find(+id).subscribe((res) => {
          console.log(res);
          this.DistrictForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getALlDistrict();
    this.loaddivisions();
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
  toggleToast() {
    this.visible = !this.visible;
  }

  onVisibleChange($event: boolean) {
    this.visible = $event;
    this.percentage = !this.visible ? 0 : this.percentage;
  }

  onTimerChange($event: number) {
    this.percentage = $event * 25;
  }
  initaialDistrict(form?: NgForm) {
    if (form != null) form.resetForm();
    this.districtService.districts = {
      districtId: 0,
      districtName: '',
      divisionId :0,
      //divisionName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    console.log(this.DistrictForm?.form.value);
    this.btnText = 'Submit';
    if (this.DistrictForm?.form != null) {
      console.log(this.DistrictForm?.form);
      this.DistrictForm.form.reset();
      this.DistrictForm.form.patchValue({
        districtId: 0,
        districtName: '',
        divisionId :0,
      //  divisionName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  loaddivisions() {
    console.log('division')
    this.districtService.getDivision().subscribe(data => {
      console.log('division'+ data)
      this.divisions = data;
    });
  }


  getALlDistrict() {
    this.subscription = this.districtService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm) {
    const id = this.DistrictForm.form.get('districtId')?.value;
    if (id) {
      this.districtService.update(+id, this.DistrictForm.value).subscribe(
        (response: any) => {
          console.log(response);
          if (response.success) {
            this.toastr.success('Successfully', 'Update', {
              positionClass: 'toast-top-right',
            });
            this.getALlDistrict();
            this.resetForm();
            this.router.navigate(['/bascisetup/district']);
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          //console.log(err);
          this.toastr.error('Somethig Wrong ! ', ` `, {
            positionClass: 'toast-top-right',})
        }
      );
    } else {
      this.subscription = this.districtService.submit(form?.value).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('Successfully', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.getALlDistrict();
            this.resetForm();
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
         // console.log(err);
         this.toastr.error('Somethig Wrong ! ', ` `, {
          positionClass: 'toast-top-right',})
        }
      );
    }
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          
          this.districtService.delete(element.districtId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
          if (index !== -1) {
            this.dataSource.data.splice(index, 1);
            this.dataSource = new MatTableDataSource(
              this.dataSource.data
            );
          }
            },
            (err) => {
             // console.log(err);

              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',})
            }
          );
        }
      });
  }
}
