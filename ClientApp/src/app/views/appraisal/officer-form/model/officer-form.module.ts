export class OfficerFormModule {
  division: string ;
  yearStartDate:Date ;
  yearEndDate:Date ;
  employeeName: string ;
  fathersName: string ;
  mothersName: string ;
  birthRegNo: number;
  dateofBirth:Date;
  designation: string;
  workplace: string;
  joiningDate:Date;
  presentDesignationJoiningDate:Date;
  education: string;
  trainingSpecialTraining: string;
  reportinFromDate:Date;
  reportingEndDate:Date;

  constructor(){
    this.division='';
    this.yearStartDate=new Date();
    this.yearEndDate=new Date();
    this.employeeName='';
    this.fathersName='';
    this.mothersName='';
    this.birthRegNo=0;
    this.dateofBirth=new Date();
    this.designation='';
    this.workplace='';
    this.joiningDate=new Date();
    this.presentDesignationJoiningDate=new Date();
    this.education='';
    this.trainingSpecialTraining='';
    this.reportinFromDate=new Date();
    this.reportingEndDate=new Date();
  }
 }
