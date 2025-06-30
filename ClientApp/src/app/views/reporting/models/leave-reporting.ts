export class LeaveReportingModal {
    leaveRequestId: number;
    empId: number;
    idCardNo: string;
    leaveTypeId: number;
    fromDate: string;
    toDate: string;
    leavePurpose: string;
    isForeignLeave: boolean | null;
    countryId: number | null;
    foreignLeavePurpose: string;
    accompanyBy: string;
    associatedFile: string;
    status: number;
    isActive: boolean | null;
    isOldLeave: boolean | null;
    reviewedBy: number | null;
    approvedBy: number | null;
    reviewerRemark: string;
    approverRemark: string;
    remark: string;
    empFirstName: string;
    empLastName: string;
    leaveTypeName: string;
    countryName: string;

    departmentName: string;
    sectionName: string;
    designationName: string;

    constructor() {
        this.leaveRequestId = 0;
        this.empId = 0;
        this.idCardNo = "";
        this.leaveTypeId = 0;
        this.fromDate = "";
        this.toDate = "";
        this.leavePurpose = "";
        this.isForeignLeave = null;
        this.countryId = null;
        this.foreignLeavePurpose = "";
        this.accompanyBy = "";
        this.associatedFile = "";
        this.status = 0;
        this.isActive = null;
        this.isOldLeave = null;
        this.reviewedBy = null;
        this.approvedBy = null;
        this.reviewerRemark = "";
        this.approverRemark = "";
        this.remark = "";
        this.empFirstName = "";
        this.empLastName = "";
        this.leaveTypeName = "";
        this.countryName = "";
        this.departmentName = "";
        this.sectionName = "";
        this.designationName = "";
    }
}
