import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BasicInfoModule } from '../../model/basic-info.module';
import { EmpJobDetailsModule } from '../../model/emp-job-details.module';
import { ActivatedRoute } from '@angular/router';
import { EmpBasicInfoService } from '../../service/emp-basic-info.service';
import { EmpJobDetailsService } from '../../service/emp-job-details.service';
import { ManageEmployeeService } from '../../service/manage-employee.service';
import { EmpPhotoSignModule } from '../../model/emp-photo-sign.module';
import { EmpPhotoSignService } from '../../service/emp-photo-sign.service';
import { PersonalInfoModule } from '../../model/personal-info.module';
import { EmpPersonalInfoService } from '../../service/emp-personal-info.service';
import { cilPrint, cilArrowLeft } from '@coreui/icons';

@Component({
  selector: 'app-emp-id-card-generate',
  templateUrl: './emp-id-card-generate.component.html',
  styleUrl: './emp-id-card-generate.component.scss'
})
export class EmpIdCardGenerateComponent implements OnInit  {

  empBasicInfo: BasicInfoModule = new BasicInfoModule;
  empJobDetails: EmpJobDetailsModule = new EmpJobDetailsModule;
  empPhotoSign : EmpPhotoSignModule = new EmpPhotoSignModule;
  empPersonalInfo : PersonalInfoModule = new PersonalInfoModule;
  empId: any = null;
  pNo: string = '';
  photoPreviewUrl: string | ArrayBuffer | null = null;
  
  empPhoto : string = '';
  empSignature : string = '';

  IdCardFront : string = `${this.empPhotoSignService.imageUrl}TempleteImage/Card_Front.jpg`;
  IdCardBack : string = `${this.empPhotoSignService.imageUrl}TempleteImage/Card_Back.jpg`;

  @ViewChild('fullIdCard', { static: false }) fullIdCard!: ElementRef;

  constructor(
    private route: ActivatedRoute,
    public empBasicInfoService: EmpBasicInfoService,
    public empJobDetailsService: EmpJobDetailsService,
    public manageEmployeeService: ManageEmployeeService,
    public empPhotoSignService: EmpPhotoSignService,
    public empPersonalInfoService: EmpPersonalInfoService,
  ) { }

  
  icons = { cilPrint, cilArrowLeft };
  
  ngOnInit(): void {
    this.handleRouteParams();
  }

  handleRouteParams() {
    this.route.paramMap.subscribe((params) => {
      this.empId = Number(params.get('id'));
    });

    this.getEmpBasicInfoByEmpId();
    this.getEmpJobDetailsByEmpId();
    this.getEmpPersonalInfoByEmpId();
    // this.getEmpPhotoSign();
  }

  
  getEmpBasicInfoByEmpId(){
    this.manageEmployeeService.getEmpBasicInfoByEmpId(this.empId).subscribe((res) => {
      this.empBasicInfo = res;
      this.pNo = res.personalFileNo;
    });
  }
  
  getEmpPersonalInfoByEmpId(){
    this.empPersonalInfoService.findByEmpId(this.empId).subscribe((res) => {
      this.empPersonalInfo = res;
      this.patchEmpPhoto(res.genderName);
    });
  }
  
  getEmpJobDetailsByEmpId(){
    this.empJobDetailsService.findByEmpId(this.empId).subscribe((res) => {
      this.empJobDetails = res;
    });
  }
  
  // getEmpPhotoSign(){
  //   this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
  //     if(res){
  //       if(res.photoUrl){
  //         this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/${res.photoUrl}`;
  //       }
  //     }
  //     else if(this.empPersonalInfo.genderName){
  //       this.patchEmpPhoto(this.empPersonalInfo.genderName);
  //     }
  //     else {
  //       this.photoPreviewUrl = `${this.empPhotoSignService.imageUrl}/EmpPhoto/default.jpg`
  //     }
  //   });
  // }

  patchEmpPhoto(empGender: string){
    this.empPhotoSignService.findByEmpId(this.empId).subscribe((res) => {
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

  printIdCard() {
    const printContents = this.fullIdCard.nativeElement.innerHTML;
    const originalContents = document.body.innerHTML;

    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
    window.location.reload();
  }

}
