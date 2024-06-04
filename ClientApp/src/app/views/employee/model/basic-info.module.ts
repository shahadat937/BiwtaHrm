export class BasicInfoModule { 
  id: number;
  firstName: string;
  lastName: string;
  firstNameBangla: string;
  lastNameBangla: string;
  dateOfBirth: Date | null;
  personalFileNo: string;
  nid: any;
  AspNetUserId: string;
  employeeTypeId: any;

  constructor() {
    this.id=0;
    this.firstName = '';
    this.lastName = '';
    this.firstNameBangla = '';
    this.lastNameBangla = '';
    this.dateOfBirth = null;
    this.personalFileNo ='';
    this.nid =null;
    this.AspNetUserId ='';
    this.employeeTypeId =null;
  }
}
