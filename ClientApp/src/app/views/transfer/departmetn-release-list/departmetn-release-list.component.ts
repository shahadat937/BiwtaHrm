import { DeptReleaseInfoService } from './../../basic-setup/service/dept-release-info.service';
import { TransferApproveInfo } from './../../basic-setup/model/transfer-approve-info';
import { AfterViewInit, Component, EventEmitter, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';

import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
import { DeptReleaseInfo } from '../../basic-setup/model/dept-release-info';


@Component({
  selector: 'app-departmetn-release-list',
  templateUrl: './departmetn-release-list.component.html',
  styleUrl: './departmetn-release-list.component.scss'
})
export class DepartmetnReleaseListComponent implements OnInit, OnDestroy, AfterViewInit {
  postingOrderInfoId: number | null = null;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('postingOrderForm', { static: true }) postingOrderForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'departmentName', 'officeName', 'designationName', 'approveBy', 'approveDate', 'approveStatus', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  transferApproveInfo: TransferApproveInfo[] = [];
  postingOrderInfo: PostingOrderInfo[] = [];
  deptReleaseInfo:DeptReleaseInfo[]=[];

  constructor(
    public postingOrderInfoService: PostingOrderInfoService,
    public transferApproveInfoService: TransferApproveInfoService,
    public deptReleaseInfoService: DeptReleaseInfoService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('postingOrderInfoId');
      if (id) {
        this.btnText = 'Update';
        this.postingOrderInfoService.find(+id).subscribe((res) => {
          this.postingOrderForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }
  ngOnInit(): void {
    this.getDepReleaseList();
    this.getTransferApprovedList();
    this.getTransferOrderList();
    this.route.paramMap.subscribe(params => {
      this.postingOrderInfoId = +params.get('postingOrderInfoId')!;
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
  
  getTransferOrderList() {
    this.subscription = this.postingOrderInfoService.getAll().subscribe((item) => {
      this.postingOrderInfo = item;
      this.mergeData();
    });
  }

  getTransferApprovedList() {
    this.subscription = this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res) => {
      this.transferApproveInfo= res
      this.mergeData();
    })
  }

  getDepReleaseList() {
    this.subscription = this.deptReleaseInfoService.getdeptReleaseInfoAll().subscribe((res) => {
      this.deptReleaseInfo = res;
      this.mergeData();
    })
  }

  mergeData() {
    if (!this.postingOrderInfo || !this.transferApproveInfo) {
      return; // Ensure both datasets are loaded
    }
    const mergedData = this.postingOrderInfo.map(posting => {
      const transferApprove = this.transferApproveInfo.find(transfer => transfer.postingOrderInfoId === posting.postingOrderInfoId);
      return {
        ...posting,
        ...transferApprove,
      };
    });

    this.dataSource = new MatTableDataSource(mergedData);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;

  }
}
