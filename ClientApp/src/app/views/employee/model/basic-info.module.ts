export class BasicInfoModule { 
  empId: number;
  empEngName: string;
  empBdName: string;
  dateOfBirth: Date | null;
  personalFileNo: string;
  nid: any;
  AspNetUserId: string;
  employeeTypeId: any;

  constructor() {
    this.empId=0;
    this.empEngName ='';
    this.empBdName= '';
    this.dateOfBirth = null;
    this.personalFileNo ='';
    this.nid =null;
    this.AspNetUserId ='';
    this.employeeTypeId =null;
  }
}
