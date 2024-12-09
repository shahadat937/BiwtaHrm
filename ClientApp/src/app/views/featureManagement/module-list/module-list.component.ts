import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { IncrementAndPromotionApprovalComponent } from '../../promotion/increment-and-promotion-approval/increment-and-promotion-approval.component';
import { PromotionIncrementInfoComponent } from '../../promotion/promotion-increment-info/promotion-increment-info.component';
import { FeatureManagementService } from '../service/feature-management.service';
import { CreateModuleComponent } from '../create-module/create-module.component';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RoleFeatureService } from '../service/role-feature.service';
import { FeaturePermission } from '../model/feature-permission';

@Component({
  selector: 'app-module-list',
  templateUrl: './module-list.component.html',
  styleUrl: './module-list.component.scss'
})
export class ModuleListComponent  implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    'title',
    'path',
    'icon',
    'menuPosition',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  featurePermission : FeaturePermission = new FeaturePermission;
  
  constructor(
    private toastr: ToastrService,
    public featureManagementService: FeatureManagementService,
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

  getAllModule() {
    this.subscription.push(
    this.featureManagementService.getAll().subscribe((item) => {
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
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }


  getPermission(){
    this.subscription.push(
    this.roleFeatureService.getFeaturePermission('module').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.getAllModule();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
   
  }


  editModuleInfo(id: number, clickedButton: string){
    if(clickedButton == "Create" && this.featurePermission.add == true || clickedButton == "Edit" && this.featurePermission.update == true){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(CreateModuleComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        modalRef.onHide.subscribe(() => {
          this.getAllModule();
        });
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
          this.featureManagementService.delete(element.moduleId).subscribe(
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
          );
        }
      })
      )
      
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

}
