import { BranchService } from './../service/branch.service';
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
  selector: 'app-branch',
  templateUrl: './branch.component.html',
  styleUrl: './branch.component.scss'
})
export class BranchComponent implements OnInit, OnDestroy, AfterViewInit {
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('BranchForm', { static: true }) BranchForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'branchName', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort; 
  constructor(
    public branchService: BranchService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('branchId');
      if (id) {
        this.btnText = 'Update';
        this.branchService.getById(+id).subscribe((res) => {
          this.BranchForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getALlBranch();
    
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
  initaialUpazila(form?: NgForm) {
    if (form != null) form.resetForm();
    this.branchService.branchs = {
      branchId: 0,
      branchName: '', 
      //districtName:"",
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    // console.log(this.BranchForm?.form.value);
    this.btnText = 'Submit';
    if (this.BranchForm?.form != null) {
      // console.log(this.BranchForm?.form);
      this.BranchForm.form.reset();
      this.BranchForm.form.patchValue({
        branchId: 0,
        branchName: '', 
        //  districtName:"",
        menuPosition: 0,
        isActive: true,
      });
    }
  }

  

  getALlBranch() {
    this.subscription = this.branchService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  onSubmit(form: NgForm) {
    const id = this.BranchForm.form.get('branchId')?.value;
    if (id) {
      this.branchService.update(+id, this.BranchForm.value).subscribe(
        (response: any) => {
          //console.log(response);
          if (response.success) {
            this.toastr.success('Successfully', 'Update', {
              positionClass: 'toast-top-right',
            });
            this.getALlBranch();
            this.resetForm();
            this.router.navigate(['/bascisetup/branch']);
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    } else {
      this.subscription = this.branchService.submit(form?.value).subscribe(
        (response: any) => {
          if (response.success) {
            this.toastr.success('Successfully', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
            this.getALlBranch();
            this.resetForm();
          } else {
            this.toastr.warning('', `${response.message}`, {
              positionClass: 'toast-top-right',
            });
          }
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          console.log('branch id ' +element.branchId);
          this.branchService.delete(element.branchId).subscribe(
            (res) => {
              this.getALlBranch();
            },
            (err) => {
              console.log(err);
            }
          );
        }
      });
  }
}
