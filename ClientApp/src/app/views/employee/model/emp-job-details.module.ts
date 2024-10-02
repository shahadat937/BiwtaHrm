export class EmpJobDetailsModule {
  id: number;
  empId: any;
  officeId: any;
  departmentId: any;
  designationId: any;
  sectionId: any;
  presentGradeId: any;
  presentScaleId: any;
  basicPay: number;
  joiningDate:  Date | null;
  confirmationDate:  Date | null = null;
  codeNo: string = '';
  firstGradeId: any;
  firstScaleId: any;
  firstDepartmentId: any;
  firstSectionId: any;
  firstDesignationId: any;
  prlDate:  Date | null;
  retirementDate:  Date | null;
  serviceStatus: boolean;
  remark: string;
  menuPosition: number;
  isActive: boolean;

  officeName: string = '';
  departmentName: string = '';
  designationName: string = '';
  designationNameBangla: string = '';
  sectionName: string = '';
  presentGradeName: string = '';
  presentScaleName: string = '';
  firstDepartmentName: string = '';
  firstSectionName: string = '';
  firstDesignationName: string = '';
  firstGradeName: string = '';
  firstScaleName: string = '';

  constructor() {
    this.id= 0;
    this.empId= null;
    this.officeId= null;
    this.departmentId= null;
    this.designationId= null;
    this.sectionId= null;
    this.presentGradeId= null;
    this.presentScaleId= null;
    this.basicPay = 0;
    this.joiningDate = null;
    this.firstGradeId= null;
    this.firstScaleId= null;
    this.firstDepartmentId= null;
    this.firstSectionId= null;
    this.firstDesignationId= null;
    this.prlDate = null;
    this.retirementDate = null;
    this.serviceStatus= true;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
