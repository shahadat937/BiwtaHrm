export class EmpEducationInfoModule { 
  id: number;
  empId: any;
  examTypeId: any;
  boardId: any;
  subGroupId: any;
  result: number | null;
  courseDuration: string;
  passingYear: number | null;
  remark: string;
  menuPosition: number;
  isActive: boolean

  constructor(){
    this.id= 0;
    this.empId= null;
    this.examTypeId = null;
    this.boardId = null;
    this.subGroupId = null;
    this.result = null;
    this.courseDuration = '';
    this.passingYear = null;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
