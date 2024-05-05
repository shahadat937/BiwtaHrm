export class ShiftModule {
  id: number;
  shiftName: string;
  startTime: string;
  endTime: string;
  startDate: Date;
  endDate: Date;
  bufferTime: string;
  absentTime : string;
  remark : string;
  isActive : boolean;
  
  constructor() {
    this.id = 0;
    this.shiftName="";
    this.startTime="";
    this.endTime="";
    this.startDate = new Date;
    this.endDate = new Date;
    this.bufferTime="";
    this.absentTime ="";
    this.remark ="";
    this.isActive = true;
  }
}