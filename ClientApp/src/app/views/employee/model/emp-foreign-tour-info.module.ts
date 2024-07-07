export class EmpForeignTourInfoModule { 
  id: number;
  empId: any;
  countryId: any;
  fromDate:  Date | null;
  toDate:  Date | null;
  purpose: string;
  remark: string;
  menuPosition: number;
  isActive: boolean;

  countryName: string = '';

  constructor() {
    this.id= 0;
    this.empId= null;
    this.countryId= null;
    this.fromDate= null;
    this.toDate= null;
    this.purpose= "";
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
