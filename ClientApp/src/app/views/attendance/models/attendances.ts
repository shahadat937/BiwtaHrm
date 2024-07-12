import { Time } from "@angular/common";

export class Attendances {
    attendanceId:number;
    attendanceDate: Date| null;
    attendanceTypeId: Number|null;
    empId: Number| null;
    officeId: Number | null;
    officeBranchId: Number | null;
    shiftId: Number | null;
    dayTypeId: Number | null;
    inTime: Time | null;
    outTime: Time | null;
    breakTime: Time | null;
    resumeTime: Time | null;
    workHour: Number | null;
    overTime: Number | null;
    shortTime: Number | null;
    attendanceStatusId: Number | null;
    remark: Number | null;
    done: Boolean | null;

    constructor() {
        this.attendanceId =0;
        this.attendanceDate = null;
        this.attendanceTypeId=1;
        this.empId = null;
        this.officeId = null;
        this.officeBranchId = null;
        this.shiftId = null;
        this.dayTypeId = null;
        this.inTime = null;
        this.outTime = null;
        this.breakTime = null;
        this.resumeTime = null;
        this.workHour = null;
        this.overTime = null;
        this.shortTime = null;
        this.attendanceStatusId = null;
        this.remark = null;
        this.done = null;
    }

}
