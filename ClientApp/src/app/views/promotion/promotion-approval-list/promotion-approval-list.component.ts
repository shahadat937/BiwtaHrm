import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { PromotionIncrementInfoComponent } from '../promotion-increment-info/promotion-increment-info.component';
import { EmpPromotionIncrementService } from '../service/emp-promotion-increment.service';
import { IncrementAndPromotionApprovalComponent } from '../increment-and-promotion-approval/increment-and-promotion-approval.component';

@Component({
  selector: 'app-promotion-approval-list',
  templateUrl: './promotion-approval-list.component.html',
  styleUrl: './promotion-approval-list.component.scss'
})
export class PromotionApprovalListComponent  implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    // 'slNo',
    'PMS Id',
    'fullName',
    'promotedFrom',
    'promotedTo',
    'basicPayFrom',
    'basicPayTo',
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
    public empPromotionIncrementService: EmpPromotionIncrementService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    const currentUserString = localStorage.getItem('currentUser');
    const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
    this.loginEmpId = currentUserJSON.empId ?? 0;
    this.getAllPromotionIncrementInfo();
  }

  getAllPromotionIncrementInfo() {
    // this.subscription = 
    this.subscription.push(
    this.empPromotionIncrementService.getAllEmpPromotionIncrementApproveInfo(this.loginEmpId).subscribe((item) => {
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

  promotionIncrementInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(PromotionIncrementInfoComponent, { initialState, backdrop: 'static' });
  }
  
  promotionIncrementApproval(id: number, clickedButton: string){
    const initialState = {
      id: id,
      clickedButton: clickedButton
    };
    const modalRef: BsModalRef = this.modalService.show(IncrementAndPromotionApprovalComponent, { initialState, backdrop: 'static' });

    if (modalRef.onHide) {
      this.subscription.push(
      modalRef.onHide.subscribe(() => {
        this.getAllPromotionIncrementInfo();
      })
      )
      
    }
  }
}
