import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { EmpTransferPostingService } from '../service/emp-transfer-posting.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { MatSort } from '@angular/material/sort';
import { TransferPostingInfoComponent } from '../transfer-posting-info/transfer-posting-info.component';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ConfirmService } from 'src/app/core/service/confirm.service';

@Component({
  selector: 'app-trainsfer-posting-list',
  templateUrl: './trainsfer-posting-list.component.html',
  styleUrl: './trainsfer-posting-list.component.scss'
})
export class TrainsferPostingListComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'PMS Id',
    'fullName',
    // 'ApprovalStatus',
    'DeptStatus',
    'JoiningStatus',
    // 'ApplicationStatus',
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
    private confirmService: ConfirmService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    this.getAllTransferPostingInfo();
  }

  getAllTransferPostingInfo() {
    this.subscription = this.empTransferPostingService.getAll().subscribe((item) => {
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

  transferPostingInfo(id: number) {
    const initialState = {
      id: id
    };
    const modalRef: BsModalRef = this.modalService.show(TransferPostingInfoComponent, { initialState, backdrop: 'static' });
  }

  delete(element: any) {
    this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.empTransferPostingService.deleteEmpTransferPosting(element.id).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
              this.toastr.success('Delete sucessfully ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          );
        }
      });
  }



}
