import { Component, ElementRef, OnInit, Renderer2 } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { UserService } from 'src/app/views/usermanagement/service/user.service';
import { BasicInfoModule } from '../../model/basic-info.module';
import { EmpBankInfoModule } from '../../model/emp-bank-info.module';
import { EmpChildInfoModule } from '../../model/emp-child-info.module';
import { EmpEducationInfoModule } from '../../model/emp-education-info.module';
import { EmpForeignTourInfoModule } from '../../model/emp-foreign-tour-info.module';
import { EmpJobDetailsModule } from '../../model/emp-job-details.module';
import { EmpLanguageInfoModule } from '../../model/emp-language-info.module';
import { EmpPermanentAddressModule } from '../../model/emp-permanent-address.module';
import { EmpPhotoSignModule } from '../../model/emp-photo-sign.module';
import { EmpPresentAddressModule } from '../../model/emp-present-address.module';
import { EmpPsiTrainingInfoModule } from '../../model/emp-psi-training-info.module';
import { EmpSpouseInfoModule } from '../../model/emp-spouse-info.module';
import { PersonalInfoModule } from '../../model/personal-info.module';
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

@Component({
  selector: 'app-emp-profile',
  templateUrl: './emp-profile.component.html',
  styleUrl: './emp-profile.component.scss'
})
export class EmpProfileComponent  implements OnInit {

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
  photoPreviewUrl: string | ArrayBuffer | null = null;
  empProfileView = true;

  pNo: string = '';
  id : number = 0;

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
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) { }

    ngOnInit(): void {
      const currentUserString = localStorage.getItem('currentUser');
      const currentUserJSON = currentUserString ? JSON.parse(currentUserString) : null;
      this.id = currentUserJSON.empId;
      if(this.id != null) {
        this.handleRouteParams();
      }
      else {
        this.empProfileView = false;
      }
    }
  
    handleRouteParams() {
      this.getEmpBasicInfoByEmpId();
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

    getEmpBasicInfoByEmpId(){
      this.manageEmployeeService.getEmpBasicInfoByEmpId(this.id).subscribe((res) => {
        if(res){
          this.empBasicInfo = res;
          this.pNo = res.idCardNo;
          this.patchEmpPhoto(res.empGenderName)
        }
        else {
          this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
        }
      });
    }
    
    getEmpPersonalInfoByEmpId(){
      this.empPersonalInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empPersonalInfo = res;
      });
    }
    
    getEmpPresentAddressByEmpId(){
      this.empPresentAddressService.findByEmpId(this.id).subscribe((res) => {
        this.empPresentAddress = res;
      });
    }
    
    getEmpPermanentAddressByEmpId(){
      this.empPermanentAddressService.findByEmpId(this.id).subscribe((res) => {
        this.empPermanentAddress = res;
      });
    }

    getEmpJobDetailsByEmpId(){
      this.empJobDetailsService.findByEmpId(this.id).subscribe((res) => {
        this.empJobDetails = res;
      });
    }
    
    getEmpSpouseInfoByEmpId(){
      this.empSpouseInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empSpouseInfo = res;
      });
    }
    
    getEmpChildInfoByEmpId(){
      this.empChildInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empChildInfo = res;
      });
    }
    
    getEmpEducationInfoByEmpId(){
      this.empEducationInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empEducationInfo = res;
      });
    }
    
    getEmpPsiTrainingInfoByEmpId(){
      this.empPsiTrainingInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empPsiTrainingInfo = res;
      });
    }
    
    getEmpBankInfoByEmpId(){
      this.empBankInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empBankInfo = res;
      });
    }
    
    getEmpLanguageInfoByEmpId(){
      this.empLanguageInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empLanguageInfo = res;
      });
    }
    
    getEmpForeignTourInfoByEmpId(){
      this.empForeignTourInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empForeignTourInfo = res;
      });
    }

    patchEmpPhoto(empGender: string){
      this.empPhotoSignService.findByEmpId(this.id).subscribe((res) => {
        if(res){
          this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}`
        }
        else {
          if(empGender){
            const gender = empGender.charAt(0).toLowerCase();
            if(gender == 'm'){
              this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default_Male.jpg`
            }
            else {
              this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default_Female.jpg`
            }
          }
          else{
            this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
          }
        }
      })
    }
}
