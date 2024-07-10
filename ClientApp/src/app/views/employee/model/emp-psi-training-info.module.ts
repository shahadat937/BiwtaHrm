export class EmpPsiTrainingInfoModule { 
  id: number;
  empId: any;
  trainingNameId: any;
  workPurpose: string;
  fromDate:  Date | null;
  toDate:  Date | null;
  remark: string;
  menuPosition: number;
  isActive: boolean;

  trainingName: string = '';

  constructor() {
    this.id= 0;
    this.empId= null;
    this.trainingNameId= null;
    this.workPurpose= "";
    this.fromDate= null;
    this.toDate= null;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
