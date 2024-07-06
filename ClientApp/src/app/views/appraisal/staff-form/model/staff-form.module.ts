export class StaffFormModule { 
  division: string ;
  yearStartDate:Date ;
  yearEndDate:Date ;
  name: string ;
  employeeCode:number;
  fathersName: string ;
  mothersName: string ;
  designation: string;
  dateofBirth:Date;
  presentSalary:number;
  scaleOfpay:string;
  joiningDate:Date;
  presentDesignationJoiningDate:Date;
  education: string;
  lingutstics:string;
  trainingSpecialTraining: string;
  reportinFromDate:Date;
  reportingEndDate:Date;

  constructor(){
    this.division='';
    this.yearStartDate=new Date();
    this.yearEndDate=new Date();
    this.name='';
    this.employeeCode=0;
    this.fathersName='';
    this.mothersName='';
    this.designation='';
    this.presentSalary=0;
    this.dateofBirth=new Date();
    this.scaleOfpay='';
    this.joiningDate=new Date();
    this.presentDesignationJoiningDate=new Date();
    this.education='';
    this.lingutstics='';
    this.trainingSpecialTraining='';
    this.reportinFromDate=new Date();
    this.reportingEndDate=new Date();
  }
}
