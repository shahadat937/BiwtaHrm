import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { Subscription } from 'rxjs';
import { ConfirmService } from 'src/app/core/service/confirm.service';
import { cilPlus, cilCloudUpload, cilUser, cilUserPlus } from '@coreui/icons';
import { UserModule } from 'src/app/views/usermanagement/model/user.module';
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { FeaturePermission } from 'src/app/views/featureManagement/model/feature-permission';

@Component({
  selector: 'app-view-employee',
  templateUrl: './view-employee.component.html',
  styleUrl: './view-employee.component.scss',
})
export class ViewEmployeeComponent implements OnInit, OnDestroy {

  // subscription: Subscription = new Subscription();
  subscription: Subscription[]=[]
  displayedColumns: string[] = [
    'slNo',
    'pNo',
    'fullName',
    'department',
    'designation',
    // 'fullNameBangla',
    // 'email', 
    'Action'];
  dataSource = new MatTableDataSource<any>();
  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  matSort!: MatSort;
  userStatus : string = '';
  loadingMap: { [key: number]: boolean } = {};
  userForm : UserModule;
  selectedEmpId!: number;
  showUpdateUserInfo: boolean = false;

  constructor(
    public userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private confirmService: ConfirmService,
    private toastr: ToastrService,
    public empBasicInfoService: EmpBasicInfoService,
    public roleFeatureService: RoleFeatureService,
    

  ) {
    this.userForm = new UserModule;
  }
  // ngOnDestroy(): void {
  //   throw new Error('Method not implemented.');
  // }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }
  icons = { cilPlus, cilCloudUpload,cilUser,cilUserPlus };

  ngOnInit(): void {
    this.getPermission();
  }
  
  getPermission(){
    this.subscription.push(
       this.roleFeatureService.getFeaturePermission('employeeList').subscribe((item) => {
      this.roleFeatureService.featurePermission.add == true;
      if(item.viewStatus == true){
        this.getAllEmpBasicInfo();
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

  getAllEmpBasicInfo() {
    // this.subscription = 
    this.subscription.push(
      this.empBasicInfoService.getAll().subscribe((item) => {
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
                  this.getAllEmpBasicInfo();
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
