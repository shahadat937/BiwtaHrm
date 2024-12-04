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
import { CreateNavbarThemComponent } from '../create-navbar-them/create-navbar-them.component';
import { FeaturePermission } from '../model/feature-permission';
import { NavbarThemService } from '../service/navbar-them.service';
import { RoleFeatureService } from '../service/role-feature.service';

@Component({
  selector: 'app-navbar-them-list',
  templateUrl: './navbar-them-list.component.html',
  styleUrl: './navbar-them-list.component.scss'
})
export class NavbarThemListComponent implements OnInit, OnDestroy {

  subscription: Subscription = new Subscription();
  displayedColumns: string[] = [
    'slNo',
    'name',
    'remark',
    'isActive',
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  featurePermission : FeaturePermission = new FeaturePermission;
  
  constructor(
    private toastr: ToastrService,
    public navbarThemService: NavbarThemService,
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

  getAllNavbarThem() {
    this.subscription = this.navbarThemService.getAll().subscribe((item) => {
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


  getPermission(){
    this.roleFeatureService.getFeaturePermission('navbar-them').subscribe((item) => {
      this.featurePermission = item;
      if(item.viewStatus == true){
        this.getAllNavbarThem();
      }
      else{
        this.roleFeatureService.unauthorizeAccress();
        this.router.navigate(['/dashboard']);
      }
    });
  }


  editNavbarThemInfo(id: number, clickedButton: string){
    if(clickedButton == "Create" && this.featurePermission.add == true || clickedButton == "Edit" && this.featurePermission.update == true){
      const initialState = {
        id: id,
        clickedButton: clickedButton
      };
      const modalRef: BsModalRef = this.modalService.show(CreateNavbarThemComponent, { initialState, backdrop: 'static' });
  
      if (modalRef.onHide) {
        modalRef.onHide.subscribe(() => {
          this.getAllNavbarThem();
        });
      }
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  delete(element: any) {
    if(this.featurePermission.delete == true){
      this.confirmService
      .confirm('Confirm delete message', 'Are You Sure Delete This  Item')
      .subscribe((result) => {
        if (result) {
          this.navbarThemService.delete(element.id).subscribe(
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
      });
    }
    else {
      this.roleFeatureService.unauthorizeAccress();
    }
  }

}
