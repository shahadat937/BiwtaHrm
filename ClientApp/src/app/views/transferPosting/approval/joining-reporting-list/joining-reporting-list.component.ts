import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpTransferPostingService } from '../../service/emp-transfer-posting.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { TransferPostingInfoComponent } from '../../transfer-posting-info/transfer-posting-info.component';
import { JoiningReportingComponent } from '../joining-reporting/joining-reporting.component';

@Component({
  selector: 'app-joining-reporting-list',
  templateUrl: './joining-reporting-list.component.html',
  styleUrl: './joining-reporting-list.component.scss'
})
export class JoiningReportingListComponent  implements OnInit, OnDestroy {

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

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId ?? 0;

    this.getAllEmpTransferPostingJoiningInfo();
  }

  getAllEmpTransferPostingJoiningInfo() {
    this.subscription.push(
    this.empTransferPostingService.getAllEmpTransferPostingJoiningInfo(this.loginEmpId).subscribe((item) => {
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

  
  transferPostingJoiningReporting(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(JoiningReportingComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      modalRef.onHide.subscribe(() => {
        this.getAllEmpTransferPostingJoiningInfo();
      });
    }
  }

}
