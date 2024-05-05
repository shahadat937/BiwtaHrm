export class ShiftModule {
  id: number;
  shiftName: string;
  startTime: string;
  endTime: string;
  startDate: Date | null;
  endDate: Date| null;
  bufferTime: string;
  absentTime : string;
  remark : string;
  isActive : boolean;
  
  constructor() {
    this.id = 0;
    this.shiftName="";
    this.startTime="";
    this.endTime="";
    this.startDate = null;
    this.endDate = null;
    this.bufferTime="";
    this.absentTime ="";
    this.remark ="";
    this.isActive = true;
  }
}