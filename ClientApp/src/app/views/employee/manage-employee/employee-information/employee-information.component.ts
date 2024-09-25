import { Component, ElementRef, HostListener, OnInit, Renderer2 } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
import { EmpOtherResponsibility } from '../../model/emp-other-responsibility';
import { EmpWorkHistory } from '../../model/emp-work-history';
import { EmpOtherResponsibilityService } from '../../service/emp-other-responsibility.service';
import { EmpWorkHistoryService } from '../../service/emp-work-history.service';

@Component({
  selector: 'app-employee-information',
  templateUrl: './employee-information.component.html',
  styleUrl: './employee-information.component.scss'
})
export class EmployeeInformationComponent implements OnInit {

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
  empOtherResponsibility : EmpOtherResponsibility[] = [];
  empWorkHistory : EmpWorkHistory[] = [];
  empPhoto : string = '';
  empSignature : string = '';

  id: number = 0;
  clickedButton: string = '';
  canEditProfile: boolean = false;
  modalOpened: boolean = false;

  visible : boolean = false;
  componentVisible : boolean = false;
  visibleComponent: string | null = null;
  empId: number = 0;
  pNo: string = '';

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
    public empOtherResponsibilityService: EmpOtherResponsibilityService,
    public empWorkHistoryService: EmpWorkHistoryService,
    public empPhotoSignService: EmpPhotoSignService,
    public manageEmployeeService: ManageEmployeeService,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) { }

    ngOnInit(): void {
      this.handleRouteParams();
      setTimeout(() => {
        this.modalOpened = true;
      }, 0);
      this.empId = this.id;
    }
  
    handleRouteParams() {
      if(this.clickedButton == 'viewProfile'){
        this.getCanEditProfileStatusByEmpId();
      }
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
      this.getEmpWorkHistory();
      this.getEmpOtherResponsibility();
    }

    getCanEditProfileStatusByEmpId(){
      this.userService.getInfoByEmpId(this.id).subscribe((res) => {
        this.canEditProfile = res.canEditProfile;
      })
    }

    getEmpBasicInfoByEmpId(){
      this.manageEmployeeService.getEmpBasicInfoByEmpId(this.id).subscribe((res) => {
        this.empBasicInfo = res;
        this.pNo = res.idCardNo;
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
    getEmpOtherResponsibility(){
      this.empOtherResponsibilityService.findByEmpId(this.id).subscribe((res) => {
        this.empOtherResponsibility = res;
      });
    }
    
    getEmpWorkHistory(){
      this.empWorkHistoryService.findByEmpId(this.id).subscribe((res) => {
        this.empWorkHistory = res;
      });
    }

    getEmpPhotoSign(){
      this.empPhotoSignService.findByEmpId(this.id).subscribe((res) => {
        if(res){
          this.empPhotoSign = res;
          this.empPhoto = `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}`;
          this.empSignature = `${this.empPhotoSignService.imageUrl}EmpSignature/${res.signatureUrl}`;
        }
      });
    }

    closeModal(): void {
      this.bsModalRef.hide();
    }
  
    @HostListener('document:click', ['$event'])
    onClickOutside(event: MouseEvent): void {
      if (this.modalOpened) {
        const modalElement = this.el.nativeElement.querySelector('.modal-content');
        if (modalElement && !modalElement.contains(event.target as Node)) {
          this.shakeModal();
        }
      }
    }
  
    shakeModal(): void {
      const modalElement = this.el.nativeElement.querySelector('.modal-content');
      if (modalElement) {
        this.renderer.addClass(modalElement, 'shake');
        setTimeout(() => {
          this.renderer.removeClass(modalElement, 'shake');
        }, 500); // duration of the shake animation
      }
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
