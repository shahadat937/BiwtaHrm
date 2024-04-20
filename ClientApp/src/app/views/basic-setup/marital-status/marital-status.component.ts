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
import { MaritalStatusService } from '../service/marital-status.service';

@Component({
  selector: 'app-marital-status',
  templateUrl: './marital-status.component.html',
  styleUrl: './marital-status.component.scss',
})
export class MaritalStatusComponent
  implements OnInit, OnDestroy, AfterViewInit
{
  btnText: string | undefined;
  @ViewChild('maritalStatusForm', { static: true }) maritalStatusForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'maritalStatusName',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public maritalStatusService: MaritalStatusService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }

  ngOnInit(): void {
    this.getMaritalStatuses();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('maritalStatusId');
      if (id) {
        this.btnText = 'Update';
        this.maritalStatusService.find(+id).subscribe((res) => {
          this.maritalStatusForm?.form.patchValue(res);
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

  initaialBloodGroup(form?: NgForm) {
    if (form != null) form.resetForm();
    this.maritalStatusService.maritalStatus = {
      maritalStatusId: 0,
      maritalStatusName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    console.log(this.maritalStatusForm?.form.value);
    this.btnText = 'Submit';
    if (this.maritalStatusForm?.form != null) {
      this.maritalStatusForm.form.reset();
      this.maritalStatusForm.form.patchValue({
        maritalStatusId: 0,
        maritalStatusName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  getMaritalStatuses() {
    this.subscription = this.maritalStatusService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  //  onSubmit(form:NgForm){
  //   this.maritalStatusService.cachedData = [];
  //   const id = this.maritalStatusForm.form.get('maritalStatusId')?.value;
  //   if (id) {
  //     this.maritalStatusService.update(+id,this.maritalStatusForm.value).subscribe((response:any) => {
  //       console.log(response)
  //       if(response.success){
  //         this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
  //         this.getMaritalStatuses()
  //         this.resetForm();
  //         this.router.navigate(["/bascisetup/marital-status"]);
  //       }else{
  //         this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
  //       }

  //     }, err => {
  //       console.log(err)
  //     })
  //   }else{
  //  this.subscription=this.maritalStatusService.submit(form?.value).subscribe((response:any)=>{
  //   if(response.success){
  //     this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
  //     this.getMaritalStatuses()
  //     this.resetForm();
  //   }else{
  //     this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
  //   }

  //  },err=>{
  //    console.log(err);
  //  })
  //   }

  // }
  onSubmit(form: NgForm): void {
    this.maritalStatusService.cachedData = [];
    const id = form.value.maritalStatusId;
    const action$ = id
      ? this.maritalStatusService.update(id, form.value)
      : this.maritalStatusService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getMaritalStatuses();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/marital-status']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.maritalStatusService.delete(element.maritalStatusId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
              console.log(err);
            }
          );
        }
      });
  }
}
