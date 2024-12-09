import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell, cilViewModule } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpTransferPostingService } from '../../service/emp-transfer-posting.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { TransferPostingInfoComponent } from '../../transfer-posting-info/transfer-posting-info.component';
import { DepartmentApprovalComponent } from '../department-approval/department-approval.component';

@Component({
  selector: 'app-department-approval-list',
  templateUrl: './department-approval-list.component.html',
  styleUrl: './department-approval-list.component.scss'
})
export class DepartmentApprovalListComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    // 'slNo',
    'PMS Id',
    'fullName',
    'transferFrom',
    'transferTo',
    'ApprovalStatus',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  loginEmpId: number = 0;
  
  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell, cilViewModule };


  ngOnInit(): void {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId ?? 0;

    this.getAllEmpTransferPostingDeptApproveInfo();
  }

  getAllEmpTransferPostingDeptApproveInfo() {
    this.subscription.push(
    this.empTransferPostingService.getAllEmpTransferPostingDeptApproveInfo(this.loginEmpId).subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    })
    )
   
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe())
    }
  }

  
  transferPostingInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(TransferPostingInfoComponent, { initialState, backdrop: 'static' });
  }

  
  transferPostingDeptApproval(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(DepartmentApprovalComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      this.subscription.push(
        modalRef.onHide.subscribe(() => {
          this.getAllEmpTransferPostingDeptApproveInfo();
        })
      )
     
    }
  }

}
