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
        this.isActive = null;
    }
}
