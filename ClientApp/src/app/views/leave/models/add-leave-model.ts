export class AddLeaveModel {
    leaveRequestId: number;
    empId: number | null;
    leaveTypeId: number | null;
    fromDate: string | null;
    toDate: string | null;
    leavePurpose: string | null;
    isForeignLeave: boolean;
    countryId: number | null;
    foreignLeavePurpose: string | null;
    accompanyBy: string;
    isActive: boolean;
    associatedFiles: File[] | null;
    remark: string;
    reviewedBy: number | null;
    approvedBy: number | null;
    reviewerRemark: string;
    approverRemark: string;
    isOldLeave: boolean;
    status: number = 0;

    applicationById: number | null;

    empCurrentDepartmentId: number | null;
    empCurrentSectionId: number | null;
    empCurrentDesignationId: number | null;
    empCurrentResponsibilityTypeId: number | null;

    reviewerCurrentDepartmentId: number | null;
    reviewerCurrentSectionId: number | null;
    reviewerCurrentDesignationId: number | null;
    reviewerCurrentResponsibilityTypeId: number | null;

    approverCurrentDepartmentId: number | null;
    approverCurrentSectionId: number | null;
    approverCurrentDesignationId: number | null;
    approverCurrentResponsibilityTypeId: number | null;
    isAdvanceLeave: boolean | null




    constructor() {
        this.leaveRequestId = 0;
        this.empId = null;
        this.leaveTypeId = null;
        this.fromDate = null;
        this.toDate = null;
        this.leavePurpose = "";
        this.isForeignLeave = false;
        this.countryId = null;
        this.foreignLeavePurpose = null;
        this.accompanyBy = "";
        this.isActive = true;
        this.remark = "";
        this.associatedFiles = null;
        this.reviewedBy = null;
        this.approvedBy = null;
        this.reviewerRemark = "";
        this.approverRemark = "";
        this.isOldLeave = false;
        this.applicationById = null;

        this.empCurrentDepartmentId = null;
        this.empCurrentSectionId = null;
        this.empCurrentDesignationId = null;
        this.empCurrentResponsibilityTypeId = null;

        this.reviewerCurrentDepartmentId = null;
        this.reviewerCurrentSectionId = null;
        this.reviewerCurrentDesignationId = null;
        this.reviewerCurrentResponsibilityTypeId = null;

        this.approverCurrentDepartmentId = null;
        this.approverCurrentSectionId = null;
        this.approverCurrentDesignationId = null;
        this.approverCurrentResponsibilityTypeId = null;
        this.isAdvanceLeave = false

    }

}
