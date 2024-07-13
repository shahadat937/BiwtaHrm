import { Time } from "@angular/common";

export class Attendances {
    attendanceId:number;
    attendanceDate: Date| null;
    empId: Number| null;
    officeId: Number | null;
    officeBranchId: Number | null;
    shiftId: Number | null;
    inTime: string | null;
    outTime: string | null;
    attendanceStatusId: Number | null;
    remark: string;

    constructor() {
        this.attendanceId =0;
        this.attendanceDate = null;
        this.empId = null;
        this.officeId = null;
        this.officeBranchId = null;
        this.shiftId = null;
        this.inTime = null;
        this.outTime = null;
        this.attendanceStatusId = null;
        this.remark = "";
    }

}
