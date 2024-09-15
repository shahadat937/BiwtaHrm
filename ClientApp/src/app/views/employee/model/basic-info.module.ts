export class BasicInfoModule { 
  id: number;
  firstName: string;
  lastName: string;
  firstNameBangla: string;
  lastNameBangla: string;
  dateOfBirth: Date | null;
  personalFileNo: string;
  nid: any;
  aspNetUserId: any;
  userStatus : boolean | null;
  employeeTypeId: any;
  shiftId: any = null;
  employeeTypeName : string;
  idCardNo: string = '';
  
  departmentName : string = '';
  designationName : string = '';
  empPhotoName : string = '';
  empGenderName : string = '';

  constructor() {
    this.id=0;
    this.firstName = '';
    this.lastName = '';
    this.firstNameBangla = '';
    this.lastNameBangla = '';
    this.dateOfBirth = null;
    this.personalFileNo ='';
    this.nid =null;
    this.aspNetUserId = null;
    this.userStatus = false;
    this.employeeTypeId =null;
    this.employeeTypeName = '';
  }
}
