// import { AfterViewInit,Input, Component, EventEmitter, OnDestroy, OnInit, ViewChild } from '@angular/core';
// import { NgForm } from '@angular/forms';
// import { MatPaginator } from '@angular/material/paginator';
// import { MatSort } from '@angular/material/sort';
// import { MatTableDataSource } from '@angular/material/table';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ToastrService } from 'ngx-toastr';
// import { Subscription } from 'rxjs';
// import { ConfirmService } from 'src/app/core/service/confirm.service';
// import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
// import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
// import { EmployeesService } from '../service/employees.service';
// import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';
// import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
// import { Employee } from '../../basic-setup/model/employees';
// import { EmpModalComponent } from '../emp-modal/emp-modal.component';
// import { BsModalService } from 'ngx-bootstrap/modal';
// import { BsModalRef } from 'ngx-bootstrap/modal';
// @Component({
//   selector: 'app-transfer-approved-list',
//   templateUrl: './transfer-approved-list.component.html',
//   styleUrl: './transfer-approved-list.component.scss'
// })
// export class TransferApprovedListComponent implements OnInit, OnDestroy, AfterViewInit {
//   cachedData: any[] = [];
//   visible1: boolean | undefined;
//   userBtnText: string | undefined;
//   postingOrderInfoId: number | null = null;
//   position = 'top-end';
//   visible = false;
//   percentage = 0;
//   btnText: string | undefined;
//   btnTextApproved: string | undefined;
//   @ViewChild('TransferApproveInfoForm', { static: true }) TransferApproveInfoForm!: NgForm;
//   subscription: Subscription = new Subscription();
//   displayedColumns: string[] = ['slNo', "departmentName", "officeName", "designationName", 'officeOrderDate', 'transferSection', 'releaseType', 'Action'];

//   approveDisplayedColumns: string[] = ['transferApproveInfoId','approveStatus','approveDate','remarks' ,'Action']

//   dataSource = new MatTableDataSource<any>();
//   dataSourceApproved=new MatTableDataSource<any>();

//   @ViewChild(MatPaginator)
//   paginator!: MatPaginator;
//   @ViewChild(MatSort)
//   matSort!: MatSort;
//   transferApproveInfo: TransferApproveInfo[] = [];
//   postingOrderInfos: PostingOrderInfo[]=[];

//   @Input() employeeSelected = new EventEmitter<Employee>();
//   constructor(
//     private modalService: BsModalService,
//     public postingOrderInfoService: PostingOrderInfoService,
//     public transferApproveInfoService: TransferApproveInfoService,
//     public employeeService: EmployeesService,
//     private route: ActivatedRoute,
//     private router: Router,
//     private confirmService: ConfirmService,
//     private toastr: ToastrService
//   ) {
//     this.route.paramMap.subscribe((params) => {
//       this.btnText = 'Submit';
//       const id = params.get('transferApproveInfoId');
//       if (id) {
//         this.btnTextApproved = 'Update';
//         this.transferApproveInfoService.find(+id).subscribe((res) => {
//           this.TransferApproveInfoForm?.form.patchValue(res);
//         });
//       } else {
//         this.btnTextApproved = 'Submit';
//       }

//     });
//   }

//   ngOnInit(): void {
//     this.loadTransferApproveInfos();
//     this.getTransferApprovedStatus();
//     this.mergeData();
//     this.getTransferApproved();
//     this.getTransferPostingOrder();
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

//   //Approved data show
//   getTransferApprovedStatus() {
//     this.subscription.add(
//       this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res) => {
//         this.transferApproveInfo = res;
//         this.dataSourceApproved.data = res;
//         this.dataSourceApproved.paginator = this.paginator;
//         this.dataSourceApproved.sort = this.matSort;
//       })
//     );
//   }

//   //Employee/Transfer
//   openApproveEmpTransferJoin(): void {
//     const modalRef: BsModalRef = this.modalService.show(EmpModalComponent);
//     modalRef.content?.employeeSelected.subscribe((selectedEmployee: Employee) => {
//       this.handleApproveEmpTransferJoin(selectedEmployee);
//     });
//   }
//   handleApproveEmpTransferJoin(employee: Employee) {
//     this.transferApproveInfoService.transferApproveInfos.approveBy= employee.empId,
//     this.transferApproveInfoService.transferApproveInfos.approveByName= employee.employeeName
//   }


//   getTransferPostingOrder() {
//     this.subscription.add(
//       this.postingOrderInfoService.getAll().subscribe((res) => {
//         this.postingOrderInfos = res;
//         this.mergeData();
//       })
//     );
//   }

//   getTransferApproved() {
//     this.subscription.add(
//       this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res) => {
//         this.transferApproveInfo = res;
//         this.mergeData();
//       })
//     );
//   }

//   mergeData() {
//     const mergedData = this.postingOrderInfos.map(posting => {
//       const transferApprove = this.transferApproveInfo.find(transfer => transfer.postingOrderInfoId === posting.postingOrderInfoId) || {};
//       return {
//         ...posting,
//         ...transferApprove,
//       };
//     });

//     // Add any transferApproveInfo items that don't have a matching postingOrderInfo
//     this.transferApproveInfo.forEach(transfer => {
//       if (!this.postingOrderInfos.some(posting => posting.postingOrderInfoId === transfer.postingOrderInfoId)) {
//         mergedData.push();
//       }
//     });
//     this.dataSource.data = mergedData;
//     this.dataSource.paginator = this.paginator;
//     this.dataSource.sort = this.matSort;
//   }



//   // transferApproveInfos

//   initaialtransferApproveInfo(form?: NgForm) {
//     if (form != null) form.resetForm();
//     this.transferApproveInfoService.transferApproveInfos = {
//       transferApproveInfoId: 0,
//       postingOrderInfoId: 0,
//       empId: 0,
//       approveStatus: true,
//       approveByName: "",
//       approveBy: 0,
//       approveDate: new Date(),
//       remarks: "",
//       menuPosition: 0,
//       isActive: true
//     };
//   }
//   resetFormTransfer() {
//     this.btnText = 'Submit';
//     if (this.TransferApproveInfoForm?.form != null) {
//       this.TransferApproveInfoForm.form.reset();
//       this.TransferApproveInfoForm.form.patchValue({
//         transferApproveInfoId: 0,
//         postingOrderInfoId: 0,
//         empId: 0,
//         approveByName: "",
//         approveBy: 0,
//         approveStatus: true,
//         approveDate: new Date(),
//         remarks: "",
//         menuPosition: 0,
//         isActive: true
//       });
//     }
//     this.router.navigate(['/transfer/transferApproveInfoList']);

//   }
//   loadTransferApproveInfos() {
//     this.subscription = this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((h) => {
//       this.transferApproveInfo = h;
//     });
//   }

// onSubmit(form: NgForm): void {
//   this.transferApproveInfoService.cachedData = [];
//   const id = form.value.transferApproveInfoId;
//   const action$ = id
//     ? this.transferApproveInfoService.update(id, form.value)
//     : this.transferApproveInfoService.submit(form.value);
//   this.subscription = action$.subscribe((response: any) => {
//     if (response.success) {
//       //  const successMessage = id ? 'Update' : 'Successfully';
//       this.toastr.success('', `${response.message}`, {
//         positionClass: 'toast-top-right',
//       });
//       this.getTransferApprovedStatus();
//       this.resetFormTransfer();
//       if (!id) {
//         this.router.navigate(['/transfer/transferApproveInfoList']);

//       }
//     } else {
//       this.toastr.warning('', `${response.message}`, {
//         positionClass: 'toast-top-right',
//       });
//     }
//   });
// }

// delete(element: any) {
//   this.confirmService
//     .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
//     .subscribe((result) => {
//       if (result) {
//         this.transferApproveInfoService.delete(element.transferApproveInfoId).subscribe(
//           (res) => {
//             const index = this.dataSource.data.indexOf(element);
//             if (index !== -1) {
//               this.dataSource.data.splice(index, 1);
//               this.dataSource = new MatTableDataSource(this.dataSource.data);
//             }
//             this.toastr.success('Delete sucessfully ! ', ` `, {
//               positionClass: 'toast-top-right',
//             });
//           },
//           (err) => {
//             // console.log(err);

//             this.toastr.error('Somethig Wrong ! ', ` `, {
//               positionClass: 'toast-top-right',
//             });
//           }
//         );
//       }
//     });
// }
// }


// import { AfterViewInit, Component, EventEmitter, OnDestroy, OnInit, ViewChild, Input } from '@angular/core';
// import { NgForm } from '@angular/forms';
// import { MatPaginator } from '@angular/material/paginator';
// import { MatSort } from '@angular/material/sort';
// import { MatTableDataSource } from '@angular/material/table';
// import { ActivatedRoute, Router } from '@angular/router';
// import { ToastrService } from 'ngx-toastr';
// import { Subscription } from 'rxjs';
// import { ConfirmService } from 'src/app/core/service/confirm.service';
// import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
// import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
// import { EmployeesService } from '../service/employees.service';
// import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';
// import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
// import { Employee } from '../../basic-setup/model/employees';
// import { EmpModalComponent } from '../emp-modal/emp-modal.component';
// import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

// @Component({
//   selector: 'app-transfer-approved-list',
//   templateUrl: './transfer-approved-list.component.html',
//   styleUrls: ['./transfer-approved-list.component.scss']
// })
// export class TransferApprovedListComponent implements OnInit, OnDestroy, AfterViewInit {
//   @ViewChild('TransferApproveInfoForm', { static: true }) TransferApproveInfoForm!: NgForm;
//   @ViewChild('postingPaginator') postingPaginator!: MatPaginator;
//   @ViewChild('postingSort') postingSort!: MatSort;
//   @ViewChild('approvePaginator') approvePaginator!: MatPaginator;
//   @ViewChild('approveSort') approveSort!: MatSort;
//   @ViewChild(MatPaginator) paginator!: MatPaginator;
//   @ViewChild(MatSort) matSort!: MatSort;

//   subscription: Subscription = new Subscription();
//   dataSource = new MatTableDataSource<any>();
//   dataSourceApproved = new MatTableDataSource<any>();
//   postingOrderInfos: PostingOrderInfo[] = [];
//   transferApproveInfo: TransferApproveInfo[] = [];
//   approveDisplayedColumns: string[] = ['transferApproveInfoId', 'approveStatus', 'approveDate', 'remarks', 'Action'];
//   displayedColumns: string[] = ['slNo', 'departmentName', 'officeName', 'designationName', 'officeOrderDate', 'transferSection', 'releaseType', 'Action'];
//   btnTextApproved: string | undefined;
//   @Input() employeeSelected = new EventEmitter<Employee>();

//   constructor(
//     private modalService: BsModalService,
//     public postingOrderInfoService: PostingOrderInfoService,
//     public transferApproveInfoService: TransferApproveInfoService,
//     public employeeService: EmployeesService,
//     private route: ActivatedRoute,
//     private router: Router,
//     private confirmService: ConfirmService,
//     private toastr: ToastrService
//   ) {
//     this.route.paramMap.subscribe((params) => {
//       const id = params.get('transferApproveInfoId');
//       this.btnTextApproved = id ? 'Update' : 'Submit';
//       if (id) {
//         this.transferApproveInfoService.find(+id).subscribe((res) => {
//           this.TransferApproveInfoForm?.form.patchValue(res);
//         });
//       }
//     });
//   }

//   ngOnInit(): void {
//     this.getTransferPostingOrder();
//     this.getTransferApprovedStatus();
//   }

//   ngAfterViewInit() {
//     this.dataSource.paginator = this.paginator;
//     this.dataSource.sort = this.matSort;
//     this.dataSourceApproved.paginator = this.approvePaginator;
//     this.dataSourceApproved.sort = this.approveSort;
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

//   getTransferPostingOrder() {
//     this.subscription.add(
//       this.postingOrderInfoService.getAll().subscribe((res) => {
//         this.postingOrderInfos = res;
//         this.dataSource.data = res;
//         this.dataSource.paginator = this.postingPaginator;
//         this.dataSource.sort = this.postingSort;
//       })
//     );
//   }

//   getTransferApprovedStatus() {
//     this.subscription.add(
//       this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res) => {
//         this.transferApproveInfo = res;
//         this.dataSourceApproved.data = res;
//         this.dataSourceApproved.paginator = this.approvePaginator;
//         this.dataSourceApproved.sort = this.approveSort;
//       })
//     );
//   }

//   onSubmit(form: NgForm): void {
//     const id = form.value.transferApproveInfoId;
//     const action$ = id
//       ? this.transferApproveInfoService.update(id, form.value)
//       : this.transferApproveInfoService.submit(form.value);

//     this.subscription = action$.subscribe((response: any) => {
//       if (response.success) {
//         this.toastr.success('', `${response.message}`, {
//           positionClass: 'toast-top-right',
//         });
//         this.getTransferApprovedStatus();
//         this.resetFormTransfer();
//         if (!id) {
//           this.router.navigate(['/transfer/transferApproveInfoList']);
//         }
//       } else {
//         this.toastr.warning('', `${response.message}`, {
//           positionClass: 'toast-top-right',
//         });
//       }
//     });
//   }

//   delete(element: any) {
//     this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item').subscribe((result) => {
//       if (result) {
//         this.transferApproveInfoService.delete(element.transferApproveInfoId).subscribe(
//           (res) => {
//             const index = this.dataSource.data.indexOf(element);
//             if (index !== -1) {
//               this.dataSource.data.splice(index, 1);
//               this.dataSource = new MatTableDataSource(this.dataSource.data);
//             }
//             this.toastr.success('Delete successfully!', '', {
//               positionClass: 'toast-top-right',
//             });
//           },
//           (err) => {
//             this.toastr.error('Something went wrong!', '', {
//               positionClass: 'toast-top-right',
//             });
//           }
//         );
//       }
//     });
//   }

//   resetFormTransfer() {
//     this.btnTextApproved = 'Submit';
//     if (this.TransferApproveInfoForm?.form != null) {
//       this.TransferApproveInfoForm.form.reset();
//       this.TransferApproveInfoForm.form.patchValue({
//         transferApproveInfoId: 0,
//         postingOrderInfoId: 0,
//         empId: 0,
//         approveByName: '',
//         approveBy: 0,
//         approveStatus: true,
//         approveDate: new Date(),
//         remarks: '',
//         menuPosition: 0,
//         isActive: true
//       });
//     }
//     this.router.navigate(['/transfer/transferApproveInfoList']);
//   }
// }




import { AfterViewInit, Component, EventEmitter, OnDestroy, OnInit, ViewChild, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
import { EmployeesService } from '../service/employees.service';
import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';
import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
import { Employee } from '../../basic-setup/model/employees';
import { EmpModalComponent } from '../emp-modal/emp-modal.component';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
interface PostingOrderInfoWithStatus extends PostingOrderInfo {
  status: 'Pending' | 'Approved' | 'Rejected';
}
@Component({
  selector: 'app-transfer-approved-list',
  templateUrl: './transfer-approved-list.component.html',
  styleUrls: ['./transfer-approved-list.component.scss']
})
export class TransferApprovedListComponent implements OnInit, OnDestroy, AfterViewInit {
  @ViewChild('TransferApproveInfoForm', { static: true }) TransferApproveInfoForm!: NgForm;
  @ViewChild('postingPaginator') postingPaginator!: MatPaginator;
  @ViewChild('postingSort') postingSort!: MatSort;
  @ViewChild('approvePaginator') approvePaginator!: MatPaginator;
  @ViewChild('approveSort') approveSort!: MatSort;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;

  subscription: Subscription = new Subscription();
  dataSource = new MatTableDataSource<any>();
  dataSourceApproved = new MatTableDataSource<any>();
  postingOrderInfos: PostingOrderInfo[] = [];
  transferApproveInfo: TransferApproveInfo[] = [];
  approveDisplayedColumns: string[] = ['transferApproveInfoId', 'approveStatus', 'approveDate', 'remarks', 'Action'];
  displayedColumns: string[] = ['slNo', 'departmentName', 'officeName', 'designationName', 'officeOrderDate', 'transferSection', 'releaseType', 'Action'];
  btnTextApproved: string | undefined;
  @Input() employeeSelected = new EventEmitter<Employee>();
  btnText: string | undefined;
  constructor(
    private modalService: BsModalService,
    public postingOrderInfoService: PostingOrderInfoService,
    public transferApproveInfoService: TransferApproveInfoService,
    public employeeService: EmployeesService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      this.btnText = 'Submit';
      const id = params.get('transferApproveInfoId');
      if (id) {
        this.btnTextApproved = 'Update';
        this.transferApproveInfoService.find(+id).subscribe((res) => {
          this.TransferApproveInfoForm?.form.patchValue(res);
        });
      } else {
        this.btnTextApproved = 'Submit';
      }

    });
  }


ngOnInit(): void {
  this.getTransferPostingOrder();
  this.getTransferApprovedStatus();
}

ngAfterViewInit() {
  this.dataSource.paginator = this.paginator;
  this.dataSource.sort = this.matSort;
  this.dataSourceApproved.paginator = this.approvePaginator;
  this.dataSourceApproved.sort = this.approveSort;
}

ngOnDestroy() {
  if (this.subscription) {
    this.subscription.unsubscribe();
  }
}

applyFilter(filterValue: string) {
  filterValue = filterValue.trim().toLowerCase();
  this.dataSource.filter = filterValue;
}

getTransferPostingOrder() {
  this.subscription.add(
    this.postingOrderInfoService.getAll().subscribe((res) => {
      this.postingOrderInfos = res;
      this.dataSource.data = res;
      this.dataSource.paginator = this.postingPaginator;
      this.dataSource.sort = this.postingSort;
    })
  );
}

getTransferApprovedStatus() {
  this.subscription.add(
    this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res) => {
      this.transferApproveInfo = res;
      this.dataSourceApproved.data = res;
      this.dataSourceApproved.paginator = this.approvePaginator;
      this.dataSourceApproved.sort = this.approveSort;
    })
  );
}

 filterByStatus(status: 'Approved' | 'Pending' | 'Rejected') {
    this.dataSource.filterPredicate = (data: PostingOrderInfoWithStatus, filter: string) => {
      return data.status === filter;
    };
    this.dataSource.filter = status;
  }
  
onSubmit(form: NgForm): void {
  const id = form.value.transferApproveInfoId;
  const action$ = id
    ? this.transferApproveInfoService.update(id, form.value)
    : this.transferApproveInfoService.submit(form.value);

  this.subscription.add(action$.subscribe((response: any) => {
    if (response.success) {
      this.toastr.success('', `${response.message}`, {
        positionClass: 'toast-top-right',
      });
      this.getTransferApprovedStatus(); // Refresh data source
      this.resetFormTransfer();
      if (!id) {
        this.router.navigate(['/transfer/transferApproveInfoList']);
        this.router.navigate(['/transfer/transferApproveInfoList']);
      }
    } else {
      this.toastr.warning('', `${response.message}`, {
        positionClass: 'toast-top-right',
      });
    }
  }));
}

delete (element: any) {
  this.confirmService.confirm('Confirm delete message', 'Are You Sure Delete This Item').subscribe((result) => {
    if (result) {
      this.transferApproveInfoService.delete(element.transferApproveInfoId).subscribe(
        (res) => {
          const index = this.dataSourceApproved.data.indexOf(element);
          if (index !== -1) {
            this.dataSourceApproved.data.splice(index, 1);
            this.dataSourceApproved = new MatTableDataSource(this.dataSourceApproved.data);
            this.dataSourceApproved.paginator = this.approvePaginator; // Update paginator
            this.dataSourceApproved.sort = this.approveSort; // Update sorting
          }
          this.toastr.success('Delete successfully!', '', {
            positionClass: 'toast-top-right',
          });
        },
        (err) => {
          this.toastr.error('Something went wrong!', '', {
            positionClass: 'toast-top-right',
          });
        }
      );
    }
  });
}

resetFormTransfer() {
  this.btnTextApproved = 'Submit';
  if (this.TransferApproveInfoForm?.form != null) {
    this.TransferApproveInfoForm.form.reset();
    this.TransferApproveInfoForm.form.patchValue({
      transferApproveInfoId: 0,
      postingOrderInfoId: 0,
      empId: 0,
      approveByName: '',
      approveBy: 0,
      approveStatus: true,
      approveDate: new Date(),
      remarks: '',
      menuPosition: 0,
      isActive: true
    });
  }
  this.router.navigate(['/transfer/transferApproveInfoList']);
}

update(element: any) {
  this.router.navigate(['/transfer/transferApproveInfoList', element.transferApproveInfoId]);
}
}
