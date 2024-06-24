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
import { ChildStatusService } from '../service/child-status.service';

@Component({
  selector: 'app-child-status',
  templateUrl: './child-status.component.html',
  styleUrl: './child-status.component.scss',
})
export class ChildStatusComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('ChildStatusForm', { static: true }) ChildStatusForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'childStatusName',
    'isActive',
    'Action',
  ];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public childStatusService: ChildStatusService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    //  const id = this.route.snapshot.paramMap.get('bloodGroupId');
  }
  ngOnInit(): void {
    this.getALlChildStatus();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('childStatusId');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Child Status';
        this.childStatusService.find(+id).subscribe((res) => {
          this.ChildStatusForm?.form.patchValue(res);
        });
      } else {
        this.resetForm();
        this.headerText = 'Add Child Status';
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
    this.childStatusService.childStatus = {
      childStatusId: 0,
      childStatusName: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.ChildStatusForm?.form != null) {
      this.ChildStatusForm.form.reset();
      this.ChildStatusForm.form.patchValue({
        childStatusId: 0,
        childStatusName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/bascisetup/child-status']);
  }

  getALlChildStatus() {
    this.subscription = this.childStatusService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }
  onSubmit(form: NgForm): void {
    this.childStatusService.cachedData = [];
    const id = form.value.childStatusId;
    const action$ = id
      ? this.childStatusService.update(id, form.value)
      : this.childStatusService.submit(form.value);

    this.subscription = action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getALlChildStatus();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/bascisetup/child-status']);
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
          this.childStatusService.delete(element.childStatusId).subscribe(
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
