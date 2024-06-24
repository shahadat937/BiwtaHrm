export class ShiftModule {
  shiftId: number;
  shiftName: string;
  startTime: Date | null;
  endTime: Date | null;
  startDate: Date | null;
  endDate: Date| null;
  bufferTime: Date | null;
  absentTime : Date | null;
  remark : string;
  isActive : boolean;
  
  constructor() {
    this.shiftId = 0;
    this.shiftName="";
    this.startTime= null;
    this.endTime= null;
    this.startDate = null;
    this.endDate = null;
    this.bufferTime= null;
    this.absentTime = null;
    this.remark ="";
    this.isActive = true;
  }
}