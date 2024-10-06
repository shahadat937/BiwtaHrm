export class EmpEducationInfoModule { 
  id: number;
  empId: any;
  examTypeId: any;
  boardId: any;
  subGroupId: any;
  courseDurationId: number | null = null;
  resultId: number | null = null;
  point: number | null = null;
  passingYear: number | null;
  remark: string;
  menuPosition: number;
  isActive: boolean;

  examTypeName: string = '';
  boardName: string = '';
  subGroupName: string = '';
  courseDuration: string = '';
  resultName: string = '';
  
  constructor(){
    this.id= 0;
    this.empId= null;
    this.examTypeId = null;
    this.boardId = null;
    this.subGroupId = null;
    this.passingYear = null;
    this.remark= '';
    this.menuPosition= 0;
    this.isActive= true;
  }
}
