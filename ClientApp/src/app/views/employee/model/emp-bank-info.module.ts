export class EmpBankInfoModule { 
  id: number;
  empId: any;
  accountName: string;
  accountNumber: string;
  accountTypeId: any;
  bankId: any;
  branchId: any;
  routingNo: string;
  remark: string;
  menuPosition: number;
  isActive: boolean;

  accountTypeName: string = '';
  bankName: string = '';
  branchName: string = '';

  constructor() {
    this.id= 0;
    this.empId= null;
    this.accountName= "";
    this.accountNumber= "";
    this.accountTypeId= null;
    this.bankId= null;
    this.branchId= null;
    this.routingNo= "";
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
