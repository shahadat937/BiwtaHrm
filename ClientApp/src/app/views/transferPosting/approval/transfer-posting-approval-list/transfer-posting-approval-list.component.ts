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
import { TransferPostingApprovalComponent } from '../transfer-posting-approval/transfer-posting-approval.component';

@Component({
  selector: 'app-transfer-posting-approval-list',
  templateUrl: './transfer-posting-approval-list.component.html',
  styleUrl: './transfer-posting-approval-list.component.scss'
})
export class TransferPostingApprovalListComponent  implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    'PMS Id',
    'fullName',
    'ApprovalStatus',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  
  constructor(
    private toastr: ToastrService,
    public empTransferPostingService: EmpTransferPostingService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell, cilViewModule };


  ngOnInit(): void {
    this.getAllEmpTransferPostingApproveInfo();
  }
  
  getAllEmpTransferPostingApproveInfo() {
    this.subscription.push(
    this.empTransferPostingService.getAllEmpTransferPostingApproveInfo().subscribe((item) => {
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
  transferPostingApproval(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(TransferPostingApprovalComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      this.subscription.push(
      modalRef.onHide.subscribe(() => {
        this.getAllEmpTransferPostingApproveInfo();
      })
      )
     
    }
  }

}
