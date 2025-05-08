import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RetiredReasonService } from '../service/retired-reason.service';

@Component({
  selector: 'app-retired-reason',
  templateUrl: './retired-reason.component.html',
  styleUrl: './retired-reason.component.scss'
})
export class RetiredReasonComponent  implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('RetiredReasonForm', { static: true }) RetiredReasonForm!: NgForm;
  loading = false;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'name', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public retiredReasonService: RetiredReasonService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
  }
  ngOnInit(): void {
    this.getAllRetiredReasons();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Retired Reason';
        this.subscription.push(
        this.retiredReasonService.find(+id).subscribe((res) => {
          this.RetiredReasonForm?.form.patchValue(res);
        })
        )
        
      } else {
        this.resetForm();
        this.headerText = 'Add Retired Reason';
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
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }
  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }

  initaialRetiredReason(form?: NgForm) {
    if (form != null) form.resetForm();
    this.retiredReasonService.RetiredReason = {
      id: 0,
      name: '',
      idNeeded: false,
      remark: '',
      menuPosition: 0,
      isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.RetiredReasonForm?.form != null) {
      this.RetiredReasonForm.form.reset();
      this.RetiredReasonForm.form.patchValue({
        RetiredReasonId: 0,
        RetiredReasonName: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/personalInfoSetup/retired-reason']);
  }

  getAllRetiredReasons() {
    this.subscription.push(
    this.retiredReasonService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.retiredReasonService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.retiredReasonService.update(id, form.value)
      : this.retiredReasonService.submit(form.value);
    
    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllRetiredReasons();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/personalInfoSetup/retired-reason']);
        }
    this.loading = false;
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
      
    this.loading = false;
    })
    )
      
  }
  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.retiredReasonService.delete(element.id).subscribe(
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