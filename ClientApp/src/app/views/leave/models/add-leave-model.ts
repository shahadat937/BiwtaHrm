export class AddLeaveModel {
    leaveRequestId: number;
    empId: number|null;
    leaveTypeId: number | null;
    fromDate: string | null;
    toDate: string | null;
    leavePurpose: string | null;
    isForeignLeave: boolean;
    countryId: number | null;
    foreignLeavePurpose: string | null;
    accompanyBy: string;
    isActive: boolean;
    associatedFiles: File | null;
    remark: string;


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
        this.associatedFiles= null;
    }

}
