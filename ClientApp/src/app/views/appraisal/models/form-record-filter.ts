export class FormRecordFilter {
    keywords: string|null;
    recordId: number|null;
    empId: number|null;
    formId: number|null;
    departmentId: number|null;
    sectionId: number|null;
    reporterId: number|null;
    counterSignatureId: number|null;
    receiverId: number|null;
    reportingOfficerApproval: boolean|null;
    counterSignatoryApproval: boolean|null;
    receiverApproval: boolean|null;
    isActive: boolean|null;

    constructor() {
        this.keywords = null;
        this.recordId = null;
        this.empId = null;
        this.formId = null;
        this.departmentId = null;
        this.sectionId = null;
        this.reporterId = null;
        this.counterSignatureId = null;
        this.receiverId = null;
        this.reportingOfficerApproval = null;
        this.counterSignatoryApproval = null;
        this.receiverApproval = null;
        this.isActive = null;
    }
}
