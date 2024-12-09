export class LeaveType {
    leaveTypeId: number;
    leaveTypeName: string;
    shortName: string;
    isActive: boolean|null;
    elWorkDayCal: boolean;
    showReport: boolean
    remark: string

    constructor() {
        this.leaveTypeId = 0;
        this.leaveTypeName = "";
        this.shortName = "";
        this.isActive = null;
        this.elWorkDayCal = true;
        this.showReport = false;
        this.remark = "";
    }
}
