import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { OrderTypeService } from '../service/order-type.service';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-order-type',
  templateUrl: './order-type.component.html',
  styleUrl: './order-type.component.scss'
})
export class OrderTypeComponent implements OnInit, OnDestroy, AfterViewInit {
  btnText: string | undefined;
  headerText: string | undefined;
  @ViewChild('OrderTypeForm', { static: true }) OrderTypeForm!: NgForm;
  loading = false;
  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = ['slNo', 'name', 'nameBangla', 'isActive', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  constructor(
    public orderTypeService: OrderTypeService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
  }
  ngOnInit(): void {
    this.getAllOrderTypes();
    this.handleRouteParams();
  }
  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');
      if (id) {
        this.btnText = 'Update';
        this.headerText = 'Update Order Type';
        this.subscription.push(
        this.orderTypeService.find(+id).subscribe((res) => {
          this.OrderTypeForm?.form.patchValue(res);
        })
        )
        
      } else {
        this.resetForm();
        this.headerText = 'Add Order Type';
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

  initaialOrderType(form?: NgForm) {
    if (form != null) form.resetForm();
    this.orderTypeService.OrderType = {
        id: 0,
        typeName: '',
        typeNameBangla: '',
        remark: '',
        menuPosition: 0,
        isActive: true,
    };
  }
  resetForm() {
    this.btnText = 'Submit';
    if (this.OrderTypeForm?.form != null) {
      this.OrderTypeForm.form.reset();
      this.OrderTypeForm.form.patchValue({
        id: 0,
        typeName: '',
        typeNameBangla: '',
        remark: '',
        menuPosition: 0,
        isActive: true,
      });
    }
    this.router.navigate(['/officeSetup/order-type']);
  }

  getAllOrderTypes() {
    this.subscription.push(
    this.orderTypeService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
    
  }
  
  onSubmit(form: NgForm): void {
    this.loading = true;
    this.orderTypeService.cachedData = [];
    const id = form.value.id;
    const action$ = id
      ? this.orderTypeService.update(id, form.value)
      : this.orderTypeService.submit(form.value);
    
    this.subscription.push(
    action$.subscribe((response: any) => {
      if (response.success) {
        //  const successMessage = id ? '' : '';
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getAllOrderTypes();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/officeSetup/order-type']);
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
          this.orderTypeService.delete(element.id).subscribe(
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