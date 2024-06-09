import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { DeptReleaseInfo } from '../../basic-setup/model/dept-release-info';
import { PostingOrderInfo } from '../../basic-setup/model/posting-order-info';
import { TransferApproveInfo } from '../../basic-setup/model/transfer-approve-info';
import { DeptReleaseInfoService } from '../../basic-setup/service/dept-release-info.service';
import { PostingOrderInfoService } from '../../basic-setup/service/posting-order-info.service';
import { TransferApproveInfoService } from '../../basic-setup/service/transfer-approve-info.service';
import { EmpTnsferPostingJoinService } from '../../basic-setup/service/emp-tnsfer-posting-join.service';
import { EmpTnsferPostingJoin } from '../../basic-setup/model/emp-tnsfer-posting-join';

@Component({
  selector: 'app-employe-posting-join-list',
  templateUrl: './employe-posting-join-list.component.html',
  styleUrl: './employe-posting-join-list.component.scss'
})
export class EmployePostingJoinListComponent implements OnInit, OnDestroy, AfterViewInit {
  depReleaseInfoId: number | null = null;
  position = 'top-end';
  visible = false;
  percentage = 0;
  btnText: string | undefined;
  @ViewChild('EmpTransferPostingJoinForm', { static: true }) EmpTransferPostingJoinForm!: NgForm;
  subscription: Subscription = new Subscription();
  displayedColumns: string[] = ['slNo', 'departmentName', 'officeName', 'designationName', 'approveBy', 'approveDate', 'depClearance','officeOrderDate', 'Action'];

  depReleasedisplayedColumns: string[] = ['slNo', 'approveStatus','joinDate','remarks','Action'];

  dataSource = new MatTableDataSource<any>();
  employeJoindataSource = new MatTableDataSource<any>();

  @ViewChild(MatPaginator)paginator!: MatPaginator;
  @ViewChild(MatSort)matSort!: MatSort;
  @ViewChild('postingPaginator') postingPaginator!: MatPaginator;
  @ViewChild('postingSort') postingSort!: MatSort;
  @ViewChild('approvePaginator') approvePaginator!: MatPaginator;
  @ViewChild('approveSort') approveSort!: MatSort;
  transferApproveInfo: TransferApproveInfo[] = [];
  postingOrderInfo: PostingOrderInfo[] = [];
  deptReleaseInfo:DeptReleaseInfo[]=[];
  empTnsferPostingJoin: EmpTnsferPostingJoin[] = [];

  constructor(
    public postingOrderInfoService: PostingOrderInfoService,
    public transferApproveInfoService: TransferApproveInfoService,
    public deptReleaseInfoService: DeptReleaseInfoService,
    public empTnsferPostingJoinService: EmpTnsferPostingJoinService,

    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService
  ) {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('empTnsferPostingJoinId');
      if (id) {
        this.btnText = 'Update';
        this.empTnsferPostingJoinService.find(+id).subscribe((res) => {
          this.EmpTransferPostingJoinForm?.form.patchValue(res);
        });
      } else {
        this.btnText = 'Submit';
      }
    });
  }

  ngOnInit(): void {
    this.getDepReleaseList();
    this.getTransferApprovedList();
    this.loadEmpTnsferPostingJoins();
    this.getTransferOrderList();
    this.route.paramMap.subscribe(params => {
      this.depReleaseInfoId = +params.get('depReleaseInfoId')!;
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
 
  loadEmpTnsferPostingJoins() {
    this.subscription = this.empTnsferPostingJoinService.getempTnsferPostingJoinAll().subscribe((h) => {
      this.empTnsferPostingJoin = h;
      console.log(h)
      this.employeJoindataSource.data = h;
      this.employeJoindataSource.paginator = this.approvePaginator;
      this.employeJoindataSource.sort = this.approveSort;
    });
  }
  getTransferOrderList() {
    this.subscription = this.postingOrderInfoService.getAll().subscribe((item) => {
      this.postingOrderInfo = item;
      this.mergeData();
    });
  }
  getTransferApprovedList() {
    this.subscription = this.transferApproveInfoService.getTransferApproveInfoAll().subscribe((res) => {
      this.transferApproveInfo= res;
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
    if (!this.postingOrderInfo.length || !this.transferApproveInfo.length || !this.deptReleaseInfo.length) {
      return; // Ensure all datasets are loaded
    }
    const mergedData = this.postingOrderInfo.map(posting => {
      const transferApprove = this.transferApproveInfo.find(transfer => transfer.postingOrderInfoId === posting.postingOrderInfoId) || {} as TransferApproveInfo;
      const deptRelease = this.deptReleaseInfo.find(dept => dept.transferApproveInfoId === transferApprove.transferApproveInfoId) || {} as DeptReleaseInfo;
      return {
        ...posting,
        ...transferApprove,
        ...deptRelease,
      };
    });
    this.dataSource = new MatTableDataSource(mergedData);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.matSort;
  }
}