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
      this.getEmpPersonalInfoByEmpId()
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
        console.log(res)
        this.empPersonalInfo = res;
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
