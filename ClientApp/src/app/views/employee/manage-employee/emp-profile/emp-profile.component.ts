import { Component, ElementRef, OnDestroy, OnInit, Renderer2, ViewChild } from '@angular/core';
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
import { EmpNomineeInfoService } from '../../service/emp-nominee-info.service';
import { EmpJobDetailsService } from '../../service/emp-job-details.service';
import { EmpLanguageInfoService } from '../../service/emp-language-info.service';
import { EmpPermanentAddressService } from '../../service/emp-permanent-address.service';
import { EmpPersonalInfoService } from '../../service/emp-personal-info.service';
import { EmpPhotoSignService } from '../../service/emp-photo-sign.service';
import { EmpPresentAddressService } from '../../service/emp-present-address.service';
import { EmpPsiTrainingInfoService } from '../../service/emp-psi-training-info.service';
import { EmpSpouseInfoService } from '../../service/emp-spouse-info.service';
import { ManageEmployeeService } from '../../service/manage-employee.service';
import { EmpWorkHistoryService } from '../../service/emp-work-history.service';
import { EmpOtherResponsibilityService } from '../../service/emp-other-responsibility.service';
import { EmpWorkHistory } from '../../model/emp-work-history';
import { EmpOtherResponsibility } from '../../model/emp-other-responsibility';
import { EmpTrainingInfoService } from '../../service/emp-training-info.service';
import { EmpTrainingInfo } from '../../model/emp-training-info';
import { EmpTransferPosting } from 'src/app/views/transferPosting/model/emp-transfer-posting';
import { EmpTransferPostingService } from 'src/app/views/transferPosting/service/emp-transfer-posting.service';
import { JobDetailsSetupService } from 'src/app/views/basic-setup/service/job-details-setup.service';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { EmpRewardPunishmentService } from '../../service/emp-reward-punishment.service';
import { EmpNomineeInfoModel } from '../../model/emp-nominee-info-model';

@Component({
  selector: 'app-emp-profile',
  templateUrl: './emp-profile.component.html',
  styleUrl: './emp-profile.component.scss'
})
export class EmpProfileComponent  implements OnInit, OnDestroy {

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
  empTrainingInfo : EmpTrainingInfo[] = [];
  empBankInfo : EmpBankInfoModule[] = [];
  empNomineeInfo : EmpNomineeInfoModel[] = [];
  empLanguageInfo : EmpLanguageInfoModule[] = [];
  empForeignTourInfo : EmpForeignTourInfoModule[] = [];
  empOtherResponsibility : EmpOtherResponsibility[] = [];
  empTransferPosting : EmpTransferPosting[] = [];
  empWorkHistory : EmpWorkHistory[] = [];
  subscription: Subscription[]=[]
  photoPreviewUrl: string | ArrayBuffer | null = null;
  empProfileView = true;
  
  prlDate: string = '';
  retirementDate: string = '';

  pNo: string = '';
  id : number = 0;
  isModal = false;
  clickedButton: any;

  educationColumns: string[] = ['slNo', 'examTypeName', 'boardName', 'subGroupName', 'result', 'courseDuration', 'passingYear', 'remark'];
  educationSource = new MatTableDataSource<any>();
  
  spouseColumns: string[] = ['slNo', 'spouseName', 'spouseNameBangla', 'dateOfBirth', 'birthRegNo', 'nidNo','occupationName', 'remark'];
  spouseSource = new MatTableDataSource<any>();
  
  childsColumns: string[] = ['slNo', 'childName', 'childNameBangla', 'dateOfBirth', 'birthRegNo', 'nidNo','genderName', 'occupationName', 'maritalStatusName', 'childStatusName','remark'];
  childsSource = new MatTableDataSource<any>();
  
  trainingColumns: string[] = ['slNo', 'trainingTypeName', 'trainingName', 'instituteName', 'trainingDuration', 'fromDate','toDate', 'countryName','remark'];
  trainingSource = new MatTableDataSource<any>();
  
  bankColumns: string[] = ['slNo', 'accountName', 'accountNumber', 'accountTypeName', 'bankName', 'branchName','routingNo', 'remark'];
  bankSource = new MatTableDataSource<any>();
  
  languageColumns: string[] = ['slNo', 'languageName', 'competenceName', 'remark'];
  languageSource = new MatTableDataSource<any>();
  
  foreignTourColumns: string[] = ['slNo', 'countryName', 'purpose', 'fromDate', 'toDate', 'remark'];
  foreignTourSource = new MatTableDataSource<any>();
  
  workHistoryColumns: string[] = ['slNo', 'departmentName', 'sectionName', 'designationName', 'joiningDate', 'releaseDate'];
  workHistorySource = new MatTableDataSource<any>();
  
  rewardPunishmentColumns: string[] = ['slNo', 'type', 'orderDate','priority', 'withdrawStatus', 'withdrawDate'];
  rewardPunishmentsSource = new MatTableDataSource<any>();
  
  nomineeInfoColumns: string[] = ['slNo', 'nomineeName', 'relationName','percentage'];
  nomineeInfoSource = new MatTableDataSource<any>();

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
    public empOtherResponsibilityService: EmpOtherResponsibilityService,
    public empWorkHistoryService: EmpWorkHistoryService,
    public empTrainingInfoService: EmpTrainingInfoService,
    public empTransferPostingService: EmpTransferPostingService,
    public jobDetailsSetupService: JobDetailsSetupService,
    public empRewardPunishmentService: EmpRewardPunishmentService,
    public empNomineeInfoService: EmpNomineeInfoService,
    private bsModalRef: BsModalRef,
    private el: ElementRef, 
    private renderer: Renderer2
  ) { }

    ngOnInit(): void {
      if(this.id == 0){
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
      else {
        this.handleRouteParams();
      }
    }
    ngOnDestroy(): void {
      if (this.subscription) {
        this.subscription.forEach(subs=>subs.unsubscribe());
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
      // this.getEmpPsiTrainingInfoByEmpId(); 
      this.getEmpTrainingInfoByEmpId();
      this.getEmpBankInfoByEmpId();
      this.getEmployeeNomineeInfoByEmpId();
      this.getEmpLanguageInfoByEmpId();
      this.getEmpForeignTourInfoByEmpId();
      this.getEmpWorkHistory();
      this.getEmpOtherResponsibility();
      // this.getEmpTransferPostingInfo();
      this.getPrlAndRetirmentDate();
      this.getRewardPunishmentInfo();
    }

    getEmpBasicInfoByEmpId(){
      this.subscription.push(
        this.manageEmployeeService.getEmpBasicInfoByEmpId(this.id).subscribe((res) => {
        if(res){
          this.empBasicInfo = res;
          this.pNo = res.idCardNo;
          this.patchEmpPhoto(res.empGenderName)
        }
        else {
          this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
        }
      })
      )
      
    }

    getEmpPersonalInfoByEmpId(){
      this.subscription.push(
        this.empPersonalInfoService.findByEmpId(this.id).subscribe((res) => {
        this.empPersonalInfo = res;
      })
      )
      
    }
    
    getEmpPresentAddressByEmpId(){
      this.subscription.push(
        this.empPresentAddressService.findByEmpId(this.id).subscribe((res) => {
          this.empPresentAddress = res;
        })
      )
      
    }
    
    getEmpPermanentAddressByEmpId(){
      this.subscription.push(
        this.empPermanentAddressService.findByEmpId(this.id).subscribe((res) => {
          this.empPermanentAddress = res;
        })
      )
      
    }

    getEmpJobDetailsByEmpId(){
      this.subscription.push(
        this.empJobDetailsService.findByEmpId(this.id).subscribe((res) => {
        this.empJobDetails = res;
      })
      )
      
    }

    printSection(sectionId: string) {
      // Get the basic information and the specific section to print
      const basicInfo = document.getElementById('basicInformation')?.innerHTML;
      const sectionToPrint = document.getElementById(sectionId)?.innerHTML;
  
      // Create a new window for printing
      const printWindow = window.open('', 'blank', 'width=800,height=600');
      printWindow?.document.write(`
        <html>
          <head>
            <title>Employee Information</title>
            <style>
              table { border-collapse: collapse; text-align: left; width: 100%}
              th, td {border: 1px solid #000; padding: 5px; }
              .no-print { display: none; }
              .borderless td, .borderless th {
                  border: none;
              }
              .border-left {
                  border-left: 3px solid cadetblue;
              }
              c-col { 
                float: left; 
                padding: 5px;
                margin-bottom: 10px;
              }
              c-card-header {
                display: inline-block;
                width: 100%;
                text-align: center;
                font-size: 1.5rem;
              }
            </style>
          </head>
          <body>
            <div>${basicInfo}</div>
            <div>${sectionToPrint}</div>
          </body>
        </html>
      `);
      printWindow?.document.close();
      printWindow?.print();
    }

    
  getPrlAndRetirmentDate(){
    this.subscription.push(
      this.empBasicInfoService.findByEmpId(this.id).subscribe((res) => {
      this.jobDetailsSetupService.getActive().subscribe((response) =>{
        if(res.dateOfBirth && response){
          let prlDate = new Date(res.dateOfBirth);
          prlDate.setFullYear(prlDate.getFullYear() + (response.prlAge || 0));
          this.prlDate = prlDate.toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' });

          let retirementDate = new Date(res.dateOfBirth);
          retirementDate.setFullYear(retirementDate.getFullYear() + (response.retirmentAge || 0));
          this.retirementDate = retirementDate.toLocaleDateString('en-GB', { day: 'numeric', month: 'long', year: 'numeric' });
      }
      })
    })
    )
    
  }
    
    getEmpSpouseInfoByEmpId(){
      this.subscription.push(
        this.empSpouseInfoService.findByEmpId(this.id).subscribe((res) => {
          this.empSpouseInfo = res;
          this.spouseSource = new MatTableDataSource(res);
      })
      )
      
    }
    
    getEmpChildInfoByEmpId(){
      this.subscription.push(
        this.empChildInfoService.findByEmpId(this.id).subscribe((res) => {
          this.empChildInfo = res;
          this.childsSource = new MatTableDataSource(res);
      })
      )
      
    }
    
    getEmpEducationInfoByEmpId(){
      this.subscription.push(
      this.empEducationInfoService.findByEmpId(this.id).subscribe((res) => {
          this.empEducationInfo = res;
          this.educationSource = new MatTableDataSource(res);
      })
      )
      
    }
    
    // getEmpPsiTrainingInfoByEmpId(){
    //   this.empPsiTrainingInfoService.findByEmpId(this.id).subscribe((res) => {
    //     this.empPsiTrainingInfo = res;
    //   });
    // }
    getEmpTrainingInfoByEmpId(){
      this.subscription.push(
      this.empTrainingInfoService.findByEmpId(this.id).subscribe((res) => {
          this.empTrainingInfo = res;
          this.trainingSource = new MatTableDataSource(res);
      })
      )
     
    }
    
    getEmpBankInfoByEmpId(){
      this.subscription.push(
        this.empBankInfoService.findByEmpId(this.id).subscribe((res) => {
            this.empBankInfo = res;
            this.bankSource = new MatTableDataSource(res);
        })
      )
      
    }

    getEmployeeNomineeInfoByEmpId() {
      this.subscription.push(
        this.empNomineeInfoService.findByEmpId(this.id).subscribe((res) => {
            this.empNomineeInfo = res;
            this.nomineeInfoSource = new MatTableDataSource(res);
        })
      )
     
    }
    
    getEmpLanguageInfoByEmpId(){
      this.subscription.push(
      this.empLanguageInfoService.findByEmpId(this.id).subscribe((res) => {
          this.empLanguageInfo = res;
          this.languageSource = new MatTableDataSource(res);
      })
      )
      
    }
    
    getEmpForeignTourInfoByEmpId(){
      this.subscription.push(
      this.empForeignTourInfoService.findByEmpId(this.id).subscribe((res) => {
          this.empForeignTourInfo = res;
          this.foreignTourSource = new MatTableDataSource(res);
      })
      )
      
    }
    
    getEmpOtherResponsibility(){
      this.subscription.push(
        this.empOtherResponsibilityService.findAllByEmpId(this.id).subscribe((res) => {
          this.empOtherResponsibility = res;
        })
      )
      
    }
    
    getEmpWorkHistory(){
      this.subscription.push(
        this.empWorkHistoryService.findCombinedById(this.id).subscribe((res) => {
            this.empWorkHistory = res;
            this.workHistorySource = new MatTableDataSource(res);
        })
      )
      
    }

    getRewardPunishmentInfo(){
      this.subscription.push(
        this.empRewardPunishmentService.findByEmpId(this.id).subscribe((res) => {
            this.rewardPunishmentsSource = new MatTableDataSource(res);
        })
      )
    }
    
    getEmpTransferPostingInfo(){
      this.subscription.push(
        this.empTransferPostingService.findAllByEmpId(this.id).subscribe((res) => {
          if(res && res.length > 0){
            this.empTransferPosting = res;
          }
        })
      )
     
    }
    
    closeModal(): void {
      this.bsModalRef.hide();
    }

    patchEmpPhoto(empGender: string){
      this.subscription.push(
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
      )
    }
}
