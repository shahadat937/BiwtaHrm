export class LeaveType {
    leaveTypeId: number;
    leaveTypeName: string;
    isActive: boolean|null;
    eLWorkDayCal: boolean;
    remark: string

    constructor() {
        this.leaveTypeId = 0;
        this.leaveTypeName = "";
        this.isActive = null;
        this.eLWorkDayCal = true;
        this.remark = "";
    }
}
