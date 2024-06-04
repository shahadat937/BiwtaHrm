import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PersonalInformationComponent } from '../employee-informations/personal-information/personal-information.component';
import { config } from 'rxjs';
import { BasicInformationComponent } from '../employee-informations/basic-information/basic-information.component';
import { ActivatedRoute } from '@angular/router';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmployeeService } from '../../service/employee.service';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';

@Component({
  selector: 'app-view-information-list',
  templateUrl: './view-information-list.component.html',
  styleUrl: './view-information-list.component.scss'
})
export class ViewInformationListComponent implements OnInit {

  gettingStatus : boolean = true;
  entryStatus : boolean = false;
  basicInfoEntryStatus : boolean = false;
  personalInfoEntryStatus : boolean = false;
  visible : boolean = false;
  componentVisible : boolean = false;
  visibleComponent: string | null = null;
  bsModelRef!: BsModalRef;
  userId : any;
  empId : any;
  userInfo:any;

  constructor(public dialog: MatDialog,
    private modalService: BsModalService,
    private route: ActivatedRoute,
    public userService: UserService,
    public employeeService: EmployeeService,
    public empBasicInfoService: EmpBasicInfoService,) { }

  ngOnInit(): void {
    this.handleRouteParams();
    this.getEmployeeByAspNetUserId();
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      this.userId = params.get('id');
      this.userService.find(this.userId).subscribe((res) => {
        
      });
    });
  }

  getEmployeeByAspNetUserId(){
    this.empBasicInfoService.findByAspNetUserId(this.userId).subscribe((res) => {
      if(res){
        console.log(res)
        this.gettingStatus = true;
        this.basicInfoEntryStatus=true;
        this.empId = res.id;
      }
      else{
        this.gettingStatus = false;
      }
    });
  }

  toggleComponent(component: string) {
    this.componentVisible = false;
    if (this.visibleComponent === component) {
      this.visibleComponent = null;
      this.visible = false;
    } else {
      this.visibleComponent = component;
      this.visible = true;
      this.componentVisible = true;
    }
  }

}
