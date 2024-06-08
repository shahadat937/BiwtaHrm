export class EmpJobDetailsModule {
  id: number;
  empId: any;
  officeId: any;
  departmentId: any;
  designationId: any;
  sectionId: any;
  presentGradeId: any;
  presentScaleId: any;
  basicPay: any;
  joiningDate:  Date | null;
  firstGradeId: any;
  firstScaleId: any;
  firstDepartmentId: any;
  firstDesignationId: any;
  prlDate:  Date | null;
  retirementDate:  Date | null;
  serviceStatus: boolean;
  remark: string;
  menuPosition: number;
  isActive: boolean

  constructor() {
    this.id= 0;
    this.empId= null;
    this.officeId= null;
    this.departmentId= null;
    this.designationId= null;
    this.sectionId= null;
    this.presentGradeId= null;
    this.presentScaleId= null;
    this.basicPay = null;
    this.joiningDate = null;
    this.firstGradeId= null;
    this.firstScaleId= null;
    this.firstDepartmentId= null;
    this.firstDesignationId= null;
    this.prlDate = null;
    this.retirementDate = null;
    this.serviceStatus= true;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
