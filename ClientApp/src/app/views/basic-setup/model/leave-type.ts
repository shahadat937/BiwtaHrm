export class LeaveType {
    leaveTypeId: number;
    leaveTypeName: string;
    isActive: boolean|null;
    remark: string

    constructor() {
        this.leaveTypeId = 0;
        this.leaveTypeName = "";
        this.isActive = null;
        this.remark = "";
    }
}
