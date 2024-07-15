export class UpdateAttendanceModel {
    attendanceId:number|null;
    attendanceDate: Date | null;
    empId : number | null;
    shiftId: number | null;
    inTime : string | null;
    outTime : string | null;
    attendanceStatusId: number | null;
    remark: string | null;
    done: boolean | null;
    
    constructor() {
        this.attendanceId = null;
        this.attendanceDate = null;
        this.empId = null;
        this.shiftId = null;
        this.inTime= null;
        this.outTime= null;
        this.attendanceStatusId = null;
        this.remark = null;
        this.done = null;
    }
}
