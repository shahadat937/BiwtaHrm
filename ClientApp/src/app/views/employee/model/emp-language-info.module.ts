export class EmpLanguageInfoModule { 
  id: number;
  empId: any;
  languageId: any;
  competenceId: any;
  remark: string;
  menuPosition: number;
  isActive: boolean

  constructor() {
    this.id= 0;
    this.empId= null;
    this.languageId= null;
    this.competenceId= null;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}