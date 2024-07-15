import { Time } from "@angular/common";

export class AttendanceRecordModel {
    attendanceId:number|null;
    empId: number|null;
    empFirstName: string|null;
    empLastName: string|null;
    inTime: Time|null;
    outTime: Time|null;
    shiftId: number | null;
    dayTypeId:number | null;
    attendanceTypeId: number|null;
    attendanceStatusId: number|null;
    attendanceTypeName: string|null;
    shiftName: string|null;
    dayTypeName: string|null;
    attendanceStatusName: string|null;

    constructor() {
        this.attendanceId=null;
        this.empId=null;
        this.empFirstName="";
        this.empLastName="";
        this.inTime = null;
        this.outTime = null;
        this.shiftId=null;
        this.dayTypeId=null;
        this.attendanceTypeId=null;
        this.attendanceTypeName=""
        this.attendanceStatusId=null;
        this.shiftName="";
        this.dayTypeName="";
        this.attendanceStatusName = "";
    }
}
