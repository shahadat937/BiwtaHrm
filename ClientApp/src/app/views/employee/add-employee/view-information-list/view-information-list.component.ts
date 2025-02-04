import { Component, HostListener, OnDestroy, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PersonalInformationComponent } from '../employee-informations/personal-information/personal-information.component';
import { config, Subscription } from 'rxjs';
import { BasicInformationComponent } from '../employee-informations/basic-information/basic-information.component';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmpPersonalInfoService } from '../../service/emp-personal-info.service';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';
import { EmpPresentAddressService } from '../../service/emp-present-address.service';
import { EmpPermanentAddressService } from '../../service/emp-permanent-address.service';
import { EmpJobDetailsService } from '../../service/emp-job-details.service';
import { EmpSpouseInfoService } from '../../service/emp-spouse-info.service';
import { EmpChildInfoService } from '../../service/emp-child-info.service';
import { EmpEducationInfoService } from '../../service/emp-education-info.service';
import { EmpPsiTrainingInfoService } from '../../service/emp-psi-training-info.service';
import { EmpBankInfoService } from '../../service/emp-bank-info.service';
import { EmpLanguageInfoService } from '../../service/emp-language-info.service';
import { EmpForeignTourInfoService } from '../../service/emp-foreign-tour-info.service';
import { EmpPhotoSignService } from '../../service/emp-photo-sign.service';
import { FeaturePermission } from 'src/app/views/featureManagement/model/feature-permission';
import { RoleFeatureService } from 'src/app/views/featureManagement/service/role-feature.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-view-information-list',
  templateUrl: './view-information-list.component.html',
  styleUrl: './view-information-list.component.scss'
})
export class ViewInformationListComponent implements OnInit, OnDestroy {

  gettingStatus : boolean = false;
  entryStatus : boolean = false;
  basicInfoEntryStatus : boolean = false;
  personalInfoEntryStatus : boolean = false;
  presentAddressEntryStatus : boolean = false;
  permanentAddressEntryStatus : boolean = false;
  empJobDetailsEntryStatus : boolean = false;
  empSpouseInfoEntryStatus : boolean = false;
  empChildInfoEntryStatus : boolean = false;
  empEducationInfoEntryStatus : boolean = false;
  empPsiTrainingInfoEntryStatus : boolean = false;
  empBankInfoEntryStatus : boolean = false;
  empLanguageInfoEntryStatus : boolean = false;
  empForeignTourInfoEntryStatus : boolean = false;
  empPhotoSignEntryStatus : boolean = false;
  visible : boolean = false;
  componentVisible : boolean = false;
  visibleComponent: string | null = null;
  bsModelRef!: BsModalRef;
  userId : any;
  empId : any;
  userInfo:any;
  pNo: string = '';
  headerText: string = '';
  selectedTabIndex: number | null = null;
  featurePermission : FeaturePermission = new FeaturePermission;
  subscription: Subscription[]=[]

  constructor(public dialog: MatDialog,
    private modalService: BsModalService,
    private route: ActivatedRoute,
    public userService: UserService,
    public empPersonalInfoService: EmpPersonalInfoService,
    public empBasicInfoService: EmpBasicInfoService,
    public empPresentAddressService: EmpPresentAddressService,
    public empPermanentAddressService: EmpPermanentAddressService,
    public empJobDetailsService: EmpJobDetailsService,
    public empSpouseInfoService: EmpSpouseInfoService,
    public empChildInfoService: EmpChildInfoService,
    public empEducationInfoService: EmpEducationInfoService,
    public empPsiTrainingInfoService: EmpPsiTrainingInfoService,
    public empBankInfoService: EmpBankInfoService,
    public empLanguageInfoService: EmpLanguageInfoService,
    public empForeignTourInfoService: EmpForeignTourInfoService,
    public empPhotoSignService: EmpPhotoSignService,
    public roleFeatureService: RoleFeatureService,
    private router: Router,
    private toastr: ToastrService,) { }

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
        this.featurePermission = item;
        if(item.viewStatus == true && ( item.add == true || item.update == true)){
          this.handleRouteParams();
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

  handleRouteParams() {
    this.subscription.push(
      this.route.paramMap.subscribe((params) => {
      this.empId = Number(params.get('id'));
      this.selectedTabIndex = 0;
      this.empBasicInfoService.findByEmpId(this.empId).subscribe((res) => {
        if(res){
          this.gettingStatus = true;
          this.pNo = res.idCardNo;
          this.basicInfoEntryStatus=true;
          this.headerText = "Update Employee Information";
          this.getStatusOfPersonalInfo();
          this.getStatusOfPresentAddress();
          this.getStatusOfPermanentAddress();
          this.getStatusOfEmpJobDetails();
          this.getStatusOfEmpSpouseInfo();
          this.getStatusOfEmpChildStatus();
          this.getStatusOfEmpEducationStatus();
          // this.getStatusOfEmpPsiTrainingStatus();
          this.getStatusOfEmpBankInfoStatus();
          this.getStatusOfEmpLanguageInfoStatus();
          this.getStatusOfEmpForeignTourInfoStatus();
          this.getStatusOfEmpPhotoSign();
        }
        else{
          this.gettingStatus = false;
          this.headerText = "Add Employee Information";
        }
      });
    })
    )
    
  }

  getEmployeeByAspNetUserId(){
    this.subscription.push(
      this.empBasicInfoService.findByAspNetUserId(this.userId).subscribe((res) => {
      if(res){
        this.gettingStatus = true;
        this.basicInfoEntryStatus=true;
        this.empId = res.id;
        this.getStatusOfPersonalInfo();
        this.getStatusOfPresentAddress();
        this.getStatusOfPermanentAddress();
        this.getStatusOfEmpJobDetails();
        this.getStatusOfEmpSpouseInfo();
        this.getStatusOfEmpChildStatus();
        this.getStatusOfEmpEducationStatus();
        // this.getStatusOfEmpPsiTrainingStatus();
        this.getStatusOfEmpBankInfoStatus();
        this.getStatusOfEmpLanguageInfoStatus();
        this.getStatusOfEmpForeignTourInfoStatus();
        this.getStatusOfEmpPhotoSign();
      }
      else{
        this.gettingStatus = false;
      }
    })
    )
    
  }

  getStatusOfPersonalInfo(){
    this.subscription.push(
      this.empPersonalInfoService.findByEmpId(this.empId).subscribe((res)=>{
      this.personalInfoEntryStatus = !!res;
    })
    )
    
  }

  getStatusOfPresentAddress(){

    this.subscription.push(
      this.empPresentAddressService.findByEmpId(this.empId).subscribe((res) => {
      this.presentAddressEntryStatus = !!res;
    })
    )
    
  }

  getStatusOfPermanentAddress(){
    this.subscription.push(
      this.empPermanentAddressService.findByEmpId(this.empId).subscribe((res) => {
      this.permanentAddressEntryStatus = !!res;
    })
    )
    
  }
  getStatusOfEmpJobDetails(){
    this.subscription.push(
      this.empJobDetailsService.findByEmpId(this.empId).subscribe((res) => {
      this.empJobDetailsEntryStatus = !!res;
    })
    )
    
  }
  getStatusOfEmpSpouseInfo(){
    this.subscription.push(
      this.empSpouseInfoService.findByEmpId(this.empId).subscribe((res: any[]) => {
      if(res.length>0){
        this.empSpouseInfoEntryStatus = true;
      }
      else {
        this.empSpouseInfoEntryStatus = false;
      }
    })
    )
    
  }
  getStatusOfEmpChildStatus() {
    this.subscription.push(
      this.empChildInfoService.findByEmpId(this.empId).subscribe((res) => {
      if(res.length>0){
        this.empChildInfoEntryStatus = true;
      }
      else {
        this.empChildInfoEntryStatus = false;
      }
    })
    )
    
  }
  getStatusOfEmpEducationStatus() {
    this.subscription.push(
      this.empEducationInfoService.findByEmpId(this.empId).subscribe((res) => {
      if(res.length>0){
        this.empEducationInfoEntryStatus = true;
      }
      else {
        this.empEducationInfoEntryStatus = false;
      }
    })
    )
    
  }
  // getStatusOfEmpPsiTrainingStatus() {
  //   this.empPsiTrainingInfoService.findByEmpId(this.empId).subscribe((res) => {
  //     if(res.length>0){
  //       this.empPsiTrainingInfoEntryStatus = true;
  //     }
  //     else {
  //       this.empPsiTrainingInfoEntryStatus = false;
  //     }
  //   })
  // }
  getStatusOfEmpBankInfoStatus() {
    this.subscription.push(
      this.empBankInfoService.findByEmpId(this.empId).subscribe((res) => {
      if(res.length>0){
        this.empBankInfoEntryStatus = true;
      }
      else {
        this.empBankInfoEntryStatus = false;
      }
    })
    )
    
  }
  getStatusOfEmpLanguageInfoStatus() {
    this.subscription.push(
      this.empLanguageInfoService.findByEmpId(this.empId).subscribe((res) => {
      if(res.length>0){
        this.empLanguageInfoEntryStatus = true;
      }
      else {
        this.empLanguageInfoEntryStatus = false;
      }
    })
    )
    
  }
  getStatusOfEmpForeignTourInfoStatus() {
    this.subscription.push(
      this.empForeignTourInfoService.findByEmpId(this.empId).subscribe((res) => {
      if(res.length>0){
        this.empForeignTourInfoEntryStatus = true;
      }
      else {
        this.empForeignTourInfoEntryStatus = false;
      }
    })
    )
    
  }
  getStatusOfEmpPhotoSign(){
    this.subscription.push(
      this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
      this.empPhotoSignEntryStatus = !!res;
    })
    )
    
  }


  toggleComponent(component: string) {
    this.componentVisible = false;
    if (this.visibleComponent === component) {
      this.visibleComponent = null;
      this.visible = false;
      this.handleRouteParams();
      } else {
        this.visibleComponent = component;
        this.visible = true;
        this.componentVisible = true;
    }
  }

}
