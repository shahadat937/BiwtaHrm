export class LeaveModel {
    leaveRequestId:number;
    empId: number;
    leaveTypeId: number;
    fromDate: string;
    toDate: string;
    leavePurpose: string;
    isForeignLeave: boolean | null;
    countryId: number | null;
    foreignLeavePurpose: string;
    accompanyBy: string;
    associatedFile: string;
    status: number | null;
    isActive: boolean | null;
    remark: string;
    empFirstName: string;
    empLastName: string;
    leaveTypeName: string;
    countryName: string;

    constructor() {
        this.leaveRequestId = 0;
        this.empId = 0;
        this.leaveTypeId = 0;
        this.fromDate = "";
        this.toDate = "";
        this.leavePurpose = "";
        this.isForeignLeave = null;
        this.countryId = null;
        this.foreignLeavePurpose = "";
        this.accompanyBy = "";
        this.associatedFile = "";
        this.status = null;
        this.isActive = null;
        this.remark = "";
        this.empFirstName = "";
        this.empLastName = "";
        this.leaveTypeName = "";
        this.countryName = "";
    }
}
