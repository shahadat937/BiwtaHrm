import { Time } from "@angular/common";

export class AttendanceRecordModel {
    attendanceId:number|null;
    empId: number|null;
    empFirstName: string|null;
    empLastName: string|null;
    InTime: Time|null;
    OutTime: Time|null;
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
        this.InTime = null;
        this.OutTime = null;
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
