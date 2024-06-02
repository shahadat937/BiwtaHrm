import { PostingComponent } from './../posting/posting.component';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
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

@Component({
  selector: 'app-transfer-approved-list',
  templateUrl: './transfer-approved-list.component.html',
  styleUrl: './transfer-approved-list.component.scss'
})
export class TransferApprovedListComponent implements OnInit, OnDestroy, AfterViewInit {
  visible1: boolean | undefined;
  userBtnText: string | undefined;
  postingOrderInfoId: number | null = null;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('PostingAndTrnsForm', { static: true }) PostingAndTrnsForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', "departmentName", "officeName", "designationName", 'officeOrderDate', 'transferSection', 'releaseType', 'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  transferApproveInfo: TransferApproveInfo[] = [];
  postingOrderInfos: PostingOrderInfo[]=[];
  constructor(
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
    });
  }

  ngOnInit(): void {
    // this.getTransferEmployes();
    this.mergeData();
    this.getTransferApproved();
    this.getTransferPostingOrder();
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


  getTransferPostingOrder() {
    this.subscription.add(
      this.postingOrderInfoService.getAll().subscribe((res) => {
        this.postingOrderInfos = res;
        this.mergeData();
         console.log(res)
      })
    );
  }

  getTransferApproved() {
    this.subscription.add(
      this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res) => {
        this.transferApproveInfo = res;
        this.mergeData();
        console.log(res)
      })
    );
  }

  mergeData() {
    const mergedData = this.postingOrderInfos.map(posting => {
      const transferApprove = this.transferApproveInfo.find(transfer => transfer.postingOrderInfoId === posting.postingOrderInfoId) || {};
      return {
        ...posting,
        ...transferApprove,
      };
    });

    // Add any transferApproveInfo items that don't have a matching postingOrderInfo
    this.transferApproveInfo.forEach(transfer => {
      if (!this.postingOrderInfos.some(posting => posting.postingOrderInfoId === transfer.postingOrderInfoId)) {
        mergedData.push();
      }
    });

    this.dataSource.data = mergedData;
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
}
