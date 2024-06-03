export class PersonalInfoModule { 
  empId: number;
  genderId: any;
  maritalStatusId: any;
  bloodGroupId: any;
  nationalatyId: any;
  religionId: any;
  hairColorId: any;
  eyesColor: any;
  mabileNumber: any;
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

  constructor() {
    this.empId= 0;
    this.genderId= null ;
    this.maritalStatusId= null ;
    this.bloodGroupId= null ;
    this.nationalatyId= null ;
    this.religionId= null ;
    this.hairColorId= null ;
    this.eyesColor= null ;
    this.mabileNumber= null ;
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
  }
}
