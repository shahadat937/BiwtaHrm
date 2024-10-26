export class LeaveType {
    leaveTypeId: number;
    leaveTypeName: string;
    isActive: boolean|null;
    elWorkDayCal: boolean;
    remark: string

    constructor() {
        this.leaveTypeId = 0;
        this.leaveTypeName = "";
        this.isActive = null;
        this.elWorkDayCal = true;
        this.remark = "";
    }
}
