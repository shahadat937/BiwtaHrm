import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
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
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-emp-id-card-generate',
  templateUrl: './emp-id-card-generate.component.html',
  styleUrl: './emp-id-card-generate.component.scss'
})
export class EmpIdCardGenerateComponent implements OnInit, OnDestroy  {

  empBasicInfo: BasicInfoModule = new BasicInfoModule;
  empJobDetails: EmpJobDetailsModule = new EmpJobDetailsModule;
  empPhotoSign : EmpPhotoSignModule = new EmpPhotoSignModule;
  empPersonalInfo : PersonalInfoModule = new PersonalInfoModule;
  empId: any = null;
  pNo: string = '';
  photoPreviewUrl: string | ArrayBuffer | null = null;
  
  empPhoto : string = '';
  empSignature : string = '';
  subscription: Subscription[]=[]

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
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.forEach(subs=>subs.unsubscribe());
    }
  }

  handleRouteParams() {
    this.subscription.push(
      this.route.paramMap.subscribe((params) => {
      this.empId = Number(params.get('id'));
    })
    )
    

    this.getEmpBasicInfoByEmpId();
    this.getEmpJobDetailsByEmpId();
    this.getEmpPersonalInfoByEmpId();
    // this.getEmpPhotoSign();
  }

  
  getEmpBasicInfoByEmpId(){
    this.subscription.push(
      this.manageEmployeeService.getEmpBasicInfoByEmpId(this.empId).subscribe((res) => {
      this.empBasicInfo = res;
      this.pNo = res.personalFileNo;
    })
    )
    
  }
  
  getEmpPersonalInfoByEmpId(){
    this.subscription.push(
      this.empPersonalInfoService.findByEmpId(this.empId).subscribe((res) => {
      this.empPersonalInfo = res;
      this.patchEmpPhoto(res.genderName);
    })
    )
    
  }
  
  getEmpJobDetailsByEmpId(){
    this.subscription.push(
this.empJobDetailsService.findByEmpId(this.empId).subscribe((res) => {
      this.empJobDetails = res;
    })
    )
    
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
    this.subscription.push(
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
    )
    
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
