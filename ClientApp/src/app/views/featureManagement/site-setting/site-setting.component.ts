import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilArrowLeft, cilPlus, cilBell } from '@coreui/icons';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { FeaturePermission } from '../model/feature-permission';
import { SiteSettingService } from '../service/site-setting.service';
import { RoleFeatureService } from '../service/role-feature.service';
import { CreateSiteSettingComponent } from '../create-site-setting/create-site-setting.component';
import { EmpPhotoSignService } from '../../employee/service/emp-photo-sign.service';

@Component({
  selector: 'app-site-setting',
  templateUrl: './site-setting.component.html',
  styleUrl: './site-setting.component.scss'
})
export class SiteSettingComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    'siteName',
    'siteTitle',
    'siteLogo',
    'defaultPassword',
    'isActive',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  featurePermission : FeaturePermission = new FeaturePermission;
  folderUrl = '';
  
  constructor(
    private toastr: ToastrService,
    public siteSettingService: SiteSettingService,
    public roleFeatureService: RoleFeatureService,
    private route: ActivatedRoute,
    private modalService: BsModalService,
    private confirmService: ConfirmService,
    private router: Router,
    public empPhotoSign : EmpPhotoSignService,
  ) {

  }

  icons = { cilArrowLeft, cilPlus, cilBell };


  ngOnInit(): void {
    this.getPermission();
    this.folderUrl = this.empPhotoSign.imageUrl + 'TempleteImage/'
  }

  getAllSiteSetting() {
    // this.subscription = 
    this.subscription.push(
    this.siteSettingService.getAll().subscribe((item) => {
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
    this.roleFeatureService.getFeaturePermission('siteSetting').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.getAllSiteSetting();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    })
    )
   
  }


  editSiteSettingInfo(id: number, clickedButton: string){
    if(clickedButton == "Create" && this.featurePermission.add == true || clickedButton == "Edit" && this.featurePermission.update == true){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(CreateSiteSettingComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        modalRef.onHide.subscribe(() => {
          this.getAllSiteSetting();
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
          this.siteSettingService.delete(element.id).subscribe(
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
