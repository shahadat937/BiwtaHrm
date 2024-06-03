export class PersonalInfoModule { 
  id: number;
  empId: any;
  genderId: any;
  maritalStatusId: any;
  bloodGroupId: any;
  nationalityId: any;
  religionId: any;
  hairColorId: any;
  eyesColor: any;
  mobileNumber: string;
  email: string;
  birthRegNo: any;
  placeOfBirth: string;
  tinNo: any;
  drivingLicenceNo: any;
  drivingLicenceDate:  Date | null;
  height: string;
  weight: string;
  passportNo: string;
  passportExpireDate:  Date | null;
  remark: string;

  constructor() {
    this.id= 0;
    this.empId= null;
    this.genderId= null ;
    this.maritalStatusId= null ;
    this.bloodGroupId= null ;
    this.nationalityId= null ;
    this.religionId= null ;
    this.hairColorId= null ;
    this.eyesColor= null ;
    this.mobileNumber= '';
    this.email= '';
    this.birthRegNo= null ;
    this.placeOfBirth="";
    this.tinNo= null ;
    this.drivingLicenceNo= null ;
    this.drivingLicenceDate= null;
    this.height= '';
    this.weight= '';
    this.passportNo= '';
    this.passportExpireDate= null;
    this.remark= '';
  }
}
