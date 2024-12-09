import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { RewardPunishmentComponent } from '../reward-punishment/reward-punishment.component';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { FeaturePermission } from '../../featureManagement/model/feature-permission';
import { EmpRewardPunishmentService } from '../service/emp-reward-punishment.service';
import { RoleFeatureService } from '../../featureManagement/service/role-feature.service';

@Component({
  selector: 'app-reward-punishment-list',
  templateUrl: './reward-punishment-list.component.html',
  styleUrl: './reward-punishment-list.component.scss'
})
export class RewardPunishmentListComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    'pmsId',
    'name',
    'rewardPunishmentTypeName',
    'withdrawStatus',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  featurePermission : FeaturePermission = new FeaturePermission;
  
  constructor(
    private toastr: ToastrService,
    public empRewardPunishmentService: EmpRewardPunishmentService,
    public roleFeatureService: RoleFeatureService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private confirmService: ConfirmService,
    private router: Router,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    this.getPermission();
  }

  getAllEmpRewardPunishment() {
    this.subscription.push(
      this.empRewardPunishmentService.getAll().subscribe((item) => {
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


  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('rewardPunishment').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.getAllEmpRewardPunishment();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
   
  }


  editInfo(id: number, clickedButton: string){
    if(clickedButton == "Create" && this.featurePermission.add == true || clickedButton == "Edit" && this.featurePermission.update == true || clickedButton == "Withdraw"){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(RewardPunishmentComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        this.subscription.push(
        modalRef.onHide.subscribe(() => {
          this.getAllEmpRewardPunishment();
        })
        )
       
      }
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  delete(element: any) {
    if(this.featurePermission.delete == true){
      this.subscription.push(
      this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.subscription.push(
          this.empRewardPunishmentService.deleteEmpRewardPunishment(element.id).subscribe(
            (res) => {
              const index = this.dataSource.data.indexOf(element);
              this.toastr.warning('Delete Successfull', ` `, {
                positionClass: 'toast-top-right',
              });
              if (index !== -1) {
                this.dataSource.data.splice(index, 1);
                this.dataSource = new MatTableDataSource(this.dataSource.data);
              }
            },
            (err) => {
              this.toastr.error('Somethig Wrong ! ', ` `, {
                positionClass: 'toast-top-right',
              });
            }
          )
          )
          
        }
      })
      )
      
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

}