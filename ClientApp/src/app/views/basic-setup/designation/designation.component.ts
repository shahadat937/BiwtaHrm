import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { DesignationService } from '../service/designation.service';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrl: './designation.component.scss',
})
export class DesignationComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  @ViewChild('DesignationForm', { static: true }) DesignationForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'designationName',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public designationService: DesignationService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getALlDesignations();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('designationId');
      if (id) {
        this.btnText = 'Update';
        this.designationService.find(+id).subscribe((res) => {
          this.DesignationForm?.form.patchValue(res);
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

  initaialDesignation(form?: NgForm) {
    if (form != null) form.resetForm();
    this.designationService.designation = {
      designationId: 0,
      designationName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.DesignationForm?.form != null) {
      this.DesignationForm.form.reset();
      this.DesignationForm.form.patchValue({
        designationId: 0,
        designationName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/designation']);
  }

  getALlDesignations() {
    this.subscription = this.designationService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  //  onSubmit(form:NgForm){
  //   this.designationService.cachedData = [];
  //   const id = this.DesignationForm.form.get('designationId')?.value;
  //   if (id) {
  //     this.designationService.update(+id,this.DesignationForm.value).subscribe((response:any) => {
  //       if(response.success){
  //         this.toastr.success('Successfully', 'Update',{ positionClass: 'toast-top-right' });
  //         this.getALlDesignations()
  //         this.resetForm();
  //         this.router.navigate(["/bascisetup/designation"]);
  //       }else{
  //         this.toastr.warning('', `${response.message}`,{ positionClass: 'toast-top-right' });
  //       }

  //     }, err => {
  //       console.log(err)
  //     })
  //   }else{
  //  this.subscription=this.designationService.submit(form?.value).subscribe((response:any)=>{
  //   if(response.success){
  //     this.toastr.success('Successfully', `${response.message}`,{ positionClass: 'toast-top-right' });
  //     this.getALlDesignations()
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
    this.designationService.cachedData = [];
    const id = form.value.designationId;
    const action$ = id
      ? this.designationService.update(id, form.value)
      : this.designationService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlDesignations();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/designation']);
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
          this.designationService.delete(element.designationId).subscribe(
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
              console.log(err);
            }
          );
        }
      });
  }
}
