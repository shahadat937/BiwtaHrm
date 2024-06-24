// import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
// import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
// import { Subscription } from 'rxjs';
// import { MatTableDataSource } from '@angular/material/table';
// import { MatPaginator } from '@angular/material/paginator';
// import { MatSort } from '@angular/material/sort';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ConfirmService } from 'src/app/core/service/confirm.service';
// import { ToastrService } from 'ngx-toastr';
// import { NgForm } from '@angular/forms';

// @Component({
//   selector: 'app-transferlist',
//   templateUrl: './transferlist.component.html',
//   styleUrl: './transferlist.component.scss'
// })
// export class TransferlistComponent implements OnInit, OnDestroy, AfterViewInit {
//   postingOrderInfoId: number | null = null;
//   position = 'top-end';
//   visible = false;
//   percentage = 0;
//   btnText: string | undefined;
//   @ViewChild('postingOrderForm', { static: true }) postingOrderForm!: NgForm;
//   subscription: Subscription = new Subscription();
//   displayedColumns: string[] = ['slNo', 'officeOrderNo',"departmentName" ,"officeName","designationName",'officeOrderDate','transferSection','releaseType','isActive', 'Action'];
//   dataSource = new MatTableDataSource<any>();
//   @ViewChild(MatPaginator)
//   paginator!: MatPaginator;
//   @ViewChild(MatSort)
//   matSort!: MatSort;
//   constructor(
//     public postingOrderInfoService: PostingOrderInfoService,
//     private route: ActivatedRoute,
//     private router: Router,
//     private confirmService: ConfirmService,
//     private toastr: ToastrService
//   ) {
//     this.route.paramMap.subscribe((params) => {
//       const id = params.get('postingOrderInfoId');
//       if (id) {
//         this.btnText = 'Update';
//         this.postingOrderInfoService.find(+id).subscribe((res) => {
//           this.postingOrderForm?.form.patchValue(res);
//         });
//       } else {
//         this.btnText = 'Submit';
//       }
//     });
//   }
//   ngOnInit(): void {
//     this.getTransferEmployes();
//     this.route.paramMap.subscribe(params => {
//       this.postingOrderInfoId = +params.get('postingOrderInfoId')!;
//     });

//   }
//   ngAfterViewInit() {
//     this.dataSource.paginator = this.paginator;
//     this.dataSource.sort = this.matSort;
//   }
//   ngOnDestroy() {
//     if (this.subscription) {
//       this.subscription.unsubscribe();
//     }
//   }
//   applyFilter(filterValue: string) {
//     filterValue = filterValue.trim();
//     filterValue = filterValue.toLowerCase();
//     this.dataSource.filter = filterValue;
//   }
//   toggleToast() {
//     this.visible = !this.visible;
//   }

//   onVisibleChange($event: boolean) {
//     this.visible = $event;
//     this.percentage = !this.visible ? 0 : this.percentage;
//   }

//   onTimerChange($event: number) {
//     this.percentage = $event * 25;
//   }
//   initaialWard(form?: NgForm) {
//     if (form != null) form.resetForm();
//     this.postingOrderInfoService.postingOrderInfos = {
//       postingOrderInfoId:   0,
//       empId:0,
//       departmentId:null,
//       subBranchId:0,
//       subDepartmentId:0,
//       designationId:null,
//       officeId: null,
//       designationName:"",
//     officeName:"",
//     departmentName:"",
//       officeOrderNo:"",
//       officeOrderDate:new Date(),
//       orderOfficeBy:"",
//       transferSection:"",
//       releaseType:"",
//       menuPosition:  0,
//       isActive:  true

//     };
//   }
//   resetForm() {

//     this.btnText = 'Submit';
//     if (this.postingOrderForm?.form != null) {
  
//       this.postingOrderForm.form.reset();
//       this.postingOrderForm.form.patchValue({
//         postingOrderInfoId:   0,
//         empId:0,
//         departmentId:null,
//         subBranchId:0,
//         subDepartmentId:0,
//         designationId:null,
//         officeId: null,
//         designationName:"",
//     officeName:"",
//     departmentName:"",
//         officeOrderNo:"",
//         officeOrderDate:new Date(),
//         orderOfficeBy:"",
//         transferSection:"",
//         releaseType:"",
//         menuPosition:  0,
//         isActive:  true
//       });
//     }
//   }

//   getTransferEmployes() {
//     this.subscription = this.postingOrderInfoService.getAll().subscribe((item) => {
//       this.dataSource = new MatTableDataSource(item);
//       this.dataSource.paginator = this.paginator;
//       this.dataSource.sort = this.matSort;
//     });
//   }
//   onSubmit(form: NgForm): void {
//     this.postingOrderInfoService.cachedData = [];
//     const id = form.value.postingOrderInfoId;
//     //console.log(form.value)
//     const action$ = id
//       ? this.postingOrderInfoService.update(id, form.value)
//       : this.postingOrderInfoService.submit(form.value);

//     this.subscription = action$.subscribe((response: any) => {
//       if (response.success) {
//         //  const successMessage = id ? 'Update' : 'Successfully';
//         this.toastr.success('', `${response.message}`, {
//           positionClass: 'toast-top-right',
//         });
//         this.getTransferEmployes();
//         this.resetForm();
//         if (!id) {
//           this.router.navigate(['/transfer/TransferOrderList']);
//         }
//       } else {
//         this.toastr.warning('', `${response.message}`, {
//           positionClass: 'toast-top-right',
//         });
//       }
//     });
//   }
//   delete(element: any) {
//     this.confirmService
//       .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
//       .subscribe((result) => {
//         if (result) {
//           console.log(result)
//           this.postingOrderInfoService.delete(element.postingOrderInfoId).subscribe(
//             (res) => {
//               const index = this.dataSource.data.indexOf(element);
//               if (index !== -1) {
//                 this.dataSource.data.splice(index, 1);
//                 this.dataSource = new MatTableDataSource(this.dataSource.data);
//               }
//               this.toastr.success('Delete sucessfully ! ', ` `, {
//                 positionClass: 'toast-top-right',
//               });
//             },
//             (err) => {
//               // console.log(err);

//               this.toastr.error('Somethig Wrong ! ', ` `, {
//                 positionClass: 'toast-top-right',
//               });
//             }
//           );
//         }
//       });
//   }
// }
// import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
// import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
// import { Subscription } from 'rxjs';
// import { MatTableDataSource } from '@angular/material/table';
// import { MatPaginator } from '@angular/material/paginator';
// import { MatSort } from '@angular/material/sort';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ConfirmService } from 'src/app/core/service/confirm.service';
// import { ToastrService } from 'ngx-toastr';
// import { NgForm } from '@angular/forms';
// import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';

// interface PostingOrderInfoWithStatus extends PostingOrderInfo {
//   status: 'Pending' | 'Approved' | 'Rejected';
// }

// @Component({
//   selector: 'app-transferlist',
//   templateUrl: './transferlist.component.html',
//   styleUrls: ['./transferlist.component.scss']
// })
// export class TransferlistComponent implements OnInit, OnDestroy, AfterViewInit {
//   postingOrderInfoId: number | null = null;
//   position = 'top-end';
//   visible = false;
//   percentage = 0;
//   btnText: string | undefined;
//   @ViewChild('postingOrderForm', { static: true }) postingOrderForm!: NgForm;
//   subscription: Subscription = new Subscription();
//   displayedColumns: string[] = ['slNo', 'officeOrderNo', 'departmentName', 'officeName', 'designationName', 'officeOrderDate', 'transferSection', 'releaseType', 'Action'];
//   dataSource = new MatTableDataSource<PostingOrderInfoWithStatus>();
//   @ViewChild(MatPaginator)
//   paginator!: MatPaginator;
//   @ViewChild(MatSort)
//   matSort!: MatSort;
//   postingOrderInfos: PostingOrderInfoWithStatus[] = [];

//   constructor(
//     public postingOrderInfoService: PostingOrderInfoService,
//     private route: ActivatedRoute,
//     private router: Router,
//     private confirmService: ConfirmService,
//     private toastr: ToastrService
//   ) {
//     this.route.paramMap.subscribe((params) => {
//       const id = params.get('postingOrderInfoId');
//       if (id) {
//         this.btnText = 'Update';
//         this.postingOrderInfoService.find(+id).subscribe((res) => {
//           this.postingOrderForm?.form.patchValue(res);
//         });
//       } else {
//         this.btnText = 'Submit';
//       }
//     });
//   }

//   filterByStatus(status: 'Approved' | 'Pending' | 'Rejected') {
//     this.dataSource.filterPredicate = (data: PostingOrderInfoWithStatus, filter: string) => {
//       console.log(data)
//       return data.status === filter;
//     };
//     this.dataSource.filter = status;
//     localStorage.setItem('filterStatus', status); // Save filter status to localStorage
//   }

  
//   filterPending() {
//     this.dataSource.filterPredicate = (data: PostingOrderInfoWithStatus, filter: string) => {
//       return data.status !== 'Approved' && data.status !== 'Rejected';
//     };
//     this.dataSource.filter = 'Pending';
//   }
//   ngOnInit(): void {
//     this.getTransferEmployees();
//     this.route.paramMap.subscribe(params => {
//       this.postingOrderInfoId = +params.get('postingOrderInfoId')!;
//     });
//     const savedFilterStatus = localStorage.getItem('filterStatus');
//     if (savedFilterStatus) {
//       // Apply saved filter status
//       if (savedFilterStatus === 'Pending') {
//         this.filterPending();

//       } else {
//         this.filterByStatus(savedFilterStatus as 'Approved' | 'Rejected');
//       }
//     } else {
//       this.getTransferEmployees(); // Load data initially
//     }
//   }

//   approve(element: PostingOrderInfoWithStatus) {
//     element.status = 'Approved';
//     this.toastr.success('Approved successfully', '', { positionClass: 'toast-top-right' });
//   }

//   reject(element: PostingOrderInfoWithStatus) {
//     element.status = 'Rejected';
//     this.toastr.error('Rejected successfully', '', { positionClass: 'toast-top-right' });
//   }

//   ngAfterViewInit() {
//     this.dataSource.paginator = this.paginator;
//     this.dataSource.sort = this.matSort;
//   }

//   ngOnDestroy() {
//     if (this.subscription) {
//       this.subscription.unsubscribe();
//     }
//   }

//   applyFilter(filterValue: string) {
//     filterValue = filterValue.trim().toLowerCase();
//     this.dataSource.filter = filterValue;
//   }

//   toggleToast() {
//     this.visible = !this.visible;
//   }

//   onVisibleChange($event: boolean) {
//     this.visible = $event;
//     this.percentage = !this.visible ? 0 : this.percentage;
//   }

//   onTimerChange($event: number) {
//     this.percentage = $event * 25;
//   }

//   initialWard(form?: NgForm) {
//     if (form != null) form.resetForm();
//     this.postingOrderInfoService.postingOrderInfos = {
//       postingOrderInfoId: 0,
//       empId: 0,
//       departmentId: null,
//       subBranchId: 0,
//       subDepartmentId: 0,
//       designationId: null,
//       officeId: null,
//       designationName: "",
//       officeName: "",
//       departmentName: "",
//       officeOrderNo: "",
//       officeOrderDate: new Date(),
//       orderOfficeBy: "",
//       transferSection: "",
//       releaseType: "",
//       menuPosition: 0,
//       isActive: true,
//       status: 'Pending' as 'Pending' // Type assertion here
//     };
//   }

//   resetForm() {
//     this.btnText = 'Submit';
//     if (this.postingOrderForm?.form != null) {
//       this.postingOrderForm.form.reset();
//       this.postingOrderForm.form.patchValue({
//         postingOrderInfoId: 0,
//         empId: 0,
//         departmentId: null,
//         subBranchId: 0,
//         subDepartmentId: 0,
//         designationId: null,
//         officeId: null,
//         designationName: "",
//         officeName: "",
//         departmentName: "",
//         officeOrderNo: "",
//         officeOrderDate: new Date(),
//         orderOfficeBy: "",
//         transferSection: "",
//         releaseType: "",
//         menuPosition: 0,
//         isActive: true,
//         status: 'Pending' as 'Pending' // Type assertion here
//       });
//     }
//   }

//   getTransferEmployees() {
//     this.subscription = this.postingOrderInfoService.getAll().subscribe((items) => {
//       const dataWithStatus: PostingOrderInfoWithStatus[] = items.map(item => ({
//         ...item,
//         status: 'Pending' as 'Pending' // Type assertion here
//       }));
//       this.dataSource = new MatTableDataSource(dataWithStatus);
//       this.dataSource.paginator = this.paginator;
//       this.dataSource.sort = this.matSort;
//     });
//   }


import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { Subscription } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { ToastrService } from 'ngx-toastr';
import { NgForm } from '@angular/forms';
import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';

interface PostingOrderInfoWithStatus extends PostingOrderInfo {
  status: 'Pending' | 'Approved' | 'Rejected';
}

@Component({
  selector: 'app-transferlist',
  templateUrl: './transferlist.component.html',
  styleUrls: ['./transferlist.component.scss']
})
export class TransferlistComponent implements OnInit, OnDestroy, AfterViewInit {
  postingOrderInfoId: number | null = null;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('postingOrderForm', { static: true }) postingOrderForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'officeOrderNo', 'departmentName', 'officeName', 'designationName', 'officeOrderDate', 'transferSection', 'releaseType', 'Action'];
  dataSource = new MatTableDataSource<PostingOrderInfoWithStatus>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  postingOrderInfos: PostingOrderInfoWithStatus[] = [];

  constructor(
    public postingOrderInfoService: PostingOrderInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getTransferEmployees();
    this.route.paramMap.subscribe(params => {
      this.postingOrderInfoId = +params.get('postingOrderInfoId')!;
    });

    // Load filter status from localStorage
    const savedFilterStatus = localStorage.getItem('filterStatus');
    if (savedFilterStatus) {
      if (savedFilterStatus === 'Pending') {
        this.filterPending();
      } else {
        this.filterByStatus(savedFilterStatus as 'Approved' | 'Rejected');
      }
    }
  }

  filterByStatus(status: 'Approved' | 'Pending' | 'Rejected') {
    this.dataSource.filterPredicate = (data: PostingOrderInfoWithStatus, filter: string) => {
      return data.status === filter;
    };
    this.dataSource.filter = status;
    localStorage.setItem('filterStatus', status);
  }

  filterPending() {
    this.dataSource.filterPredicate = (data: PostingOrderInfoWithStatus, filter: string) => {
      return data.status !== 'Approved' && data.status !== 'Rejected';
    };
    this.dataSource.filter = 'Pending';
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

  approve(element: PostingOrderInfoWithStatus) {
    element.status = 'Approved';
    this.toastr.success('Approved successfully', '', { positionClass: 'toast-top-right' });
  }

  reject(element: PostingOrderInfoWithStatus) {
    element.status = 'Rejected';
    this.toastr.error('Rejected successfully', '', { positionClass: 'toast-top-right' });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim().toLowerCase();
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

  initialWard(form?: NgForm) {
    if (form != null) form.resetForm();
    this.postingOrderInfoService.postingOrderInfos = {
      postingOrderInfoId: 0,
      empId: 0,
      departmentId: null,
      subBranchId: 0,
      subDepartmentId: 0,
      designationId: null,
      officeId: null,
      designationName: "",
      officeName: "",
      departmentName: "",
      officeOrderNo: "",
      officeOrderDate: new Date(),
      orderOfficeBy: "",
      transferSection: "",
      releaseType: "",
      menuPosition: 0,
      isActive: true,
      status: 'Pending' as 'Pending'
    };
  }

  resetForm() {
    this.btnText = 'Submit';
    if (this.postingOrderForm?.form != null) {
      this.postingOrderForm.form.reset();
      this.postingOrderForm.form.patchValue({
        postingOrderInfoId: 0,
        empId: 0,
        departmentId: null,
        subBranchId: 0,
        subDepartmentId: 0,
        designationId: null,
        officeId: null,
        designationName: "",
        officeName: "",
        departmentName: "",
        officeOrderNo: "",
        officeOrderDate: new Date(),
        orderOfficeBy: "",
        transferSection: "",
        releaseType: "",
        menuPosition: 0,
        isActive: true,
        status: 'Pending' as 'Pending'
      });
    }
  }

  getTransferEmployees() {
    this.subscription = this.postingOrderInfoService.getAll().subscribe((items) => {
      const dataWithStatus: PostingOrderInfoWithStatus[] = items.map(item => ({
        ...item,
        status: 'Pending' as 'Pending'
      }));
      this.dataSource = new MatTableDataSource(dataWithStatus);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }


  onSubmit(form: NgForm): void {
    this.postingOrderInfoService.cachedData = [];
    const id = form.value.postingOrderInfoId;
    const action$ = id
      ? this.postingOrderInfoService.update(id, form.value)
      : this.postingOrderInfoService.submit(form.value);
      console.log(form.value)
    this.subscription = action$.subscribe((response: any) => {
      console.log(response)
      if (response.success) {
        this.toastr.success('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
        this.getTransferEmployees();
        this.resetForm();
        if (!id) {
          this.router.navigate(['/transfer/TransferOrderList']);
        }
      } else {
        this.toastr.warning('', `${response.message}`, {
          positionClass: 'toast-top-right',
        });
      }
    });
  }

  delete(element: PostingOrderInfoWithStatus) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This Item')
      .subscribe((result) => {
        if (result) {
          this.postingOrderInfoService.delete(element.postingOrderInfoId).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
                this.dataSource.paginator = this.paginator;  // Reassign paginator
                this.dataSource.sort = this.matSort;        // Reassign sort
              }
              this.toastr.success('Delete successfully!', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Something went wrong!', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }
}
