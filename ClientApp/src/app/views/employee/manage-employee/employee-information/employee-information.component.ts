import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BsModalService } from 'ngx-bootstrap/modal';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { EmpBankInfoService } from '../../service/emp-bank-info.service';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';
import { EmpChildInfoService } from '../../service/emp-child-info.service';
import { EmpEducationInfoService } from '../../service/emp-education-info.service';
import { EmpForeignTourInfoService } from '../../service/emp-foreign-tour-info.service';
import { EmpJobDetailsService } from '../../service/emp-job-details.service';
import { EmpLanguageInfoService } from '../../service/emp-language-info.service';
import { EmpPermanentAddressService } from '../../service/emp-permanent-address.service';
import { EmpPersonalInfoService } from '../../service/emp-personal-info.service';
import { EmpPhotoSignService } from '../../service/emp-photo-sign.service';
import { EmpPresentAddressService } from '../../service/emp-present-address.service';
import { EmpPsiTrainingInfoService } from '../../service/emp-psi-training-info.service';
import { EmpSpouseInfoService } from '../../service/emp-spouse-info.service';
import { ManageEmployeeService } from '../../service/manage-employee.service';
import { BasicInfoModule } from '../../model/basic-info.module';
import { EmpPhotoSignModule } from '../../model/emp-photo-sign.module';
import { PersonalInfoModule } from '../../model/personal-info.module';
import { EmpPresentAddressModule } from '../../model/emp-present-address.module';
import { EmpPermanentAddressModule } from '../../model/emp-permanent-address.module';
import { EmpJobDetailsModule } from '../../model/emp-job-details.module';
import { EmpSpouseInfoModule } from '../../model/emp-spouse-info.module';
import { EmpChildInfoModule } from '../../model/emp-child-info.module';
import { EmpEducationInfoModule } from '../../model/emp-education-info.module';
import { EmpPsiTrainingInfoModule } from '../../model/emp-psi-training-info.module';
import { EmpBankInfoModule } from '../../model/emp-bank-info.module';
import { EmpLanguageInfoModule } from '../../model/emp-language-info.module';
import { EmpForeignTourInfoModule } from '../../model/emp-foreign-tour-info.module';

@Component({
  selector: 'app-employee-information',
  templateUrl: './employee-information.component.html',
  styleUrl: './employee-information.component.scss'
})
export class EmployeeInformationComponent implements OnInit {

  employeeId : any = null;
  empBasicInfo : BasicInfoModule = new BasicInfoModule;
  empPhotoSign : EmpPhotoSignModule = new EmpPhotoSignModule;
  empPersonalInfo : PersonalInfoModule = new PersonalInfoModule;
  empPresentAddress : EmpPresentAddressModule = new EmpPresentAddressModule;
  empPermanentAddress : EmpPermanentAddressModule = new EmpPermanentAddressModule;
  empJobDetails : EmpJobDetailsModule = new EmpJobDetailsModule;
  empSpouseInfo : EmpSpouseInfoModule[] = [];
  empChildInfo : EmpChildInfoModule[] = [];
  empEducationInfo : EmpEducationInfoModule[] = [];
  empPsiTrainingInfo : EmpPsiTrainingInfoModule[] = [];
  empBankInfo : EmpBankInfoModule[] = [];
  empLanguageInfo : EmpLanguageInfoModule[] = [];
  empForeignTourInfo : EmpForeignTourInfoModule[] = [];
  empPhoto : string = '';
  empSignature : string = '';

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
    public manageEmployeeService: ManageEmployeeService,
  ) { }

    ngOnInit(): void {
      this.handleRouteParams();
      this.getEmpBasicInfoByEmpId();
      this.getEmpPhotoSign();
      this.getEmpPersonalInfoByEmpId();
      this.getEmpPresentAddressByEmpId();
      this.getEmpPermanentAddressByEmpId();
      this.getEmpJobDetailsByEmpId();
      this.getEmpSpouseInfoByEmpId();
      this.getEmpChildInfoByEmpId();
      this.getEmpEducationInfoByEmpId();
      this.getEmpPsiTrainingInfoByEmpId(); 
      this.getEmpBankInfoByEmpId();
      this.getEmpLanguageInfoByEmpId();
      this.getEmpForeignTourInfoByEmpId();
    }
  
    handleRouteParams() {
      this.route.paramMap.subscribe((params) => {
        this.employeeId = Number(params.get('id'));
      });
    }

    getEmpBasicInfoByEmpId(){
      this.manageEmployeeService.getEmpBasicInfoByEmpId(this.employeeId).subscribe((res) => {
        this.empBasicInfo = res;
      });
    }
    
    getEmpPersonalInfoByEmpId(){
      this.empPersonalInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empPersonalInfo = res;
      });
    }
    
    getEmpPresentAddressByEmpId(){
      this.empPresentAddressService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empPresentAddress = res;
      });
    }
    
    getEmpPermanentAddressByEmpId(){
      this.empPermanentAddressService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empPermanentAddress = res;
      });
    }

    getEmpJobDetailsByEmpId(){
      this.empJobDetailsService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empJobDetails = res;
      });
    }
    
    getEmpSpouseInfoByEmpId(){
      this.empSpouseInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empSpouseInfo = res;
      });
    }
    
    getEmpChildInfoByEmpId(){
      this.empChildInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empChildInfo = res;
      });
    }
    
    getEmpEducationInfoByEmpId(){
      this.empEducationInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empEducationInfo = res;
      });
    }
    
    getEmpPsiTrainingInfoByEmpId(){
      this.empPsiTrainingInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empPsiTrainingInfo = res;
      });
    }
    
    getEmpBankInfoByEmpId(){
      this.empBankInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empBankInfo = res;
      });
    }
    
    getEmpLanguageInfoByEmpId(){
      this.empLanguageInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empLanguageInfo = res;
      });
    }
    
    getEmpForeignTourInfoByEmpId(){
      this.empForeignTourInfoService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empForeignTourInfo = res;
      });
    }

    getEmpPhotoSign(){
      this.empPhotoSignService.findByEmpId(this.employeeId).subscribe((res) => {
        this.empPhotoSign = res;
        this.empPhoto = `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}`;
        this.empSignature = `${this.empPhotoSignService.imageUrl}EmpSignature/${res.signatureUrl}`;
      });
    }

}
