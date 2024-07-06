export class EmpSpouseInfoModule { 
  id: number;
  empId: any;
  spouseName: string;
  spouseNameBangla: string;
  dateOfBirth: Date | null;
  birthRegNo: any;
  nid: any;
  occupationId: any;
  remark: string;
  menuPosition: number;
  isActive: boolean;

  occupationName: string = '';

  constructor(){
    this.id= 0;
    this.empId= null;
    this.spouseName= '';
    this.spouseNameBangla= '';
    this.dateOfBirth = null;
    this.birthRegNo = null;
    this.nid = null;
    this.occupationId = null;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
