import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { EmpPromotionIncrementService } from '../service/emp-promotion-increment.service';
import { PromotionIncrementInfoComponent } from '../promotion-increment-info/promotion-increment-info.component';

@Component({
  selector: 'app-manage-promotion',
  templateUrl: './manage-promotion.component.html',
  styleUrl: './manage-promotion.component.scss'
})
export class ManagePromotionComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'PMS Id',
    'fullName',
    'ApprovalStatus',
    // 'ApplicationStatus',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  
  constructor(
    private toastr: ToastrService,
    public empPromotionIncrementService: EmpPromotionIncrementService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    this.getAllPromotionIncrementInfo();
  }

  getAllPromotionIncrementInfo() {
    this.subscription = this.empPromotionIncrementService.getAll().subscribe((item) => {
      this.dataSource = new MatTableDataSource(item);
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.matSort;
    });
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.dataSource.filter = filterValue;
  }
  
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  promotionIncrementInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(PromotionIncrementInfoComponent, { initialState, backdrop: 'static' });
  }
}
