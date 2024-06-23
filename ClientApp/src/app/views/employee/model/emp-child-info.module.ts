export class EmpChildInfoModule { 
  id: number;
  empId: any;
  childName: string;
  childNameBangla: string;
  dateOfBirth: Date | null;
  birthRegNo: any;
  nid: any;
  occupationId: any;
  genderId: any;
  maritalStatusId: any;
  childStatusId: any;
  remark: string;
  menuPosition: number;
  isActive: boolean

  constructor(){
    this.id= 0;
    this.empId= null;
    this.childName= '';
    this.childNameBangla= '';
    this.dateOfBirth = null;
    this.birthRegNo = null;
    this.nid = null;
    this.occupationId = null;
    this.genderId = null;
    this.maritalStatusId = null;
    this.childStatusId = null;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}