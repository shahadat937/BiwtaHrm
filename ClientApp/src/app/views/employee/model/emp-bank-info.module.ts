export class EmpBankInfoModule { 
  id: number;
  empId: any;
  accountName: string;
  accountNumber: string;
  accountTypeId: any;
  bankId: any;
  branchName: string;
  routingNo: string;
  remark: string;
  menuPosition: number;
  isActive: boolean;

  accountTypeName: string = '';
  bankName: string = '';

  constructor() {
    this.id= 0;
    this.empId= null;
    this.accountName= "";
    this.accountNumber= "";
    this.accountTypeId= null;
    this.bankId= null;
    this.branchName= "";
    this.routingNo= "";
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
