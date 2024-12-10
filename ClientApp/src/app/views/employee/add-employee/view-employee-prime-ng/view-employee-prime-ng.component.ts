import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { cilPlus, cilCloudUpload, cilUser, cilUserPlus } from '@coreui/icons';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { UserModule } from 'src/app/views/usermanagement/model/user.module';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';
import { BasicInfoModule } from '../../model/basic-info.module';
import { Table } from 'primeng/table';
import { EmpPhotoSignService } from '../../service/emp-photo-sign.service';
import { UploadEmpBasicInfoComponent } from '../upload-emp-basic-info/upload-emp-basic-info.component';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { PaginatorModel } from 'src/app/core/models/paginator-model';

@Component({
  selector: 'app-view-employee-prime-ng',
  templateUrl: './view-employee-prime-ng.component.html',
  styleUrl: './view-employee-prime-ng.component.scss'
})
export class ViewEmployeePrimeNgComponent implements OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]

  employees: BasicInfoModule[] = [];
  departments: any[] = [];
  sections!: any[];
  imageLinkUrl: any;
  defaultImage: any;
  maleImage: any;
  femaleImage: any;
  loading: boolean = true;
  totalRecords: number = 0;
  dataSource = new MatTableDataSource<any>();
  // @ViewChild(MatPaginator);
  // @ViewChild('dt') dt: Table | undefined;
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  userStatus : string = '';
  loadingMap: { [key: number]: boolean } = {};
  userForm : UserModule;
  selectedEmpId!: number;
  showUpdateUserInfo: boolean = false;
  pagination: PaginatorModel = new PaginatorModel();

  constructor(
    public userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empBasicInfoService: EmpBasicInfoService,
    public roleFeatureService: RoleFeatureService,
    public empPhotoSign: EmpPhotoSignService,
    private modalService: BsModalService,

  ) {
    this.userForm = new UserModule;
  }
  icons = { cilPlus, cilCloudUpload,cilUser,cilUserPlus };

  ngOnInit(): void {
    this.getPermission();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }
  
  getPermission(){
    this.subscription.push(
       this.roleFeatureService.getFeaturePermission('allEmployeeList').subscribe((item) => {
      this.roleFeatureService.featurePermission.add == true;
      if(item.viewStatus == true){
        this.getAllEmpBasicInfo(this.pagination);
      }
      else{
        this.unauthorizeAccress()
        this.router.navigate(['/dashboard']);
      }
    })
    )
   
  }
  unauthorizeAccress(){
    this.toastr.warning('Unauthorized Access', ` `, {
      positionClass: 'toast-top-right',
    });
  }
  getAllEmpBasicInfo(queryParams: any) {
    // this.subscription = 
    this.subscription.push(
      this.empBasicInfoService.getAllPagination(queryParams).subscribe((employees: any) => {
      this.totalRecords = employees.totalItemsCount;
      this.employees = employees.items;
      this.loading = false;

      // this.departments = [...new Set(employees
      //   .map(emp => emp.departmentName)
      //   .filter(departmentName => departmentName !== null && departmentName.trim() !== '')
      // )].map(department => ({ name: department }));
      
      // this.sections = [...new Set(employees
      //   .map(emp => emp.sectionName)
      //   .filter(sectionName => sectionName !== null && sectionName.trim() !== '')
      // )].map(section => ({ name: section }));
    })
    )
    

    this.imageLinkUrl = this.empPhotoSign.imageUrl + '/EmpPhoto';
    this.defaultImage = this.empPhotoSign.imageUrl + '/EmpPhoto/default.jpg';
    this.maleImage = this.empPhotoSign.imageUrl + '/EmpPhoto/default_Male.jpg';
    this.femaleImage = this.empPhotoSign.imageUrl + '/EmpPhoto/default_Female.jpg';
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.toLowerCase();
    this.pagination.pageIndex = 1;
    this.pagination.searchText = filterValue;
    this.getAllEmpBasicInfo(this.pagination);
  }

  onPageChange(event: any){
    this.pagination.pageSize = event.rows;
    this.pagination.pageIndex = (event.first / event.rows) + 1;
    this.getAllEmpBasicInfo(this.pagination);
  }

  onGlobalFilter(event: Event) {
    const inputValue = (event.target as HTMLInputElement).value;
    // this.dt!.filterGlobal(inputValue, 'contains');
  }
  onSelect(selectedValue: string | null) {
    // if (!selectedValue) {
    //   this.dt!.filterGlobal('', 'contains');
    // } else {
    //   this.dt!.filterGlobal(selectedValue, 'contains');
    // }
  }

  addNewEmployee(){
    if(this.roleFeatureService.featurePermission.add == true){
      this.router.navigate(['/employee/create-new-employee']);
    }
    else{
      this.roleFeatureService.unauthorizeAccress();
    }
  }

  updateEmployee(id: number){
    if(this.roleFeatureService.featurePermission.add == true){
      this.router.navigate(['/employee/update-employee-information/', id]);
    }
    else{
      this.unauthorizeAccress();
    }
  }

  downloadExcelFile(){
    const url = this.empPhotoSign.imageUrl + '/Files/EmpBasicInfo.xlsx';
    const a = document.createElement('a');
    a.href = url;
    a.download = 'EmpBasicInfo.xlsx';
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
  }
  uploadExcelFile(){
    const modalRef: BsModalRef = this.modalService.show(UploadEmpBasicInfoComponent, { backdrop: 'static' });

    if (modalRef.onHide) {
      this.subscription.push(
        modalRef.onHide.subscribe(() => {
        this.getAllEmpBasicInfo(this.pagination);
      })
      )
      
    }
  }

  createUser(id : number){
    if(this.roleFeatureService.featurePermission.add == true){
      this.loadingMap[id] = true;
      this.subscription.push(
        this.empBasicInfoService.findByEmpId(id).subscribe((res) => {
        this.userForm.firstName = res.firstName;
        this.userForm.lastName = res.lastName;
        this.userForm.userName = res.idCardNo;
        this.userForm.password = "Admin@123";
        this.userForm.empId = id;
        this.userService.submit(this.userForm).subscribe(((res: any) => {
          if (res.success) {
            this.toastr.success('', `${res.message}`, {
              positionClass: 'toast-top-right',
            });
            this.empBasicInfoService.updateUserStatus(id).subscribe((res) =>{
                if(res){
                  this.getAllEmpBasicInfo(this.pagination);
                }
            });
            this.loadingMap[id] = false;
          } else {
            this.toastr.warning('', `${res.message}`, {
              positionClass: 'toast-top-right',
            });
            this.loadingMap[id] = false;
          }
          this.loadingMap[id] = false;
        })
        )
      })
      )
      
    }
    else{
      this.unauthorizeAccress();
    }
    
  }

  updateUser(id : number){
    this.selectedEmpId = id;
    this.showUpdateUserInfo = false;
    setTimeout(() => {
      this.showUpdateUserInfo = true;
    }, 0);
  }

  handleCancel() {
    this.showUpdateUserInfo = false;
  }

}
